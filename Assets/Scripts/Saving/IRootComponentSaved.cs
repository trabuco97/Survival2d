using UnityEngine;
using System.Collections.Generic;

namespace Survival2D.Saving
{
    public delegate ObjectSavedData LoadObjectDataMethod(string id);
    /// <summary>
    /// Implemented by all behaviours that need its data to saved or loaded
    /// </summary>
    public interface IComponentSaved
    {
        /// <summary>
        /// Setter for the data loaded. Always cast to desired data
        /// </summary>
        object DataToManage { set; }
        /// <summary>
        ///  Identifier for the manager
        /// </summary>
        string Component_ID { get; }
        /// <summary>
        /// For components that shouldnt be saved in certain gameobjects
        /// Inspector setted
        /// </summary>
        bool CanBeSaved { get; }        

        object GetComponentData();
    }


    /// <summary>
    /// Root component that manages the saving/loading of the object.
    /// <para>It has to be unique in the root gameobject</para>
    /// </summary>
    public abstract class IRootComponentSaved : MonoBehaviour, IComponentSaved
    {
        public abstract string Component_ID { get; }
        public abstract object DataToManage { set; }
        public abstract bool CanBeSaved { get; }


        private Dictionary<string, IComponentSaved> componentChild_database = new Dictionary<string, IComponentSaved>();


        /// <summary>
        /// Invoked every time the component has to load data.
        /// Method provided by the SaveManagerBehaviour 
        /// </summary>
        public LoadObjectDataMethod LoadDataMethod;

        protected virtual void Awake()
        {
            GetAllChildSavableComponents();
        }


        private void GetAllChildSavableComponents()
        {
            var child_array = GetComponentsInChildren<IComponentSaved>();
            for (int i = 0; i < child_array.Length; i++)
            {
                var child = child_array[i];
                if (child.CanBeSaved)
                {
                    componentChild_database.Add(child.Component_ID, child);
                }
            }
        }

        /// <summary>
        /// Called when the object has to load all its data
        /// </summary>
        public void LoadData()
        {
            var data = LoadDataMethod(Component_ID);
            if (data != null)
            {
                foreach (var pair in data.componentData_database)
                {
                    if (componentChild_database.TryGetValue(pair.Key, out var component))
                    {
                        component.DataToManage = pair.Value;
                    }
                }
            }
        }

        public ObjectSavedData GetRooData()
        {
            var object_data = new ObjectSavedData();
            object_data.id = Component_ID;
            object_data.componentData_database = new Dictionary<string, object>();


            foreach (var component in componentChild_database.Values)
            {
                var data = component.GetComponentData();
                object_data.componentData_database.Add(component.Component_ID, data);
            }

            return object_data;
        }

        public virtual object GetComponentData()
        {
            return null;
        }
    }

}