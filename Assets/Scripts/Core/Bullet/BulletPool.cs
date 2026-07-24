using System;
using SO.Event.Bullet;
using UnityEngine;
using UnityEngine.Pool;

namespace Core.Bullet
{
    public class BulletPool : MonoBehaviour
    {
        [Header("Bullet Prefab")]
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int initialPoolSize = 10;
        [SerializeField] private int maxPoolSize = 100;
        
        [Header("Bullet Pool")]
        [SerializeField] private BulletSOEvent shootEvent;
        
        private Vector2 _firePosition;
        private Vector2 _fireDirection;
        
        private IObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, true,
                initialPoolSize, maxPoolSize);
        }

        private void OnEnable()
        {
            shootEvent.OnEvent += OnShootEvent;
        }

        private void OnDisable()
        {
            shootEvent.OnEvent -= OnShootEvent;
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

        private void OnShootEvent(BulletInitInfo info)
        {
            _firePosition = info.position;
            _fireDirection = info.direction;
            _bulletPool.Get();
        }
        
        [Obsolete("This method has been replaced by the SO Event. Please use 'BulletSOEvent' instead.")]
        public void Shoot(Vector2 firePosition, Vector2 fireDirection)
        {
            _firePosition = firePosition;
            _fireDirection = fireDirection;
            _bulletPool.Get();
        }
    }
}