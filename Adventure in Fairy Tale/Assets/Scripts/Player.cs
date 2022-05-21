using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    
    [Header("UIComponents")]
    public Slider healthSlider;
    public Slider expSlider;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI PotionText;
    public TextMeshProUGUI MoneyText;

    [Header("Others")]
    public Animator anim;

    public GameObject FireSpellPrefab;
    public GameObject IceSpellPrefab;

    private GameObject transport;

    private GameObject selectedEnemy;

    private bool NormalAttacking = false;
    private bool SpecialAttacking = false;
    private bool selected = false;

    private float MaxHealth;
    private float curHealth;
    private int exp;
    private int next_exp;
    private int Level;
    private int MaxLevel = 10;
    private int HerbNum;
    private int money;


    // Start is called before the first frame update
    void Start()
    {
        getData();
        healthSlider.maxValue = MaxHealth;
        healthSlider.value = curHealth;
        healthText.text = "Lv."+Level.ToString()+"    "+curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
        PotionText.text = HerbNum.ToString();
        MoneyText.text = "$"+money.ToString();
        expSlider.maxValue = next_exp;
        expSlider.value = exp;
        transport = GameObject.Find("Respawn");
        if (transport!=null){
            if (PlayerData.Instance.BeenThere==false)
                transport.SetActive(false);
            else
                transport.SetActive(true);
        }
        //transport.SetActive(true);
    }

    void Update() 
    {
        if (selectedEnemy == null)
            selected = false;
        if (selected){
            Vector3 targetPos = new Vector3(selectedEnemy.transform.position.x,selectedEnemy.transform.position.y,selectedEnemy.transform.position.z);
            float dis = Vector3.Distance(selectedEnemy.transform.position,this.transform.position);
            if (dis>= 40.0f){
                if (selectedEnemy.GetComponent<EnemyPlant>() == null)
                    selectedEnemy.GetComponent<MovingEnemy>().unSelected();
                else
                    selectedEnemy.GetComponent<EnemyPlant>().unSelected();
                selectedEnemy = null;
                selected = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Heal(20);
            HerbNum--;
            PotionText.text = HerbNum.ToString();

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
        Debug.Log("Taking Damage");
        healthSlider.value = curHealth;
        if (curHealth<=0)
        {
            curHealth = 0;
            StartCoroutine(Death());
            //Application.LoadLevel(2);
        }
        healthText.text = "Lv."+Level.ToString()+"    "+curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
    }
    public void Heal(int Heal)
    {
        curHealth+=Heal;
        if(curHealth>MaxHealth)
            curHealth=MaxHealth;
        healthSlider.value=curHealth;
        healthText.text = "Lv."+Level.ToString()+"    "+curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
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
        money -= Random.Range(100,50*Level);
        if (money<=0)
            money = 0;
        MoneyText.text = "$"+money.ToString();
        curHealth = MaxHealth;
        setData();
        Application.LoadLevel(3);
        yield return new WaitForSeconds(3.5f);
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
                if (selectedEnemy.GetComponent<EnemyPlant>() == null)
                {
                    selectedEnemy.GetComponent<MovingEnemy>().onSelected();
                }
                else
                    selectedEnemy.GetComponent<EnemyPlant>().onSelected();
                selected = true;
            } else if(hit.transform.gameObject.CompareTag("Boss")){
                selectedEnemy = hit.transform.gameObject;
                selected = true;
            }
        }
    }
    public void defeatEnemy(int EnemyLevel,int getMoney, int getHerb){
        if (Level-EnemyLevel>=5)
        {
            exp+=1;
        }
        else if (Level>EnemyLevel)
        {
            double add= EnemyLevel*30.0/(Level-EnemyLevel);
            exp = exp + (int)add;
        }
        else if (Level<=EnemyLevel)
        {
            double add= EnemyLevel*30.0*(EnemyLevel-Level+1);
            exp = exp + (int)add;
        }
        if (exp>=next_exp)
        {
            LevelUp();
            healthText.text = "Lv."+Level.ToString()+"    "+curHealth.ToString("F0")+"/"+MaxHealth.ToString("F0");
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = curHealth;
        }
        expSlider.maxValue = next_exp;
        expSlider.value = exp;

        if (getHerb>0)
        {
            HerbNum += Random.Range(0,getHerb);
            Debug.Log(HerbNum);
            PotionText.text = HerbNum.ToString();
        }
        if (Level>EnemyLevel)
            money+=getMoney/(Level-EnemyLevel);
        else
            money+=getMoney;
        MoneyText.text = "$"+money.ToString();


    }

    void OnTriggerEnter(Collider other){
        if(other.tag=="CabinDoor"){
            setData();
            if (PlayerData.Instance.BeenThere==false)
                PlayerData.Instance.BeenThere = true;
            if (PlayerData.Instance.AllCollected==false)
                Application.LoadLevel(5);
            else
                Application.LoadLevel(6);
        }

        if(other.tag=="TownDoor"){
            setData();
            Application.LoadLevel(2);
        }
        if(other.tag == "Transport"){
            transform.position = new Vector3(393,5,382);
        }
    }
    void setData(){
        PlayerData.Instance.curHealth = curHealth;
        PlayerData.Instance.maxHealth = MaxHealth;
        PlayerData.Instance.Level = Level;
        PlayerData.Instance.exp = exp;
        PlayerData.Instance.next_exp = next_exp;
        PlayerData.Instance.HerbNum = HerbNum;
        PlayerData.Instance.Money = money;
    }
    void getData(){
        curHealth = PlayerData.Instance.curHealth;
        MaxHealth = PlayerData.Instance.maxHealth;
        Level = PlayerData.Instance.Level;
        exp = PlayerData.Instance.exp;
        next_exp = PlayerData.Instance.next_exp;
        HerbNum = PlayerData.Instance.HerbNum;
        money = PlayerData.Instance.Money;
    }
    void LevelUp(){
        if (Level<MaxLevel)
        {
            exp = exp - next_exp;
            next_exp +=100;
            Level++;
            MaxHealth+=20;
            curHealth+=20;
        }
        else
        {
            exp = next_exp;
        }

    }
}
