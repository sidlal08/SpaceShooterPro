using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float EnemySpeed = 4.0f;
    [SerializeField]
    private Player player;
    private Animator EnemyAnimator;
    [SerializeField]
    private AudioClip ExplosionAudio;
    private AudioSource EnemyAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.Log("Player is null");
        }

        EnemyAnimator = GetComponent<Animator>();
        if (EnemyAnimator == null)
        {
            Debug.Log("EnemyAnimator is null");
        }

        EnemyAudioSource = GetComponent<AudioSource>();
        if (EnemyAudioSource == null)
        {
            Debug.Log("Enemy Audio Source is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();    
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * EnemySpeed * Time.deltaTime);
        if (transform.position.y < -3.27)
        {
            Destroy(this.gameObject);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    { 

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.Damage();
            EnemyExp();
            EnemySpeed = 0.5f;
            Destroy(this.gameObject, 1.8f);
        }

        if (other.tag == "Laser")
        {
            player.EnemyExplosion();
            Destroy(other.gameObject);
            if (player != null)
            {
                player.ScoreAdd(); 
            }
            EnemyExp();
            EnemySpeed = 0.5f;
            Destroy(this.gameObject, 1.8f);
        }
    }

    private void EnemyExp()
    {
        EnemyAudioSource.PlayOneShot(ExplosionAudio);
        EnemyAnimator.SetTrigger("OnEnemyDeath");
    }
}
