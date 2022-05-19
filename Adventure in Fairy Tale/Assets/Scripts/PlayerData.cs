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
    public GameObject IceSpellPrefab;

    public GameObject selectedEnemy;

    private bool NormalAttacking = false;
    private bool SpecialAttacking = false;
    private bool selected = false;

    private int exp;
    private int next_exp;
    private int Level;
    private int MaxLevel = 10;
    private int HerbNum;
    private bool BeenThere = false;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.maxValue = MaxHealth;
        curHealth = MaxHealth;
        healthSlider.value = curHealth;
        Level =  5;
        exp = 0;
        next_exp = 50;
        healthText.text = "Lv."+Level.ToString()+"    "+curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
    }

    void Update() 
    {
        if (selectedEnemy == null)
            selected = false;
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
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            StartCoroutine(SpecialAtk());
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
        Vector3 targetPos = new Vector3(selectedEnemy.transform.position.x,selectedEnemy.transform.position.y,selectedEnemy.transform.position.z);
        this.transform.LookAt(targetPos);
        NormalAttacking = true;
        anim.SetBool("NormalAtk",true);
        yield return new WaitForSeconds(1.0f);
        FireSpell();
        anim.SetBool("NormalAtk",false);
        NormalAttacking = false;
    }

    IEnumerator SpecialAtk(){
        Vector3 targetPos = new Vector3(selectedEnemy.transform.position.x,selectedEnemy.transform.position.y,selectedEnemy.transform.position.z);
        this.transform.LookAt(targetPos);
        SpecialAttacking = true;
        anim.SetBool("IceAtk",true);
        yield return new WaitForSeconds(1.0f);
        IceSpell();
        anim.SetBool("IceAtk",false);
        SpecialAttacking = false;
    }
    IEnumerator Death(){
        anim.SetBool("Death",true);
        yield return new WaitForSeconds(4);
        Application.LoadLevel(3);
        anim.SetBool("Death",false);
    }

    void FireSpell(){
        if (selected){
            Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x,this.transform.position.y+2.0f,this.transform.position.z);
            GameObject fire;
            fire = Instantiate(FireSpellPrefab,SpawnSpellLoc,Quaternion.identity);
            fire.transform.GetComponent<Spell>().Target = selectedEnemy;
        }
    }

    void IceSpell(){
        if (selected){
            Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x,this.transform.position.y+2.0f,this.transform.position.z);
            GameObject ice;
            ice = Instantiate(IceSpellPrefab,SpawnSpellLoc,Quaternion.identity);
            ice.transform.GetComponent<Spell>().Target = selectedEnemy;
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
    public void defeatEnemy(int EnemyLevel,int getMoney, int getHerb){
        exp += EnemyLevel*10/(Level-EnemyLevel);

    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="CabinDoor"){
            Application.LoadLevel(5);
            if (BeenThere == false)
                BeenThere = true;
        }

        if(other.tag=="TownDoor"){
            Application.LoadLevel(2);
        }
    }
}
