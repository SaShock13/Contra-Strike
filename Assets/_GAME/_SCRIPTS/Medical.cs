using Assets.Scripts;
using UnityEngine;

public class Medical : MonoBehaviour
{
    [SerializeField] int healAmount = 50;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHealable>(out IHealable objToHeal))
        {
            objToHeal.Heal(healAmount);
            Destroy(gameObject);
        }

    }
}
