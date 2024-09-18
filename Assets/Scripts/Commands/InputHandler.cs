using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public List<Object> playerObjects, objectsInScene;
    private bool ableToMove;

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
            bool canExecute = false;

            foreach(Object _player in playerObjects) 
            {
                if(!canExecute) 
                {
                    canExecute = CheckIfExecutable(_pressedButton, _player);
                    break;
                }
            }

            if(canExecute) 
            {
                ableToMove = false;
                foreach(Object _player in playerObjects) 
                {
                    if(_pressedButton is MoveUnitCommand moveCommand)
                    {
                        Command newCommand = new MoveUnitCommand(moveCommand.direction, false);
                        ActivateCommand(newCommand, _player);
                    } 
                }

                FillEmptyMoves();
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
        //Push the command to the object's stack
        objToMove.usedMoveCommands.Push(command as MoveUnitCommand);
        
        //Run the command if its executable
        command.Execute(objToMove, false);
    }

    private Command HandleInput()
    {
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
        foreach(Object obj in objectsInScene)
        {
            if(obj.usedMoveCommands.Count > 0) 
            {
                //Debug.Log($"Stack undo: [{obj.usedMoveCommands.Peek().direction.x} , {obj.usedMoveCommands.Peek().direction.y}] undo'd from {obj.name}'s stack");
                obj.usedMoveCommands.Pop().Undo(obj);
            }
            else Debug.Log($"{obj.name} has nothing to undo");
        }
    }
    public void FillEmptyMoves()
    {
        foreach(Object obj in objectsInScene)
        {
            if(!obj.movedThisTurn && !playerObjects.Contains(obj)) obj.FillEmptyMove();
            obj.movedThisTurn = false;
        }
        ableToMove = true;
    }
    
}
