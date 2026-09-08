using UnityEngine;

public class Mushroom : EnemyMovement, IdropCoins
{
    public bool alive => base.alive;
    [Header("Prefab de Diamonds para soltar")]
    [SerializeField] private GameObject diamonds;
    public void DropCoins()
    {
        if (!alive)
        {
            Instantiate(diamonds, transform.position, Quaternion.identity);
        }
    }
    protected override void OnDeath()
    {
        DropCoins();
    }

}
