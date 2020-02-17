using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomEnemy : Enemy
{
    [SerializeField]
    private Transform obstacleDetector;
    [SerializeField]
    private float detectRadius;
    [SerializeField]
    LayerMask groundDetectorLM;
    // Update is called once per frame
    public override void Update()
    {
        Movement();
        DetectObstacles();
    }

    private void DetectObstacles()
    {
        Collider2D hit = Physics2D.OverlapCircle(obstacleDetector.position, detectRadius, groundDetectorLM);
        if (hit != null)
        {
            ChangeScale();
        }
    }

    private void ChangeScale()
    {
        horizontal = -horizontal;
        transform.localScale = new Vector3(horizontal, this.transform.localScale.y, this.transform.localScale.z);
        GetComponentInChildren<CharacterCanvasController>().transform.localScale = new Vector3(this.transform.localScale.x,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.y,
                    GetComponentInChildren<CharacterCanvasController>().transform.localScale.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(obstacleDetector.transform.position, detectRadius);
    }
}
