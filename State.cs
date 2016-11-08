using System;

namespace WestWorld 
{
    //Abstract base class to define an interface for a state
    public abstract class State<T>
    {
        public ConsoleColor Color;

        //This will execute when the state is entered
        public abstract void Enter(T p);
        
        //This is the state's normal update function
        public abstract void Execute(T p);

        //This will execute when the state is exited
        public abstract void Exit(T p);

        //This executes if the agent receives a message from the
        //MessageDispatcher
        public virtual bool OnMessage(T p1, Telegram p2)
        {
            return false;
        }
    }
}