using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Planet : MonoBehaviour {
    private GameObject keyE;

    private float lastTimeCalledShowAction;

    private float HideActionDelay = 2f;

    // Use this for initialization
    void Start() {
        keyE = transform.Find("key_E").gameObject;
        keyE.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (lastTimeCalledShowAction + HideActionDelay > Time.timeScale) {
            HideActionUi();
        }
    }

    public void ShowActionUi() {
        Debug.Log("AFFICHE " + gameObject);
        keyE.GetComponent<SpriteRenderer>().enabled = true;
        lastTimeCalledShowAction = Time.timeScale;
    }

    public void HideActionUi() {
        //keyE.GetComponent<SpriteRenderer>().enabled = false;
    }

    public virtual void DoAction() {
        Debug.Log("DoAtion");
    }
}