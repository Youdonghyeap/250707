using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject GameOverUI;
    public float timer = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerTextUI;

    public Bird bird;
    private bool gameOverCalled = false;
    private bool isGameStarted = false;
    public TextMeshProUGUI countdownText;

    void Awake()
    {
        instance = this;
        if (GameOverUI != null)
            GameOverUI.SetActive(false);
    }

    void Start()
    {
        if (countdownText != null)
            StartCoroutine(CountdownRoutine());
    }

    private System.Collections.IEnumerator CountdownRoutine()
    {
        bird.rd.simulated = false;
        bird.isLive = false;
        isGameStarted = false;
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.text = "Go!";
        yield return new WaitForSeconds(0.7f);
        countdownText.gameObject.SetActive(false);
        bird.isLive = true;
        isGameStarted = true;
        bird.rd.simulated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
            return;

        if (bird.isLive)
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("F2");
        }
        else
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (!gameOverCalled)
        {
            gameOverCalled = true;
            Invoke("GameOverUIon", 0.5f);
            timerTextUI.text = "Time : " + timer.ToString("F2");
            timerText.gameObject.SetActive(false);
        }
    }

    public void GameOverUIon()
    {
        GameOverUI.SetActive(true);
        var cg = GameOverUI.GetComponent<CanvasGroup>();
        if (cg == null)
            cg = GameOverUI.AddComponent<CanvasGroup>();
        cg.alpha = 0f;
        // DOTween의 DOFade는 UnityEngine.UI.CanvasGroup에 대해 정의되어 있습니다.
        // using DG.Tweening.DOTweenModuleUI;가 필요합니다.
        DOTween.To(() => cg.alpha, x => cg.alpha = x, 1f, 1f); // 1초 동안 페이드 인
        bird.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
