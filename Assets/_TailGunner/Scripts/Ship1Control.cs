using UnityEngine;
using Vectrosity;

public class Simple3Dship01 : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //create line "Simple3Dship01"
        var line = new VectorLine(gameObject.name, LineData.use.ship01Points, Manager.use.lineWidth);
        line.material = Manager.use.lineMaterial;
        line.texture = Manager.use.lineTexture;
        line.color = Manager.use.colorNormal;
        line.capLength = Manager.use.capLength;

        // Make this transform have the vector line object that's defined above
        // This object is a rigidbody, so the vector object will do exactly what this object does
        VectorManager.ObjectSetup(gameObject, line, Visibility.Dynamic, Brightness.None);
        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;
    }

    // Update is called once per frame
    void Update()
    {
        //rotate object around its local y axis at 1 degreee per second * 10
        transform.Rotate(Vector3.up * Time.deltaTime * 10);
    }
}
