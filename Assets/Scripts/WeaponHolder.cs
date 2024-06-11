using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public GameObject rifle;
    public GameObject granades;
    public GameObject knife;

    public WeaponBehavoiur cRifle;
    public WeaponBehavoiur cGranade;
    public WeaponBehavoiur cKnife;

    public SpriteRenderer rifleSprite;
    public SpriteRenderer knifeSprite;
    public SpriteRenderer granadeSprite;
    public SpriteRenderer playerSprite;
    public float orbitDistance = 1.0f; // ³������ �� ������
    public Transform player;

    public bool []isAviable = { true, false, false };
    private GameObject[] weapons = new GameObject[3];
    private SpriteRenderer currentSprite;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        weapons[0] = knife;
        weapons[1] = rifle;
        weapons[2] = granades;
        currentSprite = knifeSprite;
        foreach (var weapon in weapons) { weapon.SetActive(false); }
        weapons[0].SetActive(true);
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isAviable[0])
        {
            weapons[0].SetActive(true);
            weapons[1].SetActive(false);
            weapons[2].SetActive(false);
            currentSprite = knifeSprite;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && isAviable[1])
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(true);
            weapons[2].SetActive(false);
            currentSprite = rifleSprite;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && isAviable[2])
        {
            weapons[0].SetActive(false);
            weapons[1].SetActive(false);
            weapons[2].SetActive(true);
            currentSprite = granadeSprite;
        }

        // �������� ������� ����� � ���
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // ��������� Z ����������, ��� ��������� � 2D �������

        // ��������� �������� �� ������ �� �����
        Vector3 direction = mousePosition - player.position;
        direction.Normalize(); // ���������� �������� ��� ��������� ���������� �������

        // ���������� ���� ������� ���� �� ����� ������ �� ������
        Vector3 newWeaponPosition = player.position + direction * orbitDistance + new Vector3(0, 0.7f, 0);
        transform.position = newWeaponPosition;

        // �������� ����� � �������� �����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        cRifle.direction = direction;
        cGranade.direction = direction;
        cKnife.direction = direction;   
        

        if (angle > 90 && angle <= 180 || angle >= -180 && angle <= -90)
        {
            currentSprite.flipY = true;
            playerSprite.flipX = true;
        }
        else
        {
            currentSprite.flipY = false;
            playerSprite.flipX = false;
        }
    }
}
