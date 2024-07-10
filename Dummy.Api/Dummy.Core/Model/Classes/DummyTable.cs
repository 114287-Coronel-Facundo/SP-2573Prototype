using System;
using System.Collections.Generic;

namespace Dummy.Core.Model.Classes
{
    public partial class DummyTable
    {
        public int Id { get; set; }
        public string DummyDescription { get; set; } = null!;
        public decimal DummyPrice { get; set; }
    }
}
