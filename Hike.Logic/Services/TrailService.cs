﻿using Hike.Logic.Models;
using Hike.Logic.Models.Responses;
using Hike.Logic.Repositories.Interfaces;
using Hike.Logic.Services.Interfaces;

namespace Hike.Logic.Services;

public class TrailService : ITrailService
{
    private readonly ITrailRepository _trailRepository;

    public TrailService(ITrailRepository trailRepository)
    {
        _trailRepository = trailRepository;
    }
    public async Task<TrailModel?> GetTrailById(Guid id)
    {
        return await _trailRepository.GetTrailById(id);
    }

    public async Task<IEnumerable<TrailModel>> GetTrails(string? searchValue, int page, int pageSize)
    {
        return await _trailRepository.SearchTrailByTitle(searchValue, page, pageSize);
    }

    public async Task<AddTrailResponse> AddTrail(TrailModel model)
    {
        if (model.Description?.Length > 255)
        {
            return new AddTrailResponse(FailureType.User,"Description has too many characters. Only 255 characters allowed");
        }

        if (!await _trailRepository.AddTrail(model))
        {
            return new AddTrailResponse(FailureType.Server,"Database failure");
        }

        return new AddTrailResponse();
    }
}