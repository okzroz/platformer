using Cinemachine.Utility;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float life = 3;
    public GameObject explosion;

    private void Awake()
    {
        Destroy(gameObject, life);
        transform.rotation = new Quaternion();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnDestroy()
    {
        var expl = Instantiate(explosion, transform.position + new Vector3(0, 0.1f, 0), transform.rotation.normalized);
    }
}
