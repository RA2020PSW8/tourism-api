import pandas as pd 
import numpy as np 
import torch
import torch.nn as nn 
from sklearn import model_selection, metrics, preprocessing
import matplotlib.pyplot as plt 
from torch.utils.data import Dataset, DataLoader
from  sklearn.metrics import mean_squared_error
from collections import defaultdict
import os

from model import RecSysModel

class TourDataset: 
    def __init__(self, users, tours, ratings): 
        self.users = users 
        self.tours = tours 
        self.ratings = ratings 

    def __len__(self): 
        return len(self.users)
    
    def __getitem__(self, item): 

        users = self.users[item] 
        tours = self.tours[item]
        ratings = self.ratings[item]

        return {

            "users": torch.tensor(users, dtype=torch.long), 
            "tours": torch.tensor(tours, dtype=torch.long),
            "ratings": torch.tensor(ratings, dtype=torch.long)

        }
    

df = pd.read_csv('ratings.csv')
df = df[df['TourId'] <= 1025]
    

def main(): 
    device  = torch.device('cuda' if torch.cuda.is_available() else 'cpu')

  
    # we have to encode, because embeding is 32 
    lbl_user = preprocessing.LabelEncoder()
    lbl_tour = preprocessing.LabelEncoder()

    df.UserId = lbl_user.fit_transform(df.UserId.values)
    df.TourId = lbl_tour.fit_transform(df.TourId.values)

    df_train, df_valid = model_selection.train_test_split(df, test_size = 0.1, random_state=42, stratify=df.Rating.values)

    train_dataset = TourDataset(
        users = df_train.UserId.values, 
        tours = df_train.TourId.values, 
        ratings = df_train.Rating.values
    )

    valid_dataset = TourDataset(
        users = df_valid.UserId.values, 
        tours = df_valid.TourId.values, 
        ratings = df_valid.Rating.values

    )

    train_loader = DataLoader(dataset = train_dataset, batch_size = 4, shuffle = True, num_workers = 2)

    validation_loader = DataLoader(dataset = valid_dataset, batch_size = 4, shuffle = True, num_workers= 2)

    dataiter = iter(train_loader)

    dataloader_data = next(dataiter)


    model = RecSysModel(n_users=len(lbl_user.classes_), n_tours = len(lbl_tour.classes_),).to(device)

    optimizer = torch.optim.Adam(model.parameters())
    sch = torch.optim.lr_scheduler.StepLR(optimizer, step_size = 3, gamma = 0.7)

    loss_func = nn.MSELoss()


    ### training loop 
    epochs = 1
    total_loss = 0 
    plot_steps, print_steps = 5000, 5000 
    step_cnt = 0 
    all_losses_list = [] 

    model.train()
    for epoch_i in range(epochs): 
        for i, train_data in enumerate(train_loader): 
            output = model(train_data["users"], train_data["tours"])

            # .view(4,-1) to reshape the rating to match the output of the model 
            rating = train_data["ratings"].view(4, -1).to(torch.float32)

            loss = loss_func(output, rating)
            total_loss += loss.sum().item()

            optimizer.zero_grad()
            loss.backward()
            optimizer.step()

            step_cnt += len(train_data["users"])

            if(step_cnt % plot_steps == 0): 
                avg_loss = total_loss/(len(train_data["users"])* plot_steps)
                print(f"epoch {epoch_i} loss at step: {step_cnt} is {avg_loss}")
                all_losses_list.append(avg_loss)
                total_loss = 0 

 

    model_output_list = [] 
    target_rating_list = [] 

    model.eval()

    with torch.no_grad(): 
        for i, batched_data in enumerate(validation_loader): 
            model_output = model(batched_data['users'], batched_data['tours'])

            model_output_list.append(model_output.sum().item() / len (batched_data['users']))

            target_rating = batched_data['ratings']

            target_rating_list.append(target_rating.sum().item()/ len(batched_data['users']))

            

    rms = mean_squared_error(target_rating_list, model_output_list, squared = False)
    print(f"rms: {rms}")


    user_est_true = defaultdict(list)

    with torch.no_grad(): 
        for i, batched_data in enumerate(validation_loader): 
            users = batched_data['users']
            tours = batched_data['tours']
            ratings = batched_data['ratings']

            model_output = model(batched_data['users'], batched_data['tours'])

            for i in range(len(users)):
                user_id = users[i].item()
                tour_id = tours[i].item()
                true_rating = ratings[i].item()

                pred_rating = model_output[i][0].item()

                user_est_true[user_id].append((pred_rating, true_rating))

    with torch.no_grad(): 
        precisions = dict()
        recalls = dict()

        k = 100 
        threshold = 3.5 

        for uid , user_ratings in user_est_true.items(): 

            user_ratings.sort(key = lambda x: x[0], reverse = True)

            n_rel = sum((true_r >= threshold) for (_, true_r) in user_ratings)

            n_rec_k = sum((est >= threshold) for (est, _) in user_ratings[:k])

            n_rel_and_rec_k = sum(
                ((true_r >= threshold) and (est >= threshold))
                for(est, true_r) in user_ratings[:k]

            )

            precisions[uid] = n_rel_and_rec_k / n_rec_k if n_rec_k !=0 else 0 
            recalls[uid] = n_rel_and_rec_k / n_rel if n_rel !=0 else 0 

    print(f"precision @ {k}: {sum(prec for prec in precisions.values())/ len(precisions)}")
    print(f"precision @ {k}: {sum(rec for rec in recalls.values())/ len(recalls)}")

    #plt.figure()
    #plt.plot(all_losses_list)
    #plt.show()

   


    current_directory = os.getcwd()

    model_filename = 'recommendation_model.pth'
    model_path = os.path.join(current_directory, model_filename)

    torch.save({
        'model_state_dict': model.state_dict(),
    }, model_path)

    print(f"Model saved to '{model_path}'")

   
    

if __name__ == '__main__': 
    main()
