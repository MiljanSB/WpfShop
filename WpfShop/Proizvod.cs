using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfShop
{
    class Proizvod
    {
        public int ProizvodId { get; set; }
        public string Naziv { get; set; }
        public decimal Cena { get; set; }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
