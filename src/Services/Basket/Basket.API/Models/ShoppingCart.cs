﻿namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public string UserName { get; set; }

        public IList<ShoppingCartItem> Items { get; set; } = [];

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        // Required for Mapping
        public ShoppingCart()
        {

        }

        public ShoppingCart(string userName) : this()
        {
            UserName = userName;
        }
    }
}
