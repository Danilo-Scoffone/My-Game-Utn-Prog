using UnityEngine;

public class Mushroom : EnemyMovement, IdropCoins,ITakeDamage
{
    
    [Header("Prefab de Diamonds para soltar")]
    [SerializeField] private GameObject diamonds;
    private bool hasDropped = false; // Control para evitar soltar múltiples veces
    public void DropCoins()
    {
        if (!alive && !hasDropped)
        {
            hasDropped = true;
            Instantiate(diamonds, transform.position, Quaternion.identity);
        }
    }
    protected override void OnDeath()
    {
        DropCoins();
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            alive = false;
            anim.SetBool("Death", true);
            Invoke("Die", 0.6f);
            OnDeath();
        }
    }
}
