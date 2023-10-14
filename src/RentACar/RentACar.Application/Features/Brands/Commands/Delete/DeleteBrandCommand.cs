﻿using AutoMapper;
using MediatR;
using RentACar.Application.Services.Repositories;

namespace RentACar.Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeleteBrandResponse>
{
    public Guid Id { get; set; }
}

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeleteBrandResponse>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<DeleteBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _brandRepository.GetAsync(predicate: x => x.Id == request.Id);

        await _brandRepository.DeleteAsync(brand);

        return _mapper.Map<DeleteBrandResponse>(brand);
    }
}