using Pathfinding;
using UnityEngine;

namespace Core.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private AIPath _aiPath;
        public Transform player;

        void Start()
        {
            _aiPath = GetComponent<AIPath>();
        }

        void Update()
        {
            if (player != null)
            {
                // 只要传入目标坐标，多线程寻路、避墙、RVO互相避让全自动完成
                _aiPath.destination = player.position; 
            }
        }
    }
}