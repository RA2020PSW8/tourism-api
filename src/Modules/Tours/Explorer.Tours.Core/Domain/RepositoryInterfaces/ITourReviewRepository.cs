﻿using Explorer.BuildingBlocks.Core.UseCases;

namespace Explorer.Tours.Core.Domain.RepositoryInterfaces;

public interface ITourReviewRepository: ICrudRepository<TourReview>
{
    public PagedResult<TourReview> GetByTourId(long tourId, int page, int pageSize);
}