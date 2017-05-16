using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdzScientificDiscoveries
{
    [Serializable]
    class Discovery
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _scientist;

        public string Scientist
        {
            get { return _scientist; }
            set { _scientist = value; }
        }

        private string _sphere;

        public string Sphere
        {
            get { return _sphere; }
            set { _sphere = value; }
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

        private string _NobelPrize;

        public string NobelPrize
        {
            get { return _NobelPrize; }
            set { _NobelPrize = value; }
        }


        public Discovery(string name,string scientist, string country, string sphere,  int date, string nobelPrize)
        {
            _name = name;
            _scientist = scientist;
            _sphere = sphere;
            _country = country;
            _date = date;
            _NobelPrize = nobelPrize;
        }


    }
}
