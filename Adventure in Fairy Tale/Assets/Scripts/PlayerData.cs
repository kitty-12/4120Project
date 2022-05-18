using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayerData : MonoBehaviour
{
    [Header("PlayerStatus")]
    public float MaxHealth;
    public float curHealth;

    [Header("UIComponents")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = MaxHealth;
        curHealth = MaxHealth;
        healthSlider.value = curHealth;
        healthText.text = curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
    }

    // Update is called once per frame
    public void TakeDamage(float Damage)
    {
        curHealth-=Damage;
        healthSlider.value = curHealth;
        if (curHealth<=0)
        {
            Application.LoadLevel(2);
        }
        healthText.text = curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
    }
    public void Heal(float Heal)
    {
        curHealth+=Heal;
        if(curHealth>MaxHealth)
            curHealth=MaxHealth;
        healthSlider.value=curHealth;
        healthText.text = curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
    }
}
