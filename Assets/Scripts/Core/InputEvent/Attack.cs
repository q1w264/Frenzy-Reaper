using Core.Bullet;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.InputEvent
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Health.Health))]
    public class Attack : MonoBehaviour
    {
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        public float searchRadius = 10f;
        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private BulletPool bulletPool;

        private readonly Collider2D[] _results = new Collider2D[20];

        private ContactFilter2D _contactFilter;
        
        private Health.Health _health;
        
        private Animator _animator;
        private void Awake()
        {
            _health = GetComponent<Health.Health>();
            _contactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = enemyLayer,
                useTriggers = false
            };
            if (bulletPool == null)
            {
                Debug.LogError("BulletPool is not assigned in the inspector.");
            }
            _animator = GetComponent<Animator>();
        }

        public void OnAttackEvent(InputAction.CallbackContext ctx)
        {
            if (_health.IsDead()) return;
            if (ctx.performed)
            {
                if (_nearestEnemy != null)
                {
                    Vector2 fireDirection = (_nearestEnemy.transform.position - transform.position).normalized;
                    bulletPool.Shoot(transform.position, fireDirection);
                }
                else
                {
                    Vector2 fireDirection = new Vector2(_animator.GetFloat(MoveX), _animator.GetFloat(MoveY));
                    bulletPool.Shoot(transform.position, fireDirection);
                }
            }
        }

        private GameObject GetNearestEnemy()
        {
            int hitCount = Physics2D.OverlapCircle(transform.position, searchRadius, _contactFilter, _results);

            if (hitCount == 0) return null;

            GameObject nearestObj = null;
            float minDistanceSqr = Mathf.Infinity;
            Vector3 currentPos = transform.position;

            // 2. 遍历结果，找出最近的一个
            for (int i = 0; i < hitCount; i++)
            {
                // 使用 sqrMagnitude 比 Vector2.Distance 快得多
                float distSqr = (_results[i].transform.position - currentPos).sqrMagnitude;
                var health = _results[i].GetComponent<Health.Health>();

                if (health == null || health.IsDead()) continue;
                if (!(distSqr < minDistanceSqr)) continue;
                minDistanceSqr = distSqr;
                    
                nearestObj = _results[i].gameObject;
            }

            return nearestObj;
        }

        private GameObject _nearestEnemy;
        
        private void Update()
        {
            _nearestEnemy = GetNearestEnemy();
        }

        private void OnDrawGizmos()
        {
            if (Camera.current != null && Camera.current.name == "SceneCamera")
            {
                return; // 如果当前是 Scene 窗口，直接跳过，什么都不画！
            }
            Gizmos.color = Color.darkOrange;
            if (_nearestEnemy != null)
            {
                Gizmos.DrawLine(transform.position, _nearestEnemy.transform.position);
            }
        }
    }
}