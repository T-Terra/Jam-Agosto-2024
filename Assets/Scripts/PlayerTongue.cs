using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerTongue : MonoBehaviour
{
    public static PlayerTongue Instance { get; private set; }

    public Text score;
    public Text level_;
    public int points = 0;
    private int streak; //Usar para mecânica de aumentar pontuação conforme acertos consecutivos.
    public float cooldown = 0.5f;
    public bool CanAtk = true;
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
        streak = 1;
        SpawnManager_ = gameObject.GetComponent<SpawnManager>();
        level_.text = "Level " + SpawnManager_.level.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Cooldown();
        score.text = "Pontuação: " + points.ToString();
        level_.text = "Level " + SpawnManager_.level.ToString();
        if (SpawnManager_.timer == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
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
            Destroy(collision.gameObject);
        }
    }

   

    private void Attack()
    {
        if(Input.GetMouseButtonDown(0) && CanAtk == true)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
            Invoke(nameof(Return), 0.3f);
            CanAtk = false;
            cooldown = 0.5f;
        }
    }

    private void Return()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
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
