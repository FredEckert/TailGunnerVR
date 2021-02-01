using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class _UnityEvent : UnityEvent { }
[System.Serializable] public class _UnityEventInt : UnityEvent<int> { }
[System.Serializable] public class _UnityEventFloat : UnityEvent<float> { }

public class Manager : MonoBehaviour
{
    public Color32 colorNormal = new Color(0, 0.5019608f, 0.7254902f, 1);
    public Color32 colorIntense = Color.cyan;
    public float lineWidth = 2;
    public Material lineMaterial;
    public Texture lineTexture;
    [UnityEngine.HideInInspector]
    public float capLength;

    public Transform shipPart;
    public float explodeForce;

    public ParticleSystem Starfield;

    public GameObject EnemyShipPrefab;
    [UnityEngine.HideInInspector]
    public GameObject eShip1;
    [UnityEngine.HideInInspector]
    public int attackWave = 0;

    [UnityEngine.HideInInspector]
    public System.Collections.Generic.List<Transform> objects;

    public _UnityEvent Bump;
    public _UnityEventInt Incr;
    public _UnityEventFloat Log;

    public static Manager use;

    private int editSphereIndex = 0;
    private GameObject editSphere;

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
        //delay for oculus fade in complete
        float startUpDelay = 2.5f;

        //Display UI
        TailgUI.use.StartUp();
        TailgUI.use.score = -1;
        TailgUI.use.highscore = -1;
        TailgUI.use.AddToScore(1);

        //Display Reticle
        TailgReticle.use.StartUp();

        //Display Starfield
        if (Starfield != null)
        {
            var main = Starfield.main;
            main.startDelay = startUpDelay + 0.9f;
            main.startLifetime = 4.0f;
            main.startColor = new ParticleSystem.MinMaxGradient(this.colorNormal);
            Instantiate(Starfield);
        }

        //Display Lead in particle
        Invoke("DisplayLeadIn", startUpDelay);

        //Display Title
        //InvokeRepeating("DisplayTitle", startUpDelay, 10);

        //Display Enemy
        Invoke("DisplayEnemy", startUpDelay); // + 10.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(eShip1);
            attackWave = 0;
            eShip1 = Instantiate(EnemyShipPrefab);
            eShip1.GetComponent<EnemyShipControl>().showPoints = true;
            eShip1.GetComponent<EnemyShipControl>().showSpline = true;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(eShip1);
            attackWave = 1;
            eShip1 = Instantiate(EnemyShipPrefab);
            eShip1.GetComponent<EnemyShipControl>().showPoints = true;
            eShip1.GetComponent<EnemyShipControl>().showSpline = true;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //if (Bump != null) Bump.Invoke();
            //if (Incr != null) Incr.Invoke(attackWave);
            //if (Log != null) Log.Invoke(1.4455f);

            if (editSphere != null)
                editSphere.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            string sphereName = "Sphere" + editSphereIndex.ToString();
            editSphere = GameObject.Find(sphereName);
            if (editSphere != null)
            {
                editSphere.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                editSphereIndex++; //Int32.Parse(editSphere.name.Substring(6, 1));

            }
            else
            {
                editSphereIndex = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Incr != null) Incr.Invoke(attackWave);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (editSphere != null)
            {
                Vector3 pos = editSphere.transform.position;
                pos.y += 1;
                editSphere.transform.position = pos;
                editSphereIndex = Int32.Parse(editSphere.name.Substring(6, 1));
                Debug.Log(editSphere.name + ": " + editSphereIndex);
                LineData.use.w1t1sPoints[editSphereIndex] = pos;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (editSphere != null)
            {
                Vector3 pos = editSphere.transform.position;
                pos.y -= 1;
                editSphere.transform.position = pos;
                editSphereIndex = Int32.Parse(editSphere.name.Substring(6, 1));
                Debug.Log(editSphere.name + ": " + editSphereIndex);
                LineData.use.w1t1sPoints[editSphereIndex] = pos;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (editSphere != null)
            {
                Vector3 pos = editSphere.transform.position;
                pos.x += 1;
                editSphere.transform.position = pos;
                editSphereIndex = Int32.Parse(editSphere.name.Substring(6, 1));
                Debug.Log(editSphere.name + ": " + editSphereIndex);
                LineData.use.w1t1sPoints[editSphereIndex] = pos;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (editSphere != null)
            {
                Vector3 pos = editSphere.transform.position;
                pos.x -= 1;
                editSphere.transform.position = pos;
                editSphereIndex = Int32.Parse(editSphere.name.Substring(6, 1));
                Debug.Log(editSphere.name + ": " + editSphereIndex);
                LineData.use.w1t1sPoints[editSphereIndex] = pos;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (editSphere != null)
            {
                Vector3 pos = editSphere.transform.position;
                pos.z += 1;
                editSphere.transform.position = pos;
                editSphereIndex = Int32.Parse(editSphere.name.Substring(6, 1));
                Debug.Log(editSphere.name + ": " + editSphereIndex);
                LineData.use.w1t1sPoints[editSphereIndex] = pos;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (editSphere != null)
            {
                Vector3 pos = editSphere.transform.position;
                pos.z -= 1;
                editSphere.transform.position = pos;
                editSphereIndex = Int32.Parse(editSphere.name.Substring(6, 1));
                Debug.Log(editSphere.name + ": " + editSphereIndex);
                LineData.use.w1t1sPoints[editSphereIndex] = pos;
            }
        }

    }

    void DisplayEnemy()
    {
        if (EnemyShipPrefab != null)
        {
            //EnemyShipTransform.position = new Vector3(-10, 5, 25);
            eShip1 = Instantiate(EnemyShipPrefab);
            //EnemyShipTransform.position = new Vector3(10, 5, 25);
            //Instantiate(EnemyShip);
            //EnemyShipTransform.position = new Vector3(-10, -5, 25);
            //Instantiate(EnemyShip);
            //EnemyShipTransform.position = new Vector3(10, -5, 25);
            //Instantiate(EnemyShip);
        }
    }

    void DisplayLeadIn()
    {
        new GameObject("LeadInParticle").AddComponent<LeadInParticle>();
    }

    void DisplayTitle()
    {
        new GameObject("TitleBanner").AddComponent<TitleBanner>();
    }

    //Since Unity can't directly multiply Color32 by a float, make a function to do that
    public virtual Color32 MultiplyColor(Color32 color, float value)
    {
        byte cRed = color.r;
        byte cGreen = color.g;
        byte cBlue = color.b;
        byte cAlpha = color.a;

        byte valuePercent = (byte)(value * 255);

        byte tRed = (byte)((cRed * valuePercent + 0xFF) >> 8);
        byte tGreen = (byte)((cGreen * valuePercent + 0xFF) >> 8);
        byte tBlue = (byte)((cBlue * valuePercent + 0xFF) >> 8);
        byte tAlpha = (byte)((cAlpha * valuePercent + 0xFF) >> 8);

        return new Color32(tRed, tGreen, tBlue, tAlpha);
    }

    public virtual int ArrayAdd(Transform thisTransform, System.Collections.Generic.List<Transform> list)
    {
        int i = 0;
        // If there are any unused slots in the array, use the first one found for this transform
        i = 0;
        while (i < list.Count)
        {
            if (list[i] == null)
            {
                list[i] = thisTransform;
                return i;
            }
            i++;
        }
        // Otherwise, if the array is full, make it bigger
        list.Add(thisTransform);
        return i;
    }

    public virtual void ArrayRemove(int objectNumber, System.Collections.Generic.List<Transform> list)
    {
        list[objectNumber] = null;
    }

}
