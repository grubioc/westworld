using System;

namespace WestWorld
{
    public class EatStew:State<Miner>
    {
        private static EatStew _Instance;

        public static EatStew Instance 
        { get 
            {
                if (_Instance == null)
                {
                    _Instance = new EatStew();
                }
                return _Instance;
            }
        }

        public EatStew(){}

        public override void Enter(Miner miner)
        {
            Console.WriteLine("{0}: Smells Reaaaal gooood Elsa!", EnumsNames.GetNameOfEntity(miner.UniqueID));
        }
        public override void Execute(Miner miner)
        {
            Console.WriteLine("{0}: Tastes real good too!", EnumsNames.GetNameOfEntity(miner.UniqueID));
            miner.StateMachine.RevertToPreviousState();
        }

        public override void Exit(Miner miner)
        {
            Console.WriteLine("{0}: Thankya li'lle lady. Ah better get back to whatever ah wuz doin", EnumsNames.GetNameOfEntity(miner.UniqueID));
        }

    }
}