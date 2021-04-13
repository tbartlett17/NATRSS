using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpeditionProject.Models
{
    public class userAndFormArrayVM
    {
        public User thisUser { get; set; }

        public IList<Form> thisFormArray { get; set; }
    }
}
