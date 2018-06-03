using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class LineData : MonoBehaviour
{
    public static LineData use;

    public TextAsset ship01;
    public TextAsset ship02;
    public TextAsset ship03;
    public TextAsset ship04;
    public TextAsset title;

    public System.Collections.Generic.List<Vector3> ship01Points;
    public System.Collections.Generic.List<Vector3> ship02Points;
    public System.Collections.Generic.List<Vector3> ship03Points;
    public System.Collections.Generic.List<Vector3> ship04Points;
    public System.Collections.Generic.List<Vector3> titlePoints;

    public System.Collections.Generic.List<Vector3> targetReticlePoints;

    public virtual void Awake()
    {
        LineData.use = this;
        this.ship01Points = VectorLine.BytesToVector3List(this.ship01.bytes);
        this.ship02Points = VectorLine.BytesToVector3List(this.ship02.bytes);
        this.ship03Points = VectorLine.BytesToVector3List(this.ship03.bytes);
        this.ship04Points = VectorLine.BytesToVector3List(this.ship04.bytes);
        this.titlePoints = VectorLine.BytesToVector3List(this.title.bytes);

        // Exploded ship parts

        // Reticle
        //this.targetReticlePoints = new List<Vector3>(new Vector3[] { };
    }
}
