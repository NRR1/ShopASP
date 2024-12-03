﻿using Microsoft.AspNetCore.Identity;
using ShopASP.Domain.Entities;

public class Order
{
    public int ID { get; set; }
    public string UserID { get; set; }
    public IdentityUser User { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
}
