// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using OperationService.Infrastructure.Repositories;

namespace OperationService.DTOs;

public class FamilyTypeDto
{
    public int Id { get; set; }
    
    public Guid WalletId { get; set; }
    public string TagFamilyName { get; set; }
    public bool IsActive { get; set; }
    
    //todo this should not be TypeDto cause there is a lot of duplication
    public List<TypeBaseDto> Types { get; set; }

}