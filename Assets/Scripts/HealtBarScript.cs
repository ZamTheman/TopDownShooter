using UnityEngine;
using UnityEngine.UI;

public class HealtBarScript : MonoBehaviour
{
    public Slider Slider;
    public Transform FollowTarget;
    public float YOffset = 0.9f;

    public void Update()
    {
        if (FollowTarget == null)
            Destroy(gameObject);

        transform.position = new Vector3(
            FollowTarget.position.x,
            FollowTarget.position.y + YOffset,
            FollowTarget.position.z);
    }

    public void SetHealth(int maxHealth, int currentHealth)
    {
        Slider.value = (float)currentHealth / (float)maxHealth;
    }
}
