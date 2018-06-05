using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Color32 colorNormal = new Color(0, 0.5019608f, 0.7254902f, 1);
    public Color32 colorIntense = Color.cyan;
    public float lineWidth = 2;
    public Material lineMaterial;
    public Texture lineTexture;
    [UnityEngine.HideInInspector]
    public float capLength;

    public ParticleSystem Starfield;
    public GameObject EnemyShip1;
    public GameObject EnemyShip2;
    public GameObject EnemyShip3;
    public GameObject EnemyShip4;

    public static Manager use;

    public virtual void Awake()
    {
        //if (Application.isEditor)
        //{
        //    Debug.Log("For best results, make sure the scene view isn't active in play mode, since the scene view camera interferes with visibility calculations");
        //}
        Manager.use = this;

        this.capLength = this.lineWidth * 0.05f;
    }

    // Use this for initialization
    void Start()
    {
        //Display UI
        TailgUI.use.StartUp();
        TailgUI.use.score = -1;
        TailgUI.use.highscore = -1;
        TailgUI.use.AddToScore(1);

        //Display Starfield
        if (Starfield != null)
        {
            var main = Starfield.main;
            main.startDelay = 2.0f;
            main.startLifetime = 2.0f;
            main.startColor = new ParticleSystem.MinMaxGradient(this.colorNormal);
            Instantiate(Starfield);
        }

        //Display Title
        InvokeRepeating("TitleDisplay", 0, 10);

        //Display EnemyShip1 after 10 seconds
        Invoke("EnemyShip1Display", 10.0f);

        //Display EnemyShip2 after 20 seconds
        Invoke("EnemyShip2Display", 20.0f);

        //Display EnemyShip3 after 30 seconds
        Invoke("EnemyShip3Display", 30.0f);

        //Display EnemyShip4 after 30 seconds
        Invoke("EnemyShip4Display", 40.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void EnemyShip1Display()
    {
        if (EnemyShip1 != null)
        {
            Instantiate(EnemyShip1);
        }
    }

    void EnemyShip2Display()
    {
        if (EnemyShip2 != null)
        {
            Instantiate(EnemyShip2);
        }
    }

    void EnemyShip3Display()
    {
        if (EnemyShip3 != null)
        {
            Instantiate(EnemyShip3);
        }
    }

    void EnemyShip4Display()
    {
        if (EnemyShip4 != null)
        {
            Instantiate(EnemyShip4);
        }
    }

    void TitleDisplay()
    {
        new GameObject("TitleBanner").AddComponent<TitleBanner>();
    }

}
