using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace  Core.Entities.Concrete
{
    public class OperationClaim: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<UserOperationClaim> UserOperationClaims { get; set; } = new List<UserOperationClaim>();
    }
}
