using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public Image healthBar;
    public float playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playerTakeDamage(float damage) // Use this function to make the player take a certain amount of damage
    {
        playerHealth -= damage;
        healthBar.fillAmount = playerHealth / 100f;
    }
}
