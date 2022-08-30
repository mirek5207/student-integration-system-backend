﻿namespace student_integration_system_backend.Entities;

public class UserRole
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
    
}