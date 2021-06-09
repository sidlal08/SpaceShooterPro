using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Image LivesImage;
    [SerializeField]
    private Sprite[] LivesSprite;
    [SerializeField]
    private Text GameOverText;
    [SerializeField]
    private Text RestartText;
    private GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score: " + 0;
        GameOverText.gameObject.SetActive(false);
        RestartText.gameObject.SetActive(false);
        gamemanager = GameObject.Find("GameManager").GetComponent< GameManager >();
        if (gamemanager == null)
            Debug.Log("GameManager is null");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ScoreUpdate(int PlayerScore)
    {
        ScoreText.text = "Score: " + PlayerScore;
    }

    public void LivesUpdate(int CurrentLives)
    {
        LivesImage.sprite = LivesSprite[CurrentLives];
    }

    public void GameOverTextDisplay()
    {
        gamemanager.GameOver();
        GameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverTextFlicker());
        RestartText.gameObject.SetActive(true);
    }

    IEnumerator GameOverTextFlicker()
    {
        while (true)
        {
            GameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            GameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
