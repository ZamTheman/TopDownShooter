using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantShootBox : MonoBehaviour
{
    public GameObject character;

    private Animator animator;
    private Dictionary<Collider2D, bool> colliders;

    void Start()
    {
        animator = character.GetComponent<Animator>();
        colliders = new Dictionary<Collider2D, bool>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "CameraConfiler"
            || other.transform.tag == "Player")
            return;

        colliders.Add(other, true);

        animator.SetBool("CantShoot", true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "CameraConfiler"
            || other.transform.tag == "Player")
            return;

        if (colliders.ContainsKey(other))
            colliders.Remove(other);

        if (colliders.Count == 0)
            animator.SetBool("CantShoot", false);
    }
}
