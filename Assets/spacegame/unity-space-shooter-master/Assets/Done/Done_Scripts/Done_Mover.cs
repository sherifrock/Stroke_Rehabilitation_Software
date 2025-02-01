using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}
    void Update()
    {
        

        SavePosition();
    }
    private void SavePosition()
    {
        // Save hazard position in PlayerPrefs
        PlayerPrefs.SetFloat("HazardX", transform.position.x);
        PlayerPrefs.SetFloat("HazardY", transform.position.z);
    }
}
