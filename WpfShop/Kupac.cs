﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfShop
{
    class Kupac
    {
        public int KupacId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public override string ToString()
        {
            return Ime + " " + Prezime;
        }
    }
}
