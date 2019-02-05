using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class RandomUtil {
        private static RandomUtil instance;
        private Random Generator { get; set; }

        private RandomUtil() { }

        public static RandomUtil Instance {
            get {
                if (instance == null) {
                    instance = new RandomUtil {
                        Generator = new Random()
                    };
                }
                return instance;
            }
        }

        public int GetRandomNumber(int min, int max) {
            return this.Generator.Next(min, max);
        }
    }
}
