using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [Header("스크롤 설정")]
    [Tooltip("오브젝트가 왼쪽으로 이동하는 속도")]
    [SerializeField] private float _scrollSpeed = 8f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left *(_scrollSpeed * Time.deltaTime));    
    }
}
