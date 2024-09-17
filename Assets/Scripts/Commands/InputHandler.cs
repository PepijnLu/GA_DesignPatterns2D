using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject player;
    public Command debug_Q = new QCommand();
    public Command debug_W = new WCommand(); 
    public Command debug_E = new ECommand(); 
    public Command debug_R = new RCommand();
    void Update()
    {
        Command pressedButton = HandleInput();
        if(pressedButton != null) {pressedButton.Execute(player);}
    }

    Command HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) return debug_Q;
        if (Input.GetKeyDown(KeyCode.W)) return debug_W;
        if (Input.GetKeyDown(KeyCode.E)) return debug_E;
        if (Input.GetKeyDown(KeyCode.R)) return debug_R;

        return null;
    }
}
