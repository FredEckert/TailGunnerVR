using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class SplineFollow3D : MonoBehaviour {

    //// Use this for initialization
    //void Start () {
    //}

    //// Update is called once per frame
    //void Update () {
    //}


    public int segments = 250;
    public bool doLoop = true;
    public float speed = .05f;

    public Transform cube;


    IEnumerator Start()
    {
        var splinePoints = new List<Vector3>();
        var i = 1;
        var obj = GameObject.Find("Sphere" + (i++));
        while (obj != null)
        {
            splinePoints.Add(obj.transform.position);
            obj = GameObject.Find("Sphere" + (i++));
        }

        var line = new VectorLine("Spline", new List<Vector3>(segments + 1), 2.0f, LineType.Continuous);
        line.MakeSpline(splinePoints.ToArray(), segments, doLoop);
        line.Draw3D();

        // Make the cube "ride" the spline at a constant speed
        do
        {
            for (var dist = 0.0f; dist < 1.0f; dist += Time.deltaTime * speed)
            {
                cube.position = line.GetPoint3D01(dist);
                yield return null;
            }
        } while (doLoop);
    }

}
