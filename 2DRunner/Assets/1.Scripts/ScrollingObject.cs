using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    [Header("스크롤 설정")]
    [Tooltip("오브젝트가 왼쪽으로 이동하는 속도")]
    [SerializeField] private float _baseScrollSpeed = 8f;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }

        float finalScrollSpeed = _baseScrollSpeed;

        if (GameManager.Instance != null)
        {
            finalScrollSpeed += GameManager.Instance.CurrentBonusSpeed;
        }
        transform.Translate(Vector3.left *(finalScrollSpeed * Time.deltaTime));    
    }
}
