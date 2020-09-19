using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

using Newtonsoft.Json;

namespace Survival2D.Saving
{
    public enum FormatType { Binary, JSON }

    public static class SaveManager
    {
        public static bool Save(string file_path, SaveData data, FormatType type)
        {
            if (File.Exists(file_path))
            {
                File.Delete(file_path);
            }

            var stream = File.Create(file_path);
            if (type == FormatType.Binary)
            {
                var formatter = GetBinaryFormatter();
                formatter.Serialize(stream, data);
            }
            else
            {
                using (StreamWriter writter = new StreamWriter(stream))
                {
                    writter.Write(GetDataParsed(data));
                }

            }

            stream.Close();

            return true;
        }

        public static void EraseSave(string name)
        {
            string file_path = Application.persistentDataPath + "/saves/" + name + ".save";
            if (File.Exists(file_path))
            {
                File.Delete(file_path);
            }
        }

        public static SaveData Load(string file_path, FormatType type, EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> error_handler = null)
        {
            SaveData output = null;
            if (!File.Exists(file_path))
            {
                return output;
            }


            var stream = File.Open(file_path, FileMode.Open);
            
            if (type == FormatType.Binary)
            {
                var formatter = GetBinaryFormatter();
                output = (SaveData)formatter.Deserialize(stream);
            }
            else
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    output = GetDataFromParse(reader.ReadToEnd(), error_handler);
                }
            }


            stream.Close();

            return output;
        }

        private static string GetDataParsed(SaveData data)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };


            return JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        }


        private static SaveData GetDataFromParse(string data_parsed, EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> error_handler = null)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All
            };

            if (error_handler != null)
            {
                settings.Error = error_handler;
            }

            return JsonConvert.DeserializeObject<SaveData>(data_parsed, settings);
        }

        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter;
        }

    }
}