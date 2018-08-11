using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class EnemyShipControl : MonoBehaviour
{
    public int segments = 250;
    public bool doLoop = false;
    public float speed = 0.05f;
    public bool showSpline = false;
    public bool showPoints = false;

    IEnumerator Start()
    {
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
        lim.min = -1f;
        lim.max = 1f;
        lim.bounciness = 0;
        lim.bounceMinVelocity = 0;
        joint.limits = lim;
        joint.useLimits = true;
        joint.axis = Vector3.forward;
        joint.breakForce = 1000000.0f;
        joint.breakTorque = 1000000.0f;
        joint.enablePreprocessing = true;
        //connect EnemyShip to joint
        Rigidbody rb = GetComponent<Rigidbody>();
        joint.connectedBody = rb;

        cube.GetComponent<MeshRenderer>().enabled = showPoints;

        var splinePoints = new List<Vector3>();
        var i = 1;
        var obj = GameObject.Find("Sphere" + (i++));

        while (obj != null)
        {
            splinePoints.Add(obj.transform.position);
            obj.GetComponent<MeshRenderer>().enabled = showPoints;
            obj = GameObject.Find("Sphere" + (i++));
        }

        var sline = new VectorLine("Spline", new List<Vector3>(segments + 1), 2.0f, LineType.Continuous);
        sline.MakeSpline(splinePoints.ToArray(), segments, doLoop);
        if (showSpline)
            sline.Draw3D();

        var line = new VectorLine("EnemyShip", LineData.use.ship1Points, Manager.use.lineWidth);
        //var line = new VectorLine("EnemyShip", LineData.use.ship2Points, Manager.use.lineWidth);
        //var line = new VectorLine("EnemyShip", LineData.use.ship3Points, Manager.use.lineWidth);
        //var line = new VectorLine("EnemyShip", LineData.use.ship4Points, Manager.use.lineWidth);

        line.material = Manager.use.lineMaterial;
        line.texture = Manager.use.lineTexture;
        line.color = Manager.use.colorNormal;
        line.capLength = Manager.use.capLength;
        line.drawTransform = cube.transform;

        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;
        // Make this transform have the vector line object that's defined above
        // This object is a rigid body, so the vector object will do exactly what this object does
        VectorManager.ObjectSetup(gameObject, line, Visibility.Dynamic, Brightness.None);

        // Make the EnemyShip "ride" the spline at a constant speed
        do
        {
            for (var dist = 0.0f; dist < 1.0f; dist += Time.deltaTime * speed)
            {
                cube.transform.position = sline.GetPoint3D01(dist);
                cube.transform.LookAt(sline.GetPoint3D01(dist + 0.001f));
                //line.Draw3D();
                yield return null;
            }
        } while (doLoop);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision");
        // force is how forcefully we will push the EnemyShip away from the shield.
        float force = 7000000;

        // Calculate Angle Between the collision point and the EnemyShip
        Vector3 dir = collision.contacts[0].point - transform.position;
        // We then get the opposite (-Vector3) and normalize it
        dir = -dir.normalized;
        // And finally we add force in the direction of dir and multiply it by force.
        // This will push back the EnemyShip
        GetComponent<Rigidbody>().AddForce(dir * force);
    }

}
