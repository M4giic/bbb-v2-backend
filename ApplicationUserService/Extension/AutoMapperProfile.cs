// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using ApplicationUserService.Domain.Dto;
using ApplicationUserService.Service;
using AutoMapper;
using BBBv2.Infrastructure.Entities;

namespace ApplicationUserService;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AccountDto, AccountEntity>().ReverseMap();
        CreateMap<RegisterUserRequest, AccountDto>();
        CreateMap<AccountDto, UserDto>().ReverseMap();
        CreateMap<AccountInformationDto, AccountEntity>().ReverseMap();
    }
}