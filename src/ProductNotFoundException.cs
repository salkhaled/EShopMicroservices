﻿using System;

namespace Catalog.a

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base("Product not found!")
    {
        
    }
}
