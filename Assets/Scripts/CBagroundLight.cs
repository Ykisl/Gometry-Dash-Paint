using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBagroundLight : MonoBehaviour
{
    SpriteRenderer MyRenderer;
    public SpriteRenderer GLRender;
    void Start()
    {
        MyRenderer = GetComponent<SpriteRenderer>();
    }

    

    private void LateUpdate()
    {
        MyRenderer.color = GLRender.color;
    }
}
