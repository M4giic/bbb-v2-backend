// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using AutoMapper;
using Microsoft.AspNetCore.Razor.Language;
using OperationService.DTOs;
using OperationService.Entities;
using OperationService.Request;
using OperationService.Request.Request.Wallet;

namespace OperationService.Configuration;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<WalletDto, WalletEntity>().ReverseMap();
        CreateMap<WalletDto, AddWalletRequest>().ReverseMap();
        CreateMap<WalletDto, UpdateWalletRequest>().ReverseMap();
    }
}