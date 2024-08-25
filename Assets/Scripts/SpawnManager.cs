using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject DunotPrefab;
    [SerializeField] private GameObject EmpityPrefab;
    private int MinRange = 0;
    private int MaxRange = 11;
    public float timer = 30f;

    public int level;
    public int CountDunot = 0;
    public int CountBomb = 0;
    public float DirectionRotateItens;
    private GameObject[] DunotObj;
    private GameObject[] BombObj;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnItens();
        InvokeRepeating(nameof(CheckQt), 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyItens();
        timer -= 1 * Time.deltaTime;
        Rotation_(DirectionRotateItens);
    }

    public void SpawnItens()
    {
        var RandomSpawn = Random.Range(MinRange, MaxRange);
        if (RandomSpawn <= 3)
        {
            GameObject newObject = Instantiate(BombPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
        else
        {
            GameObject newObject = Instantiate(DunotPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
    }

    public void DestroyItens()
    {
        if (CountDunot == 0)
        {
            var Bomb = GameObject.FindGameObjectsWithTag("Bomb");
            for (int i = 0; i < Bomb.Length; i++)
            {
                Destroy(Bomb[i]);
            }
        }

    }

    public void CheckQt()
    {
        DunotObj = GameObject.FindGameObjectsWithTag("Donut");
        BombObj = GameObject.FindGameObjectsWithTag("Bomb");
        CountDunot = DunotObj.Length;
        CountBomb = BombObj.Length;

        if (CountDunot == 0 && CountBomb == 0)
        {
            level++;
            Invoke(nameof(SpawnItens), 1f);
            timer = 30f;
        }
    }

    public void Rotation_( float Direction )
    {
        this.transform.Rotate(new Vector3(0, 0, Direction), Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(GameObject.FindGameObjectWithTag("Donut"));
        }
    }
}
