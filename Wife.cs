//using System;
namespace WestWorld
{
    public class Wife: BaseGameEntity
    {
        
        private LocationType location;
        private StateMachine<Wife> stateMachine;


        public LocationType Location { get{return this.location;} set{this.location=value;} }
        public StateMachine<Wife> StateMachine { get{return this.stateMachine;} set{this.stateMachine=value;} }
        public bool Cooking { get; set; }
        

        //Constructor
        public Wife(int uniqueID): base(uniqueID)
        {
            Location = LocationType.Shack;
            StateMachine = new StateMachine<Wife>(this);
            StateMachine.SetCurrentState(DoHouseWork.Instance);
            StateMachine.SetGlobalState(WifesGlobalState.Instance);
        }

        //Base class methods
        public override void Update()
        {
            //Console.ForegroundColor = ConsoleColor.Green;
            StateMachine.Update();

        }
        public override bool HandleMessage(Telegram msg)
        {
            return StateMachine.HandleMessage(msg);
        }
    }
    
}