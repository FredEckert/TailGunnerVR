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

    private bool collided = false; //fwe190102

    public GameObject splineRidingCube;
    private VectorLine sline;
    private List<Vector3> splinePoints;
    private List<GameObject> splinePointSpheres;

    IEnumerator Start()
    {
        switch (Manager.use.attackWave)
        {
            case 1:
                splinePoints = LineData.use.w1t1sPoints;
                break;
            case 2:
                splinePoints = LineData.use.w1t2sPoints;
                break;
        }

        if (showPoints)
        {
            splinePointSpheres = new List<GameObject>();
            foreach (Vector3 sPoint in splinePoints)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = sPoint;
                sphere.GetComponent<Collider>().enabled = false;
                sphere.GetComponent<MeshRenderer>().enabled = true;
                splinePointSpheres.Add(sphere);
            }
            splineRidingCube.GetComponent<MeshRenderer>().enabled = true;
        }

        sline = new VectorLine("Spline", new List<Vector3>(segments + 1), 2.0f, LineType.Continuous);
        sline.MakeSpline(splinePoints.ToArray(), segments, doLoop);
        if (showSpline)
            sline.Draw3D();

        //put enemy ship at start point before showing it
        gameObject.transform.position = sline.GetPoint3D01(0);
        gameObject.transform.LookAt(sline.GetPoint3D01(0.001f));

        var line = new VectorLine("EnemyShip", LineData.use.ship1Points, Manager.use.lineWidth)
        //var line = new VectorLine("EnemyShip", LineData.use.ship2Points, Manager.use.lineWidth)
        //var line = new VectorLine("EnemyShip", LineData.use.ship3Points, Manager.use.lineWidth)
        //var line = new VectorLine("EnemyShip", LineData.use.ship4Points, Manager.use.lineWidth)
        {
            material = Manager.use.lineMaterial,
            texture = Manager.use.lineTexture,
            color = Manager.use.colorNormal,
            capLength = Manager.use.capLength,
            drawTransform = splineRidingCube.transform
        };

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
                splineRidingCube.transform.position = sline.GetPoint3D01(dist);
                splineRidingCube.transform.LookAt(sline.GetPoint3D01(dist + 0.001f));
                //line.Draw3D();
                yield return null;
            }
        } while (doLoop);

    }

    private void OnDestroy()
    {
        if (showPoints)
        {
            foreach (GameObject obj in splinePointSpheres)
            {
                Destroy(obj);
            }
            splinePointSpheres.RemoveAll((o) => o == null);
        }

        VectorLine.Destroy(ref sline);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    // Make 100% sure that only one collision registers
    //    if (this.collided)
    //    {
    //        return;
    //    }
    //    this.collided = true;
    //    //Debug.Log("Collision");
    //    // force is how forcefully we will push the EnemyShip away from the shield.
    //    float force = 7000000;
    //    // Calculate Angle Between the collision point and the EnemyShip
    //    Vector3 dir = collision.contacts[0].point - transform.position;
    //    // We then get the opposite (-Vector3) and normalize it
    //    dir = -dir.normalized;
    //    // And finally we add force in the direction of dir and multiply it by force.
    //    // This will push back the EnemyShip
    //    GetComponent<Rigidbody>().AddForce(dir * force);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        // Make 100% sure that only one collision registers
        if (this.collided)
        {
            return;
        }
        this.collided = true;
        //erase EnemyShip
        UnityEngine.Object.Destroy(gameObject);
        //show EnemyShip exploding parts
        MakeShipExplosion(transform.position,transform.rotation);
    }

    public virtual void MakeShipExplosion(Vector3 pos, Quaternion rot)
    {
        //fwe181230 this.PlayAudioClip(this.explosionSound, pos);
        // Make a temporary object that the exploded ship parts will be attached to temporarily, so we can set their positions easily with localPosition
        Transform temp = new GameObject().transform;
        temp.position = pos;
        // Make every explosion a little different by changing the position of the explosion force
        Vector3 randomize = new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), Random.Range(-1f, 1f));
        // Instantiate all ship parts at their corresponding locations and add explosion force to each
        int i = 0;
        while (i < LineData.use.partLocations.Length)
        {

            Transform part = UnityEngine.Object.Instantiate(Manager.use.shipPart);
            part.parent = temp;
            part.localPosition = LineData.use.partLocations[i];

            ((ShipPart)part.GetComponent(typeof(ShipPart))).line = new VectorLine("ShipPart", LineData.use.partPoints[i], Manager.use.lineWidth)
            {
                material = Manager.use.lineMaterial,
                color = Manager.use.colorNormal
            };
            float modify = i == 0 ? 1.4f : 1f; // Make radar (which is the first part) go farther because it's supposedly lighter
            //((Rigidbody)part.GetComponent(typeof(Rigidbody))).AddExplosionForce((Manager.use.explodeForce * modify) + Random.Range(-4f, 4f), pos + randomize, 10f, 5f, ForceMode.VelocityChange);
            ((Rigidbody)part.GetComponent(typeof(Rigidbody))).AddExplosionForce((Manager.use.explodeForce * modify) + Random.Range(-4f, 4f), pos + randomize, 10f, 0f, ForceMode.VelocityChange);
            i++;
        }
        // Rotate all parts
        temp.rotation = rot;
        temp.DetachChildren();
        UnityEngine.Object.Destroy(temp.gameObject);
    }

}
