using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Findex : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public short Score { get; set; }
    }
}
