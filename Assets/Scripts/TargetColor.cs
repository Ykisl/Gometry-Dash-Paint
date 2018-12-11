using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetColor : MonoBehaviour {

    SpriteRenderer myspriteRenderer;
    public SpriteRenderer colorTDLB;
	void Start () {
        myspriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	
	void Update () {
        myspriteRenderer.color = colorTDLB.color;
	}
}
