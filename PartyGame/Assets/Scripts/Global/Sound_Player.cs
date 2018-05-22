using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Player : MonoBehaviour {


    private AudioSource _audioSource;


    IEnumerator playSound()
    {
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(this.gameObject);

    }

    void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }
	

    public void Play(AudioClip audioClip)
    {
        //_audioClip = audioClip;
        _audioSource.clip = audioClip;
        StartCoroutine(playSound());

    }
}
