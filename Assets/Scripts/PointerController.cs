using UnityEngine;

public class PointerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        rb.position = cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
