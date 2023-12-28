
import numpy as np 
import torch
import torch.nn as nn 


class RecSysModel(nn.Module): 
    def __init__(self, n_users, n_tours): 
        super().__init__()

        self.user_embed = nn.Embedding(n_users, 32)
        self.tour_embed = nn.Embedding(n_tours, 32) 

        self.out = nn.Linear(64, 1)

    def forward(self, users, tours, ratings = None): 
        user_embeds = self.user_embed(users) 
        tour_embeds = self.tour_embed(tours)

        output = torch.cat([user_embeds, tour_embeds], dim =1)

        output = self.out(output)

        return output 
