using UnityEngine;

public class PointerController : MonoBehaviour
{
    public Camera cam;

    // Update is called once per frame
    void Update()
    {
        var position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 0);
    }
}
