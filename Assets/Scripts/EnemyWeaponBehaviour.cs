using System.Collections;
using UnityEngine;

public class EnemyWeaponBehaviour : MonoBehaviour
{
    [SerializeField] float detectionRange;
    public Vector3 direction;
    public Rigidbody2D player_rb;
    public Transform spawnPoint;
    public Transform player;
    public GameObject prefab;
    public int ammo = 0;
    public float bulletSpeed = 20;
    public float fireRate = 0.5f; // �������� �� ��������� (0.5 �������)
    public EnemyStandartBehaviour standartBehaviour;

    private Coroutine shootingCoroutine;

    private void Start()
    {

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            standartBehaviour.toGo = false;
            // ���� �������� �� �� ��������, ��������� ��
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootWithInterval());
            }
        }
        else
        {
            standartBehaviour.toGo = true;
            // ��������� ��������, ���� ������� �������� �� ��� ��������
            if (shootingCoroutine != null)
            {
                StopCoroutine(shootingCoroutine);
                shootingCoroutine = null;
            }
        }
    }

    private IEnumerator ShootWithInterval()
    {
        while (true)
        {
            // �������� �������� ��������� �� ������
            if (player == null)
            {
                Debug.LogError("��������� �� ������ ������!");
                yield break;
            }

            if (player.transform.position.x < standartBehaviour.transform.position.x) transform.rotation = new Quaternion(0, 180f, 0, 0);
            else transform.rotation = new Quaternion(0, 0, 0, 0);

            // ���������� �������� �� ������
            Vector3 direction = player.position - transform.position;

            // ��������, �� �������� �� � ��������
            if (direction == Vector3.zero)
            {
                Debug.LogError("������� ����������� �� ���� ������� � �����!");
                yield break;
            }

            // ���������� ��������
            direction.Normalize();

            // ��������� ���
            var bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

            // �������� Rigidbody2D ��� ���
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // �������� �������� Rigidbody2D
            if (bulletRb == null)
            {
                Debug.LogError("Prefab ��� �� ������ Rigidbody2D!");
                yield break;
            }

            // ������������ �������� ���
            Vector2 bulletVelocity = new Vector2(direction.x, direction.y) * bulletSpeed;

            // ������������ �������� ���
            bulletRb.velocity = bulletVelocity;

            // �������� �������� ��� � ��� ��� ��������
            Debug.Log("���� �������� � ��������: " + bulletRb.velocity);

            // ������� �������� ��� ����� ��������� ��������
            yield return new WaitForSeconds(fireRate);
        }
    }
}
