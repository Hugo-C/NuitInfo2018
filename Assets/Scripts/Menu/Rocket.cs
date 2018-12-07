using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour { // TODO set particule speed faster when (accelerating / velocity > 0)
    private const float MOVE_COEF = 4f;
    private const float PLANET_ACTIVATION_RANGE = 7f;
    private Rigidbody2D rb2D;
    private MyCameraMenu myCamera;

    private bool isNearPlanet;

    // Use this for initialization
    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        myCamera = GameObject.Find("Main Camera").GetComponent<MyCameraMenu>();
    }

    // Update is called once per frame
    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (vertical > 0f) {
            rb2D.AddRelativeForce(new Vector2(0f, vertical * MOVE_COEF));
        } else if (vertical < 0f) {
            rb2D.velocity *= 0.95f;
        }

        rb2D.velocity *= 0.99f;

        if (horizontal != 0f)
            rb2D.AddTorque(-horizontal * MOVE_COEF);

        rb2D.angularVelocity *= 0.98f;

        // handle being near a planet
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, PLANET_ACTIVATION_RANGE);

        isNearPlanet = false;
        foreach (var hitCollider in hitColliders) {
            if (hitCollider.CompareTag("Planet")) {
                isNearPlanet = true;
                Planet planet = hitCollider.gameObject.GetComponent<Planet>();
                planet.ShowActionUi();
                if (Input.GetKey(KeyCode.E)) {
                    planet.DoAction();
                    break;
                }
            }
        }

        if (isNearPlanet) {
            myCamera.ZoomIn();
        } else {
            myCamera.ZoomOut(); 
        }
    }
}