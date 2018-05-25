using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple3DTitleDisplay : MonoBehaviour {

	void Start ()
    {
        InvokeRepeating("Display", 0, 10);
	}
	
	void Display ()
    {
        new GameObject("Title").AddComponent<Simple3DTitle>();
	}
}
