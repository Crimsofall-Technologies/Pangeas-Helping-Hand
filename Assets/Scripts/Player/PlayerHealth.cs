using UnityEngine;

[DefaultExecutionOrder(2)]
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;

    public int currentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Start()
    {
        IsDead = false;
        currentHealth = maxHealth;
        GameManager.ui.pHealth = this;
        GameManager.ui.UpdateUI();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        GameManager.ui.UpdateUI();
    }

    public virtual void Die()
    {
        IsDead = true;
    }
}
