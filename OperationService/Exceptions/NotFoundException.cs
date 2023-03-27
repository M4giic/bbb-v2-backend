// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

namespace OperationService.Exceptions;

public class NotFoundException : Exception
{

    public NotFoundException(string message) : base(message) 
    {
    }
}