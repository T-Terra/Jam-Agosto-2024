using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTongue : MonoBehaviour
{
    public static PlayerTongue Instance { get; private set; }

    public Text score;
    public int points = 0;
    private int streak; //Usar para mecânica de aumentar pontuação conforme acertos consecutivos.


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
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        score.text = "Pontuação: " + points.ToString();
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
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.down, ForceMode2D.Impulse);
            Invoke(nameof(Return), 0.3f);
        }
    }

    private void Return()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up, ForceMode2D.Impulse);
    }
}
