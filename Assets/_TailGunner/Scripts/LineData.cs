using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class LineData : MonoBehaviour
{
    public static LineData use;

    public TextAsset ship1;
    public TextAsset ship2;
    public TextAsset ship3;
    public TextAsset ship4;
    public TextAsset title;

    public TextAsset[] shipParts;


    public System.Collections.Generic.List<Vector3> ship1Points;
    public System.Collections.Generic.List<Vector3> ship2Points;
    public System.Collections.Generic.List<Vector3> ship3Points;
    public System.Collections.Generic.List<Vector3> ship4Points;
    public System.Collections.Generic.List<Vector3> titlePoints;

    public System.Collections.Generic.List<System.Collections.Generic.List<Vector3>> partPoints;

    public System.Collections.Generic.List<Vector3> reticlePoints;


    public Vector3[] partLocations;

    public virtual void Awake()
    {
        LineData.use = this;
        this.ship1Points = VectorLine.BytesToVector3List(this.ship1.bytes);
        this.ship2Points = VectorLine.BytesToVector3List(this.ship2.bytes);
        this.ship3Points = VectorLine.BytesToVector3List(this.ship3.bytes);
        this.ship4Points = VectorLine.BytesToVector3List(this.ship4.bytes);
        this.titlePoints = VectorLine.BytesToVector3List(this.title.bytes);

        // Exploded ship parts
        this.partLocations = new Vector3[] { new Vector3(-0.02f, 3.17f, -1.52f), new Vector3(0f, 1.91f, -0.12f), new Vector3(0f, 1.8f, -2.29f), new Vector3(-0.6f, -0.36f, 0.13f), new Vector3(0.84f, -0.75f, 3f), new Vector3(-1.3f, -0.79f, -2.35f), new Vector3(1.12f, 0f, -1.89f) };
        this.partPoints = new List<System.Collections.Generic.List<Vector3>>(7);
        int i = 0;
        while (i < 7)
        {
            this.partPoints.Add(VectorLine.BytesToVector3List(this.shipParts[i].bytes));
            i++;
        }

        // Reticle
        this.reticlePoints = new List<Vector3>(new Vector3[] { new Vector3(-30f, 0f), new Vector3(30f, 0f), new Vector3(0f, -30f), new Vector3(0f, 30f),
                                                               new Vector3(-5f, 10f), new Vector3(5f, 10f), new Vector3(10f, 5f), new Vector3(10f, -5f),
                                                               new Vector3(5f, -10f), new Vector3(-5f, -10f), new Vector3(-10f, 5f), new Vector3(-10f, -5f),
                                                               new Vector3(-10f, 20f), new Vector3(10f, 20f), new Vector3(20f, 10f), new Vector3(20f, -10f),
                                                               new Vector3(10f, -20f), new Vector3(-10f, -20f), new Vector3(-20f, 10f), new Vector3(-20f, -10f) } );

    }
}
