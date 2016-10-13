using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class NPC : AI
    {
        public NPC(string name, string description, int level, char icon) : base(name, description, level, true, icon)
        {

        }
    }
}
