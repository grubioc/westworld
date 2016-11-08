using System;
namespace WestWorld
{
    //Entity will go to a bank and deposit any nuggets he is carrying. If the  
    //miner is subsequently wealthy enough he'll walk home, otherwise he'll 
    //keep going to get more gold 

    public class VisitBankAndDepositGold:State<Miner>
    {
        //Instance is static
        private static VisitBankAndDepositGold _Instance;
        public static VisitBankAndDepositGold Instance 
        { 
            get 
            {
                if (_Instance==null)
                {
                    _Instance = new VisitBankAndDepositGold();
                }
            return _Instance;}
        }
        
        //Constructor is protected
        protected VisitBankAndDepositGold()
        {
            Color = ConsoleColor.Green;
        }    


        public override void Enter(Miner miner)
        {
            //on entry the miner  makes sure he is located at the bank
            if (miner.Location!=LocationType.Bank)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("{0}: Goin' to the bank. Yes siree", EnumsNames.GetNameOfEntity(miner.UniqueID));
                miner.Location = LocationType.Bank;
            }
        }
        public override void Execute(Miner miner)
        {
            //Deposit the gold
            miner.MoneyInBank += miner.GoldCarried;
            miner.GoldCarried = 0;

            Console.ForegroundColor = Color;
            Console.WriteLine("{0}: Depositing gold. Total savings now:{1}", EnumsNames.GetNameOfEntity(miner.UniqueID), miner.MoneyInBank);

            //Whealty enough to have a well earned rest?
            if (miner.MoneyInBank>=Common.ComfortLevel)
            {
                Console.ForegroundColor = Color;
                Console.WriteLine("{0}: WooHoo! Rich enough for now. Back home to mah li'lle lady", EnumsNames.GetNameOfEntity(miner.UniqueID));
                miner.StateMachine.ChangeState(GoHomeAndSleepTilRested.Instance); 
            } else
            {
                //Otherwise, get more gold
                miner.StateMachine.ChangeState(EnterMineAndDigForNugget.Instance);
            }
        }

        public override void Exit(Miner miner)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine("{0}: Leavin' the bank", EnumsNames.GetNameOfEntity(miner.UniqueID));
        }

    }
}