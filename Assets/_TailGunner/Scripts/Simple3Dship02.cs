﻿// This is the same thing as the Simple3D script, except it draws the line in "real" 3D space, so it can be occluded by other 3D objects and so on.
// If the vector object doesn't appear, make sure the scene view isn't visible while in play mode
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class Simple3Dship02 : MonoBehaviour
{

    public Material lineMaterial;
    public float lineWidth = 3.0f;
    public Color lineColor = Color.cyan;

    public TextAsset ship02Vector;

    // Use this for initialization
    void Start()
    {
        // Make a Vector3 array that contains points for a cube that's 1 unit in size
        var ship02Points = new List<Vector3> { new Vector3(3.226f, 0f, 0f), new Vector3(2.151f, 0f, -1.613f), new Vector3(2.151f, 0f, -1.613f), new Vector3(-2.151f, 0f, -1.613f), new Vector3(-2.151f, 0f, -1.613f), new Vector3(-3.226f, 0f, 0f), new Vector3(2.151f, 0f, 1.613f), new Vector3(3.226f, 0f, 0f), new Vector3(-3.226f, 0f, 0f), new Vector3(-2.151f, 0f, 1.613f), new Vector3(-2.151f, 0f, 1.613f), new Vector3(2.151f, 0f, 1.613f), new Vector3(4.624f, -1.247f, 0f), new Vector3(3.226f, -0.968f, -2.151f), new Vector3(3.226f, -0.968f, -2.151f), new Vector3(-2.151f, 0f, -1.613f), new Vector3(-2.151f, 0f, 1.613f), new Vector3(3.226f, -0.968f, 2.151f), new Vector3(3.226f, -0.968f, 2.151f), new Vector3(4.624f, -1.247f, 0f), new Vector3(2.151f, 0f, 1.613f), new Vector3(3.226f, -0.968f, 2.151f), new Vector3(2.151f, 0f, -1.613f), new Vector3(3.226f, -0.968f, -2.151f), new Vector3(-2.151f, 0f, 1.613f), new Vector3(-5.376f, 0f, 1.613f), new Vector3(-5.376f, 0f, 1.613f), new Vector3(-5.376f, 0.86f, 2.151f), new Vector3(-5.376f, 0.86f, 2.151f), new Vector3(-2.151f, 0.86f, 2.151f), new Vector3(-2.151f, 0.86f, 2.151f), new Vector3(-2.151f, 0f, 1.613f), new Vector3(-2.151f, 0f, -1.613f), new Vector3(-5.376f, 0f, -1.613f), new Vector3(-5.376f, 0f, -1.613f), new Vector3(-5.376f, 0.86f, -2.151f), new Vector3(-5.376f, 0.86f, -2.151f), new Vector3(-2.151f, 0.86f, -2.151f), new Vector3(-2.151f, 0.86f, -2.151f), new Vector3(-2.151f, 0f, -1.613f), new Vector3(3.226f, 0f, 0f), new Vector3(4.624f, -1.247f, 0f) };

        // Make a Vector3 array from the data stored in the vectorCube text asset
        // Try using different assets from the Vectors folder for different shapes (the collider will still be a cube though!)
        //var ship01Points = VectorLine.BytesToVector3List(ship01Vector.bytes);

        // Make a line using the above points, with a width of 3 pixels
        //var line = new VectorLine(gameObject.name, cubePoints, 3.0f);
        var line = new VectorLine(gameObject.name, ship02Points, lineWidth);
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
        // Rotate the object around its local X axis at 1 degree per second * 50
        //transform.Rotate(Vector3.right * Time.deltaTime * 50);
        // ...also rotate around the World's Y axis * 50
        transform.Rotate(Vector3.up * Time.deltaTime * 50, Space.World);
    }
}
