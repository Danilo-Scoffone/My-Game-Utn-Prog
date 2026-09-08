using UnityEngine;

public class Skeleton : EnemyMovement, IdropCoins
{
    public bool alive => base.alive;

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
   
}
