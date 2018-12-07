using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyText : MonoBehaviour {

    public GameObject target;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void LateUpdate () {
        var wantedPos = Camera.main.WorldToScreenPoint(target.transform.position);
        transform.position = wantedPos;

    }
}
