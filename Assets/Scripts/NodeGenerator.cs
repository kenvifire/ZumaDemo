using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PathCreation;
using UnityEngine;

public class NodeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject node;
    [SerializeField] private PathCreator pathCreator;
    private float deltaTime = 0.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > 1)
        {
            if (GameStatusManager.status == GameStatus.Shooting)
            {
                Ball ball = Instantiate(node, transform).GetComponent<Ball>();
                ball.role = BallRole.Follower;
                NodeManager.AddBall(ball);
            }

            deltaTime -= 1;
        } 
    }
}
