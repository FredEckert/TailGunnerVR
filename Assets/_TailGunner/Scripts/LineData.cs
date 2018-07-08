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

    public System.Collections.Generic.List<Vector3> ship1Points;
    public System.Collections.Generic.List<Vector3> ship2Points;
    public System.Collections.Generic.List<Vector3> ship3Points;
    public System.Collections.Generic.List<Vector3> ship4Points;
    public System.Collections.Generic.List<Vector3> titlePoints;

    public System.Collections.Generic.List<Vector3> reticlePoints;

    public virtual void Awake()
    {
        LineData.use = this;
        this.ship1Points = VectorLine.BytesToVector3List(this.ship1.bytes);
        this.ship2Points = VectorLine.BytesToVector3List(this.ship2.bytes);
        this.ship3Points = VectorLine.BytesToVector3List(this.ship3.bytes);
        this.ship4Points = VectorLine.BytesToVector3List(this.ship4.bytes);
        this.titlePoints = VectorLine.BytesToVector3List(this.title.bytes);

        // Exploded ship parts

        // Reticle
        //this.reticlePoints = new List<Vector3>(new Vector3[] { };
    }
}
