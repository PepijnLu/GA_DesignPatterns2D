using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public List<Object> playerObjects, objectsInScene;
    private bool ableToMove;
    public Command debug_Q = new QCommand();
    public Command debug_W = new WCommand(); 
    public Command debug_E = new ECommand(); 
    public Command debug_R = new RCommand();

    private void Start()
    {
        ableToMove = true;
    }
    private void Update()
    {
        Command pressedButton = HandleInput();
        TryInitiateMovement(pressedButton);
        if(Input.GetKeyDown(KeyCode.Z)) InputUndo();
    }

    private void TryInitiateMovement(Command _pressedButton)
    {
        if(_pressedButton != null)
        {
            bool canMoveSomething = false;

            foreach(Object _player in playerObjects) 
            {
                if(!canMoveSomething) 
                {
                    canMoveSomething = CheckIfExecutable(_pressedButton, _player);
                    break;
                }
            }
            
            if(canMoveSomething) 
            {
                foreach(Object _player in playerObjects) 
                {
                    Command newCommand = _pressedButton;
                    ActivateCommand(newCommand, _player);
                }
            }
        }
    }

    public bool CheckIfExecutable(Command command, Object objToMove)
    {
        //Check if the command is executable
        if(command.IsExecutable(objToMove)) return true;
        else return false;
    }

    public void ActivateCommand(Command command, Object objToMove)
    { 
        //Run the command if its executable
        command.Execute(objToMove);

        //If it's a move command, that means a player moved
        if(command is MoveUnitCommand moveCommand)
        {
            //For every object, add the move command to it's stack
            objToMove.usedMoveCommands.Push(moveCommand);
            Debug.Log($"Stack: [{moveCommand.direction.x} , {moveCommand.direction.y}] added to {objToMove.name}'s stack");
        }  
    }

    private Command HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) return debug_Q;
        if (Input.GetKeyDown(KeyCode.W)) return debug_W;
        if (Input.GetKeyDown(KeyCode.E)) return debug_E;
        if (Input.GetKeyDown(KeyCode.R)) return debug_R;

        if(ableToMove)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)) return new MoveUnitCommand(new Vector2(0, 1), true);
            if(Input.GetKeyDown(KeyCode.DownArrow)) return new MoveUnitCommand(new Vector2(0, -1), true);
            if(Input.GetKeyDown(KeyCode.LeftArrow)) return new MoveUnitCommand(new Vector2(-1, 0), true);
            if(Input.GetKeyDown(KeyCode.RightArrow)) return new MoveUnitCommand(new Vector2(1, 0), true);
        }

        return null;
    }

    private void InputUndo()
    {
        // foreach(Object obj in objectsInScene)
        // {
        //     if(obj.usedMoveCommands.Count > 0) 
        //     {
        //         Debug.Log($"Stack undo: [{obj.usedMoveCommands.Peek().direction.x} , {obj.usedMoveCommands.Peek().direction.y}] undo'd from {obj.name}'s stack");
        //         obj.usedMoveCommands.Pop().Undo(obj);
        //     }
        //     else Debug.Log($"{obj.name} has nothing to undo");
        // }

        // Object object1 = objectsInScene[0];
        // Object object2 = objectsInScene[1];
        // Object object3 = objectsInScene[2];
        // Object object4 = objectsInScene[3];

        // if(object1.usedMoveCommands.Count > 0)object1.usedMoveCommands.Pop().Undo(object1);
        // if(object2.usedMoveCommands.Count > 0)object2.usedMoveCommands.Pop().Undo(object2);
        // if(object3.usedMoveCommands.Count > 0)object3.usedMoveCommands.Pop().Undo(object3);
        // if(object4.usedMoveCommands.Count > 0)object4.usedMoveCommands.Pop().Undo(object4);


        // for(int i = 0; i < objectsInScene.Count; i++)
        // {
        //     if(objectsInScene[i].usedMoveCommands.Count > 0)
        //     { 
        //         Debug.Log("Undo: " + objectsInScene[i].name + " of " + objectsInScene.Count);
        //         objectsInScene[i].usedMoveCommands.Pop().Undo(objectsInScene[i]);
        //     }
        // }
    }
}
