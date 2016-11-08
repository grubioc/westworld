using System;
namespace WestWorld
{
    public class DoHouseWork: State<Wife>
    {
        //Instance is static
        private static DoHouseWork _Instance; 

        public static DoHouseWork Instance {  
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DoHouseWork();
                }
                return _Instance;
            }
        }

        //Constructor is protected
        protected DoHouseWork()
        {

        }
        public override void Enter (Wife wife)
        { 

        }

        public override void Execute (Wife wife)
        {
            int Chore = Common.Random.Next(0,2);
            switch (Chore)
            {
                case 0: 
                    Console.WriteLine("{0}: Moppin' the floor", EnumsNames.GetNameOfEntity(wife.UniqueID));
                    break;
                case 1:
                    Console.WriteLine("{0}: Washin' the dishes", EnumsNames.GetNameOfEntity(wife.UniqueID));
                    break;
                case 3:
                    Console.WriteLine("{0}: Makin' the bed", EnumsNames.GetNameOfEntity(wife.UniqueID));
                    break;    
                default:
                    Console.WriteLine("ERROR");
                    break;
            }
        }

        public override void Exit(Wife wife)
        {

        }

    }

}