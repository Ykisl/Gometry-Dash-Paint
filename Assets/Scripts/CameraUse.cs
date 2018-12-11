using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUse : MonoBehaviour {

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    void Start () {
		
	}
	
	
	void Update () {
		
	}

    public void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

            if (currentSwipe.y > 0 & currentSwipe.x > -0.5f & currentSwipe.x < 0.5f)
        {
                Debug.Log("up swipe");
            }
            if (currentSwipe.y < 0 & currentSwipe.x > -0.5f & currentSwipe.x < 0.5f)
        {
                Debug.Log("down swipe");
            }
            if (currentSwipe.x < 0 & currentSwipe.y > -0.5f & currentSwipe.y < 0.5f)
        {
                Debug.Log("left swipe");
            }
            if (currentSwipe.x > 0 & currentSwipe.y > -0.5f & currentSwipe.y < 0.5f)
        {
                Debug.Log("right swipe");
            }
        }
    }

    private void LateUpdate()
    {
      if(transform.position.x < 0f)
        {
            transform.position = new Vector2(0f, transform.position.y);
        }
        if (transform.position.y < 0f)
        {
            transform.position = new Vector2(transform.position.x, 0f);
        }
    }
}
