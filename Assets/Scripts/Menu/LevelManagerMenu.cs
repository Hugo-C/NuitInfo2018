using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class LevelManagerMenu : MonoBehaviour {

    private const float PlanetDistanceBeforeRespawn = 75f;
    private const float PlanetDistanceAtSpawn = 50f;

    public GameObject player;
    public float FieldWidth;
    public float FieldHeight;
    public GameObject[] planetsPrefab;

    private Dictionary<float, Vector3> _angleToVectorDp = new Dictionary<float, Vector3>();
    
    private float xOffset;
    private float yOffset;
    private GameObject[] planetsGO;

    // Use this for initialization
    void Start() {
        Assert.IsTrue(PlanetDistanceAtSpawn <= PlanetDistanceBeforeRespawn);
        xOffset = FieldWidth * 0.5f; // Offset the coordinates to distribute the spread
        yOffset = FieldHeight * 0.5f; // around the object's center

        planetsGO = new GameObject[planetsPrefab.Length];
        for (int i = 0; i < planetsPrefab.Length; i++) {
            planetsGO[i] = Instantiate(planetsPrefab[i], GetRandomPlanetPosition(), Quaternion.identity);
        }
    }

    private Vector2 GetRandomPlanetPosition() {
        // first we found where the player is heading
        Vector3 futurePosition = AngleToVector(player.transform.rotation.eulerAngles.z);
        futurePosition *= PlanetDistanceAtSpawn;
        futurePosition += player.transform.position;
        
        // then we add some randomness
        float x = Random.Range(0, FieldWidth) - xOffset;
        float y = Random.Range(0, FieldWidth) - yOffset;
        futurePosition.x += x;
        futurePosition.y += y;
        return futurePosition;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        foreach (var planet in planetsGO) {
            if (Vector3.Distance(planet.transform.position, player.transform.position) > PlanetDistanceBeforeRespawn) {
                planet.transform.position = GetRandomPlanetPosition();
            }
        }
    }
    
    private Vector3 AngleToVector(float zAngle) {
        if (!_angleToVectorDp.ContainsKey(zAngle)) {
            var horizontal = Mathf.Cos((zAngle + 90f) * Mathf.PI / 180f);
            var vertical = Mathf.Sin((zAngle + 90f) * Mathf.PI / 180f);
            _angleToVectorDp.Add(zAngle, new Vector3(horizontal, vertical));
        }

        return _angleToVectorDp[zAngle];
    }
}