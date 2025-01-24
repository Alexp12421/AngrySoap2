using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int NumberOfEnemies = 0;

    [SerializeField] private GameObject EnemyType;
    void Start()
    {
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            GameObject newObject = Instantiate(EnemyType, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
