using UnityEngine;
using Vectrosity;

public class TitleBanner : MonoBehaviour
{
    private VectorLine line;

    private Color32 colorNormal;
    private Color32 colorIntense;
    private Color32 color1;
    private Color32 color2;

    private float starttime;
    float duration = (float)0.2;
    private bool toggle = false;

    // Use this for initialization
    void Start()
    {
        //initialize object
        this.gameObject.transform.position = new Vector3(0, 0, 1940);
        this.gameObject.transform.rotation = new Quaternion(90, 0, 0, 0);

        //initialize colors
        color1 = colorNormal = Manager.use.colorNormal;
        color2 = colorIntense = Manager.use.colorIntense;

        //create line "TitleBanner"
        line = new VectorLine(gameObject.name, LineData.use.titlePoints, Manager.use.lineWidth);
        line.material = Manager.use.lineMaterial;
        line.texture = Manager.use.lineTexture;
        line.color = Manager.use.colorNormal;
        line.capLength = Manager.use.capLength;

        // Make this transform have the vector line object that's defined above
        // This object is a rigidbody, so the vector object will do exactly what this object does
        VectorManager.ObjectSetup(gameObject, line, Visibility.Dynamic, Brightness.None);

        // Make VectorManager lines be drawn in the scene instead of as an overlay
        VectorManager.useDraw3D = true;

        Destroy(gameObject, 8.4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 200)
        {
            //blank out title when banner is inverted
            if (transform.eulerAngles.x < 180)
                line.color = colorNormal;
            else
                line.SetColor(Color.clear, 0, 30);
            //translate title banner through world space towards the player at 1 meter per second * 240
            transform.Translate(Vector3.back * Time.deltaTime * 240, Space.World);
            //rotate title banner around its local X axis at 1 degreee per second * 240
            transform.Rotate(Vector3.left * Time.deltaTime * 360);
            //set color lerp start time
            starttime = Time.time;
        }
        else
        {
            //lerp color
            var myLerp = Mathf.Lerp(0, 1, (Time.time - starttime) / duration);
            var c = Color.Lerp(color1, color2, myLerp);
            line.color = c;
            //invert colors
            if (c == color2)
            {
                if (toggle)
                {
                    toggle = !toggle;
                    color1 = colorNormal;
                    color2 = colorIntense;
                }
                else
                {
                    toggle = !toggle;
                    color1 = colorIntense;
                    color2 = colorNormal;
                }
                starttime = Time.time;
            }
        }
    }
}
