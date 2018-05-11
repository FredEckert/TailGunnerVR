// This is the same thing as the Simple3D script, except it draws the line in "real" 3D space, so it can be occluded by other 3D objects and so on.
// If the vector object doesn't appear, make sure the scene view isn't visible while in play mode
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class Simple3DTitle : MonoBehaviour
{

    public Material lineMaterial;
    public float lineWidth = 3.0f;
    public Color lineColor = Color.cyan;

    public TextAsset titleVector;

    private VectorLine line;

    // Use this for initialization
    void Start()
    {
        // Make a Vector3 array that contains points for a cube that's 1 unit in size
        //entire title and boarder
        var titlePoints = new List<Vector3> { new Vector3(-9.693f, 0f, -12.45f), new Vector3(-18.424f, 0f, -12.45f), new Vector3(-13.926f, 0f, -12.45f), new Vector3(-13.926f, 0f, -2.663f), new Vector3(-4.136f, 0f, -12.186f), new Vector3(-7.312f, 0f, -2.663f), new Vector3(-4.136f, 0f, -12.186f), new Vector3(-0.961f, 0f, -2.663f), new Vector3(-2.496f, 0f, -7.424f), new Vector3(-5.777f, 0f, -7.424f), new Vector3(4.86f, 0f, -12.186f), new Vector3(4.86f, 0f, -2.663f), new Vector3(11.474f, 0f, -12.186f), new Vector3(11.474f, 0f, -2.663f), new Vector3(17.824f, 0f, -2.663f), new Vector3(11.474f, 0f, -2.663f), new Vector3(-20.806f, 0f, -0.017f), new Vector3(-27.156f, 0f, -0.017f), new Vector3(-27.156f, 0f, 9.77f), new Vector3(-27.156f, 0f, -0.017f), new Vector3(-20.541f, 0f, 9.77f), new Vector3(-27.156f, 0f, 9.77f), new Vector3(-20.541f, 0f, 9.77f), new Vector3(-20.541f, 0f, 5.802f), new Vector3(-19.218f, 0f, 5.802f), new Vector3(-21.864f, 0f, 5.802f), new Vector3(-17.366f, 0f, -0.017f), new Vector3(-17.366f, 0f, 9.77f), new Vector3(-10.751f, 0f, 9.77f), new Vector3(-17.366f, 0f, 9.77f), new Vector3(-10.751f, 0f, 9.77f), new Vector3(-10.751f, 0f, -0.017f), new Vector3(-7.312f, 0f, -0.017f), new Vector3(-7.312f, 0f, 9.77f), new Vector3(-0.961f, 0f, 9.77f), new Vector3(-7.312f, 0f, -0.017f), new Vector3(-0.961f, 0f, 9.77f), new Vector3(-0.961f, 0f, -0.017f), new Vector3(1.684f, 0f, -0.017f), new Vector3(1.684f, 0f, 9.77f), new Vector3(8.035f, 0f, 9.77f), new Vector3(1.684f, 0f, -0.017f), new Vector3(8.035f, 0f, 9.77f), new Vector3(8.035f, 0f, -0.017f), new Vector3(18.089f, 0f, -0.017f), new Vector3(11.474f, 0f, -0.017f), new Vector3(11.474f, 0f, 9.77f), new Vector3(11.474f, 0f, -0.017f), new Vector3(11.474f, 0f, 9.77f), new Vector3(18.089f, 0f, 9.77f), new Vector3(11.474f, 0f, 4.744f), new Vector3(14.649f, 0f, 4.744f), new Vector3(21.264f, 0f, 9.77f), new Vector3(21.264f, 0f, -0.017f), new Vector3(27.879f, 0f, -0.017f), new Vector3(21.264f, 0f, -0.017f), new Vector3(27.879f, 0f, 4.744f), new Vector3(27.879f, 0f, -0.017f), new Vector3(21.264f, 0f, 4.744f), new Vector3(27.879f, 0f, 4.744f), new Vector3(27.879f, 0f, 9.77f), new Vector3(26.291f, 0f, 4.744f), new Vector3(-30.331f, 0f, -15.625f), new Vector3(31.054f, 0f, -15.625f), new Vector3(31.054f, 0f, -15.625f), new Vector3(31.054f, 0f, 12.945f), new Vector3(-30.331f, 0f, 12.945f), new Vector3(31.054f, 0f, 12.945f), new Vector3(-30.331f, 0f, -15.625f), new Vector3(-30.331f, 0f, 12.945f), new Vector3(-34.961f, 0f, -20.122f), new Vector3(35.42f, 0f, -20.122f), new Vector3(35.42f, 0f, -20.122f), new Vector3(35.42f, 0f, 17.442f), new Vector3(-34.961f, 0f, 17.442f), new Vector3(35.42f, 0f, 17.442f), new Vector3(-34.961f, 0f, -20.122f), new Vector3(-34.961f, 0f, 17.442f) };
        //just border
        //var titlePoints = new List<Vector3> { new Vector3(-30.331f, 0f, -15.625f), new Vector3(31.054f, 0f, -15.625f), new Vector3(31.054f, 0f, -15.625f), new Vector3(31.054f, 0f, 12.945f), new Vector3(-30.331f, 0f, 12.945f), new Vector3(31.054f, 0f, 12.945f), new Vector3(-30.331f, 0f, -15.625f), new Vector3(-30.331f, 0f, 12.945f), new Vector3(-34.961f, 0f, -20.122f), new Vector3(35.42f, 0f, -20.122f), new Vector3(35.42f, 0f, -20.122f), new Vector3(35.42f, 0f, 17.442f), new Vector3(-34.961f, 0f, 17.442f), new Vector3(35.42f, 0f, 17.442f), new Vector3(-34.961f, 0f, -20.122f), new Vector3(-34.961f, 0f, 17.442f) };

        // Make a Vector3 array from the data stored in the vectorCube text asset
        // Try using different assets from the Vectors folder for different shapes (the collider will still be a cube though!)

        //var ship01Points = VectorLine.BytesToVector3List(ship01Vector.bytes);

        // Make a line using the above points, with a width of 3 pixels
        //var line = new VectorLine(gameObject.name, cubePoints, 3.0f);
        line = new VectorLine(gameObject.name, titlePoints, lineWidth);
        line.material = lineMaterial;
        line.color = lineColor;

        // Make this transform have the vector line object that's defined above
        // This object is a rigidbody, so the vector object will do exactly what this object does
        // "false" is added at the end, so that the cube mesh is not replaced by an invisible bounds mesh
        //VectorManager.ObjectSetup(gameObject, line, Visibility.Dynamic, Brightness.None, false);
        VectorManager.ObjectSetup(gameObject, line, Visibility.Dynamic, Brightness.None);

        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.z > 200)
        {
            //blank out title when banner is inverted
            if (transform.eulerAngles.x < 180)
                line.color = lineColor;
            else
                line.SetColor(Color.clear, 0, 30);
            //translate title banner through world space towards the player at 1 meter per second * 240
            transform.Translate(Vector3.back * Time.deltaTime * 240, Space.World);
            //rotate title banner around its local X axis at 1 degreee per second * 240
            transform.Rotate(Vector3.left * Time.deltaTime * 240);
        }

    }
}
