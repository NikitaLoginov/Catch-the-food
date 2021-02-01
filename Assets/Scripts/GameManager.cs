using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // text mesh pro

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public bool isGameActive;

    int score;
    float spawnRate = 1.0f;
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        // while game is active - spawn stuff
        while (isGameActive)
        { 
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loading currently active scene by name
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty; // to make stuff spawn faster when difficulty is harder by dividing spawn rate by difficulty
        titleScreen.gameObject.SetActive(false);

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }
}
