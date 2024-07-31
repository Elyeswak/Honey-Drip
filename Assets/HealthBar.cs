using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider hslider;
    public float maxHealth = 200f;
    public float health;

    void Start()
    {
        
        GameManager.Player.SetHealth(150f);
        health = GameManager.Player.GetHealth();

    }

    // Update is called once per frame
    void Update()
    {
        health = GameManager.Player.GetHealth();

        if( hslider.value != health )
        {
            hslider.value = health;
        }


    }
}
