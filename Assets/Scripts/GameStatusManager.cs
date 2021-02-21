namespace DefaultNamespace
{
    public static class GameStatusManager
    {
        public static GameStatus status = GameStatus.Shooting;
        

        public static void StartGame()
        {
            
        }

        public static void StartShooting()
        {
            status = GameStatus.Shooting;
        }

        public static void InsertingNode()
        {
            status = GameStatus.Inserting;
        }
    }
}