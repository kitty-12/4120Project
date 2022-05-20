using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject Target;
    public float atk;
    private float start_dis;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPos = new Vector3(Target.transform.position.x,Target.transform.position.y+2.0f,Target.transform.position.z);
        this.transform.LookAt(targetPos);
        start_dis = Vector3.Distance(Target.transform.position,this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Target!=null)
        {
            Vector3 targetPos = new Vector3(Target.transform.position.x,Target.transform.position.y+2.0f,Target.transform.position.z);
            this.transform.LookAt(targetPos);
            float dis = Vector3.Distance(Target.transform.position,this.transform.position);
            if (start_dis-dis>=30.0f){
                Destroy(gameObject,1.0f);
            }
            if (dis > 2.0f){
                transform.Translate(Vector3.forward*8.0f*Time.deltaTime);
            } else{
                HitTarget();
            }
        }
        else{
            Destroy(gameObject,1.0f);
        }
    }
    void HitTarget(){
        Destroy(gameObject);
        //Target.transform.GetComponent<EnemyPlant>().takeOwnDamage(atk);
        Component comp = Target.transform.GetComponent<EnemyPlant>();
        if (comp!=null)
        {
            Target.transform.GetComponent<EnemyPlant>().takeOwnDamage(atk);
        }
        else
        {
            Target.transform.GetComponent<MovingEnemy>().takeOwnDamage(atk);
        }
    }
}
