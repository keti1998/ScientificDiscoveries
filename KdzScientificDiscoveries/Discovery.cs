using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdzScientificDiscoveries
{
    class Discovery
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _sphere;

        public string Sphere
        {
            get { return _sphere; }
            set { _sphere = value; }
        }

        private double _budget;

        public double Budget
        {
            get { return _budget; }
            set { _budget = value; }
        }

        private string _country;

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        private int _date;

        public int Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private bool _NobelPrize;

        public bool NobelPrize
        {
            get { return _NobelPrize; }
            set { _NobelPrize = value; }
        }


        public Discovery(string name, string sphere, double budget, string country, int date, bool nobelPrize)
        {
            _name = name;
            _budget = budget;
            _sphere = sphere;
            _country = country;
            _date = date;
            _NobelPrize = nobelPrize;
        }


    }
}
