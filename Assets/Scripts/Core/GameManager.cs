using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private IPlayer player;
    private float gameTime = 0f;
    private bool gameOver = false;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("GameManager could not find a PlayerController.");
            return;
        }

        PlayerController controller = player as PlayerController;
        controller.OnDeath += HandleGameOver;
        controller.OnScoreChanged += CheckWinCondition;
    }

    private void Update()
    {
        if (gameOver) return;

        gameTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void CheckWinCondition(int score)
    {
        if (score >= 30)
        {
            Debug.Log("You Win!");
            gameOver = true;
            Invoke(nameof(RestartGame), 2f);
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Game Over!");
        gameOver = true;
        Invoke(nameof(RestartGame), 2f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
