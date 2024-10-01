using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerTongue : MonoBehaviour
{
    public static PlayerTongue Instance { get; private set; }

    public TextMeshProUGUI score;
    public TextMeshProUGUI score_gameover;
    public TextMeshProUGUI high_score;
    public TextMeshProUGUI level_;
    public int points = 0;
    private int streak; //Usar para mecânica de aumentar pontuação conforme acertos consecutivos.
    public float cooldown = 0.5f;
    public bool CanAtk = true;
    public float bombHit;
    public GameObject[] Fails;

    //da animação de "ataque"
    public AudioSource AudioDonut;
    public AudioClip ClipDonut;

    public AudioSource AudioBomb;
    public AudioClip ClipBomb;
    private Animator playerAnim;
    SpawnManager SpawnManager_;
    private bool bombCollision;
    private bool donutCollision;

    private int SavedHighScore_;

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

    
    void Start()
    {
        SavedHighScore_ = GetHighScore();
        playerAnim = this.gameObject.GetComponent<Animator>();
        streak = 1;
        SpawnManager_ = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    
    void Update()
    {
        Attack();
        Cooldown();
        Chances();
        score.text = points.ToString();
        score_gameover.text = points.ToString();
        SaveHighScore();
        high_score.text = GetHighScore().ToString();
        level_.text = $"AREA {SpawnManager_.level.ToString()}";
        if (playerAnim.GetBool("gotDonut") || playerAnim.GetBool("gotBomb"))
        {
            StartCoroutine(ResetCollisionAnimations());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Donut") && !donutCollision)
        {
            donutCollision = true;
            points += 10 * streak / 2;
            streak += 1;
            AudioDonut.PlayOneShot(ClipDonut);
            playerAnim.SetBool("gotDonut", true);
            playerAnim.SetBool("gotBomb", false);
            Destroy(collision.gameObject);
            //print(streak);
            donutCollision = false;
        }
        else if (collision.gameObject.CompareTag("Bomb") && !bombCollision)
        {
            bombCollision = true;
            points -= 20;
            streak = 1;
            bombHit += 0.5f;
            AudioBomb.PlayOneShot(ClipBomb);
            Destroy(collision.gameObject);
            playerAnim.SetBool("gotBomb", true);
            playerAnim.SetBool("gotDonut", false);
            print(bombHit);
            bombCollision = false;
        }
    }

    private void Attack()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && CanAtk == true)
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

    private IEnumerator ResetCollisionAnimations()
    {
        yield return new WaitForSeconds(0.2f);
        playerAnim.SetBool("gotDonut", false);
        playerAnim.SetBool("gotBomb", false);
    }

    void Cooldown()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            CanAtk = true;
        }
    }

    private void SaveHighScore() {
        if(points > SavedHighScore_) {
            PlayerPrefs.SetInt("highScore", points);
            PlayerPrefs.Save();
        }
    }

    private int GetHighScore() {
        var savedHighScore = PlayerPrefs.GetInt("highScore");
        return savedHighScore;
    }
    private void Chances()
    {
        if (bombHit > 0)
        {
            var bhi = System.Convert.ToInt32(bombHit);
            Fails[bhi-1].SetActive(true);
        } 
    }

}
