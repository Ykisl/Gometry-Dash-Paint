using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUse : MonoBehaviour {

    public int LevelArea = 100;


    public int DragSpeed = 100;

    void Start () {
		
	}


	
	void Update () {

        var translation = Vector3.zero;


        if (Input.GetMouseButton(2))
        {

            translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime, 0);
        }


        Camera.main.transform.position += translation;
    }

    

    private void LateUpdate()
    {
      if(transform.position.x < 0f)
        {
            transform.position = new Vector3(0f, transform.position.y, -499);
        }
        if (transform.position.y < 0f)
        {
            transform.position = new Vector3(transform.position.x, 0f, -499);
        }
    }
}
