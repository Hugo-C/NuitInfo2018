using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public GameObject myText;
    private Text text;

    private double timeEnd;
    private double counter;

    bool effectActive = true;
    public bool counterActive = false;

    // Use this for initialization
    void Start () {
        text = myText.GetComponent<Text>();
        System.Random random = new System.Random();
        timeEnd = random.NextDouble() * (15.0 - 5.0) + 5.0;
        timeEnd = Math.Round(timeEnd, 2);
        text.text = "Pressez le bouton\nà " + timeEnd;
        SystemFlash();
        StartCoroutine(waitBegin());
        counter = 0.0;
    }
	
	// Update is called once per frame
	void Update () {
        if (counterActive)
        {
            text.text = System.Convert.ToString(counter);
            counter += Time.deltaTime;
            counter = Math.Round(counter, 2);
        }
    }

    private void SystemFlash()
    {
        StartCoroutine(blinkText());   
    }

    IEnumerator blinkText()
    {
        while (effectActive)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            yield return new WaitForSeconds(.25f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            yield return new WaitForSeconds(.5f);
        }   
    }

    IEnumerator waitBegin()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(timeEnabled());
        effectActive = false;
        counterActive = true;
        text.fontSize = 45;
        text.fontStyle = FontStyle.Bold;
    }

    IEnumerator timeEnabled()
    {
        yield return new WaitForSeconds((float)(timeEnd / 3.0) * 2f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }

    public void StopCounter()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        counterActive = false;
        text.fontStyle = FontStyle.Normal;
        text.fontSize = 25;
        int percentage = (int)((counter * 100) / timeEnd);
        if (counter > timeEnd) percentage = 100 - (percentage - 100);
        text.text = "<b>" + counter + " - " + timeEnd + "</b>\n" + percentage + "% de précision";
        StartCoroutine(ReturnToMenu());
    }

    private IEnumerator ReturnToMenu() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Menu");
    }
}
