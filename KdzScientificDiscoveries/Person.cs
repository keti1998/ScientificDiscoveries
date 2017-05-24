using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdzScientificDiscoveries
{
    [Serializable]
    class Person
    {

        public Person(string login, string password)
        {
            _password = password;
            _login = login;
        }
       
        private string _login, _password;
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
