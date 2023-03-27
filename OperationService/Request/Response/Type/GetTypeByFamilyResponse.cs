// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

using OperationService.Request.Request.Type;

namespace OperationService.Request.Response.Type;

public class GetTypeByFamilyResponse
{
    public int TypeFamilyId { get; set; }
    public string TypeFamilyName { get; set; }
    public List<AddNewTypeRequest> Types { get; set; }
}