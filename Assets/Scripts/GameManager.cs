using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    public Enemy enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateEnemy();
    }

    void InstantiateEnemy()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        newEnemy.SetInputHandler(inputHandler);
        newEnemy.TestQCommand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
