using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject ball;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mousePosition = Input.mousePosition;

        Vector3 objPosition = Camera.main.WorldToScreenPoint(transform.position);

        float deltaX = mousePosition.x - objPosition.x;
        float deltaY = mousePosition.y - objPosition.y;

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(transform.position, new Vector3(deltaX, deltaY, 0));    
        }


    }
    void Shoot(Vector3 initPos, Vector3 direction)
    {
        Ball bulletBall = Instantiate(ball, initPos, Quaternion.identity).GetComponent<Ball>();
        bulletBall.role = BallRole.Bullet;
        bulletBall.direction = direction;
    }
}
