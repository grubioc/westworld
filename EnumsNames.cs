namespace WestWorld
{
    public static class EnumsNames
    {
        public static string GetNameOfEntity(int entity)
        {
            switch (entity)
            {
                case (int)EntityType.Miner:
                    return "Miner Bob";
                case (int)EntityType.Wife:
                    return "Elsa";
                default:
                    return "Not Recognized!";
            }

        }

        public static string GetMessageDescription(int msg)
        {
            switch (msg)
            {
                case (int)MessageType.HiHoneyImHome:
                    return "HiHoneyImHome";
                case (int)MessageType.StewReady:
                    return "StewReady";
                default:
                    return "Not Recognized!";
            }
        }
    }
}