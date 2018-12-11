using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFreeze : MonoBehaviour {

    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, 0f);
    public bool On_X;
    public bool On_Z;
    public bool On_Y;


    void Start () {
		
	}
	
	
	void Update () {
		
	}

    private void LateUpdate()
    {
        if (On_X)
        {
            transform.position = new Vector3(target.position.x + offset.x, transform.position.y, transform.position.z);
        }
        if (On_Y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y + offset.y, transform.position.z);
        }
        if (On_Z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
        }
    }
}
