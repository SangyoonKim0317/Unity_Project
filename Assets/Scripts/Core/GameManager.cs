using UnityEngine;

// 게임 전체 흐름을 관리하는 핵심 매니저
// 점수, 시간, 난이도, 게임 시작/종료를 담당
public class GameManager : MonoBehaviour
{

    public ScoreManager scoreManager;
    public MergeManager mergeManager;
    public GameOverChecker gameOverChecker;
    public UIManager uiManager;

    public GameState currentState; // 현재 게임 상태

    public int score;              // 현재 점수
    public float playTime;         // 플레이 시간

    public int waterSpawnMinLevel; // 물 스폰 최소 레벨
    public int waterSpawnMaxLevel; // 물 스폰 최대 레벨

    // 게임 시작 처리
    public void StartGame()
    {
        // TODO: 게임 시작 초기화
    }

    // 점수 추가
    public void AddScore(int amount)
    {
        // TODO: 점수 증가 처리
    }

    // 난이도 상승 (시간 또는 점수 기준)
    public void UpdateDifficulty()
    {
        // TODO: 스폰 레벨 증가 로직
    }

    // 게임 오버 처리
    public void GameOver()
    {
        // TODO: 게임 종료 로직
    }

    // 게임 재시작
    public void RestartGame()
    {
        // TODO: 상태 초기화 및 재시작
    }
}