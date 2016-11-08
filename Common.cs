using System;

namespace WestWorld
{
    public class Common
    {
        //The amount of gold a miner must have before he feels comfortable
        public const int ComfortLevel = 5;
        //The amount of nuggets a miner can carry
        public const int MaxNuggets = 3;
        //Above this value a miner is thirsty
        public const int ThisrtLevel = 5;
        //Above this value a miner is sleepy
        public const int TirednessThreshold = 5;

        private static Random _Random;
        public static Random Random 
        { 
            get
                {
                    if (_Random==null)
                        _Random = new Random(DateTime.Now.Millisecond);
                    return _Random;
                }
        }
    }
}