using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Target;
    public Camera Camera;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y, -10);
    }
}
