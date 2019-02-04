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

        public Area(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            //Vullen met testdata
            Npcs = GetDummyData();
        }

        private List<Npc> GetDummyData() {
            List<Npc> npcs = new List<Npc>();

            npcs.Add(new Npc(true, 5, 20.0f, 1, "Chicken"));
            npcs.Add(new Npc(false, 1_000, 100.0f, 50, "Bartender"));
            npcs.Add(new Npc(true, 10, 30.0f, 2, "Goblin"));

            return npcs;
        }
    }
}
