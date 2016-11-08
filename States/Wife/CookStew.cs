using System;
namespace WestWorld
{
    public class CookStew:State<Wife>
    {
        private static CookStew _Instance;
        public static CookStew Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CookStew();
                }
                return _Instance;
            }
        }
        
        //Constructor
        protected CookStew()
        {

        }

        public override void Enter(Wife wife)
        {
            if (!wife.Cooking)
            {
                Console.WriteLine("{0}: Putting the stew in the oven", EnumsNames.GetNameOfEntity(wife.UniqueID));
                //Send a delayed message myself so that I know when to take the stew out of the oven
                MessageDispatcher.Instance.DispatchMessage(1.5, wife.UniqueID, wife.UniqueID, (int)MessageType.StewReady,
                MessageDispatcher.NoAdditionalInfo);
                
                wife.Cooking=true;
            }
        }

        public override void Execute(Wife wife)
        {
            Console.WriteLine("{0}: Fussin' over food", EnumsNames.GetNameOfEntity(wife.UniqueID));
        }

        public override void Exit(Wife wife)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0}: Puttin' the stew on the table", EnumsNames.GetNameOfEntity(wife.UniqueID));
       
        }

        public override bool OnMessage(Wife wife, Telegram telegram)
        {
            switch (telegram.Msg)
            {
                case (int)MessageType.StewReady:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Message received by {0} at time: {1}",
                    EnumsNames.GetNameOfEntity(wife.UniqueID), DateTime.Now.Second);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0}: StewReady! Lets eat", EnumsNames.GetNameOfEntity(wife.UniqueID));
                    
                    //Let hubby know the stew is ready
                    MessageDispatcher.Instance.DispatchMessage(MessageDispatcher.SendMsgImmediately,
                    wife.UniqueID, (int)EntityType.Miner,(int)MessageType.StewReady, MessageDispatcher.NoAdditionalInfo);
                    
                    wife.Cooking = false;
                    wife.StateMachine.ChangeState(DoHouseWork.Instance);
                    return true;

                default:
                    return false;
            }
        }

    }
}