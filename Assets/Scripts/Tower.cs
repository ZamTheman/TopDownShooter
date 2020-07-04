using UnityEngine;

public class Tower : MonoBehaviour
{
    public float FireTimerSeconds;
    private float currentTimer;

    private float rotationSpeed = 100;
    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        currentTimer = FireTimerSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime; 
        if (currentTimer < 0)
        {
            weapon.Shoot();
            currentTimer = FireTimerSeconds;
        }

        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}
