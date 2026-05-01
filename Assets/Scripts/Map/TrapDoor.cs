using UnityEngine;

// 바닥을 열고 닫는 트랩도어
// 원소를 아래로 떨어뜨리는 역할
public class TrapDoor : MonoBehaviour
{
    public bool isOpen;             // 열림 여부
    public Collider2D doorCollider; // 충돌 판정
    public Transform doorTransform; // 위치/회전

    public float openAngle;         // 열림 각도
    public float closeAngle;        // 닫힘 각도

    // 상태 토글
    public void Toggle()
    {
        // TODO: 열기/닫기 전환
    }

    public void Open()
    {
        // TODO: 열기
    }

    public void Close()
    {
        // TODO: 닫기
    }
}
