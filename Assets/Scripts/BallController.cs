using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    Vector3 newVelocity;
    GameObject[] pointsArray;
    GameObject lastPoint;
    GameObject targetSpot;


    bool hasShooted;

    void Start()
    {
        pointsArray = TrajectoryController.Instance.points;
        lastPoint = pointsArray[pointsArray.Length - 1];
        targetSpot = GameObject.FindGameObjectWithTag("Target");
    }

    void Update()
    {        
        if (!hasShooted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                UIManager.Instance.TurnHintTextOff();
                TrajectoryController.Instance.velocity.x -= 0.3f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                foreach (var point in TrajectoryController.Instance.points)
                {
                    point.gameObject.SetActive(true);
                }
                TrajectoryController.Instance.velocity.y += 0.03f;                
            }

            if (Input.GetKeyUp(KeyCode.Space) || lastPoint.transform.position.x >= 8.2f)
            {
                hasShooted = true;
                newVelocity = TrajectoryController.Instance.velocity;
                TrajectoryController.Instance.velocity = new Vector3(newVelocity.x, 0, 0);
                foreach (var point in TrajectoryController.Instance.points)
                {
                    point.gameObject.SetActive(false);
                }
                ApplyTrajectory();                
            }            
        } else
        {
            ThrowCheckDistance();
        }        
    }

    void ApplyTrajectory()
    {
        rb.velocity = TrajectoryController.Instance.velocity;
        rb.velocity = newVelocity;
    }

    void ThrowCheckDistance()
    {
        float distanceX = targetSpot.transform.position.x - transform.position.x;

        if (distanceX <= 2 && transform.position.y <= -2.5f)
        {
            if(GameManager.Instance.CurrentGameState != GameState.OVER)
            {
                GameManager.Instance.ChangeState(GameState.OVER);
                hasShooted = false;
            }            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            StartCoroutine(Wait());            
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.UpdateScore(1);
    }
}