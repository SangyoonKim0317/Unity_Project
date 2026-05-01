using UnityEngine;

// 맵의 경계 역할을 하는 클래스
// 원소가 특정 영역을 벗어나지 않도록 제한
public class Boundary : MonoBehaviour
{
    public Collider2D boundaryCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: 경계 충돌 처리
    }
}