using System;

namespace Catalog.API.Excpetions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found!")
    {
        
    }
}
