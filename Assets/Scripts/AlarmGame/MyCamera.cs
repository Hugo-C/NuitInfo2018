using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {

    Vector2 startingPos;

    float speed = 7.0f; //how fast it shakes
    float amount = .1f; //how much it shakes

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3(startingPos.x + Mathf.Sin(Time.time * speed) * amount, startingPos.y + (Mathf.Sin(Time.time * speed) * amount), transform.position.z);
    }
}
