// Copyright 2022 Motorola Solutions, Inc.
// All Rights Reserved.
// Motorola Solutions Confidential Restricted

namespace BBBv2.Infrastructure.Entities;

public class SingleSignOnTokenEntity
{
    public int Id { get; set; }
    public Guid AccountGuid{ get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
}
