using System;

namespace WestWorld
{
    public class VisitBathroom: State<Wife>
    {
        //Instance is static
        private static VisitBathroom _Instance;
        public static VisitBathroom Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new VisitBathroom();
                }
                return _Instance;
            }
        }


        //Constructor is protected
        protected VisitBathroom()
        {

        }


        public override void Enter(Wife wife)
        {
            Console.WriteLine("{0}: Walkin' to the can. Need to powda mah pretty li'lle nose", EnumsNames.GetNameOfEntity(wife.UniqueID));
        }

        public override void Execute(Wife wife)
        {
            Console.WriteLine("{0}: Ahhhhhhh! Sweet relief!", EnumsNames.GetNameOfEntity(wife.UniqueID));
            wife.StateMachine.RevertToPreviousState();
        }

        public override void Exit (Wife wife)
        {
            Console.WriteLine("{0}: leavin' the Jon", EnumsNames.GetNameOfEntity(wife.UniqueID));
        }

    }
}