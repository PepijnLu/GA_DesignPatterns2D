using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject player;
    private bool ableToMove;
    public Command debug_Q = new QCommand();
    public Command debug_W = new WCommand(); 
    public Command debug_E = new ECommand(); 
    public Command debug_R = new RCommand();
    private readonly Stack<MoveUnitCommand> usedMoveCommands = new();

    void Start()
    {
        ableToMove = true;
    }
    void Update()
    {
        Command pressedButton = HandleInput();
        if(pressedButton != null) 
        {   
            pressedButton.Execute(player);
            if(pressedButton is MoveUnitCommand moveCommand) 
            {
                usedMoveCommands.Push(moveCommand);
                Debug.Log("Move added to stack");
            }
        }

        if(Input.GetKeyDown(KeyCode.Z) && usedMoveCommands.Count > 0) {usedMoveCommands.Pop().Undo();}
    }

    Command HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) return debug_Q;
        if (Input.GetKeyDown(KeyCode.W)) return debug_W;
        if (Input.GetKeyDown(KeyCode.E)) return debug_E;
        if (Input.GetKeyDown(KeyCode.R)) return debug_R;

        if(ableToMove)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)) return new MoveUnitCommand(0, 1);
            if(Input.GetKeyDown(KeyCode.DownArrow)) return new MoveUnitCommand(0, -1);
            if(Input.GetKeyDown(KeyCode.LeftArrow)) return new MoveUnitCommand(-1, 0);
            if(Input.GetKeyDown(KeyCode.RightArrow)) return new MoveUnitCommand(1, 0);
        }

        return null;
    }
}
