using TMPro;
using UnityEngine;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;
    [SerializeField] TextMeshProUGUI enemyScore;
    [SerializeField] TextMeshProUGUI timeLeft;
    [SerializeField] TextMeshProUGUI gameState;

    void OnEnable()
    {
        GameStateManager.timerUpdate += TimerUpdate;
        GameStateManager.stateUpdate += StateUpdate;
    }
    void OnDisable()
    {
        GameStateManager.timerUpdate -= TimerUpdate;
        GameStateManager.stateUpdate -= StateUpdate;
    }
    void TimerUpdate(float time)
    {
        timeLeft.text = Mathf.RoundToInt(time) + " Secs";
    }
    void StateUpdate(GamePhase phase)
    {
        gameState.text = phase.ToString();
    }
    void ScoreUpdate(int _player,int _enemy)
    {
        playerScore.text = _player.ToString();
        enemyScore.text = _enemy.ToString();
    }
}
