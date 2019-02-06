using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maandag {
    class FileManager {
        private static FileManager instance;
        private static string LOAD_FILE_PATH = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\save_files\";
        private static string DEFAULT_FILE = "default.txt";

        private FileManager() { }

        public static FileManager Instance {
            get {
                if (instance == null) {
                    instance = new FileManager();
                }
                return instance;
            }
        }

        public Boolean Save(List<String> parameters = null) {
            string filename = DEFAULT_FILE;

            if (parameters != null) {
                filename = parameters[0];
                if (!filename.EndsWith(".txt")) {
                    filename += ".txt";
                }
            }

            JsonSerializer serializer = new JsonSerializer();

            try {
                using (StreamWriter sw = new StreamWriter(LOAD_FILE_PATH + filename))
                using (JsonWriter writer = new JsonTextWriter(sw)) {
                    serializer.Serialize(writer, instance);
                }
                Console.WriteLine("Current progress has been succesfully saved to file {0}", filename);
            } catch (Exception ex) {
                Console.WriteLine("Error:" + ex.Message);
                return false;
            }

            return true;
        }

        public Game Load(List<String> parameters = null) {
            string filename = DEFAULT_FILE;
            Game loadedGame = null;

            if (parameters != null) {
                filename = parameters[0];
                if (!filename.EndsWith(".txt")) {
                    filename += ".txt";
                }
            }

            try {
                using (StreamReader r = new StreamReader(LOAD_FILE_PATH + filename)) {
                    string json = r.ReadToEnd();
                    loadedGame = JsonConvert.DeserializeObject<Game>(json);
                    Console.WriteLine("Existing progress succesfully loaded from {0}", filename);
                }
            } catch (IOException ex) {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }

            return loadedGame;
        }

        public List<String> getSaveFiles() {
            List<String> files = Directory.GetFiles(LOAD_FILE_PATH).ToList();
            return files;
        }
    }
}
