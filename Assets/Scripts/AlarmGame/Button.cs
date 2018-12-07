using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite;

    private SpriteRenderer spriteRenderer;

    public GameObject levelManager;

    // Use this for initialization
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseDown()
    {
        if (levelManager.GetComponent<LevelManager>().counterActive)
        {
            spriteRenderer.sprite = sprite;
            levelManager.GetComponent<LevelManager>().StopCounter();
        }
    }
}
