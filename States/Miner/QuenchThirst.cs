using System;

namespace WestWorld
{
    public class QuenchThirst:State<Miner>
    {
        //Instance is static
        private static QuenchThirst _Instance;
        public static QuenchThirst Instance 
        { 
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new QuenchThirst();
                } 
            return _Instance;}
        }


        //Constructor is protected
        protected QuenchThirst()
        {
            Color = ConsoleColor.Yellow;
        }

        public override void Enter(Miner miner)
        {
            if (miner.Location!=LocationType.Saloon)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("{0}: Boy, ah sure is thusty! Walking to the saloon!", EnumsNames.GetNameOfEntity(miner.UniqueID));
            }
        }

        public override void Execute(Miner miner)
        {
            if (miner.Thirsty())
            {
                miner.BuyAndDrinkAWhisley();

                Console.ForegroundColor= Color;
                Console.WriteLine("{0}: That's mighty fine sippin liquer", EnumsNames.GetNameOfEntity(miner.UniqueID));
                miner.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance);
            } else
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("ERROR");
            }
        }

        public override void Exit(Miner miner)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("{0}: Leaving the saloon, feelin' good", EnumsNames.GetNameOfEntity(miner.UniqueID));
        }
        
    }
}