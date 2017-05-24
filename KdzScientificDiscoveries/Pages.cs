using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdzScientificDiscoveries
{
  static  class Pages
    {
        private static DiscoveriesPage _discp = new DiscoveriesPage();
        private static NewDiscoveryPage _newd = new NewDiscoveryPage();
        private static LoginPage _logp = new LoginPage();
        private static SignUp _sign = new SignUp();

        public static SignUp SignUp
        {
            get
            {
                return _sign;
            }
        }




        public static LoginPage LoginPage
        { get
            {
                return _logp;

            }
        }





       
        public static DiscoveriesPage DiscoveriesPage
        {
            get
            {
                return _discp;
            }
        }
        public static NewDiscoveryPage NewDiscoveryPage
        {
            get
            {
                return _newd;
            }
        }
    }
}
