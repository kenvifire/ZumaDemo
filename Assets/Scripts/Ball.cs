using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PathCreation;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    [SerializeField] public BallRole role;
    public PathCreator pathCreator;
    public float speed = 5;
    public float bulletSpeed = 10;
    private float distanceTravelled;
    public Vector3 direction;
    public Color color;

    private Color[] colors = new[]
    {
        Color.black,
        Color.blue,
        Color.green,
        Color.yellow
    };
    
    // Start is called before the first frame update
    void Start()
    {
        var colorIdx = new System.Random().Next(colors.Length);
        GetComponent<SpriteRenderer>().color = colors[colorIdx];
    }

    // Update is called once per frame
    void Update()
    {

        switch (role)
        {
            case BallRole.Follower:
                FollowPath();
                break;
            case BallRole.Bullet:
                MovingForward();
                break;
        }
    }

    void FollowPath()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        // transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }

    void MovingForward()
    {
        transform.Translate(speed * Time.deltaTime * direction.normalized);
    }
}
