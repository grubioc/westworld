using System.Diagnostics;

namespace WestWorld
{
    public class StateMachine<T>
    {
        private T owner;
        private State<T> currentState;
        private State<T> previousState;
        private State<T> globalState;

        //Pointer to the agent that owns this instance
        public T Owner { get {return this.owner;} set{this.owner=value;} }
        public State<T> CurrentState { get{return this.currentState;} set{this.currentState=value;}}
        public State<T> PreviousState { get{return this.previousState;} set{this.previousState=value;}}
        public State<T> GlobalState { get{return this.globalState;} set{this.globalState=value;}}

        //Constructor
        public StateMachine(T owner)
        {
            Owner = owner;
            CurrentState = null;
            PreviousState = null;
            GlobalState = null;
        }

        
        //Use this methods to initialize the FSM:
        public void SetCurrentState(State<T> s)
        {
            CurrentState = s;
        }
        public void SetGlobalState(State<T> s)
        {
            GlobalState = s;
        }
        public void SetPreviousState(State<T> s)
        {
            PreviousState = s;
        }
       
        //Call this to update the FSM
        public void Update()
        {
            //If a global state exists, calls its execute method, else do nothing
            if (GlobalState!=null)
            {
                GlobalState.Execute(Owner);
            }
            
            //Same for the current state 
            if (CurrentState!=null)
            {
                CurrentState.Execute(Owner);
            }
        }
        
        //Change to a new state
        public void ChangeState(State<T> newState)
        {
            //Makes sure both states are valid before attemping to
            //call their methods
            Debug.Assert(CurrentState!=null && newState!=null);
            //Keep a record of the previous state
            PreviousState = CurrentState;
            //call the exit method of the existing state
            CurrentState.Exit(Owner);
            //Change state to the new state
            CurrentState = newState;
            //Call the entry method of the new state
            CurrentState.Enter(Owner);
        }

        //Change state back to the previous state
        public void RevertToPreviousState()
        {
            ChangeState(PreviousState);
        }

 
        //Returns true if the current state's type is equal to the type ot the
        //class passed as a parameter
        public bool isInState(State<T> st)
        {
            return CurrentState==st;
        }

        public bool HandleMessage(Telegram msg)
        {
            //First see if the current state is valid and that it can handle 
            //the message
            if (CurrentState!=null && CurrentState.OnMessage(Owner, msg))
            {
                return true;
            }
            //If not, and if a global state has been implemented, send
            //the message to the global state
            if (GlobalState!=null && GlobalState.OnMessage(Owner, msg))
            {
                return true;
            }
            return false;
        }

    }

}