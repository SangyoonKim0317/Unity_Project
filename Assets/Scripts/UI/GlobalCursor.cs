using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GlobalCursor : MonoBehaviour
{
    public static GlobalCursor instance;

    [Header("--- UI References ---")]
    public Image cursorImage;

    [Header("--- Cursor Sprites ---")]
    public Sprite defaultCursorSprite;
    public Sprite clickCursorSprite;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        SetDefaultCursor();
    }

    private void Update()
    {
        cursorImage.transform.position = Input.mousePosition;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetDefaultCursor(); // 어떤 씬이든 이동이 끝나면 커서를 무조건 기본 모양으로 리셋합니다!
    }

    public void SetDefaultCursor()
    {
        cursorImage.sprite = defaultCursorSprite;
    }

    public void SetClickCursor()
    {
        cursorImage.sprite = clickCursorSprite;
    }
}