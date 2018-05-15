using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public bool x = false;

    float speed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        speed += 50 * Time.deltaTime;

        if (x) { 
            transform.Rotate(Vector3.right, speed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime);

        }
    }
}
