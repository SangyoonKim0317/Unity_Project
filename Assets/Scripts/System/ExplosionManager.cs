using UnityEngine;

// 게임오버 시 연출 담당
// 모든 원소를 사방으로 튕겨내는 효과
public class ExplosionManager : MonoBehaviour
{
    public float explosionForce;   // 힘 크기
    public float explosionRadius;  // 영향 범위
    public Transform explosionCenter; // 중심 위치

    // 모든 원소 폭발 처리
    public void ExplodeAllElements()
    {
        // TODO: 힘 적용
    }
}