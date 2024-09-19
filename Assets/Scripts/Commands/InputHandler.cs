using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public List<Object> objectsInScene;
    [SerializeField] private StateMachine stateMachine;
    private void Update()
    {
        Command pressedButton = HandleInput();

        if (pressedButton is MoveUnitCommand)
        {
            HandleMoveCommand(pressedButton);
        }

        //Testing (cannot undo this in game)
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Object _obj in objectsInScene)
            {
                if(_obj.objectType == ObjectType.objectTypes["Crate"])
                {
                    stateMachine.SetType(ObjectType.objectTypes["StartChar"] , _obj);
                }
            }
        }
        //Testing end
    }
    private void HandleMoveCommand(Command _pressedButton)
    {
        TryInitiateMovement(_pressedButton);
        FillEmptyMoves();
    }

    private void TryInitiateMovement(Command _pressedButton)
    {
        bool canExecute = false;

        foreach(Object _player in objectsInScene) 
        {
            if(_player.objectProperties.Contains(ObjectProperty.objectProperties["You"]))
            {
                if(_pressedButton is MoveUnitCommand moveCommand)
                {
                    Command newCommand = new MoveUnitCommand(moveCommand.direction, false);
                    if (!canExecute) canExecute = ActivateCommand(newCommand, _player);
                    else ActivateCommand(newCommand, _player);
                } 
            }
        }

        if(!canExecute)
        {
            foreach(Object obj in objectsInScene)
            {
                obj.movedThisTurn = true;
            }
            Debug.Log("No move happened");
        }
        
    }
    public bool ActivateCommand(Command _command, Object _objToMove)
    {         
        //Run the command if its executable
        if(_command.Execute(_objToMove, false)) 
        {
            //Push the command to the object's stack
            if(_command is MoveUnitCommand moveCommand)
            {
                if(!moveCommand.justCheck) 
                {
                    _objToMove.usedMoveCommands.Push(_command as MoveUnitCommand);
                }
                _objToMove.movedThisTurn = true;
                return true;
            }
        }   
        return false;
    }

    private Command HandleInput()
    {
        
        if(Input.GetKeyDown(KeyCode.UpArrow)) return new MoveUnitCommand(new Vector2(0, 1), false);
        if(Input.GetKeyDown(KeyCode.DownArrow)) return new MoveUnitCommand(new Vector2(0, -1), false);
        if(Input.GetKeyDown(KeyCode.LeftArrow)) return new MoveUnitCommand(new Vector2(-1, 0), false);
        if(Input.GetKeyDown(KeyCode.RightArrow)) return new MoveUnitCommand(new Vector2(1, 0), false);
        if(Input.GetKeyDown(KeyCode.Z)) return new UndoCommand(objectsInScene);
    
        return null;
    }
    public void FillEmptyMoves()
    {
        foreach(Object _obj in objectsInScene)
        {
            if(!_obj.movedThisTurn) _obj.FillEmptyMove();
            _obj.movedThisTurn = false;
        }
    }
}
