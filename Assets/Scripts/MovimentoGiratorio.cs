using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MovimentoGiratorio : MonoBehaviour
{
    public float radius = 5f; // Raio do movimento circular
    public float angularSpeed = 1f; // Velocidade angular (em radianos por segundo)

    private float angle = 0f;

    public float cooldown = 0;

    public bool troca = true;

    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        MovimentoSimples();
        /*if (SpawnManager.Instance.level == 1)
        {
            MovimentoSimples();
        }
        else 
        {
            Sorteio();
            MovimentoAcelerado();
        }*/
        
    }

    void MovimentoAcelerado()
    {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = troca ? -1f : 1f; // Determina a direção com base no estado de troca
        angle += direction * angularSpeed * Time.deltaTime; //Operador += dá aceleração

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }

    void MovimentoSimples()
    {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = troca ? -1f : 1f; // Determina a direção com base no estado de troca
        angle = direction * angularSpeed * radius;

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }

    void Sorteio()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            troca = !troca;
            cooldown = Random.Range(6,8);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tongue"){
            Destroy(this.gameObject);
        }
    }
}
