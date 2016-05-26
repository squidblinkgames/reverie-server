namespace Reverie.Models
{
    using System;
    using PrimitiveEngine.Components;


    // TODO: This is probably going to only have data from base Component type; may have to fix.
    public class ComponentDatabaseEntry
    {
        #region Fields
        private int entityInstanceId;
        private Component component;
        #endregion


        #region Properties
        public Component Component
        {
            get { return this.component; }
            set { this.component = value; }
        }


        public int EntityInstanceId
        {
            get { return this.entityInstanceId; }
            set { this.entityInstanceId = value; }
        }
        #endregion
    }
}