using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private int maxHealth = 100;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
        if (healthBarImage)
        {
            healthBarImage.fillAmount = 1;
        }
    }

    public void GetDamage(int damage)
    {
        _currentHealth -= damage;
        if (healthBarImage)
        {
            healthBarImage.fillAmount = _currentHealth / 100f;
        }

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameManager.Instance.GameWin();
    }
}
