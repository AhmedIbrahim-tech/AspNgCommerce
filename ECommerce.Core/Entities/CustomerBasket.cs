﻿namespace ECommerce.Core.Entities;

public class CustomerBasket
{
    public CustomerBasket(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
    public List<BasketItem> basketItems { get; set; }
}
