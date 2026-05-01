using System.Security.Cryptography;
using UnityEngine;

// 플레이어가 조작하는 원소 생성기
// 좌우 이동 및 드롭 기능 담당
public class Spawner : MonoBehaviour
{
    public ElementType spawnType;   // 생성할 원소 타입 (물/불)
    public Transform spawnPoint;    // 생성 위치

    public float moveSpeed;         // 이동 속도
    public float minX;              // 이동 최소 범위
    public float maxX;              // 이동 최대 범위

    public float dropCooldown;      // 드롭 쿨타임
    public bool canDrop;            // 드롭 가능 여부

    public int nextLevel;           // 다음 생성될 원소 레벨

    // 좌우 이동 처리
    public void Move(float direction)
    {
        // TODO: 위치 이동
    }

    // 원소 드롭
    public void Drop()
    {
        // TODO: 원소 생성
    }

    // 다음 원소 설정
    public void SetNextElement()
    {
        // TODO: 다음 레벨 결정
    }

    // 랜덤 레벨 반환
    public int GetRandomLevel()
    {
        return 0;
    }
}
