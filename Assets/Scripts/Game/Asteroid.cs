using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float RotSpeed = 25.0f;
    [SerializeField]
    private GameObject Explosion;
    private SpawnManager spawnManager;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if(spawnManager == null)
        {
            Debug.Log("Spawn Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * RotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject, 0.2f);
            Instantiate(Explosion, transform.position, Quaternion.identity);
            spawnManager.StartSpawning();
        }
    }
}
