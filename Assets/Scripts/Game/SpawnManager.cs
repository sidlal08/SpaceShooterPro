using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private GameObject EnemyContainer;
    private bool IsSpawning = true;
    [SerializeField]
    private GameObject[] PowerUps;
    private UIManager uimanager;
    [SerializeField]
    private GameObject PlayerExplosion;
    [SerializeField]
    private GameObject player;
  
    

    // Start is called before the first frame update
    void Start()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uimanager == null)
        {
            Debug.Log("UIManager is NULL on Spawn Manager");
        }

     
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (IsSpawning == true)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(-9.68f, 9.68f), 9.5f, 0);
            GameObject NewEnemy = Instantiate(EnemyPrefab, SpawnPos, Quaternion.identity);
            NewEnemy.transform.parent = EnemyContainer.transform;
            yield return new WaitForSeconds(2);
        }
    }

    public void OnPlayerDeath()
    {
        IsSpawning = false;
        PlayerExp();
        uimanager.GameOverTextDisplay();
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        while (IsSpawning == true)
        {
            Vector3 SpawnPos = new Vector3(Random.Range(-9.68f, 9.68f), 9.5f, 0);
            Instantiate(PowerUps[Random.Range(0,3)], SpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    private void PlayerExp()
    {
        Instantiate(PlayerExplosion, player.transform.position, Quaternion.identity);
    }
}
