using UnityEngine;
using System.Collections.Generic;
public class MapController : MonoBehaviour
{
    [Header("맵 생성 설정")]
    [Tooltip("생성할 바닥(ground)프리팹")]
    [SerializeField] private GameObject _groundPrefab;
    [Tooltip("처음 만들어 둘 바닥의 개수")]
    [SerializeField] private int _poolSize =5;
    [Tooltip("바닥 하나당 가로의 길이")]
    [SerializeField] private float _groundWidth = 20f;
    [Tooltip("바닥이 이 x좌표보다 왼쪽으로 가면 재배치")]
    [SerializeField] private float _relocateXPosition = -25f;

    private Queue<GameObject> _groundPool;
    private GameObject _lastSpawnedGround;
    private Vector3 _nextSpawnPosition;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        _groundPool = new Queue<GameObject>();
        Vector3 currentSpawnPosition = Vector3.zero;

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject newGround = Instantiate(_groundPrefab, currentSpawnPosition, Quaternion.identity);
            _groundPool.Enqueue(newGround);
            
            // 방금 생성한 바닥을 '마지막 바닥'으로 갱신합니다.
            _lastSpawnedGround = newGround; 
            
            currentSpawnPosition.x += _groundWidth;
        }
    }
    // Update is called once per frame
    void Update()
    {
        GameObject oldestGround = _groundPool.Peek();

        if (oldestGround.transform.position.x <= _relocateXPosition)
        {
            RelocateGround();
        }
    }

    private void SpawnGround()
    {
        GameObject newGround = Instantiate(_groundPrefab, _nextSpawnPosition, Quaternion.identity);
        _groundPool.Enqueue(newGround);
        
        _nextSpawnPosition.x +=_groundWidth;
    }

    private void RelocateGround()
    {
        GameObject oldGround = _groundPool.Dequeue();
        float newXPosition = _lastSpawnedGround.transform.position.x + _groundWidth;
        
        oldGround.transform.position = new Vector3(newXPosition, oldGround.transform.position.y, 0f);

        _groundPool.Enqueue(oldGround);
        _lastSpawnedGround = oldGround;
        
    }
}
