using UnityEngine;

public class Skeleton : EnemyMovement,ITakeDamage
{
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            alive = false;
            anim.SetBool("Death", true);
            Invoke("Die", 0.6f);
            
        }
    }
}
