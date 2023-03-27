// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted
using System.ComponentModel.DataAnnotations;

namespace OperationService.Entities;

public class TypeEntity
{
    [Key]
    public int Id { get; set; }

    public string TypeName { get; set; }
    
}