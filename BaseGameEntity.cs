using System.Diagnostics;

namespace WestWorld
{
    //Base class for a Game Object
    public abstract class BaseGameEntity
    {
        //This is the next valid ID. Each time a BaseGameEntity is instantiated
        //this value is updated  
        private static int _NextID = 0;
        //Every entity must have a unique identifying number
        private int uniqueID;


        public static int NextID { get {return _NextID;}}
        public int UniqueID { 
            get
            {
                return this.uniqueID;
            } 
            set
            {
                this.uniqueID = value;
                _NextID   = this.uniqueID + 1;
            } 
        }

        //Contructor
        protected BaseGameEntity(int id){
            SetId(id);
        }

        //This must be called within the constructor to make sure the ID is set
        //correctly. It verifies that the value passed to the method is greater
        //or equal to the next valid ID, before setting the ID and incrementing
        //the next valid ID
        internal void SetId(int id)
        {
            //Make sure the val is equal or greater than the next available ID
            Debug.Assert(id >= NextID);
            this.UniqueID = id;
        }

        //All entities must implement an update function
        public abstract void Update();

        //All subclasses can communicate using messages
        public abstract bool HandleMessage(Telegram msg);
    }   
}