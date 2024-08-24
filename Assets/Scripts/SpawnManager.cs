using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject DunotPrefab;
    [SerializeField] private GameObject EmpityPrefab;
    private int MinRange = 0;
    private int MaxRange = 11;

    public int level = 1;
    public int CountDunot = 0;
    public int CountBomb = 0;
    public GameObject[] DunotObj;
    public GameObject[] BombObj;

    // Start is called before the first frame update
    void Start()
    {
        SpawnItens();
        InvokeRepeating(nameof(CheckQt), 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        print(CountDunot);
        DestroyItens();

        if (Input.GetMouseButton(0))
        {
            foreach (GameObject obj in DunotObj)
            {
                Destroy(obj);
            }
        }
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
            Invoke(nameof(SpawnItens), 1f);
        }
    }
}
