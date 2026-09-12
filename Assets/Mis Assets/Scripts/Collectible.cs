using UnityEngine;

public abstract class Collectible : MonoBehaviour,ITakeObject
{

    protected abstract void OnCollect(Movement player);
    public void TakeObject()
    {
        Movement player= FindFirstObjectByType<Movement>();
        if(player != null)
        {
            OnCollect(player);
            Destroy(gameObject);
        }
    }

    
    
}
