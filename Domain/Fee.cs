﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Fee
    {
        public int Id { get; set; }
        public FeeType Type { get; set; }
        public decimal FeeValue { get; set; }
    }
}
