using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBlockInfo : MonoBehaviour {
    public int ColorID;
    SpriteRenderer sr;
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        
	}
	
	
	void Update () {
        sr.color = LevelEditor.GdColors[ColorID];
	}
}
