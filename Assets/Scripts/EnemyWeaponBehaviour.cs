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
    public float fireRate = 0.5f; // Інтервал між пострілами (0.5 секунди)
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
            // Якщо корутина ще не запущена, запускаємо її
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(ShootWithInterval());
            }
        }
        else
        {
            standartBehaviour.toGo = true;
            // Зупиняємо корутину, якщо гравець виходить за межі діапазону
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
            // Перевірка наявності посилання на гравця
            if (player == null)
            {
                Debug.LogError("Посилання на гравця відсутнє!");
                yield break;
            }

            if (player.transform.position.x < standartBehaviour.transform.position.x) transform.rotation = new Quaternion(0, 180f, 0, 0);
            else transform.rotation = new Quaternion(0, 0, 0, 0);

            // Розрахунок напрямку до гравця
            Vector3 direction = player.position - transform.position;

            // Перевірка, чи напрямок не є нульовим
            if (direction == Vector3.zero)
            {
                Debug.LogError("Гравець знаходиться на одній позиції з кулею!");
                yield break;
            }

            // Нормалізуємо напрямок
            direction.Normalize();

            // Створення кулі
            var bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

            // Отримуємо Rigidbody2D для кулі
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            // Перевірка наявності Rigidbody2D
            if (bulletRb == null)
            {
                Debug.LogError("Prefab кулі не містить Rigidbody2D!");
                yield break;
            }

            // Встановлюємо швидкість кулі
            Vector2 bulletVelocity = new Vector2(direction.x, direction.y) * bulletSpeed;

            // Встановлюємо швидкість кулі
            bulletRb.velocity = bulletVelocity;

            // Виводимо швидкість кулі в лог для перевірки
            Debug.Log("Куля створена зі швидкістю: " + bulletRb.velocity);

            // Очікуємо вказаний час перед наступним пострілом
            yield return new WaitForSeconds(fireRate);
        }
    }
}
