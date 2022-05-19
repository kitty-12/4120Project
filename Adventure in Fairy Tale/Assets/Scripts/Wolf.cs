using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public GameObject player;
    public Animator anim;

    public PlayerData data;
    
    public float minDamage;
    public float maxDamage;

    private bool isAttacking = false;
    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(this.transform.position,player.transform.position);
        if (dist<=20.0f&&player!=null)
        {
            anim.SetBool("Run",true);
            agent.destination = player.transform.position;
        }
        if (dist<=15.0f&&player!=null)
        {
            StartCoroutine(Attack());
        }
        if (player==null)
        {
            anim.SetBool("Run",false);
        }
    }

    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            isAttacking=true;
            anim.SetBool("Attack",true);
            yield return new WaitForSeconds(2.2f);
            float dist = Vector3.Distance(this.transform.position,player.transform.position);
            if (dist<8.0f)
                data.TakeDamage(Random.Range(minDamage,maxDamage));
            isAttacking=false;
            anim.SetBool("Attack",false);
        }
    }
}
