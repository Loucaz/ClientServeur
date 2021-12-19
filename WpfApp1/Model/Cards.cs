using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{

    public class Cards
    {
        public Cards()
        {
            Line = new List<Card>();
        }

        public List<Card> Line { get; set; }
    }

    public class Card
    {
        public int Num { get; set; }
    }
}
