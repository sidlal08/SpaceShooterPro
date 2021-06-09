using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private float PUSpeed = 3.0f;
    [SerializeField]
    private int PowerUpID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * PUSpeed * Time.deltaTime);
        if (transform.position.y < -3.7f)
        {
            Destroy(this.gameObject);
        }

    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (PowerUpID)
                {
                    case 0:
                        { 
                            player.TripleShotActive();
                            break;
                        }
                    case 1:
                        {
                            player.SpeedBoostActive();
                            break;
                        }
                    case 2:
                        {
                            player.ShieldActive();
                            break;
                        }
                }
            }
            Destroy(this.gameObject);
        }
    }
}
