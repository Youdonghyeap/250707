using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameOverUI;
    public float timer = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerTextUI;

    public Bird bird;

    void Awake()
    {
        instance = this;
        if (GameOverUI != null)
            GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bird.isLive)
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("F2");
        }

        if (!bird.isLive)
        {
            GameOver();
            GameOverUIon();
        }
    }

    public void GameOver()
    {
        Invoke("GameOverUIon", 1.5f);
        timerTextUI.text = "Time : " + timer.ToString("F2");
        timerText.gameObject.SetActive(false);
    }

    public void GameOverUIon()
    {
        GameOverUI.SetActive(true);
        bird.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
