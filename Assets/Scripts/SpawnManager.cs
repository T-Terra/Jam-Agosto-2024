using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private GameObject DunotPrefab;
    [SerializeField] private GameObject EmpityPrefab;
    [SerializeField] private int MinRange;
    [SerializeField] private int MaxRange;

    // Start is called before the first frame update
    void Start()
    {
        var RandomSpawn = Random.Range(MinRange, MaxRange); 
        if (RandomSpawn == 0) 
        {
            GameObject newObject = Instantiate(BombPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
        else if (RandomSpawn == 1)
        {
            GameObject newObject = Instantiate(EmpityPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
        else
        {
            GameObject newObject = Instantiate(DunotPrefab, this.transform.position, Quaternion.identity, this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
