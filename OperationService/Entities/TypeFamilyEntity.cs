// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using System.ComponentModel.DataAnnotations;

namespace OperationService.Entities;

public class TypeFamilyEntity
{
    [Key]
    public int Id { get; set; }

    public WalletEntity WalletEntity { get; set; }
    public string TypeFamilyName { get; set; }
    public bool IsActive { get; set; }

    public List<TypeEntity> Types { get; set; }
}