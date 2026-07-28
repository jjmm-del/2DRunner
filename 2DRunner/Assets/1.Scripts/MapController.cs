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
    private Vector3 _nextSpawnPosition;

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        _groundPool = new Queue<GameObject>();
        _nextSpawnPosition = Vector3.zero;

        for (int i = 0; i < _poolSize; i++)
        {
            SpawnGround();
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
        
        oldGround.transform.position = new Vector3(_nextSpawnPosition.x, oldGround.transform.position.y, 0f);

        _groundPool.Enqueue(oldGround);
        _nextSpawnPosition.x += _groundWidth;
    }
}
