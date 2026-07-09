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
            Length.Percent(currentHealth <= 0 ? 0 : (currentHealth / maxHealth) * 100f);
        
        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            // You can add additional logic here, such as triggering events or animations when health changes.
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
    }
}