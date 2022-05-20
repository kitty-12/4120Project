using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Rotkappchen : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public Animator anim;

    public Player data;

    public GameObject UI;
    
    public Slider HealthSlider;
    public TextMeshProUGUI BossName;
    private float minDamage= 10;
    private float maxDamage= 20;

    private float health;
    private float maxHealth = 500;

    private bool isAttacking = false;

    private bool Dead = false;
    private UnityEngine.AI.NavMeshAgent agent;

    private bool StartAttack;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        health = maxHealth;
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = health;
        UI.SetActive(false);
        StartAttack = false;
        //StartAttack = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (StartAttack == true && !Dead)
        {
            UI.SetActive(true);
            transform.LookAt(player.transform);
            anim.SetBool("Walk",true);
            agent.destination = player.transform.position;
            float dist = Vector3.Distance(this.transform.position,player.transform.position);
            if (dist<=3.0f)
            {
                StartCoroutine(Attack());
            }else{
                agent.isStopped = false;
                anim.SetBool("Walk",true);
            }
        }else if (Dead){
            agent.isStopped = true;
        }
    }

    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking=true;
            anim.SetBool("Walk",false);
            //agent.isStopped = true;
            int state = Random.Range(0,1);
            if (state == 0)
                anim.SetBool("attack1",true);
            else
                anim.SetBool("attack2",true);
            yield return new WaitForSeconds(1.2f);
            float dist = Vector3.Distance(this.transform.position,player.transform.position);
            if (dist<1.0f)
            {
                data.TakeDamage(Random.Range(minDamage,maxDamage));
            }
            isAttacking = false;
            agent.isStopped = false;
            anim.SetBool("Walk",true);
            agent.destination = player.transform.position;
            if (state == 0)
                anim.SetBool("attack1",false);
            else
                anim.SetBool("attack2",false);
        }
    }

    public void takeOwnDamage(float damage)
    {
        health -= damage*10;
        Debug.Log(health);
        if (health<=0)
        {
            health = 0;
            StartCoroutine(Damage());
            anim.SetBool("Dying",true);
            Dead = true;
            StartCoroutine(Dying());
            //data.defeatEnemy(Level,500,0);
            //Destroy(gameObject,2.0f);
        }
        HealthSlider.value = health;
        StartCoroutine(Damage());
    }

    IEnumerator Damage()
    {
        anim.SetBool("Hit1",true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Hit1",false);
    }
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2.0f);
        Application.LoadLevel(1);
    }
}
