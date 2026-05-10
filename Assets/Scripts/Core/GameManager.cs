using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static GameManager Instance;

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (currentState != GameState.Playing)
            return;

        playTime += Time.deltaTime;

        UpdateDifficulty(); // 점점 높은 레벨 물체만 나오게 만들기, 난이도 수정사항
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 게임 시작 처리
    public void StartGame()
    {
        currentState = GameState.Playing;

        score = 0;
        playTime = 0f;

        waterSpawnMinLevel = 0;
        waterSpawnMaxLevel = 1;

        Time.timeScale = 1f;

        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
            uiManager.HideGameOverPanel();  //게임 시작할 때 게임 오버 화면을 숨기는 함수
        }
    }

    // 점수 추가
    public void AddScore(int amount)
    {
        if (currentState != GameState.Playing)
            return;

        score += amount;

        if (uiManager != null)
        {
            uiManager.UpdateScore(score);
        }
    }

    // 난이도 상승 (시간 또는 점수 기준)
    public void UpdateDifficulty()
    {
        // 30초가 지나면 물 스폰 최소 레벨 증가
        if (playTime >= 30f)
        {
            waterSpawnMinLevel = 1;
            waterSpawnMaxLevel = 2;
        }

        // 60초가 지나면 더 어려워짐
        if (playTime >= 60f)
        {
            waterSpawnMinLevel = 2;
            waterSpawnMaxLevel = 3;
        }

        // 90초 이후 최종 난이도
        if (playTime >= 90f)
        {
            waterSpawnMinLevel = 3;
            waterSpawnMaxLevel = 3;
        }
    }

    // 게임 오버 처리
    public void GameOver()
    {
        UnityEngine.Debug.Log("GameOver 함수 실행됨");

        if (currentState == GameState.GameOver)
            return;

        currentState = GameState.GameOver;

        Time.timeScale = 0f;

        if (uiManager != null)
        {
            UnityEngine.Debug.Log("UIManager 연결됨");
            uiManager.ShowGameOverPanel(score);
        }
        else
        {
            UnityEngine.Debug.Log("UIManager 연결 안 됨");
        }
    }

    // 게임 재시작
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // 지금 플레이 중인 씬을 다시 시작
    }

    // 다른 스크립트에서 게임 진행 중인지 확인할 때 사용
    public bool IsPlaying()
    {
        return currentState == GameState.Playing;
    }
}