using UnityEngine;

// 플레이어 입력을 처리하는 클래스
// 키보드 입력 -> Spawner / TrapDoor로 전달
public class PlayerController : MonoBehaviour
{
    private bool isInputEnabled = true; // 입력 가능 여부 변수

    [Header("Object Settings")]
    public Spawner spawner;         // 연결된 스포너
    public TrapDoor trapDoor;       // 트랩 도어

    [Header("Player Settings")]
    [Range(1, 2)]
    public int playerNumber = 1;   // 1P 또는 2P

    [Header("Key Bindings")]
    public KeyCode leftKey;             // 좌 이동
    public KeyCode rightKey;            // 우 이동
    public KeyCode dropKey;             // 드롭 키
    public KeyCode trapDoorKey;         // 트랩도어 키

    float dir = 0f;

    private void OnValidate()
    {
        SetDefaultKeyBindings();
    }

    private void Awake()
    {
        SetDefaultKeyBindings();
    }

    void SetDefaultKeyBindings()
    {
        if (playerNumber == 2)
        {
            leftKey = KeyCode.LeftArrow;
            rightKey = KeyCode.RightArrow;
            dropKey = KeyCode.DownArrow;
            trapDoorKey = KeyCode.UpArrow;
        }
        else
        {
            leftKey = KeyCode.A;
            rightKey = KeyCode.D;
            dropKey = KeyCode.S;
            trapDoorKey = KeyCode.W;
        }
    }

    public void HandleMoveInput() 
    {
        if (spawner == null) return; // 스포너 연결을 깜빡했다면 에러 없이 넘어가기

        dir = 0f; // 변수 초기화

        if (Input.GetKey(leftKey) == true) 
        {
            dir = -1f;
        }
        else if (Input.GetKey(rightKey) == true) 
        {
            dir = 1f;
        }

        spawner.Move(dir);
    }

    public void HandleDropInput() 
    {
        if (spawner == null) return;

        if (Input.GetKeyDown(dropKey) == true)
        {
            spawner.Drop();
        }
    }

    public void HandleTrapDoorInput() 
    {
        if (trapDoor == null) return; // 트랩도어 연결을 깜빡했다면 넘어가기

        if (Input.GetKeyDown(trapDoorKey) == true) 
        {
            trapDoor.Toggle();
        }
    }

    public void DisableInput() 
    {
        isInputEnabled = false;
    }

    void Update() 
    {
        if (isInputEnabled == false)
        {
            return;
        }

        HandleMoveInput();
        HandleDropInput();
        HandleTrapDoorInput();
    }
}