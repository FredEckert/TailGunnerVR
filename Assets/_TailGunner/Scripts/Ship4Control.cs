using UnityEngine;
using Vectrosity;

public class Ship4Control : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var line = new VectorLine("EnemyShip", LineData.use.Ship4Points, Manager.use.lineWidth);
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
