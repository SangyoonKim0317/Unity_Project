using System.Security.Permissions;
using UnityEngine;

// 플레이어 입력을 처리하는 클래스
// 키보드 입력 -> Spawner / TrapDoor로 전달
public class PlayerContoller : MonoBehaviour
{
    public Spawner spawner;         // 연결된 스포너
    public TrapDoor trapDoor;       // 트랩 도어

    public KeyCode leftKey;             // 좌 이동
    public KeyCode rightKey;            // 우 이동
    public KeyCode dropKey;             // 드롭 키
    public KeyCode trapDoorKey;         // 트랩도어 키

    // 이동 입력 처리
    public void HandleMoveInput() 
    {
     // TODO: 좌우 이동 입력
    }

    // 드롭 입력 처리
    public void HandleDropInput() 
    {
    // TODO: 드롭 실행
    }

    // 트랩도어 입력 처리
    public void HandleTrapDoorInput() 
    {
    // TODO: 트랩도어 토글    
    }

    // 입력 비활성화 ( 게임오버 시 )
    public void DisableInput() 
    {
    // TODO: 입력 차단
    }
}
