using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Armor : Item
    {
        public int protectionRating;
        public EquipmentSlot requiredSlot;
        public Armor(string name, string description, int value, int protectionRating, EquipmentSlot requiredSlot) : base(name, description, value)
        {
            this.protectionRating = protectionRating;
            this.requiredSlot = requiredSlot;
        }

        public override string ToString()
        {
            string s = base.ToString();
            s += " - " + protectionRating;
            return s;
        }
    }
}
