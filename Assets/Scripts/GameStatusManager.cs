namespace DefaultNamespace
{
    public static class GameStatusManager
    {
        public static GameStatus status = GameStatus.NotStart;
        

        public static void StartGame()
        {
            status = GameStatus.Started;
        }

        public static void StartShooting()
        {
            status = GameStatus.Shooting;
        }

        public static void InsertingNode()
        {
            status = GameStatus.Inserting;
        }

        public static void Ready()
        {
            status = GameStatus.Ready;
        }
    }
}