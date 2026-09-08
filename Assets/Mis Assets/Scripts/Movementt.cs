using System.ComponentModel;
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
    [SerializeField] private TextMeshProUGUI textPotions;
    [SerializeField] private TextMeshProUGUI textDiamonds;
    [SerializeField] private Image healt;

    [Header("Movement")]
    private float horizontal;
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private int damage;
    [SerializeField] private float maxHealt;
    [SerializeField] private int Diamonds = 0;
    [SerializeField] private int healtPotions=0;
    [Header("Layers")]
    [SerializeField] private LayerMask floor;
    [SerializeField] private LayerMask enemy;
    
    private bool jump;
    private bool alive = true;
    public float timeAttacks = 0.3f;
    private float cooldownAttack= 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Spawn
        GameObject spawn = GameObject.FindWithTag("SpawnPoint");
        if(spawn != null)
        {
            transform.position=spawn.transform.position;
          
        }
        anim= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>(); 
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
                EnemyMovement EnemyScript= enemys.GetComponent<EnemyMovement>();
                if (EnemyScript!=null)
                {
                    EnemyScript.TakeDamage(damage);
                }
                
            }
            cooldownAttack = timeAttacks;
        }
        if (healtPotions>0 && Input.GetKeyDown(KeyCode.K))
        {
            maxHealt += 30;
            healt.fillAmount = maxHealt / 100f;
            healtPotions--;
            textPotions.text = healtPotions.ToString();
        }
        if (openStore && Input.GetKeyDown(KeyCode.E))
        {
            canvasStore.SetActive(true);
            Time.timeScale = 0f;
            
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
        maxHealt -= Damage;
        healt.fillAmount = maxHealt / 100;
        if (maxHealt <= 0)
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
    //Diamantes


    public int GetDiamonds()
    {
        return Diamonds;
    }
    public void SetDiamonds(int NewDiamonds)
    {
        Diamonds = NewDiamonds;
    }

    //Speed
    public int GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(int NewSpeed)
    {
        speed = NewSpeed;
    }
    //DAño
    public int Getdamage()
    {
        return damage;
    }
    public void Setdamage(int Newdamage)
    {
        damage = Newdamage;
    }

    //vida
    public float GetmaxHealt()
    {
        return maxHealt;
    }
    public void SetmaxHealt(float NewmaxHealt)
    {
        maxHealt = NewmaxHealt;
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
        if (collision.gameObject.CompareTag("DeadZone"))
        {
            Death(1f);
        }
        if (collision.gameObject.CompareTag("Diamond"))
        {
            Destroy(collision.gameObject);
            Diamonds ++;
            textDiamonds.text = Diamonds.ToString();
            
        }
        if (collision.gameObject.CompareTag("HealtPotion"))
        {
            Destroy(collision.gameObject);
            healtPotions ++;
            textPotions.text=healtPotions.ToString();
            
        }

        if (collision.gameObject.CompareTag("Store"))
        {
            SceneManager.LoadScene("Store");
        }

        if (collision.gameObject.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene("Level 2");
        }

        if (collision.gameObject.CompareTag("PreviusLevel"))
        {
            SceneManager.LoadScene("SceneGame");
        }
        if (collision.gameObject.CompareTag("DoorEnd"))
        {
            SceneManager.LoadScene("SceneVictory");
        }

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
