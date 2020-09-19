using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Survival2D.Saving
{
    // This class load in awake the game data
    // IComponentSaved implementations are responsible to get the data 
    // This class is responsible for saving getting the data from each IRootComponentSaved
    public class SaveManagerBehaviour : MonoBehaviour
    {
        private const string FOLDER_PATH_FORMAT = "{0}/saves";
        private const string FILE_PATH_FORMAT = "{0}/{1}";


        [SerializeField] private int save_index = 0;

        private string[] save_files;

        // For error handling
        private string current_file_path = string.Empty;
        private bool is_current_error_handled = false;

        private List<IRootComponentSaved> componentToSave_collection = new List<IRootComponentSaved>();
        private static SaveData current_save_data = null;


        public string FolderPath { get { return string.Format(FOLDER_PATH_FORMAT, Application.persistentDataPath); } }

        private void Awake()
        {
            GetAllObjectToSave();
            GetAllSaveFiles();

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            LoadDataFromFile(save_index);
        }

        public void SaveData()
        {
            foreach (var component_root in componentToSave_collection)
            {
                var object_data = component_root.GetRooData();
                current_save_data.SaveObjectData(object_data);
            }
        }


        public void SaveDataToFile(int index)
        {
            string file_name = string.Empty;
            if (index < 0 || index >= save_files.Length)
            {
                file_name = GenerateFileName();
            }
            else
            {
                file_name = save_files[index];
            }

            SaveDataToFile(file_name);
        }


        public void SaveDataToFile(string file_name)
        {
            if (current_save_data == null) return;

            // Generates the file path
            var file_path = string.Format(FILE_PATH_FORMAT, FolderPath, file_name);

            SaveManager.Save(file_path, current_save_data, FormatType.JSON);
            GetAllSaveFiles();
        }

        public void LoadDataFromFile(int index)
        {
            string file_name = string.Empty;
            if (index < 0 || index >= save_files.Length)
            {
#if UNITY_EDITOR
                Debug.Log("ERROR: index out of the file array");
#endif
                current_save_data = new SaveData();
                return;
            }
            else
            {
                file_name = save_files[index];
            }

            LoadDataFromFile(file_name);
        }


        public void LoadDataFromFile(string file_name)
        {
            current_file_path = string.Format(FILE_PATH_FORMAT, FolderPath, file_name);
            is_current_error_handled = false;

            current_save_data = SaveManager.Load(current_file_path, FormatType.JSON, Handler_OnErrorLoadingSave);

            // For handling error in loading data, change extension and try to load another file
            // TODO: maybe change how to handle the corrupted file
            if (current_save_data == null)
            {
                if (is_current_error_handled) return;
                Debug.Log(current_file_path);
                File.Move(current_file_path, Path.ChangeExtension(current_file_path, ".save_corrupted"));
                GetAllSaveFiles();
                LoadDataFromFile(save_index);

                is_current_error_handled = true;
            }
        }

        private void GetAllObjectToSave()
        {
            var componentToSaveArray = FindObjectsOfType<MonoBehaviour>().OfType<IRootComponentSaved>();
            foreach (var component in componentToSaveArray)
            {
                componentToSave_collection.Add(component);
                component.LoadDataMethod = Handler_LoadData;
            }
        }

        private void GetAllSaveFiles()
        {
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            var complete_path_array = Directory.GetFiles(FolderPath, "*.save");
            save_files = new string[complete_path_array.Length];

            for (int i = 0; i < save_files.Length; i++)
            {
                save_files[i] = Path.GetFileName(complete_path_array[i]);
            }
        }

        private string GenerateFileName()
        {
            var time = DateTime.Now;
            var time_string = time.ToString("u");
            var length = time_string.Length;
            var time_formatted = time_string.Replace(':', ';').Substring(0, length - 1);
            return time_formatted + ".save";
        }

        private ObjectSavedData Handler_LoadData(string id)
        {
            if (current_save_data.TryLoadObjectData(id, out var data))
            {
                return data;
            }

            return null;
        }


        private void Handler_OnErrorLoadingSave(object o, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            if (e.CurrentObject == e.ErrorContext.OriginalObject)
            {
                e.ErrorContext.Handled = true;
            }
        }

        #region DEBUG_CODE
#if UNITY_EDITOR
        [ContextMenu("Save data")]
        private void Debug1()
        {
            SaveDataToFile(save_files.Length - 1);
        }

        [ContextMenu("Save new data")]
        private void Debug2()
        {
            SaveDataToFile(GenerateFileName());
        }

        [ContextMenu("load last data")]
        private void Debug3()
        {
            LoadDataFromFile(save_files.Length - 1);
        }
#endif
        #endregion
    }
}