using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimentoGiratorio : MonoBehaviour
{
     public Transform centerPoint; // Ponto central ao redor do qual o objeto se moverá
    public float radius = 5f; // Raio do movimento circular
    public float angularSpeed = 2f; // Velocidade angular (em radianos por segundo)

    private float angle = 0f;

    public float cooldown = 0;

    public bool troca = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sorteio();
        Movimento();
    }

    void Movimento()
    {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = troca ? -1f : 1f; // Determina a direção com base no estado de isReversed
        angle += direction * angularSpeed * Time.deltaTime;

        // Calcula a nova posição do objeto usando seno e cosseno
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        // Atualiza a posição do objeto, mantendo a profundidade (z) original
        transform.position = new Vector3(centerPoint.position.x + x, centerPoint.position.y + y, transform.position.z);
    
    }

    void Sorteio()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            troca = !troca;
            cooldown = Random.Range(3,6);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tongue"){
            Destroy(this.gameObject);
        }
    }



}
