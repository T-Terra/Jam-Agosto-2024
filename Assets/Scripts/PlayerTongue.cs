using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTongue : MonoBehaviour
{
    public static PlayerTongue Instance { get; private set; }

    public GameObject tongue;
    public Text score;
    public int points = 0;
    public int streak = 0; //Usar para mecânica de aumentar pontuação conforme acertos consecutivos.


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
        score.text = points.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Donut"){
            points += 10;
            streak += 1;
        }
        if (collision.gameObject.tag == "Bomb"){
            points -= 20;
            streak = 0;
        }
    }
}
