using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Obstacle : MonoBehaviour
{
    [Header("장애물 설정")]
    [Tooltip("플레이어와 부딪혔을 때 깎일 체력 수치")]
    [SerializeField] private float _damageAmount = 20f;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = otherCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(_damageAmount);
            }
        }
    }
}
