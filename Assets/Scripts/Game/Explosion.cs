using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip ExplosionAudio;
    private AudioSource ExplosionAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        ExplosionAudioSource = GetComponent<AudioSource>();
        if (ExplosionAudioSource == null)
        {
            Debug.Log("Explosion Audio Source is NULL");
        }
        ExplosionAudioSource.PlayOneShot(ExplosionAudio);
        Destroy(this.gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
