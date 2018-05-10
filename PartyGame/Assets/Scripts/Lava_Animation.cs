using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava_Animation : MonoBehaviour {

    private int direction = 1;
    private Material material;
    public float currentFloat = 0.08f;

	// Use this for initialization
	void Start () {
        material = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {

	    if(direction == -1)
        {
            currentFloat += Time.deltaTime * 0.1f;
            if(currentFloat >= 0.25f)
            {
                direction *= -1;
            }
        }
        else
        {
            currentFloat -= Time.deltaTime * 0.1f;
            if (currentFloat <= -0.0f)
            {
                direction *= -1;
            }
        }

        material.SetFloat("_Parallax", currentFloat);

    }
}
