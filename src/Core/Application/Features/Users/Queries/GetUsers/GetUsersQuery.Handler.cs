﻿using Application.Contracts.Identity;
using Application.Models.Common;
using AutoMapper;
using Domain.Entities.User;
using Mediator;

namespace Application.Features.Users.Queries.GetUsers;

internal class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, OperationResult<List<GetUsersQueryResponse>>>
{
    readonly IAppUserManager _userManager;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IAppUserManager userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async ValueTask<OperationResult<List<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var usersModel =
            (await _userManager.GetAllUsersAsync()).Select(_mapper.Map<User, GetUsersQueryResponse>).ToList();

        if(!usersModel.Any())
            return OperationResult<List<GetUsersQueryResponse>>.NotFoundResult("No Users Found!");

        return OperationResult<List<GetUsersQueryResponse>>.SuccessResult(usersModel);
    }
}