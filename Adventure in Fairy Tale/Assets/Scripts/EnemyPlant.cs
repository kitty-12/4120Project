using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyPlant : MonoBehaviour
{
    public GameObject player;
    public Animator anim;

    public Player data;
    
    public float minDamage;
    public float maxDamage;

    private float health;

    public TextMeshProUGUI LevelText;

    public Slider HealthSlider;
    public float MaxHealth;
    public int Level;
    private Color oldColor;

    private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        oldColor = HealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color;
        HealthSlider.maxValue = MaxHealth;
        health = MaxHealth;
        HealthSlider.value = health;
        LevelText.text = "Lv."+Level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position,player.transform.position);
        if (dist<=15){
            //Vector3 player_pos = player.transform.position;
            //player_pos.Set(player_pos.x,-player_pos.y,player_pos.z);
            transform.LookAt(player.transform);
            if (dist<=5)
            {
                //Attack
                //anim.SetBool("Spell",true);
                if(!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                anim.SetBool("Spell",false);
                isAttacking=false;
            }
        }
    }
    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking=true;
            //anim.SetBool("Spell",true);
            anim.SetBool("MeleeAttack",true);
            yield return new WaitForSeconds(1.2f);
            data.TakeDamage(Random.Range(minDamage,maxDamage));
            isAttacking=false;
            //anim.SetBool("Spell",false);
            anim.SetBool("MeleeAttack",false);
        }
    }
    public void takeOwnDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health<=0)
        {
            health = 0;
            anim.SetBool("Death",true);
            data.defeatEnemy(Level,100,2);
            Destroy(gameObject,0.5f);
        }
        HealthSlider.value = health;
    }
    public void onSelected()
    {
        Color color = new Color(254f/255f, 255f/255f, 11f/255f);
        Debug.Log("change color");
        HealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;          
    }
    public void unSelected()
    {
        HealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = oldColor;
    }
}
