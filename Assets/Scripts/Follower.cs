using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed = 5;

    private float distanceTravelled;
    private Ball ball;

    private void Awake()
    {
        ball = GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.role == BallRole.Bullet)
        {
            ball.transform.Translate(speed * Time.deltaTime * ball.direction.normalized);
        }
        else
        {
            float factor = 1.0f;

            if (ball.moveType == MoveType.Backward) factor = -1.0f;
            distanceTravelled += speed * Time.deltaTime * factor;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            //Rotation will make the 2D ball disappear
            // transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        }
    }

    public void SetDistanceTravelled(float newDistance)
    {
        this.distanceTravelled = newDistance;
    }

    public float GetDistanceTravelled()
    {
        return this.distanceTravelled;
    }
    
}
