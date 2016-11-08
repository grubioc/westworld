using System;
namespace WestWorld
{
    public class WifesGlobalState: State<Wife>
    {
        //Instance is static
        private static WifesGlobalState _Instance;
        public static WifesGlobalState Instance { 
            get
            {
                if (_Instance == null)
                {
                    _Instance = new WifesGlobalState();
                }
                return _Instance;
            }            
        }
        
        
        //Constructor is protected
        protected WifesGlobalState()
        {

        }

        public override void Enter(Wife wife)
        {

        }

        public override void Execute(Wife wife)
        {
            //1 in 10 chances of needing the bathroom
            if (Common.Random.Next(0,100)<=10)
            {
                wife.StateMachine.ChangeState(VisitBathroom.Instance);
            }
        }

        public override void Exit(Wife wife)
        {

        }

        public override bool OnMessage(Wife wife, Telegram msg)
        {
            switch (msg.Msg)
            {
                case (int)WestWorld.MessageType.HiHoneyImHome:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Message handled by {0} at time:{1}",
                    EnumsNames.GetNameOfEntity(wife.UniqueID),
                    DateTime.Now.Second);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0}: Hi honey. Let me make you some of mah fine country stew",
                    EnumsNames.GetNameOfEntity(wife.UniqueID));
                    
                    wife.StateMachine.ChangeState(CookStew.Instance);
                    return true;
                default:
                    return false;
            }
        }

    }
}