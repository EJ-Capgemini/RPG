using Maandag.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class Area {
        public string Name { get; set; }
        public List<Npc> Npcs { get; set; }

        public Area(string name, List<Npc> npcs) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Npcs = npcs ?? throw new ArgumentNullException(nameof(npcs));
        }
    }
}
