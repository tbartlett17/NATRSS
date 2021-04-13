using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpeditionProject.Models
{
    public class AccountVM
    {
        public User User { get; set; }
        public IEnumerable<Expedition> UsersExpeditions { get; set; }
    }
}
