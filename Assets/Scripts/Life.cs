using UnityEngine;

public class Life : MonoBehaviour
{
    public int MaxLife;
    public HealtBarScript healthBarScript;

    private int currentLife;

    public void Start()
    {
        currentLife = MaxLife;
        healthBarScript.SetHealth(MaxLife, currentLife);
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;
        healthBarScript.SetHealth(MaxLife, currentLife);
        if (currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}
