using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    enum GamePhase {afficheAlienRandom, afficheValeurs, afficheResultat, afficheScore}

    public GameObject prefabAlien;
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

    // Use this for initialization
    void Start () {
        phase = GamePhase.afficheAlienRandom;
        initialiseCouleurs();
        textValeurCouleurs = new Text[NB_COLORS];
    }

	// Update is called once per frame
	void Update () {
        if (timer > 0)
            timer -= Time.deltaTime;
       
        switch (phase)
        {
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
                    creationAliensParCouleurEtAffichageTextes();
                    
                }
                break;

            case GamePhase.afficheResultat:
              
                break;

            case GamePhase.afficheScore:
               
                break;

            default:
                break;

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
        timer = 2;
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
        float halfWidth = camera.aspect * halfHeight;

        float x = -halfWidth + 1;
        float y = halfHeight - 3;
        int i = 0;
        while(i < NB_COLORS)
        {
            Vector2 position = new Vector2(x,y);
            aliens[i] = creerAlien(colors[i], position);
            i++;
            if ((x+3) < halfWidth)
            {
                x+=3;
            }
            else
            {
                x = -halfWidth + 1;
                y -= 3;
            }
            
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
