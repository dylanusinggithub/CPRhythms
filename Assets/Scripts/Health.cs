using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;

    float health;
    float maxHealth = 100;

    private void Start()
    {
        health = 0;
    }


    public void Update()
    {
        healthText.text = health + "%";
        if (health > maxHealth) health = maxHealth;

        HealthBarFiller();
        ColorChanger();
    }

    void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }

    void HealthBarFiller()
    {
        healthBar.fillAmount = health / maxHealth;
    }

    public void Damage()
    {
        if (health > 0){
            health -= 1;}
    }

    public void Heal()
    {
        if (health < maxHealth){
            health += 1;}
    }
}

// Hit = up
// Miss = down