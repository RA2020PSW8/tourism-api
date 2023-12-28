from model import RecSysModel
import pandas as pd 
import torch 
from sklearn import preprocessing
import os 
import sys 

df = pd.read_csv('ratings.csv')
df = df[df['TourId'] <= 1025]

def get_recommendations_for_user(model, user_id, lbl_user, lbl_tour, num_recommendations=10):
    if user_id >= len(lbl_user.classes_):
     
        mapped_user_id = user_id % len(lbl_user.classes_)
    else:
        mapped_user_id = user_id
    
  
    tour_ids = torch.arange(len(lbl_tour.classes_)) 
    user_ids = torch.tensor([mapped_user_id] * len(lbl_tour.classes_), dtype=torch.long)
    
    with torch.no_grad():
      
        user_embeds = model.user_embed(user_ids)
        tour_embeds = model.tour_embed(tour_ids)

     
        concatenated = torch.cat([user_embeds, tour_embeds], dim=1)

        output = model.out(concatenated)

       
        predictions = output[:, 0] 

   
    rated_tours = df[df['UserId'] == mapped_user_id]['TourId'].values
    predictions[rated_tours] = float('-inf')  

  
    sorted_indices = torch.argsort(predictions, descending=True)

   
    recommended_tour_ids = sorted_indices[:num_recommendations]

    recommended_tours = lbl_tour.inverse_transform(recommended_tour_ids.cpu().numpy())

    return recommended_tours

def main(argv): 

    current_directory = os.getcwd()
    device  = torch.device('cuda' if torch.cuda.is_available() else 'cpu')

  
    
    model_filename = 'recommendation_model.pth'
    model_path = os.path.join(current_directory, model_filename)

    lbl_user = preprocessing.LabelEncoder()
    lbl_tour = preprocessing.LabelEncoder()

    df.UserId = lbl_user.fit_transform(df.UserId.values)
    df.TourId = lbl_tour.fit_transform(df.TourId.values)

    loaded_model = RecSysModel(n_users=len(lbl_user.classes_), n_tours=len(lbl_tour.classes_)).to(device)
    checkpoint = torch.load(model_path)
    loaded_model.load_state_dict(checkpoint['model_state_dict'])
    loaded_model.eval()  

    recommendations = get_recommendations_for_user(loaded_model, int(argv[1],10), lbl_user, lbl_tour)

    

    formatted_recommendations = '['+', '.join(str(x) for x in recommendations)+']'
    print(formatted_recommendations)

if __name__ == '__main__': 

    main(sys.argv)