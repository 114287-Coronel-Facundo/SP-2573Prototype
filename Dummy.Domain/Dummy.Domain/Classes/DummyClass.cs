using Dummy.Domain.Classes.BaseConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Domain.Classes
{
    internal class DummyClass : IdentificableConfiguration
    {
        public string DummyDescription { get; set; }
        public decimal DummyPrice { get; set; }
    }
}
