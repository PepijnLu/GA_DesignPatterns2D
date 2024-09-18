using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [SerializeField] private InputHandler inputHandler;
    // public Enemy enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

//     void InstantiateGameObject(GameObject obj)
//     {
//         Enemy newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
//         newEnemy.SetInputHandler(inputHandler);
//         newEnemy.TestQCommand();
//     }

//     private void InstantiateActor<T>(string prefabName, Vector3 position) where T : class, IInitializable, new()
//     {
//         T instance = new T();
//         if (instance is IPushable pushableInstance) pushableInstance.SetInputHandler(inputHandler);
//     }
}
