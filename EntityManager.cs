using System.Collections.Generic;

namespace WestWorld
{
    public class EntityManager
    {
        private static EntityManager _Instance;
        public static EntityManager Instance 
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EntityManager();
                }
                return _Instance;
            }
        }

        public Dictionary<int, BaseGameEntity> EntityMap = new Dictionary<int, BaseGameEntity>();


        //Constructor
        public EntityManager()
        {

        }


        //This method stores a pointer to the entity in the Dictionary 
        //EntityMap at the index position indicated by the entity's ID 
        //(makes for faster access)
        public void RegisterEntity(BaseGameEntity e)
        {
            EntityMap.Add(e.UniqueID, e);
        } 

        //Returns a pointer to the entity with the ID given as a parameter 
        public BaseGameEntity GetEntityFromId(int e)
        {
            //Find the Entity
            return EntityMap[e];
        }

        //Removes the entity from the list
        public void RemoveEntity(BaseGameEntity e)
        {
            EntityMap.Remove(e.UniqueID);
        }
    }
}