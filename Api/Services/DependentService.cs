﻿using Api.Dtos.Dependent;
using Api.Repositories;
using AutoMapper;

namespace Api.Services
{
    public class DependentService : IDependentService
    {
        private readonly IDependentsRepository _dependentsRepository;
        private readonly IMapper _mapper;

        public DependentService(IDependentsRepository dependentsRepository, IMapper mapper)
        {
            _dependentsRepository = dependentsRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<List<GetDependentDto>> GetAll(CancellationToken cancellationToken)
        {
            var dependents = await _dependentsRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetDependentDto>>(dependents);
        }

        /// <inheritdoc/>
        public async Task<GetDependentDto?> GetById(int id, CancellationToken cancellationToken)
        {
            var dependent = await _dependentsRepository.GetById(id, cancellationToken);
            return _mapper.Map<GetDependentDto>(dependent);
        }
    }
}
