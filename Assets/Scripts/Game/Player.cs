using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    private float BoostSpeed = 8.0f;
    [SerializeField]
    private GameObject LaserPrefab;
    [SerializeField]
    private float FireRate = 0.5f;
    private float CanFire;
    [SerializeField]
    private int lives = 3;
    private SpawnManager spawnManager;
    private bool IsTripleShotActive = false;
    [SerializeField]
    private GameObject TripleShotPrefab;
    private bool IsSpeedBoostActive = false;
    private bool IsShieldActive = false;
    [SerializeField]
    private GameObject Shield;
    [SerializeField]
    private int Score = 0;
    private UIManager uimanager;
    [SerializeField]
    private GameObject PlayerThruster;
    [SerializeField]
    private GameObject PlayerHurtLeft;
    [SerializeField]
    private GameObject PlayerHurtRight;
    [SerializeField]
    private AudioClip LaserAudio;
    private AudioSource PlayerAudioSource;
    [SerializeField]
    private AudioClip ExplosionAudio;
    [SerializeField]
    private AudioClip PowerUpCollectionAudio;



    // Start is called before the first frame update
    void Start()
    {
        PlayerThruster.SetActive(true);
        transform.position = new Vector3(0, 0, 0);

        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.Log("SpawnManager is Null");
        }

        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(uimanager == null)
        {
            Debug.Log("UIManagr is Null");
        }

        PlayerAudioSource = GetComponent<AudioSource>();
        if (PlayerAudioSource == null)
        {
            Debug.Log("Player Audio Source is Null");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        ShieldAndThrusterMovement();
        PlayerHurtPos();
        PlayerMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > CanFire)
        {
            FireLaser();
            PlayerAudioSource.PlayOneShot(LaserAudio, 1.0f);
        }

    }

    void PlayerMovement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(HorizontalInput, VerticalInput, 0);
        if (IsSpeedBoostActive == true)
        {
            transform.Translate(direction * BoostSpeed * Time.deltaTime);
            StartCoroutine(SpeedBoostCooldown());
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }


        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2, 8), transform.position.z);
    }

    void FireLaser()
    {
        CanFire = Time.time + FireRate;
        if (IsTripleShotActive == true)
        {
            Instantiate(TripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(LaserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }

    }

    public void Damage()
    {
        if (IsShieldActive == true)
        {
            Shield.SetActive(false);
            IsShieldActive = false;
            return;
        }
        lives--;
        uimanager.LivesUpdate(lives);

        if (lives == 2)
        {
            PlayerHurtLeft.SetActive(true);
        }

        else if (lives == 1)
        {
            PlayerHurtRight.SetActive(true);
        }

        if (lives < 1)
        {
            spawnManager.OnPlayerDeath();
            PlayerHurtLeft.SetActive(false);
            PlayerHurtRight.SetActive(false);
            Destroy(this.gameObject);
            PlayerThruster.SetActive(false);
        }
    }


    public void TripleShotActive()
    {
        IsTripleShotActive = true;
        StartCoroutine(TripleShotCooldown());
    }

    IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(5);
        IsTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        IsSpeedBoostActive = true;
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(5);
        IsSpeedBoostActive = false;
    }

    public void ShieldActive()
    {
        IsShieldActive = true;
        Shield.SetActive(true);
    }

    

    private void ShieldAndThrusterMovement()
    {
        Shield.transform.position = transform.position;
        PlayerThruster.transform.position = transform.position - new Vector3(0, 1.55f, 0);
    }

    public void ScoreAdd()
    {
        Score += 10;
        uimanager.ScoreUpdate(Score);
    }

    private void PlayerHurtPos()
    {
        PlayerHurtLeft.transform.position = transform.position + new Vector3(-0.8f, -1.76f, 0);
        PlayerHurtRight.transform.position = transform.position + new Vector3(0.8f, -1.76f, 0);
    }

    public void EnemyExplosion()
    {
        PlayerAudioSource.PlayOneShot(ExplosionAudio);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PowerUp")
        {
            PlayerAudioSource.PlayOneShot(PowerUpCollectionAudio);
        }
    }




}
