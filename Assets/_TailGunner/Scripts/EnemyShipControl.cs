using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class EnemyShipControl : MonoBehaviour
{
    public int segments = 250;
    public bool doLoop = true;
    public float speed = .05f;

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

        var sline = new VectorLine("Spline", new List<Vector3>(segments + 1), 2.0f, LineType.Continuous);
        sline.MakeSpline(splinePoints.ToArray(), segments, doLoop);
        sline.Draw3D();

        var line = new VectorLine("EnemyShip", LineData.use.ship1Points, Manager.use.lineWidth);
        //var line = new VectorLine("EnemyShip", LineData.use.ship2Points, Manager.use.lineWidth);
        //var line = new VectorLine("EnemyShip", LineData.use.ship3Points, Manager.use.lineWidth);
        //var line = new VectorLine("EnemyShip", LineData.use.ship4Points, Manager.use.lineWidth);

        line.material = Manager.use.lineMaterial;
        line.texture = Manager.use.lineTexture;
        line.color = Manager.use.colorNormal;
        line.capLength = Manager.use.capLength;

        // Make this transform have the vector line object that's defined above
        // This object is a rigid body, so the vector object will do exactly what this object does
        VectorManager.ObjectSetup(gameObject, line, Visibility.Dynamic, Brightness.None);
        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;

        float RotationSpeed = 3000.0f;
        // Make the EnemyShip "ride" the spline at a constant speed
        do
        {
            for (var dist = 0.0f; dist < 1.0f; dist += Time.deltaTime * speed)
            {
                transform.position = sline.GetPoint3D01(dist);
                transform.LookAt(sline.GetPoint3D01(dist + 0.001f));

                //add pitch yaw and roll to ship
                Quaternion AddRot = Quaternion.identity;
                float pitch = 0;
                float yaw = 0;
                float roll = 0;
                pitch = Input.GetAxis("Pitch") * (Time.fixedDeltaTime * RotationSpeed);
                yaw = Input.GetAxis("Yaw") * (Time.fixedDeltaTime * RotationSpeed);
                roll = Input.GetAxis("Roll") * (Time.fixedDeltaTime * RotationSpeed);
                Debug.Log("p=" + pitch.ToString() + " y=" +  yaw.ToString() + " r="+ roll.ToString());
                AddRot.eulerAngles = new Vector3(-pitch, yaw, -roll);
                transform.Rotate(AddRot.eulerAngles);

                yield return null;
            }
        } while (doLoop);


    }

}
