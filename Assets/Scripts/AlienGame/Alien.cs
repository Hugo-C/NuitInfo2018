using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {
    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void SetColor(Color c) {
        spriteRenderer.color = c;
    }

    public Color GetColor()
    {
        return spriteRenderer.color;
    }

}
