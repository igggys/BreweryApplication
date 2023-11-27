﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ModelsLib.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public ProductText[] ProducTexts { get; set; }
        public float WholesalePrice { get; set; }
        [Range(0, 60)]
        public uint AlcoholByVolume { get; set; } //
        [Range(0, 120)]
        public uint? InternationalBitternessUnits { get; set; }
        public EuropeanBrewingConvention EuropeanBrewingConvention { get; set; }
    }

    public enum EuropeanBrewingConvention
    {
        PaleStraw = 1,
        Straw = 2,
        PaleGold = 3,
        DeepGold = 4,
        PaleAmber = 5,
        MediumAmber = 6,
        DeepAmber = 7,
        AmberBrown = 8,
        Brown = 9,
        RubyBrown = 10,
        DeepBrown = 11,
        Black = 12
    }

    public class ProductText
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CultureInfo Language { get; set; }
    }
}
