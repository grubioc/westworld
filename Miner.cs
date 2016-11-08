namespace WestWorld
{
    public class Miner: BaseGameEntity
    {
        //The place where the miner is currently situated
        private LocationType location;
        //How many nuggets the miner has in his pockets
        private int goldCarried;
        //How much money the miner has deposited in the bank
        private int moneyInBank;
        //The higher the value, the thirstier the miner
        private int thirst;
        //The higher the value, the more tired the miner
        private int fatigue;
        private StateMachine<Miner> stateMachine;


        public LocationType Location { get {return this.location;} set {this.location=value;} }
        public int GoldCarried { get{return this.goldCarried;} set{this.goldCarried=value;} }
        public int MoneyInBank { get {return this.moneyInBank;} set {this.moneyInBank=value;} }
        public int Thirst { get {return this.thirst;} set{this.thirst=value;} }
        public int Fatigue { get {return this.fatigue;} set{this.fatigue=value;} }
        public StateMachine<Miner> StateMachine { get{return this.stateMachine;} set{this.stateMachine=value;} }


        //Call the base class constructor from within derived class's constructor
        public Miner(int uniqueID): base(uniqueID)
        {
            Location = LocationType.Shack;
            GoldCarried = 0;
            MoneyInBank = 0;
            Thirst = 0;
            Fatigue = 0;
            StateMachine = new StateMachine<Miner>(this);
            StateMachine.SetCurrentState(GoHomeAndSleepTilRested.Instance);
        }
        
        //Base class methods
        public override void Update()
        {
            Thirst += 1;
            StateMachine.Update();
        }
        public override bool HandleMessage(Telegram msg)
        {
            return StateMachine.HandleMessage(msg);
        }

        //Class methods
        public bool Fatigued()
        {
            return Fatigue > Common.TirednessThreshold;
        }

        public bool Thirsty()
        {
            return Thirst >= Common.ThisrtLevel;
        }

        public void DecreaseFatigue()
        {
            Fatigue -= 1;
        }

        public void IncreaseFatigue()
        {
            Fatigue += 1;
        }
        public void BuyAndDrinkAWhisley()
        {
            Thirst = 0;
            MoneyInBank -= 2;
        }
        public bool PocketsFull()
        { 
            if (this.goldCarried >= Common.MaxNuggets)
            {
                return true;
            }
            return false;
        }

    }
}