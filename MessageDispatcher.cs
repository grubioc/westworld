using System;
using System.Collections.Generic;

namespace WestWorld
{
    public class MessageDispatcher
    {
        //To make code easy to read
        public const double SendMsgImmediately = 0.0f;
        public const int NoAdditionalInfo = 0;
        
        
        private static MessageDispatcher _Instance;
        public static MessageDispatcher Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MessageDispatcher();
                }
                return _Instance;
            }
        }
        
        public SortedSet<Telegram> PriorityQ = new SortedSet<Telegram>(new TelegramComparer());
        internal class TelegramComparer: IComparer<Telegram>
        {
            public int Compare(Telegram x, Telegram y)
            {
                //First by dispatchtime
                int result = x.DispatchTime.CompareTo(y.DispatchTime);

                return result;
            }
        }

        //Constructor
        protected MessageDispatcher()
        {

        }

        private void Discharge (BaseGameEntity receiver, Telegram telegram)
        {
            if (!receiver.HandleMessage(telegram))
            {
                //Telegram could not be handled
                Console.WriteLine("Message not handled");
            }
        }
        
        //send a message to another agent
        public void DispatchMessage(double delay, int sender, int receiver, int msg, object extraInfo)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
           
            //Get pointers to the sender and receiver
            BaseGameEntity msgSender = EntityManager.Instance.GetEntityFromId(sender);
            BaseGameEntity msgReceiver = EntityManager.Instance.GetEntityFromId(receiver);
           
            //Make sure the receiver is valid
            if (receiver.Equals(null))
            {
                Console.WriteLine("Warning! No Receiver with ID of {0} found", receiver);
            }
            //Create the Telegram
            Telegram telegram = new Telegram(sender, receiver, msg, 0.0f, extraInfo);

            //if there is no delay, route the telegram immediatly
            if (delay<=0.0)
            {
                Console.WriteLine("Instant telegram dispatched at time {0} by {1} for {2}", 
                DateTime.Now, EnumsNames.GetNameOfEntity(msgSender.UniqueID),  
                EnumsNames.GetNameOfEntity(msgReceiver.UniqueID));
                Console.WriteLine("Msg is {0}", EnumsNames.GetMessageDescription(msg));
                
                //Send the telegram to the recipient
                Discharge(msgReceiver, telegram);
            }
            else
            {
                //else calculate the time when the telegram should be dispatched
                double currentTime = DateTime.Now.Second;
                telegram.DispatchTime = currentTime + delay;
                //and put it in the queue
                PriorityQ.Add(telegram);

                Console.WriteLine("Delayed telegram from {0} recorded at time {1} for {2}", 
                EnumsNames.GetNameOfEntity(msgSender.UniqueID), DateTime.Now, 
                EnumsNames.GetNameOfEntity(msgReceiver.UniqueID));
                Console.WriteLine("Msg is {0}", EnumsNames.GetMessageDescription(msg));
                
            }
        }
        //Send out any delayed messages. This method is called each time through
        //the main game loop
        public void DispatchDelayedMessages()
        {
            //first get current time
            double currentTime = DateTime.Now.Second;

            //Now peek at the queue to see if ane telegrams need dispatching.
            //remove all telegrams from the front of the queue that have gone
            //past their sell-by date.
            while(PriorityQ.Min.DispatchTime < currentTime && PriorityQ.Min.DispatchTime >0)
            {
                //Read the telegram from the front of the queue
                Telegram telegram = PriorityQ.Min;
                
                //Find the recipient
                BaseGameEntity msgReceiver = EntityManager.Instance.GetEntityFromId(telegram.Receiver);
                    
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Queude telegram ready for dispatch: Sent to {0}",
                EnumsNames.GetNameOfEntity(msgReceiver.UniqueID));
                Console.WriteLine("Msg is {0}", EnumsNames.GetMessageDescription(telegram.Msg));

                //Send the telegram to the recipient
                Discharge(msgReceiver,telegram);
                    
                //Remove it from the queue
                PriorityQ.Remove(telegram);
            }
        }
    }
}