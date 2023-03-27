// Copyright 2023 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

namespace OperationService.Request.Response.Type;

public class TypeFamilyResponse
{
    public int Id { get; set; }
    
    public string TagFamilyName { get; set; }
    
    //todo this should be simplier type
    public List<TypeResponse> Types { get; set; }

}