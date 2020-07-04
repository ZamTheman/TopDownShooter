using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int WeaponDamage;
    public Transform GunPoint;
    public GameObject BulletPrefab;
    public GameObject ImpactEffectPrefab;
    public GameObject Flare;
    public GameObject Bullet;

    public void Shoot()
    {
        var flareScaleX = Random.Range(0.5f, 2f);
        var flareScaleY = Random.Range(0.5f, 2f);
        var flare = Instantiate(Flare, GunPoint.position, GunPoint.rotation);
        flare.transform.localScale = new Vector3(flareScaleX, flareScaleY, 1);
        flare.transform.Rotate(0, 0, 90);
        StartCoroutine(FadeRoutine(flare.GetComponent<Renderer>()));

        RaycastHit2D hitInfo = Physics2D.Raycast(GunPoint.position, -GunPoint.right);

        if (hitInfo && hitInfo.transform.tag != "Bullet")
        {
            Instantiate(ImpactEffectPrefab, hitInfo.point, Quaternion.identity);

            var bullet = Instantiate(BulletPrefab, GunPoint.position, GunPoint.rotation);
            StartCoroutine(ShootBulletRoutine(bullet, GunPoint.position, hitInfo.point));

            var damageScript = hitInfo.transform.GetComponent<Life>();
            if (damageScript != null)
            {
                damageScript.TakeDamage(WeaponDamage);
            }
        }
    }

    IEnumerator FadeRoutine(Renderer renderer)
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            Color c = renderer.material.color;
            c.a = ft;
            renderer.material.color = c;
            yield return null;
        }

        Destroy(renderer.transform.gameObject);
    }

    IEnumerator ShootBulletRoutine(GameObject bullet, Vector2 startPos, Vector2 endPos)
    {
        var distance = Vector2.Distance(startPos, endPos);
        var direction = endPos - startPos;
        direction.Normalize();

        var bulletRb = bullet.GetComponent<Rigidbody2D>();

        for (float i = 0; i < distance; i += distance / 4f)
        {
            bulletRb.position = startPos + direction * i;
        }

        yield return null;
    }
}
