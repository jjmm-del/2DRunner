using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{   
    [Header("생성 설정")]
    [Tooltip("생성할 장애물의 프리팹")]
    [SerializeField]private GameObject _obstaclePrefab;
    [Tooltip("장애물이 생성되는 최소 간격(초)")]
    [SerializeField]private float _minSpawnTime = 1.5f;
    [Tooltip("장애물이 생성되는 최대 간격(초)")]
    [SerializeField]private float _maxSpawnTime = 3f;

    private float _timeUntilNextSpawn;
    private float _timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetNextSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            return;
        }
        _timer += Time.deltaTime;

        if (_timer >= _timeUntilNextSpawn)
        {
            SpawnObstacle();
            SetNextSpawnTime();
            _timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        GameObject newObstacle = Instantiate(_obstaclePrefab,transform.position, Quaternion.identity);
        Destroy(newObstacle, 10f);
    }

    private void SetNextSpawnTime()
    {
        _timeUntilNextSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
    }
}
