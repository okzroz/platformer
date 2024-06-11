using UnityEngine;
public class CameraBehaviour : MonoBehaviour
{
    private Transform _transform;
    public GameObject _object;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.position = _object.transform.position;
        _transform.position += new Vector3(0,0,-10);
    }
}
