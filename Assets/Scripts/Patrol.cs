using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Vector2 PatrolDistance;
    public float WalkSpeed = 5;
    public Rigidbody2D MyRigidBody;

    public bool IsPatrolling { get; set; }

    private Vector2 patrolStart;
    private Vector2 patrolTarget;
    private bool toTarget;
    private Vector2 direction;

    void Start()
    {
        patrolStart = transform.position;
        patrolTarget = patrolStart + PatrolDistance;
        toTarget = true;
        direction = NewDirection(patrolTarget);
        IsPatrolling = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsPatrolling)
            return;

        if (toTarget && Vector2.Distance(patrolTarget, transform.position) < 0.2f){
            direction = NewDirection(patrolStart);
            toTarget = false;
        }
        else if (!toTarget && Vector2.Distance(patrolStart, transform.position) < 0.2f)
        {
            direction = NewDirection(patrolTarget);
            toTarget = true;
        }

        MyRigidBody.MovePosition(MyRigidBody.position + direction * WalkSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = (toTarget ? patrolTarget : patrolStart) - (Vector2)MyRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        MyRigidBody.rotation = angle;
    }

    public void FlipDirection()
    {
        direction = NewDirection(toTarget ? patrolStart : patrolTarget);
        toTarget = !toTarget;
    }

    private Vector2 NewDirection(Vector2 target)
    {
        return target - (Vector2)transform.position;
    }
}
