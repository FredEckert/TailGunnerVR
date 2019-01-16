using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class TailgReticle : MonoBehaviour {

    public static VectorLine Reticle;
    public float reticleScale = 0.001f;

    public static TailgReticle use;

    public virtual void Awake()
    {
        TailgReticle.use = this;
    }

    public void StartUp() {

        //Set up target reticle lines and colors...
        //
        //set reticle to one meter in front of player
        //set reticle up 1.7 meters to match player's offset from zero
        this.gameObject.transform.position = new Vector3(0, 1.7f, 1);
        this.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        //scale reticle
        for (int index = 0; index < LineData.use.reticlePoints.Count; index++)
        {
            Vector3 point = LineData.use.reticlePoints[index];
            //point.x = point.x + 30f;
            //point.y = point.y + 20f;
            //point.y = point.y + (1 / this.reticleScale);
            point = point * this.reticleScale;
            LineData.use.reticlePoints[index] = point;
        }

        Reticle = new VectorLine(gameObject.name, LineData.use.reticlePoints, Manager.use.lineWidth);
        Reticle.material = Manager.use.lineMaterial;
        Reticle.texture = Manager.use.lineTexture;
        Reticle.color = Manager.use.colorNormal;
        Reticle.capLength = Manager.use.capLength;
        
        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;
        // Make this transform have the vector line object that's defined above
        VectorManager.ObjectSetup(gameObject, Reticle, Visibility.Dynamic, Brightness.None);

        //TailgUI.Reticle = this.MakeLine("Reticle", LineData.use.reticlePoints);
        //TailgUI.Reticle.color = Manager.use.MultiplyColor(Manager.use.colorNormal, 0.75f);
        //TailgUI.Reticle.color = Manager.use.colorNormal;
        //this.SetReticleLines();
        //TailgUI.Reticle.active = false;

        //public virtual void SetReticleLines()
        //{
        //    //Manager.use.ScalePointsToScreen(TailgUI.targetReticleNormal.points2);
        //    //Manager.use.ScalePointsToScreen(TailgUI.targetReticleActive.points2);
        //    TailgUI.Reticle.Draw3D();
        //}

    }

    // Update is called once per frame
    void Update () {

    }
}
