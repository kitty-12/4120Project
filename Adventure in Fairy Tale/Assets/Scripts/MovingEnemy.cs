using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MovingEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Transform[] points;
    public Animator anim;

    public float scanRange = 30.0f;

    public Player data;
    
    public float minDamage;
    public float maxDamage;

    private float health;

    public TextMeshProUGUI LevelText;

    public Slider HealthSlider;
    public float MaxHealth;
    public int Level;

    private bool isAttacking = false;
    private UnityEngine.AI.NavMeshAgent agent;
    private int destPoint = 0;
    private bool isPatroling = true;
    private bool takeD = false;
    private Color oldColor;
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        HealthSlider.maxValue = MaxHealth;
        health = MaxHealth;
        HealthSlider.value = health;
        LevelText.text = "Lv."+Level.ToString();
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position,player.transform.position);
        Vector3 heading = player.transform.position - transform.position;
        if (dist<=20.0f && heading.sqrMagnitude < scanRange * scanRange){
            isPatroling = false;
            takeD = false;
            agent.destination = player.transform.position;
            agent.speed=3.5f;
            if (dist<=5.0f)
            {
                StartCoroutine(Attack());
            }
        }
        if(dist>=30.0f && takeD == false)
        {
            isPatroling = true;
            agent.destination = points[destPoint].position;
            agent.speed=2.5f;

        }
        if (dist>=50.0f && takeD == true)
        {
            isPatroling = true;
            agent.destination = points[destPoint].position;
            agent.speed=2.5f;
        }
        if (!agent.pathPending && agent.remainingDistance < 10.0f && isPatroling==true){
            GotoNextPoint();
        }
    }
    void GotoNextPoint(){
        anim.SetBool("Walk",true);
        if (points.Length==0)
            return;
        agent.destination = points[destPoint].position;
        destPoint = (destPoint+1)%points.Length;
    }

    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking=true;
            anim.SetBool("Attack",true);
            yield return new WaitForSeconds(1.2f);
            float dist = Vector3.Distance(this.transform.position,player.transform.position);
            if (dist<5.0f)
                data.TakeDamage(Random.Range(minDamage,maxDamage));
            isAttacking=false;
            anim.SetBool("Attack",false);
        }
    }

    public void takeOwnDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health<=0)
        {
            health = 0;
            StartCoroutine(Damage());
            anim.SetBool("Fall",true);
            data.defeatEnemy(Level,500,0);
            Destroy(gameObject,1.0f);
        }
        HealthSlider.value = health;
        StartCoroutine(Damage());
        isPatroling = false;
        takeD = true;
        agent.destination = player.transform.position;
    }

    IEnumerator Damage()
    {
        anim.SetBool("Hit",true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Hit",false);
    }

    public void onSelected()
    {
        oldColor = HealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color;
        Color color = new Color(233f/255f, 79f/255f, 55f/255f);
        HealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = color;          
    }
    public void unSelected()
    {
        HealthSlider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = oldColor;
    }
}
