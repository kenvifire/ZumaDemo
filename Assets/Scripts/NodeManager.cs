using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class NodeManager
    {
        private static LinkedList<Ball> ballList = new LinkedList<Ball>();

        public static bool HasBall(Ball ball)
        {
            return ballList.Contains(ball);
        }
        public static void AddBall(Ball ball)
        {
            ballList.AddFirst(ball);
        }

        public static void DestroyBall(Ball ball)
        {
            
        }

        public static int GetBallPosition(Ball ball)
        {
            int idx = 0;
            foreach (var b in ballList)
            {
                if (b == ball)
                {
                    return idx;
                }

                idx++;
            }

            return -1;
        }

        public static void InsertBallAfter(Ball ballToInsert, Ball position)
        {
            //set position
            
            
            //add to list
            LinkedListNode<Ball> previous = ballList.Find(position);
            ballList.AddAfter(previous, ballToInsert);
            
            
        }
    }
}