﻿using System;
using System.Collections.Generic;

#nullable disable

namespace NorthWind_EntityFramework.Models
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
