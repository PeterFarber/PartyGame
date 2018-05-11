using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {

    public Color color;

    // Use this for initialization
    void Start()
    {
        this.GetComponentInChildren<Renderer>().material.SetColor("_Color", color);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
