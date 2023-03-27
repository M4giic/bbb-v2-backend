// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using OperationService.DTOs;

namespace OperationService.Infrastructure.Repositories;

public class TypeDto : TypeBaseDto
{
    public int TypeFamilyId { get; set; }
    public string TypeFamilyName { get; set; }

}