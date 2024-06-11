using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

	[SerializeField] float life;
	void Update ()
	{

		Destroy(transform.gameObject, life);
	
	}
}
