using UnityEngine;
using UnityEngine.UI;

public class HealtBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(int maxHealth, int currentHealth)
    {
        slider.value = (float)currentHealth / (float)maxHealth;
    }
}
