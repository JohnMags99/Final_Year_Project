using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int damage = 5;
    public int resistance = 5;
    public int luck = 5;
    
    public Text healthText; 
    public Text damageText;
    public Text resistanceText;
    public Text luckText;
    
    private void Start()
    {
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        damageText.text = damage.ToString() + "/100";
        resistanceText.text = resistance.ToString() + "/100";
        luckText.text = luck.ToString() + "/100";
    }

    void Update()
    {
        healthText.text = health.ToString() + "/" + maxHealth.ToString();
        damageText.text = damage.ToString() + "/100";
        resistanceText.text = resistance.ToString() + "/100";
        luckText.text = luck.ToString() + "/100";
        
        if (health <= 0)
        {
            SceneManager.LoadScene("Prototype");
        } else if (maxHealth >= 200)
        {
            maxHealth = 200;
        }
        
        maxStat(damage);
        maxStat(resistance);
        maxStat(luck);
    }

    void maxStat(int stat)
    {
        if (stat >= 100)
        {
            stat = 100;
        }
    }
}
