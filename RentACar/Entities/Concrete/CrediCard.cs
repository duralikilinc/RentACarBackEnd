using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
   public class CrediCard:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public long Number { get; set; }
        public string FullName { get; set; }
        public short Ccv { get; set; }
        public short ExpirationMonth { get; set; }
        public short ExpirationYear { get; set; }
    }
}
