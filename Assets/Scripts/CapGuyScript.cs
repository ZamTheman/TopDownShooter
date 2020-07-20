using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapGuyScript : MonoBehaviour
{
    public Patrol Patrol;
    public LineOfSight LineOfSight;
    private enum CharacterState
    {
        Patrolling,
        Attacking
    }
    private CharacterState characterState;
    void Start()
    {
        characterState = CharacterState.Patrolling;
    }

    void FixedUpdate()
    {
        if (LineOfSight.ISeeThePlayer && characterState != CharacterState.Attacking)
            characterState = CharacterState.Attacking;

        if (!LineOfSight.ISeeThePlayer && characterState != CharacterState.Patrolling)
            characterState = CharacterState.Patrolling;

        switch(characterState)
        {
            case CharacterState.Patrolling:
                break;
            case CharacterState.Attacking:
                Patrol.IsPatrolling = false;
                break;
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        Patrol.FlipDirection();
    }
}
