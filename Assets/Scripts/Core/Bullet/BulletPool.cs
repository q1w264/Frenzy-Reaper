using UnityEngine;
using UnityEngine.Pool;

namespace Core.Bullet
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int initialPoolSize = 10;
        [SerializeField] private int maxPoolSize = 100;
        
        private Vector2 _firePosition;
        private Vector2 _fireDirection;
        
        private IObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true,
                initialPoolSize, maxPoolSize);
        }

        private Bullet CreateBullet()
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.Initialization(_bulletPool);
            return bullet;
        }

        private void OnGetBullet(Bullet bullet)
        {
            bullet.transform.position = _firePosition;
            bullet.SetDirection(_fireDirection);
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        public void Shoot(Vector2 firePosition, Vector2 fireDirection)
        {
            _firePosition = firePosition;
            _fireDirection = fireDirection;
            _bulletPool.Get();
        }
    }
}