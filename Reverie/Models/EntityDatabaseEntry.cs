namespace Reverie.Models
{
    using System;


    public class EntityDatabaseEntry
    {
        #region Fields
        private int instanceId;
        private Guid uniqueId;
        #endregion


        #region Properties
        public int InstanceId
        {
            get { return this.instanceId; }
            set { this.instanceId = value; }
        }


        public Guid UniqueId
        {
            get { return this.uniqueId; }
            set { this.uniqueId = value; }
        }
        #endregion
    }
}