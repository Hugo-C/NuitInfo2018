using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlarm : MonoBehaviour {

    private SpriteRenderer lightSprite;
    bool fade = true;

    // Use this for initialization
    void Start () {
        lightSprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (fade)
        {
            lightSprite.color += new Color(1f, 1f, 1f, Time.deltaTime*.5f);
            if (lightSprite.color.a >= .4f)
                fade = false;
        }
        else
        {
            lightSprite.color -= new Color(1f, 1f, 1f, Time.deltaTime*.5f);
            if (lightSprite.color.a <= 0f)
                fade = true;
        }
    }
}
