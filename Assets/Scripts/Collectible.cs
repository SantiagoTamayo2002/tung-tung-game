using UnityEngine;

public class Collectible : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.AddCollectible();
        Destroy(gameObject);
    }
}
