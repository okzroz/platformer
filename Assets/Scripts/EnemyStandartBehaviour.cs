using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;
public class EnemyStandartBehaviour : MonoBehaviour
{
    [SerializeField] int hp;
    [SerializeField] float speed;

    public bool toGo = true;
    public int direction;
    public Transform weaponSprite;
    public Text hpCount;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private RaycastHit2D hit;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        hp = 100;
        direction = 1;
    }

    private void Update()
    {
        hpCount.text = hp.ToString();
        hpCount.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        if (hp <= 0)
        {
            Destroy(hpCount);
            Destroy(gameObject);
        }
        if(toGo) body.velocity = new Vector2(direction * speed, body.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                hp -= 20;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Explosion":
                hp -= 80;
                break;
            case "Knife":
                hp -= 70;
                break;
            case "ReversePoint":
                direction *= -1;
                if (transform.rotation.y == 0)
                {
                    transform.rotation = new Quaternion(0, 180f, 0, 0);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                break;
            default:
                break;
        }
    }
}
