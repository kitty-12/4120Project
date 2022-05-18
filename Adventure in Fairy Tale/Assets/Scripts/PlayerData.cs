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

    public GameObject FireSpellPrefab;

    public GameObject selectedEnemy;

    private bool NormalAttacking = false;
    private bool selected = false;
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
        if (selected){
            Vector3 targetPos = new Vector3(selectedEnemy.transform.position.x,selectedEnemy.transform.position.y,selectedEnemy.transform.position.z);
            float dis = Vector3.Distance(selectedEnemy.transform.position,this.transform.position);
            if (dis>= 30.0f){
                selectedEnemy = null;
                selected = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //StartCoroutine(NormalAtk());
            SelectTarget();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
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
        yield return new WaitForSeconds(1.0f);
        FireSpell();
        anim.SetBool("NormalAtk",false);
        NormalAttacking = false;
    }
    IEnumerator Death(){
        anim.SetBool("Death",true);
        yield return new WaitForSeconds(4);
        Application.LoadLevel(3);
        anim.SetBool("Death",false);
    }

    void FireSpell(){
        if (selected){
            Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
            GameObject fire;
            fire = Instantiate(FireSpellPrefab,SpawnSpellLoc,Quaternion.identity);
            fire.transform.GetComponent<Spell>().Target = selectedEnemy;
        }
    }

    void SelectTarget(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,10000)){
            if(hit.transform.gameObject.CompareTag("Enemy")){
                //Debug.Log("here");
                selectedEnemy = hit.transform.gameObject;
                selected = true;
            }
        }
    }
}
