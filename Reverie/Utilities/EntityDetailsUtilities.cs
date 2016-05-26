namespace Reverie.Utilities
{
    using System.Collections.Generic;
    using System.Linq;
    using PrimitiveEngine;
    using Reverie.Components;


    public static class EntityDataUtilities
    {
        public static Entity CreateEntity(
            this EntityWorld entityWorld,
            string name,
            string description,
            Dictionary<string, DynamicValue> properties = null)
        {
            Entity entity = entityWorld.CreateEntity();
            EntityDetails entityDetails = new EntityDetails(name, description);
            entity.AddComponent(entityDetails);

            return entity;
        }


        public static Entity GetEntityByName(this IEnumerable<Entity> entityList, string name)
        {
            // TODO: Parse indexes for finding specific items out of duplicates.
            return
                (from item in entityList
                 where item.GetEntityName().ToLower().Equals(name)
                 select item).FirstOrDefault();
        }


        public static EntityDetails GetEntityDetails(this Entity entity)
        {
            return entity.GetComponent<EntityDetails>();
        }


        public static string GetEntityName(this Entity entity)
        {
            EntityDetails entityDetails = entity.GetComponent<EntityDetails>();
            if (entityDetails == null)
                return null;

            return entityDetails.Name;
        }
    }
}