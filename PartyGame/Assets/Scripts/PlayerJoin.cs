using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoin : MonoBehaviour {

    public int _player = -1;

    private GameObject _persitedController;

	// Use this for initialization
	void Awake () {
        _persitedController = GameObject.Find("Persited_Controller");
    }
	
	// Update is called once per frame
	void Update () {
        bool joined = _persitedController.GetComponent<Persist>().CheckPlayer(_player-1);
        
        if (joined)
        {
            GetComponent<UnityEngine.UI.Text>().text = "Player " + _player;
        }
	}
}
