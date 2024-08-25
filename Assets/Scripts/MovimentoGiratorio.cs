using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MovimentoGiratorio : MonoBehaviour
{
    public float radius = 5f; // Raio do movimento circular
    public float angularSpeed = 1f; // Velocidade angular (em radianos por segundo)

    private float angle = 0f;

    public float cooldown = 0;

    public bool troca = true;

    SpawnManager SpawnManager_;

    void Start()
    {
        SpawnManager_ = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (SpawnManager_.level >= 11)
        {
            Sorteio();
            MovimentoAcelerado();
        }
        else if (SpawnManager_.level == 4 ||SpawnManager_.level == 5 || SpawnManager_.level == 7 )
        {
            MovimentoSimplesAntiHorario();
        }
        else if (SpawnManager_.level == 8 ||SpawnManager_.level == 9)
        {
            Sorteio();
            MovimentoSimplesAlternado();
        }
        else
        {
            MovimentoSimplesHorario();
        }
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

    void MovimentoSimplesHorario()
    {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = 1f;
        angle = direction * angularSpeed * radius;

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }

    void MovimentoSimplesAntiHorario()
    {
        // Calcula o ângulo baseado no tempo e na velocidade angular
        float direction = -1f;
        angle = direction * angularSpeed * radius;

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }

    void MovimentoSimplesAlternado()
    {
        float direction = troca ? -1f : 1f;
        angle = direction * angularSpeed * radius;

        // Cria um novo vetor para representar a rotação no eixo Z (ou qualquer outro eixo)
        Vector3 rotationAxis = new Vector3(0, 0, 1 * direction); // Eixo Z como exemplo

        // Aplica a rotação ao redor do eixo definido
        transform.Rotate(rotationAxis, angle * direction);
    }

    void Sorteio()
    {
        cooldown -= Time.deltaTime;

        if (cooldown <= 0)
        {
            troca = !troca;
            cooldown = Random.Range(10,15);
        }
    }

}
