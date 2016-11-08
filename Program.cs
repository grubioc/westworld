using System;
using System.Threading;

namespace WestWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Creates a Miner
            Miner Bob = new Miner((int)EntityType.Miner);   
            //Creates his wife
            Wife  Elsa = new Wife((int)EntityType.Wife);

            //Register them with the entity manager
            EntityManager.Instance.RegisterEntity(Bob);
            EntityManager.Instance.RegisterEntity(Elsa);
            
            //Runs the miner and wife through a few Update calls
            for (int i = 0; i < 30; i++)
            {
                Bob.Update();
                Elsa.Update();

                MessageDispatcher.Instance.DispatchDelayedMessages();
                Thread.Sleep(800);
            }
            Console.Read();
        }
    }
    
}
