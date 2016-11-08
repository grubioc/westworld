using System;

namespace WestWorld
{
    //Miner will go home and sleep until his fatigue is decreased sufficiently 
    public class GoHomeAndSleepTilRested:State<Miner>
    {
        //Instance is static
        private static GoHomeAndSleepTilRested _Instance;
        
        public static GoHomeAndSleepTilRested Instance { 
            get 
            {
                if(_Instance == null)
                {
                    _Instance = new GoHomeAndSleepTilRested();
                }
                return _Instance;
            } 
        }

        //Constructor is protected
        protected GoHomeAndSleepTilRested()
        {
            Color = ConsoleColor.Blue;
        }

        public override void Enter(Miner miner)
        {
            if (miner.Location!=LocationType.Shack)
            {
                Console.ForegroundColor=Color;
                Console.WriteLine("{0}: Walkin' home", EnumsNames.GetNameOfEntity(miner.UniqueID));
                miner.Location = LocationType.Shack;

                //Let the wife know I'm home
                MessageDispatcher.Instance.DispatchMessage(MessageDispatcher.SendMsgImmediately, 
                miner.UniqueID, (int)EntityType.Wife, (int)WestWorld.MessageType.HiHoneyImHome,
                MessageDispatcher.NoAdditionalInfo );
            }
        }

        public override void Execute(Miner miner)
        {
            //If miner is not fatigued start for nuggets again
            if (!miner.Fatigued())
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("{0}: What a God darn fantastic nap! Time to find more gold", EnumsNames.GetNameOfEntity(miner.UniqueID));
                miner.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance);
            } else
            {
                //Sleep
                miner.DecreaseFatigue();
                Console.ForegroundColor = Color;
                Console.WriteLine("{0}: ZZZZzzz...", EnumsNames.GetNameOfEntity(miner.UniqueID));
            }
        }

        public override void Exit(Miner miner)
        {
            Console.WriteLine("{0}: Leaving the house",EnumsNames.GetNameOfEntity(miner.UniqueID));
        }

        public override bool OnMessage(Miner miner, Telegram msg)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            switch (msg.Msg)
            {
                case (int)MessageType.StewReady:
                    Console.WriteLine("Message handled by {0} at time: {1}",
                    EnumsNames.GetNameOfEntity(miner.UniqueID), DateTime.Now.Second);

                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.WriteLine("{0}: Okay Hun, ahm a comin'",
                    EnumsNames.GetNameOfEntity(miner.UniqueID));

                    miner.StateMachine.ChangeState(EatStew.Instance);
                    return true;
                default:
                    return false;
            }


        }
    }
}