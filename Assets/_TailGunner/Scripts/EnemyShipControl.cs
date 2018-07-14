using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class EnemyShipControl : MonoBehaviour
{
    public int segments = 250;
    public bool doLoop = true;
    public float speed = 0.01f;

    IEnumerator Start()
    {
        //GameObject cube = GameObject.Find("Cube");
        //FixedJoint joint = cube.GetComponent<FixedJoint>();
        //Rigidbody rb = GetComponent<Rigidbody>();
        //joint.connectedBody = rb;

        //create a cube with a joint to ride the spline
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Rigidbody>();
        cube.AddComponent<HingeJoint>();
        Rigidbody cuberb = cube.GetComponent<Rigidbody>();
        HingeJoint joint = cube.GetComponent<HingeJoint>();
        //cube has to have a rigidbody that is kinematic for physics to work
        cuberb.isKinematic = true;
        //configure joint
        JointLimits lim = joint.limits;
        lim.min = -100f;
        lim.max = 100f;
        lim.bounciness = 0;
        lim.bounceMinVelocity = 0;
        joint.axis = Vector3.forward;
        joint.limits = lim;
        joint.useLimits = true;
        //connect EnemyShip to joint
        Rigidbody rb = GetComponent<Rigidbody>();
        joint.connectedBody = rb;

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

        // Make the cube "ride" the spline at a constant speed
        do
        {
            for (var dist = 0.0f; dist < 1.0f; dist += Time.deltaTime * speed)
            {
                cube.transform.position = sline.GetPoint3D01(dist);
                cube.transform.LookAt(sline.GetPoint3D01(dist + 0.001f));
                yield return null;
            }
        } while (doLoop);


    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //rotate object around its local y axis at 1 degree per second * 10
    //    transform.Rotate(Vector3.up * Time.deltaTime * 10);
    //}
}
