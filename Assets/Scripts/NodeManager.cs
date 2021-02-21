using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class NodeManager
    {
        private static LinkedList<Ball> ballList = new LinkedList<Ball>();
        private static Ball nodeBeingInserted = null;

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

            nodeBeingInserted = ballToInsert;
            //add to list
            LinkedListNode<Ball> previous = ballList.Find(position);
            ballList.AddBefore(previous, ballToInsert);
        }

        public static void MoveNode(MoveType moveType, LinkedListNode<Ball> node)
        {
            LinkedListNode<Ball> maybeTouchingNode = moveType == MoveType.Forward ? node.Next : node.Previous;

            node.Value.follower.Follow(moveType);

            if (maybeTouchingNode != null && node.Value.IsTouching(maybeTouchingNode.Value))
            {
                MoveNode(moveType, maybeTouchingNode);
            }
        }

        public static void Update()
        {
            switch (GameStatusManager.status)
            {
                case GameStatus.Shooting:
                    MoveBalls();
                    break;
                case GameStatus.Inserting:
                    InsertNode();
                    break;
            }
        }


        private static void InsertNode()
        {
            LinkedListNode<Ball> ballPos = ballList.Find(nodeBeingInserted);

            LinkedListNode<Ball> prev = ballPos.Previous;

            LinkedListNode<Ball> next = ballPos.Next;

            bool isTouching = false;
            if (prev != null && ballPos.Value.IsTouching(prev.Value))
            {
               MoveNode(MoveType.Backward, prev); 
               isTouching = true;
            }

            if (next != null && ballPos.Value.IsTouching(next.Value))
            {
                MoveNode(MoveType.Forward, next);
                isTouching = true;
            }

            if (!isTouching)
            {
                GameStatusManager.StartShooting();
            }
            
        }
        private static void MoveBalls()
        {
            if (ballList.Count > 0)
            {
                MoveNode(MoveType.Forward, ballList.First);
            }
        }
    }
}