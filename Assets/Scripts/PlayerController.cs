using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public Rigidbody2D Rb;
    public Animator Animator;
    public Camera cam;

    private Vector2 mousePos;
    private Vector2 movement;
    private float shootCooldown = 0.25f;
    private bool isFiring;
    private Weapon weapon;
    private float almostZero = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        weapon = GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
            Animator.SetBool("IsWalking", true);
        else
            Animator.SetBool("IsWalking", false);

        if (isFiring)
        {
            shootCooldown -= Time.deltaTime;
            if (shootCooldown < 0)
            {
                shootCooldown = 0.25f;
                isFiring = false;
            }
        }

        if (!isFiring && Input.GetMouseButton(0) && !Animator.GetBool("CantShoot"))
        {
            isFiring = true;
            weapon.Shoot();
        }

        mousePos =  cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {

        Vector2 lookDir = mousePos - Rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Rb.rotation = angle;

        if (movement.magnitude < almostZero)
        {
            movement = Vector2.zero;
            Rb.velocity = Vector2.zero;
        }

        Rb.MovePosition(Rb.position + movement.normalized * MoveSpeed * Time.fixedDeltaTime);
    }

    void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
