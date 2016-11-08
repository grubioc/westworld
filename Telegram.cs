using System;

namespace WestWorld
{
    public struct Telegram
    {
        //The entity that sent this Telegram
        public int Sender {get;set;}
        //The entity that is to receive this Telegram
        public int Receiver {get;set;}
        //The message itself. These are all enumerated in the file
        //MessageTypes.cs
        public int Msg {get;set;}
        //Messages can be dispatched immediately or delayed for a specified amount 
        //of time. If a delay is necessary, this field is stamped with the time 
        //the message should be dispatched.
        public double DispatchTime {get;set;}
        //Any additional information that may accompany the message
        public object ExtraInfo {get;set;}

        //Constructor
        public Telegram (int sender, int receiver, int msg, Double dispatchTime, object extraInfo)
        {
            Sender = sender;
            Receiver = receiver;
            Msg = msg;
            DispatchTime = dispatchTime;
            ExtraInfo = extraInfo;

        }
    }
}