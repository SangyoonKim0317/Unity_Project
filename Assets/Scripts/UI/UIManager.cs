using UnityEngine;
using UnityEngine.UI;

// UI 표시를 담당하는 클래스
// 점수, 다음 원소, 게임오버 화면 관리
public class UIManager : MonoBehaviour
{
    public Text scoreText;      // 점수 텍스트
    public Text gameOverText;   // 게임오버 텍스트

    public Image player1NextImage; // 1P 다음 원소
    public Image player2NextImage; // 2P 다음 원소

    // 점수 갱신
    public void UpdateScore(int score)
    {
        // TODO: 텍스트 업데이트
    }

    // 다음 원소 표시
    public void UpdateNextElement(ElementType type, int level)
    {
        // TODO: 이미지 변경
    }

    // 게임오버 UI 표시
    public void ShowGameOver(int finalScore)
    {
        // TODO: UI 활성화
    }

    public void HideGameOver()
    {
        // TODO: UI 숨김
    }
}
