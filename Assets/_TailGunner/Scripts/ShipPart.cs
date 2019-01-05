using UnityEngine;
using System.Collections;
using Vectrosity;

[System.Serializable]
public partial class ShipPart : MonoBehaviour
{
    public VectorLine line;

    private float timer;

    private Color initialColor;

    private int objectNumber;

    private bool collided;

    public virtual void Start()
    {
        // Set the initial brightness based on distance from player...we're not using fog since that interferes with the fading we do in FadeOut
        //fwe181230 this.initialColor = Manager.use.MultiplyColor(Manager.use.normalColor, (float) VectorManager.GetBrightnessValue(this.transform.position));
        this.initialColor = Manager.use.colorNormal;
        this.line.SetColor(this.initialColor);
        VectorManager.useDraw3D = true;
        VectorManager.ObjectSetup(this.gameObject, this.line, Visibility.Dynamic, Brightness.None);
        this.objectNumber = Manager.use.ArrayAdd(this.transform, Manager.use.objects);
        //fwe181230 Physics.IgnoreCollision((Collider) this.GetComponent(typeof(Collider)), Manager.playerCollider);
        this.InvokeRepeating("FadeOut", 0.2f, 0.2f);
    }

    //public virtual void OnCollisionEnter(Collision collisionInfo)
    //{
    //    if ((!this.collided && (collisionInfo.collider.gameObject.name == "Ground")) || (collisionInfo.collider.gameObject.name == "Obstacle"))
    //    {
    //        this.collided = true;
    //        this.DestroySelf();
    //    }
    //}

    public virtual void DestroySelf()
    {
        UnityEngine.Object.Destroy(this.gameObject);
        Manager.use.ArrayRemove(this.objectNumber, Manager.use.objects);
    }

    public virtual void FadeOut()
    {
        this.timer = this.timer + 0.05f;
        this.line.SetColor(MathS.ColorLerp(this.initialColor, Color.black, this.timer));
        if (this.timer > 1f)
        {
            this.DestroySelf();
        }
    }

}