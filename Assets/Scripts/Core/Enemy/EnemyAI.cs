using Pathfinding;
using UnityEngine;

namespace Core.Enemy
{
    [RequireComponent(typeof(AnimationHandler))]
    [RequireComponent(typeof(AIPath))]
    public class EnemyAI : MonoBehaviour
    {
        private AIPath _aiPath;
        public Transform player;
        private AnimationHandler _animationHandler;
        
        public float detectionRange = 5f; // 检测范围
        public float attackRange = 1.5f;
        
        [SerializeField] private float decisionInterval = 0.15f; // 逻辑缓冲时间
        [SerializeField] private float attackInterval  = 2f; // 攻击间隔
        private float _nextDecisionTime;
        private float _nextAttackTime;
        private Health.Health _health;

        private void Start()
        {
            if (player == null)
            {
                Debug.LogError("Player not assigned in EnemyAI.");
                return;
            }
            _health = player.GetComponent<Health.Health>();
            _aiPath = GetComponent<AIPath>();
            _animationHandler = GetComponent<AnimationHandler>();
        }

        private void Update()
        {
            if (player != null)
            {
                if (Time.time < _nextDecisionTime)
                {
                    return;
                }
                _nextDecisionTime = Time.time + decisionInterval;
                
                var currentDistance = Vector2.Distance(player.position, transform.position);
                
                if (currentDistance <= detectionRange && currentDistance > attackRange)
                {
                    _aiPath.isStopped = false;
                    // 只要传入目标坐标，多线程寻路、避墙、RVO互相避让全自动完成
                    _aiPath.destination = player.position; 
                }
                else if (currentDistance <= attackRange && Time.time >= _nextAttackTime)
                {
                    _animationHandler.OnAttack();
                    if (_health != null)
                    {
                        _health.TakeDamage(10); // 假设每次攻击造成10点伤害
                    }
                    _nextAttackTime = Time.time + attackInterval;
                    _aiPath.isStopped = true;
                }
                else
                {
                    _aiPath.isStopped = true;
                }
            }
        }
        
        private void OnDrawGizmos()
        {
            if (Camera.current != null && Camera.current.name == "SceneCamera")
            {
                return; // 如果当前是 Scene 窗口，直接跳过，什么都不画！
            }
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}