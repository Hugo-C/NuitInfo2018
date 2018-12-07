using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    enum GamePhase {afficheEnonce, afficheAlienRandom, afficheValeurs, afficheResultat}

    public GameObject prefabAlien;
    public Text prefabValeur;
    public int NB_COLORS;
    public int NB_ALIENS;
    private GameObject[] aliens;
    private Color[] colors;
    private Color[] alienColors;
    public float timer = 0;
    private bool aliensVisibles = false;
    private bool valeurAffichees = false;
    private GamePhase phase;
    private Text[] textValeurCouleurs;
    public GameObject myText;
    private Text text;
    public Canvas canvas;
    private int result;
    public GameObject inputField;

    int resultTrue;
    bool test;

    // Use this for initialization
    void Start () {
        text = myText.GetComponent<Text>();
        phase = GamePhase.afficheEnonce;
        initialiseCouleurs();
        textValeurCouleurs = new Text[NB_COLORS];
        timer = 5;
        resultTrue = 0;
        test = false;
    }

	// Update is called once per frame
	void Update () {
        if (timer > 0)
            timer -= Time.deltaTime;
       
        switch (phase)
        {
            case GamePhase.afficheEnonce:
                if (timer < 0)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
                    phase = GamePhase.afficheAlienRandom;         
                }
                    break;
            case GamePhase.afficheAlienRandom:
                if (!aliensVisibles)
                {
                    initialiseGame();
                }
                if(timer < 0)
                {
                    desactiveAliens();
                    phase = GamePhase.afficheValeurs;
                }
                break;

            case GamePhase.afficheValeurs:
                if (!valeurAffichees)
                {
                    inputField.SetActive(true);
                    creationAliensParCouleurEtAffichageTextes();
                }
                break;

            case GamePhase.afficheResultat:
                string textR;
                if (result == resultTrue) textR = "Bravo !";
                else textR = "Dommage ! Tu peux faire mieux";
                text.text = result + " / " + resultTrue + "\n" + textR;
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
                break;

            default:
                break;

        }
    }

    public void getEntrer(string entre)
    {
        Debug.Log(inputField.GetComponent<InputField>().text);
        result = int.Parse(inputField.GetComponent<InputField>().text);
        Debug.Log("wow");
        phase = GamePhase.afficheResultat;
        inputField.SetActive(false);
        desactiveAliens();

        var clones = GameObject.FindGameObjectsWithTag("Valeur");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    void initialiseCouleurs()
    {
        colors = new Color[NB_COLORS];
        for (int i = 0; i < NB_COLORS; i++)
        {
            float r = Random.Range((float)0.4, 1);
            float g = Random.Range((float)0.4, 1);
            float b = Random.Range((float)0.4, 1);
            colors[i] = new Color(r, g, b);
        }
    }

    void initialiseAliens()
    {
        aliens = new GameObject[NB_ALIENS];
        for (int i = 0; i < NB_ALIENS; i++)
        {
            Vector2 position;
            if ((i + 1) % 2 == 0)
            {
                position = new Vector2(i + 1, 0);
            }
            else
            {
                position = new Vector2(-i, 0);
            }

            GameObject alien = creerAlien(colors[Random.Range(0, NB_COLORS - 1)], position);
            aliens[i] = alien;
        }
    }

    void initialiseGame()
    {
        initialiseAliens();
        aliensVisibles = true;
        timer = 3;
    }

    void desactiveAliens()
    {
        alienColors = getColorFromAliens(aliens);
        for (int i = 0; i < aliens.Length; i++)
        {
            Destroy(aliens[i]);
        }
        aliensVisibles = false;
    }

    Dictionary<Color, int> donneValeurCouleurs(int min, int max)
    {
        Dictionary<Color, int> valeurs = new Dictionary<Color, int>();
        foreach (Color c in colors)
        {
            valeurs[c] = Random.Range(min, max);

        }
        return valeurs; 
    }

    void creationAliensParCouleurEtAffichageTextes()
    {
        aliens = new GameObject[NB_COLORS];
        Dictionary<Color, int> valeurCouleurs = donneValeurCouleurs(0, 10); //valeurs random entre 0 et 10 ici
        //view infos : 
        Camera camera = Camera.main;
        float halfHeight = camera.orthographicSize;
        float halfWidth = camera.aspect * halfHeight - 6;

        float x = -halfWidth + 1;
        float y = halfHeight - 3;
        int i = 0;
    
        float xv = -9;
        float yv = -3;

        while (i < NB_COLORS)
        {
            Vector2 position = new Vector2(x,y);
            aliens[i] = creerAlien(colors[i], position);
            i++;
            if (i == 6)
            {
                xv = 0;
                yv -= 9;
            }
            else
            {
                xv +=9;
            }
            if (i == 5)
            {
                x = -halfWidth + 1;
                y -= 3;
            }
            else
            { 
                x += 3;
            }

            Color colorAlien = aliens[i-1].GetComponent<Alien>().GetColor();
            Text valeur = Instantiate(prefabValeur, new Vector2(0,0), Quaternion.identity);
            valeur.transform.SetParent(canvas.transform, false);
            resultTrue += valeurCouleurs[colorAlien];
            valeur.text = valeurCouleurs[colorAlien].ToString();
            RectTransform assignText = valeur.GetComponent<RectTransform>();
            assignText.transform.position = new Vector2(assignText.transform.position.x + xv*10, assignText.transform.position.y + yv*10);
            
        }

        valeurAffichees = true;
    }

    GameObject creerAlien(Color couleur, Vector2 position)
    {
        GameObject alien = Instantiate(prefabAlien, position, Quaternion.identity);
        alien.GetComponent<Alien>().SetColor(couleur);
        return alien;

    }

    Color[] getColorFromAliens(GameObject[] tabAliens)
    {
        Color[] colors = new Color[tabAliens.Length];

        int i = 0;
        foreach(GameObject alien in tabAliens)
        {
            colors[i] = alien.GetComponent<Alien>().GetColor();
            i++;
        }
        return colors;
    }

}
