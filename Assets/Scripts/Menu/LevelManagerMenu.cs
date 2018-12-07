using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerMenu : MonoBehaviour {

    private const float DISTANCE_BEFORE_RESPAWN = 75f;
    
    public GameObject player;
    public float FieldWidth;
    public float FieldHeight;
    public GameObject[] planetsPrefab;

    private float xOffset;
    private float yOffset;
    private GameObject[] planetsGO;

    // Use this for initialization
    void Start() {
        xOffset = FieldWidth * 0.5f; // Offset the coordinates to distribute the spread
        yOffset = FieldHeight * 0.5f; // around the object's center

        planetsGO = new GameObject[planetsPrefab.Length];
        for (int i = 0; i < planetsPrefab.Length; i++) {
            planetsGO[i] = Instantiate(planetsPrefab[i], GetRandomPlanetPosition(), Quaternion.identity);
        }
    }

    private Vector2 GetRandomPlanetPosition() {
        float x = Random.Range(0, FieldWidth);
        float y = Random.Range(0, FieldWidth);
        return new Vector2(x - xOffset, y - yOffset);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        foreach (var planet in planetsGO) {
            if (Vector3.Distance(planet.transform.position, player.transform.position) > DISTANCE_BEFORE_RESPAWN) {
                Vector3 rng = GetRandomPlanetPosition();
                if (Vector3.Distance(rng + player.transform.position, player.transform.position) < 15f) {  // BIDOUILLE to have a planet a bit far from player
                    rng = GetRandomPlanetPosition();
                }
                planet.transform.position = rng + player.transform.position;
            }
        }
    }
}