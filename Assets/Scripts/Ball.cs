using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PathCreation;
using UnityEngine;
using System;
using PathCreation.Examples;

public class Ball : MonoBehaviour
{
    [SerializeField] public BallRole role;
    public PathCreator pathCreator;
    public Follower follower;
    public float speed = 5;
    public float bulletSpeed = 20;
    public Vector3 direction;
    public Color color;
    [SerializeField] public MoveType moveType;
    public CircleCollider2D circleCollider2D;
    public string nodeGuid;

    private Color[] colors = new[]
    {
        Color.black,
        Color.blue,
        Color.green,
        Color.yellow
    };

    private void Awake()
    {
        follower = GetComponent<Follower>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        nodeGuid = Guid.NewGuid().ToString();
    }

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
                // FollowPath();
                break;
            case BallRole.Bullet:
                MovingForward();
                break;
        }
    }



    void MovingForward()
    {
        transform.Translate(speed * Time.deltaTime * direction.normalized);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Ball otherBall = other.GetComponent<Ball>();
        if (otherBall == null) return;
        if (otherBall.role == BallRole.Follower)
        {
            return;
        }
        else
        {
            float distanceTravelled = follower.GetDistanceTravelled() - 2.0f;
            otherBall.role = BallRole.Follower;
            NodeManager.InsertBallAfter(otherBall, GetComponent<Ball>());
            
            otherBall.GetComponent<Follower>().SetDistanceTravelled(distanceTravelled);
            otherBall.transform.position = follower.pathCreator.path.GetPointAtDistance(distanceTravelled);
            GameStatusManager.InsertingNode();

        }
        
        
    }

    public bool IsTouching(Ball other)
    {
        if (other != null)
        {
            return this.circleCollider2D.IsTouching(other.circleCollider2D);
        }

        return false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
