using System;
namespace WestWorld
{
    //In this state the miner will walk to a goldmine and pick up a nugget
    //of gold. If the miner already has a nugget of gold he'll change state
    //to VisitBankAndDepositGold. If he gets thirsty he'll change state
    //to QuenchThirst
    public class EnterMineAndDigForNugget:State<Miner>
    {
        //Instance is static
        private static EnterMineAndDigForNugget _Instance;
        public static EnterMineAndDigForNugget Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EnterMineAndDigForNugget();
                }
                return _Instance;
            }
        }


        //Constructor is protected
        protected EnterMineAndDigForNugget()
        {
            Color = ConsoleColor.Red;
        }

        public override void Enter (Miner miner)
        {
            //If the miner is not already located at the goldmine, he must
            //change location to the gold mine
            if (miner.Location!=LocationType.Goldmine)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("{0}: Walkin' to the goldmine",EnumsNames.GetNameOfEntity(miner.UniqueID));
                miner.Location = LocationType.Goldmine;
            }
        }

        public override void Execute(Miner miner)
        {
            //the miner digs for gold until he is carrying in excess of MaxNuggets. 
            //'If he gets thirsty during his digging he packs up work for a while and 
            //'changes state to go to the saloon for a whiskey.
            miner.GoldCarried+=1;
            miner.IncreaseFatigue();

            Console.ForegroundColor = Color;
            Console.WriteLine("{0}: Pickin' up a nugget",EnumsNames.GetNameOfEntity(miner.UniqueID));

            //If enough gold mined, go and put it int the bank
            if (miner.PocketsFull())
            {
                miner.StateMachine.ChangeState(VisitBankAndDepositGold.Instance);
            }

            if (miner.Thirsty())
            {
                miner.StateMachine.ChangeState(QuenchThirst.Instance);
            }
        }

        public override void Exit(Miner miner)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("{0}:Ah'm leavin' the gold mine with mah pockets full o' sweet gold", EnumsNames.GetNameOfEntity(miner.UniqueID));
        }

    }
}