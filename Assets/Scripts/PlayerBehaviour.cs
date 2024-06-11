using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] int hp = 100;
    public Transform holderTransform;
    public WeaponHolder holder;
    public WeaponBehavoiur rifle;
    public WeaponBehavoiur granade;

    public Text hpCount;
    public Text riffleAmmo;
    public Text granadeAmmo;

    private Rigidbody2D body;
    private Transform body_trasform;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body_trasform = GetComponent<Transform>();
    }

    void Update()
    {
        if (hp <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        hpCount.text = hp.ToString();
        riffleAmmo.text = rifle.ammo.ToString();
        granadeAmmo.text = granade.ammo.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Vector3 direction = body_trasform.position - collision.gameObject.transform.position;
                body.AddForceAtPosition(new Vector2(direction.x, direction.y), direction);
                hp -= 50;
                break;
            case "Bullet":
                hp -= 20;
                break;
            case "RifleActivator":
                holder.isAviable[1] = true;
                rifle.ammo += 30;
                Destroy(collision.gameObject);
                break;
            case "GranadeActivator":
                holder.isAviable[2] = true;
                granade.ammo += 5;
                Destroy(collision.gameObject);
                break;
            default:
                Debug.Log("Гравець зіткнувся з " + collision.gameObject.tag);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Riffle":
                holder.isAviable[1] = true;
                rifle.ammo += 30;
                Destroy(collision.gameObject);
                break;
            case "Granade":
                holder.isAviable[2] = true;
                granade.ammo += 5;
                Destroy(collision.gameObject);
                break;
            case "HP":
                if (hp + 20 <= 100) hp += 20;
                else hp = 100;
                Destroy(collision.gameObject);
                break;
            case "Explosion":
                hp -= 30;
                break;
            default:
                Debug.Log("Гравець взаємодіє з " + collision.gameObject.tag);
                break;
        }
    }
}
