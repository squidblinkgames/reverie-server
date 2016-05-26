namespace Reverie.State
{
    using System;
    using System.Collections.Generic;
    using PrimitiveEngine;
    using PrimitiveEngine.Components;
    using Reverie.Models;


    public class ReverieWorld
    {
        #region Fields
        private IReverieDatabase database;
        private EntityWorld entityWorld;
        #endregion


        #region Constructors
        public ReverieWorld(IReverieDatabase database)
        {
            this.database = database;
        }
        #endregion


        #region Properties
        public IReverieDatabase Database
        {
            get { return this.database; }
            set { this.database = value; }
        }
        #endregion


        public void Load()
        {
            this.entityWorld = new EntityWorld();
            IList<EntityDatabaseEntry> entities = this.database.GetAllEntities();
            IList<ComponentDatabaseEntry> components = this.database.GetAllComponents();

            foreach (EntityDatabaseEntry entry in entities)
            {
                this.entityWorld.CreatEntity(entry.InstanceId, entry.UniqueId);
            }
            
            foreach (ComponentDatabaseEntry entry in components)
            {
                Entity entity = this.entityWorld.GetEntity(entry.EntityInstanceId);
                if (entity != null)
                    entity.AddComponent(entry.Component);
            }
        }


        public void Save()
        {
            var allComponents = this.entityWorld.EntityManager.ComponentsByType;
            foreach (Bag<Component> componentCollection in allComponents)
            {
                if (componentCollection.IsEmpty)
                    continue;
                string table = componentCollection[0].GetType().Name;
                for (int i = 0; i < componentCollection.Count; i++)
                {
                    ComponentDatabaseEntry entry = new ComponentDatabaseEntry();
                    entry.EntityInstanceId = i;
                    entry.Component = componentCollection[i];
                    this.database.UpdateComponent(table, entry);
                }
            }
        }
    }
}