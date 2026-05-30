using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UI 표시를 담당하는 클래스
public class UIManager : MonoBehaviour
{
    [Header("Text Settings")]
    public TextMeshProUGUI scoreText;       

    public Image player1NextImage; // 1P 다음 원소
    public Image player2NextImage; // 2P 다음 원소

    // 인게임 진행 중 점수 갱신
    public void UpdateScore(int currentScore)
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    // 다음 원소 표시
    public void UpdateNextElement(ElementType type, int level)
    {
        // TODO: 이미지 변경
    }

    public void ShowGameOverPanel(int finalScore)
    {
    }

    public void HideGameOverPanel()
    {
    }

    public void OnClickRestart()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame(); 
        }
    }
}