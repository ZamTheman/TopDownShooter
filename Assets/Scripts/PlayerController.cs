using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 mousePos;
    float shootCooldown = 0.25f;
    bool isFiring;
    public Camera cam;
    public Animator animator;
    public Transform gunPosition;
    public Transform toPosition;
    public LineRenderer bulletTraceLineRenderer;
    public GameObject Flare;
    private Weapon weapon;

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
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);

        if (isFiring)
        {
            shootCooldown -= Time.deltaTime;
            if (shootCooldown < 0)
            {
                shootCooldown = 0.25f;
                isFiring = false;
            }
        }

        if (!isFiring && Input.GetMouseButton(0))
        {
            isFiring = true;
            toPosition.position = cam.ScreenToWorldPoint(Input.mousePosition);
            weapon.Shoot();
        }

        mousePos =  cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = angle;
    }
}
