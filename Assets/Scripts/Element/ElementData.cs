using UnityEngine;

// 원소의 레벨별 데이터 정보를 저장하는 클래스
// 프리팹, 이미지, 질량, 점수 등 관리
[System.Serializable]
public class ElementData
{
    public ElementType elementType;
    public int level;

    public GameObject prefab;
    public Sprite sprite;

    public float mass;
    public float scale;

    public int score;
}