using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject winTextObject;

    public TextMeshProUGUI timerText;

    public AudioSource audioSource;
    public AudioClip winSound;
    public AudioClip loseSound;

    private float timer = 0f;

    private bool isPaused = false;
    private bool gameEnded = false;

    void Start()
    {
        Time.timeScale = 1f;

        pauseMenu.SetActive(false);
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if (!gameEnded && !isPaused)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + Mathf.FloorToInt(timer);
        }

        if (!gameEnded && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void WinGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        winTextObject.SetActive(true);

        TextMeshProUGUI text =
            winTextObject.GetComponent<TextMeshProUGUI>();

        text.text = "You Win!\nTime: " + Mathf.FloorToInt(timer);
        text.color = Color.green;

        audioSource.PlayOneShot(winSound);

        Time.timeScale = 0;
    }

    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;

        winTextObject.SetActive(true);

        TextMeshProUGUI text =
            winTextObject.GetComponent<TextMeshProUGUI>();

        text.text = "You Lose!\nTime: " + Mathf.FloorToInt(timer);
        text.color = Color.red;

        audioSource.PlayOneShot(loseSound);

        Time.timeScale = 0;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}