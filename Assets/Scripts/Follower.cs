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
    public float bulletSpeed = 20;

    private float distanceTravelled;
    private Ball ball;

    private void Awake()
    {
        ball = GetComponent<Ball>();
        pathCreator = FindObjectOfType<PathCreator>();
    }

    // Update is called once per frame

    public void Follow(MoveType moveType)
    {
        float factor = 1.0f;

        if (moveType == MoveType.Backward) factor = -1.0f;
        
        if (ball.role == BallRole.Bullet)
        {
            ball.transform.Translate(bulletSpeed * Time.deltaTime * ball.direction.normalized * factor);
        }
        else
        {

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

    public bool IsOutOfRoad()
    {
        return this.distanceTravelled > pathCreator.path.length;
    }
    
}
