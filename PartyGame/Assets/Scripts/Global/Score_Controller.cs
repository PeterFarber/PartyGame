using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Controller : MonoBehaviour {

    public float[] _playerScores;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	void Start () {
        _playerScores = new float[4];
	}
	
    public void IncrementScore(int _player)
    {
        _playerScores[_player]++;
    }

    public float GetScore(int _player)
    {
        return _playerScores[_player];
    }

}
