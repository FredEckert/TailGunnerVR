using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class LeadInParticle : MonoBehaviour
{
    public int segments = 120;
    public int visibleLineSegments = 2;
    public float speed = 60.0f;
    private float startIndex;
    private float endIndex;
    private VectorLine line;

    void Start()
    {
        startIndex = -visibleLineSegments;
        endIndex = 0;

        // Make Vector3 array where the size is the number of segments plus one, since we'll use a continuous line
        var linePoints = new List<Vector3>(segments + 1);
        // Make a VectorLine object using the above points, with a width of 2 pixels
        line = new VectorLine("LeadIn", linePoints, 2.0f, LineType.Continuous, Joins.Weld);
        line.texture = Manager.use.lineTexture;
        line.material = Manager.use.lineMaterial;
        line.color = Manager.use.colorNormal;
        line.capLength = Manager.use.capLength;

        var x = -22;
        var y = -15;
        line.MakeSpline(new Vector3[] { new Vector3(x, y, 10), new Vector3(x, y, 200) });
    }

    void Update()
    {
        // Change startIndex and endIndex over time, wrapping around as necessary
        startIndex += Time.deltaTime * speed;
        endIndex += Time.deltaTime * speed;

        if (startIndex >= segments + 1)
        {
            //do not wrap on lead in particle, destroy particle after one cycle
            //startIndex = -visibleLineSegments;
            //endIndex = 0;
            VectorLine.Destroy(ref line);
            Destroy(gameObject);
            return;
        }
        else if (startIndex < -visibleLineSegments)
        {
            startIndex = segments;
            endIndex = segments + visibleLineSegments;
        }

        line.drawStart = (int)startIndex;
        line.drawEnd = (int)endIndex;
        line.Draw3D();
    }
}
