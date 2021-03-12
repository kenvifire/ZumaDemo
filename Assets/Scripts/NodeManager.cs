using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            switch (ball.role)
            {
                case BallRole.Bullet:
                    ball.Destroy();
                    GameStatusManager.Ready();
                    break;
                case BallRole.Follower:
                    ball.Destroy();
                    break;
            }
            
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
                case GameStatus.Started:
                case GameStatus.Ready:
                case GameStatus.Shooting:
                    DeleteDeadNodes();    
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
                CheckComb(); 
                GameStatusManager.Ready();
            }
            
        }

        private static void CheckComb()
        {
            Ball node = NodeManager.nodeBeingInserted;
            LinkedListNode<Ball> listNode = ballList.Find(node);
            Color color = node.color;
            LinkedListNode<Ball> prev = listNode;
            LinkedListNode<Ball> next = listNode;
            int leftCnt = 0, rightCnt = 0;
            while (prev.Previous != null && prev.Previous.Value.color == color)
            {
                prev = prev.Previous;
                leftCnt++;
            }

            while (next.Next != null && next.Next.Value.color == color)
            {
                next = next.Next;
                rightCnt++;
            }

            if (leftCnt + rightCnt >= 2)
            {
                LinkedListNode<Ball> curr = prev;
                for (int i = 0; i <= leftCnt + rightCnt; i++)
                {
                    curr.Value.isAlive = false;
                    curr = curr.Next;
                }
                    
            }
                
        }
        private static void MoveBalls()
        {
            if (ballList.Count > 0)
            {
                MoveNode(MoveType.Forward, ballList.First);
            }
        }

        private static void DeleteDeadNodes()
        {
            List<Ball> balls = new List<Ball>();

            foreach (var ball in ballList)
            {
                if (ball.IsDead())
                {
                   balls.Add(ball); 
                }
            }

            foreach (var ball in balls)
            {
                ballList.Remove(ball);
                DestroyBall(ball);
            }
            
        }
    }
}