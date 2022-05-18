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

    [Header("Others")]
    public Animator anim;

    private bool NormalAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = MaxHealth;
        curHealth = MaxHealth;
        healthSlider.value = curHealth;
        healthText.text = curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(NormalAtk());
        }
    }

    // Update is called once per frame
    public void TakeDamage(float Damage)
    {
        curHealth-=Damage;
        healthSlider.value = curHealth;
        if (curHealth<=0)
        {
            curHealth = 0;
            StartCoroutine(Death());
            //Application.LoadLevel(2);
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

    IEnumerator NormalAtk(){
        NormalAttacking = true;
        anim.SetBool("NormalAtk",true);
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("NormalAtk",false);
        NormalAttacking = false;
    }
    IEnumerator Death(){
        anim.SetBool("Death",true);
        yield return new WaitForSeconds(4);
        Application.LoadLevel(2);
        anim.SetBool("Death",false);
    }
}
