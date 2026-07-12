using UnityEngine;
using UnityEngine.Pool;

namespace Core.Bullet
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public int damage  = 10;
        public float lifetime  = 10f;
        
        private Rigidbody2D _rigidbody;
        private float _lifeTimer;
        
        private IObjectPool<Bullet> _pool;
        
        private Vector2 _fireDirection = Vector2.right;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Initialization(IObjectPool<Bullet> pool)
        {
            _pool = pool;
        }

        public void SetDirection(Vector2 direction)
        {
            _fireDirection = direction;
            _rigidbody.linearVelocity = direction.normalized * speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 2. 将角度应用给 Z 轴旋转 (2D游戏只能绕 Z 轴旋转)
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void OnEnable()
        {
            _rigidbody.linearVelocity = _fireDirection.normalized * speed;
            _lifeTimer = lifetime;
        }

        private void Update()
        {
            _lifeTimer -= Time.deltaTime;
            if (_lifeTimer <= 0f)
            {
                _pool.Release(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                var health = other.GetComponent<Health.Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                _pool.Release(this);
            }
            else  if (other.CompareTag("Wall"))
            {
                _pool.Release(this);
            }
        }
    }
}