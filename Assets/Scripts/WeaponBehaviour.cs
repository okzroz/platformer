using UnityEngine;
using UnityEngine.UI;

public class WeaponBehavoiur : MonoBehaviour
{
    public Vector3 direction;
    public Rigidbody2D player_rb;
    public Transform spawnPoint;
    public GameObject prefab;
    public int ammo = 0;
    public float bulletSpeed = 20;

    private void Start()
    {
        transform.rotation.Normalize();
        transform.position.Normalize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo > 0)
        {
            var bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation.normalized);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * (bulletSpeed + player_rb.velocity.magnitude);
            ammo--;
        }
    }
}
