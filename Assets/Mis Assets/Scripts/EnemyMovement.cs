using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Configuración de Referencias")]
    public Transform player;
    

    [Header("Parámetros de Movimiento")]
    public float detectionRadius = 10.0f;
    public float speed = 3.0f;
    public Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    
    [Header("Estadísticas de Combate")]
    public int maxHealth = 100;
    protected int currentHealth;
    protected bool alive = true;
    [SerializeField] private int damageSkeleton;
    public float timeAttacks = 0.6f; 
    private float cooldownAttack = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        spriteRenderer =GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRadius && alive == true)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = new Vector2(direction.x, 0);
            anim.SetBool("Walk", true);
            if (direction.x > 0)
            {
                spriteRenderer.flipX = false; 
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = true; 
            }
        }
        else
        {
            movement = Vector2.zero;
            anim.SetBool("Walk", false);
        }
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    
  
    protected virtual void OnDeath() { }
    public void Die()
    {
        Destroy(gameObject);
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && cooldownAttack <= 0 && alive==true)
        {

           Movement scriptJugador = collision.gameObject.GetComponent<Movement>();
            if (scriptJugador!=null)
            {
                anim.SetTrigger("Attack");
                scriptJugador.TakeDamage(damageSkeleton);
                cooldownAttack = timeAttacks;
            }
            
        }
    }
   

}
