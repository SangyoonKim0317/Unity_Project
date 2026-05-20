using UnityEngine;

public class Element : MonoBehaviour
{
    public ElementType elementType; // 원소 타입
    public int level;               // 원소 레벨

    public bool isGameOverTarget;   // 게임오버 감시 대상 여부

    public Rigidbody2D rb;          // 물리 이동 담당
    
    // CircleCollider2D에서 Collider2D로 변경
    // 원소가 원형이 아닌 경우도 있을 수 있으니 Collider2D로 범용적으로 사용하기 위해 변경함
    public Collider2D col;          

    public MergeManager mergeManager; // 충돌 후 머지 처리를 담당하는 매니저

    private bool isProcessingCollision = false; // 충돌 중복 처리 방지용

    private void Awake()
    {
        Debug.LogWarning("Element Awake 실행됨: " + gameObject.name);
        
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (col == null)
        {
            col = GetComponent<Collider2D>();
        }

        if (mergeManager == null)
        {
            mergeManager = FindFirstObjectByType<MergeManager>();
        }
    }

    public void Init(ElementType type, int level)
    {
        this.elementType = type;
        this.level = level;

        // MergeManager에 입력한 ElementData에 이미 원하는 Scale, Mass, Radius가 다 들어있기 때문에 강제 Scale, Mass, Radius 덮어쓰기 코드 삭제

        isGameOverTarget = false;
        isProcessingCollision = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Element other = collision.gameObject.GetComponent<Element>();

        if (other == null)
        {
            return;
        }

        if (isProcessingCollision || other.isProcessingCollision)
        {
            return;
        }

        if (GetInstanceID() > other.GetInstanceID())
        {
            return;
        }
/*
        if (mergeManager != null)
        {
            isProcessingCollision = true;
            other.isProcessingCollision = true;

            mergeManager.CheckMerge(this, other);
        }
 */
        if (mergeManager != null) {
            isProcessingCollision = true;
            other.isProcessingCollision = true;

            bool isMerged = mergeManager.CheckMerge(this, other);

            if (isMerged == false) {
                isProcessingCollision = false;
                other.isProcessingCollision = false;
            }
        }
        
        else {
            Debug.LogWarning("MergeManager를 찾을 수 없습니다.");
        }
    }
}