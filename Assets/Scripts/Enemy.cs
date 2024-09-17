using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    InputHandler inputHandler;
    public void SetInputHandler(InputHandler _inputHandler)
    {
        inputHandler = _inputHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TestQCommand()
    {
        inputHandler.debug_Q.Execute(gameObject);
    }
}
