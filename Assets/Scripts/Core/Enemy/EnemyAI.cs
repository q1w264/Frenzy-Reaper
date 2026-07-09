using Pathfinding;
using UnityEngine;

namespace Core.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private AIPath _aiPath;
        public Transform player;
        public float detectionRange = 5f; // 检测范围
        public float attackRange = 1.5f;
        
        [SerializeField] private float decisionInterval = 0.15f; // 逻辑缓冲时间
        private float _nextDecisionTime;
        
        void Start()
        {
            _aiPath = GetComponent<AIPath>();
        }

        void Update()
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
                else if (currentDistance <= attackRange)
                {
                    //TODO Attack logic
                    _aiPath.isStopped = true;
                }
                else
                {
                    _aiPath.isStopped = true;
                }
            }
        }
        
        // void OnDrawGizmos()
        // {
        //     if (Camera.current != null && Camera.current.name == "SceneCamera" && !isShowGizmos)
        //     {
        //         return; // 如果当前是 Scene 窗口，直接跳过，什么都不画！
        //     }
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(transform.position, detectionRange);
        // }
    }
}