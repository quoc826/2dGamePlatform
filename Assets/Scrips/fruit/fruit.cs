using UnityEngine;

public class fruit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // if (gameManager.Instance != null)
            // {
            //     gameManager.Instance.addFruit(1);
            // }
            // else
            // {
            //     Debug.LogWarning("gameManager.Instance is null!");
            // }

            gameManager.Instance.addFruit(1);
            Destroy(gameObject);
        }
    }
}
