using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ScrollManager : MonoBehaviour {
    public float TheScrollSpeed = 0.025f;

    Transform theCamera;

    void Start() {
        theCamera = Camera.main.transform;
    }

    void Update() {
        theCamera.position = new Vector3(theCamera.position.x, theCamera.position.y + TheScrollSpeed, theCamera.position.z);
    }
}