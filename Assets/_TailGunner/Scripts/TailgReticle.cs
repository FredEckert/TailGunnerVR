using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class TailgReticle : MonoBehaviour {

    public static VectorLine Reticle;
    public float reticleScale = 0.001f;
    private float defaultPosZ;

    public static TailgReticle use;

    public virtual void Awake()
    {
        TailgReticle.use = this;
    }

    public void StartUp()
    {
        //Set up target reticle lines and colors...
        //set reticle up 1.7 meters to match player's offset from zero
        //set reticle distance to just inside far clip plane
        defaultPosZ = 1860f;
        this.gameObject.transform.position = new Vector3(0, 1.7f, defaultPosZ);
        this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        //scale reticle
        for (int index = 0; index < LineData.use.reticlePoints.Count; index++)
        {
            Vector3 point = LineData.use.reticlePoints[index];
            point = point * this.reticleScale;
            LineData.use.reticlePoints[index] = point;
        }
        //Create reticle
        Reticle = new VectorLine(gameObject.name, LineData.use.reticlePoints, Manager.use.lineWidth);
        Reticle.material = Manager.use.lineMaterial;
        Reticle.texture = Manager.use.lineTexture;
        Reticle.color = Manager.use.colorNormal;
        Reticle.capLength = Manager.use.capLength;
        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;
        // Make this transform have the vector line object that's defined above
        VectorManager.ObjectSetup(gameObject, Reticle, Visibility.Dynamic, Brightness.None);
    }

    // Update is called once per frame
    void Update()
    {
        Transform camera = Camera.main.transform;
        Ray ray = new Ray(camera.position, camera.rotation * Vector3.forward);
        RaycastHit hit;
        float distance;
        if (Physics.Raycast(ray, out hit))
        {
            distance = hit.distance;
            Reticle.color = Manager.use.colorIntense;
        }
        else
        {
            distance = defaultPosZ;
            Reticle.color = Manager.use.colorNormal;
        }
        transform.localPosition = new Vector3(0, 0, distance);
        transform.localScale = Vector3.one * distance;
    }

}
