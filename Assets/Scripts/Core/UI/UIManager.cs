using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBarUI healthBarUI;
    [SerializeField] private ScoreUI scoreUI;

    public void Bind(PlayerController player)
    {
        player.OnHealthChanged += healthBarUI.UpdateHealth;
        player.OnScoreChanged += scoreUI.UpdateScore;
    }
}
