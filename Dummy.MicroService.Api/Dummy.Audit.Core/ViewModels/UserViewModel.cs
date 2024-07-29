using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dummy.Audit.Core.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int LegalStatusId { get; set; }
        public string BusinessName { get; set; }
    }
}
