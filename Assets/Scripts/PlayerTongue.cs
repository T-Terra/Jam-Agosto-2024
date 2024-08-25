using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerTongue : MonoBehaviour
{
    public static PlayerTongue Instance { get; private set; }

    public TextMeshProUGUI score;
    public TextMeshProUGUI level_;
    public int points = 0;
    private int streak; //Usar para mecânica de aumentar pontuação conforme acertos consecutivos.
    public float cooldown = 0.5f;
    public bool CanAtk = true;
    public int bombHit;

    //da animação de "ataque"
    private Animator playerAnim;
    SpawnManager SpawnManager_;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        streak = 1;
        SpawnManager_ = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Cooldown();
        score.text = "Score: " + points.ToString();
        level_.text = $"AREA {SpawnManager_.level.ToString()}";

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Donut")){
            points += 10 * streak;
            streak += 1;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Bomb")){
            points -= 20;
            streak = 1;
            bombHit += 1;
            Destroy(collision.gameObject);
        }
    }

   

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) && CanAtk == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
            playerAnim.SetBool("Atk", true);
            Invoke(nameof(Return), 0.3f);
            CanAtk = false;
            cooldown = 0.5f;
        }
        
    }

    private void Return()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
        playerAnim.SetBool("Atk", false);
    }

    void Cooldown()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            CanAtk = true;
        }
    }
}
