using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float currentHealth;

        [CreateProperty]
        public StyleLength HpMaskWidth =>
            Length.Percent(maxHealth <= 0f ? 0f : Mathf.Clamp((currentHealth / maxHealth) * 100f, 0f, 100f));

        public event Action OnDamaged;
        
        public event Action OnDeath;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if(currentHealth <= 0) return;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDeath?.Invoke();
            }

            OnDamaged?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            TakeDamage((int)damage);
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            // You can add additional logic here, such as triggering events or animations when health changes.
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }

        private void OnDestroy()
        {
            OnDeath = null;
            OnDamaged = null;
        }
    }
}