using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraUse : MonoBehaviour {

    public int LevelArea = 100;

    bool mbt;
    public int DragSpeed = 500;

    void Start () {
		
	}


	
	void Update () {

        var translation = Vector3.zero;
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                mbt = true;
                
            }
        }
        if (!Input.GetMouseButton(0))
        {
            mbt = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DragSpeed = 300;
        }
        else DragSpeed = 500;

        if (LevelEditor.ModeSelectID == 0)
        {
            if (mbt == true)
            {
                translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime, 0);
            }
            if (Input.GetMouseButton(2))
            {

                translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime, 0);
            }

        }
        else
        {
            if (Input.GetMouseButton(2))
            {

                translation -= new Vector3(Input.GetAxis("Mouse X") * DragSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * DragSpeed * Time.deltaTime, 0);
            }
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
        if(transform.position.y > 80f)
        {
            transform.position = new Vector3(transform.position.x, 80f, -499);
        }
    }
}
