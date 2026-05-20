using UnityEngine;

// 점수를 관리하는 클래스
public class ScoreManager : MonoBehaviour
{
    public int currentScore;

    // 점수 추가
    public void AddScore(int amount)
    {
        // TODO: 점수 추가
        currentScore += amount;
        GameManager.Instance.AddScore(amount);
    }

    // 점수 초기화
    public void ResetScore()
    {
        // TODO: 점수 초기화
        currentScore = 0;
    }

    public int GetScore()
    {
        return currentScore;
    }
}