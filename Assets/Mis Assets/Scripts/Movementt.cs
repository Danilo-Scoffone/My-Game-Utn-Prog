using JetBrains.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Movement : MonoBehaviour
{

    [Header("Canvas Store")]

    [SerializeField] private GameObject PanelBuy;
    [SerializeField] private GameObject PanelPurchaseRejected;
    [SerializeField] private GameObject canvasE;
    [SerializeField] private GameObject canvasStore;
    private bool openStore = false;
    [SerializeField] private GameObject storeUI;
    [SerializeField] private GameObject ePromptUI;
    [Header("Components Pj")]
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [Header("Sprites")]

    [SerializeField] private TextMeshProUGUI textDiamonds;
    [SerializeField] private TextMeshProUGUI textPotions;
    [SerializeField] private Image healt;

    [Header("Movement")]
    private float horizontal;
    [SerializeField] private int _speed = 4;
    public int speed { get => _speed; set => _speed = value; }
    [SerializeField] private int jumpForce;
    [SerializeField] private int _damage = 25;
    public int damage { get => _damage; set => _damage = value; }
    [SerializeField] private float _maxHealth = 100f;
    public float maxHealth { get => _maxHealth; set => _maxHealth = value; }
    [SerializeField] private int Diamonds = 0;
    public int _diamonds { get => Diamonds; set => Diamonds = value; }

    [Header("Layers")]
    [SerializeField] private LayerMask floor;
    [SerializeField] private LayerMask enemy;

    private bool jump;
    private bool alive = true;
    public float timeAttacks = 0.3f;
    private float cooldownAttack = 0f;

    protected int _healthPotions;
    public int healtPotions { get => _healthPotions; set => _healthPotions = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {

        //Spawn
        GameObject spawn = GameObject.FindWithTag("SpawnPoint");
        if (spawn != null)
        {
            transform.position = spawn.transform.position;

        }
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        if (GameManager.Instance != null)
        {
            _diamonds = GameManager.Instance.diamonds;
            healtPotions = GameManager.Instance.healthPotions;
            maxHealth = GameManager.Instance.savedHealth;
            _speed = GameManager.Instance.savedSpeed;

        }
        UpdateUI();//Actualizar sprites para cada escena 

    }

         public void SaveStatsToManager()
         {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.diamonds = _diamonds;
                GameManager.Instance.healthPotions = healtPotions;
                GameManager.Instance.savedHealth = maxHealth;
                GameManager.Instance.savedSpeed = speed;
                GameManager.Instance.savedDamage = damage;
            }
         }

    private void UpdateUI()
    {
        if (healt != null)
        {
            healt.fillAmount = _maxHealth / 100f;
        }

        if (textPotions != null)
        {
            textPotions.text=healtPotions.ToString();
        }

        if (textDiamonds != null)
        {
            textDiamonds.text = _diamonds.ToString();
        }

    }
        

    // Update is called once per frame
    void Update()
    {
        if (cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }
        if (alive == true)
        {
            
            horizontal = Input.GetAxis("Horizontal");
            

            //jump
            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded())
                {
                    jump = true;
                }

            }
        }
        
   
        //Attack 
        
        if (Input.GetKeyDown(KeyCode.J) && alive==true && cooldownAttack <= 0)
        {
            anim.SetTrigger("Attack");
            Collider2D enemys= Physics2D.OverlapCircle(transform.position, 1.5f, enemy);
            if (enemys != null)
            {
                ITakeDamage takeDamage=enemys.GetComponentInChildren<ITakeDamage>();
                if (takeDamage!=null)
                {
                    takeDamage.TakeDamage(damage);
                }
                
            }
            cooldownAttack = timeAttacks;
        }
        if (healtPotions>0 && Input.GetKeyDown(KeyCode.K))
        {
            _maxHealth += 30;
            healt.fillAmount = _maxHealth / 100f;
            healtPotions--;
            textPotions.text = healtPotions.ToString();
        }
        if (openStore && Input.GetKeyDown(KeyCode.E))
        {
            canvasStore.SetActive(true);
            
            
        }
    }
    //movement with physics

    private void FixedUpdate()
    {
        if (alive == true)
        {
            rb.linearVelocityX = horizontal * speed;
            if (jump)
            {
                rb.linearVelocityY = jumpForce;
                jump = false;
            }
            
            if (rb.linearVelocityX > 0.1)
            {
                sprite.flipX = false;

            }
            else if (rb.linearVelocityX < -0.1)
            {
                sprite.flipX = true;

            }

            anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));

            if (rb.linearVelocityY > 0.1)
            {
                anim.SetBool("Jump", true);
                anim.SetBool("Falling", false);

            }
            else if (rb.linearVelocityY < -0.1)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jump", false);

            }
            else                             
            {
                anim.SetBool("Jump", false);
                anim.SetBool("Falling", false);
            }
        }
        

        
    }
    bool IsGrounded()
    {
        
        if (Physics2D.Raycast(transform.position, Vector2.down, 1, floor))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            return true;
        }
        return false;
    }

    public void TakeDamage(int Damage)
    {
        _maxHealth -= Damage;
        healt.fillAmount = _maxHealth / 100;
        if (_maxHealth <= 0)
        { 
            Death(1.5f);
        }
    }
    public void Death(float time)
    {
        alive = false;
        rb.linearVelocity = Vector2.zero;
        anim.SetBool("Death", true);
        Invoke("Derrota", time);
    }
    public void Derrota()
    {
        SceneManager.LoadScene("SceneDead");
    }
    


    
   public GameObject GetPanelBuy()
    {
        return PanelBuy;
    }
    public GameObject GetPanelPurchaseRejected()
    {
        return PanelPurchaseRejected;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IChangeScene ChangeScene = collision.gameObject.GetComponent<IChangeScene>();
        if (ChangeScene!= null) { ChangeScene.Teleport();}
        
        ITakeObject TakeObject=collision.gameObject.GetComponent<ITakeObject>();
        if (TakeObject != null) { TakeObject.TakeObject(); }

        

        if (collision.gameObject.CompareTag("CanvasStore") )
        {
            canvasE.SetActive(true);
            openStore=true;
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanvasStore"))
        {
            canvasE.SetActive(false);
            
        }
    }
}
