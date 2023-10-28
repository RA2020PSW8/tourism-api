﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public.Tourist;
using Explorer.Stakeholders.Core.Domain;

namespace Explorer.Stakeholders.Core.UseCases.Tourist
{
    public class ClubService: CrudService<ClubDto, Club>, IClubService
    {
        public ClubService(ICrudRepository<Club> repository, IMapper mapper) : base(repository, mapper) { }

        public PagedResult<ClubDto> GetAllByUser(int page, int pageSize, int currentUserId)
        {
            var result = this.GetPaged(page, pageSize);
            var filteredItems = new List<ClubDto>();

            foreach (ClubDto c in result.ValueOrDefault.Results)
            {
                if (c.UserId == currentUserId)
                {
                    filteredItems.Add(c);
                }
            }

            return new PagedResult<ClubDto>(filteredItems, filteredItems.Count);
        }

    }
}
