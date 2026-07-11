using UnityEngine;

namespace Core.InputEvent
{
    public class Attack : MonoBehaviour
    {
        public float searchRadius = 10f;
        [SerializeField] private LayerMask enemyLayer;

        private readonly Collider2D[] _results = new Collider2D[20];

        private ContactFilter2D _contactFilter;

        private void Awake()
        {
            _contactFilter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = enemyLayer,
                useTriggers = false
            };
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

                if (distSqr < minDistanceSqr)
                {
                    minDistanceSqr = distSqr;
                    nearestObj = _results[i].gameObject;
                }
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