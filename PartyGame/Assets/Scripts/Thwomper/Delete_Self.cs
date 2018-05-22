using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Self : MonoBehaviour {

    public float Seconds = 1.5f;

    private bool _shrinking = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!_shrinking)
        {
            Seconds -= Time.deltaTime;
            if (Seconds <= 0)
            {
                _shrinking = true;
            }
        }
        else
        {
            this.transform.localScale -= Vector3.one * Time.deltaTime * 0.25f;
            if(this.transform.localScale.y <= 0)
            {
                Destroy(this.gameObject);
            }
        }
	}
}
