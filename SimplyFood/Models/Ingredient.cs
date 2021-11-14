﻿using System;
using System.Collections.Generic;

namespace SimplyFood.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
        }
        public int Id { get; set; }
        public string Aisle { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
        public string UnitShort { get; set; }
        public string UnitLong { get; set; }
        public string OriginalString { get; set; }
        public IEnumerable<string> MetaInformation { get; set; }
    }

}
