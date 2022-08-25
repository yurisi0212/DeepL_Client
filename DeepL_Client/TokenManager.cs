using System;
using System.IO;
using System.Text;

namespace DeepL_Client {
    public class TokenManager {

        private readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\yurisi\DeepL_Client\";

        private readonly string filename = "token";

        public string DeepL_Token { get; private set; }

        public TokenManager() {
            Directory.CreateDirectory(path);
            if (!File.Exists(path + filename)) {
                File.Create(path + filename).Close();
            }
            DeepL_Token = Read();
        }


        public void SetToken(string token) {
            if (File.Exists(path + filename)) {
                File.Delete(path + filename);
            }
            File.Create(path + filename).Close();
            Encoding enc = Encoding.GetEncoding("utf-8");
            using (StreamWriter writer = new StreamWriter(path + filename, true, enc)) {
                writer.WriteLine(token);
            }
            DeepL_Token = token;
        }

        private string Read() {
            using (StreamReader file = new StreamReader(path + filename)) {
                string line;
                while ((line = file.ReadLine()) != null) {
                    return line;
                }
            }
            return "";
        }
    }
}
