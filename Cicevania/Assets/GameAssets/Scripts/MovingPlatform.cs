using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    List<Transform> wayPoints = new List<Transform>();
    [SerializeField]
    Stack<Transform> returnPath = new Stack<Transform>();
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    float waitTime;
    [SerializeField]
    PhysicsMaterial2D antibug;
    [SerializeField]
    PhysicsMaterial2D onPlatform;

    Transform currentWP;
    private bool canMove = true;
    private int wpIndex = 0;


    private void Start()
    {
        currentWP = wayPoints[wpIndex];
        returnPath.Push(wayPoints[wpIndex]);
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        Vector2 dir = (currentWP.position - this.transform.position).normalized;
        Vector2 nextPos = (Vector2)this.transform.position + dir * moveSpeed * Time.deltaTime;
        this.GetComponent<Rigidbody2D>().MovePosition(nextPos);
        if (Vector3.Distance(this.transform.position, currentWP.transform.position) < 0.1f)
        {
            canMove = false;
            StartCoroutine(WaitOnStop());
        }
    }

    IEnumerator WaitOnStop()
    {
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        NextWayPoint();
    }

    void NextWayPoint()
    {
        if (wpIndex < wayPoints.Count-1)
        {
            wpIndex++;
            returnPath.Push(wayPoints[wpIndex]);
            currentWP = wayPoints[wpIndex];
        }
        else
        {
            if (returnPath.Count > 0) currentWP = returnPath.Pop();
            else
            {
                returnPath.Push(currentWP);
                wpIndex = 1;
                currentWP = wayPoints[wpIndex];
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            col.collider.sharedMaterial = onPlatform;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            col.collider.sharedMaterial = antibug;
        }
    }
}
