using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [HideInInspector] public List<Object> objectsInScene;
    private void Start()
    {
        StartCoroutine(ExecuteAfterStart());
    }
    private void  InitializeStatements()
    {
        foreach(Object _obj in objectsInScene)
        {
            new StatementsCheck(_obj.gameObject.transform.position, _obj, objectsInScene);
        }
    }
    private void Update()
    {
        //Get what button is pressed this frame
        Command pressedButton = HandleInput();
        
        //If it's a move command, handle that
        if (pressedButton is MoveUnitCommand)
        {
            HandleMoveCommand(pressedButton);
        }
    }
    private void HandleMoveCommand(Command _pressedButton)
    {
        //Try iniating the move command
        TryInitiateMovement(_pressedButton);

        //For each object that didn't move, add a move command with a direction of [0, 0]
        //This way the undo commands dont get desynced between objects
        FillEmptyMoves();

        //For each object, check if they make a new statement
        //Currently doesn't work, so feel free to ignore
        RecheckStatements();
        
    }

    void RecheckStatements()
    {
        foreach(Object _obj in objectsInScene)
        {
            if (_obj is not Word) _obj.objectProperties.Clear();
        }
        foreach(Object _obj in objectsInScene)
        {
            new StatementsCheck(_obj.gameObject.transform.position, _obj, objectsInScene);
        }
    }

    private void TryInitiateMovement(Command _pressedButton)
    {
        bool canExecute = false;

        foreach(Object _player in objectsInScene) 
        {
            //Checks if commands should move the object or not
            if(_player.objectProperties.Contains(ObjectProperty.objectProperties["You"]))
            {
                if(_pressedButton is MoveUnitCommand moveCommand)
                {
                    //Create the command
                    Command newCommand = new MoveUnitCommand(moveCommand.direction, false);

                    //Check if the movement would actually move something
                    if (!canExecute) canExecute = ActivateCommand(newCommand, _player);
                    else ActivateCommand(newCommand, _player);
                } 
            }
        }

        //If the move wouldn't have moved something, don't add a move command with direction [0,0] to each objects stack
        if(!canExecute)
        {
            foreach(Object _obj in objectsInScene)
            {
                _obj.movedThisTurn = true;
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
            if(!_obj.movedThisTurn) 
            {
                _obj.FillEmptyMove();
            }
            _obj.movedThisTurn = false;
        }
    }

    System.Collections.IEnumerator ExecuteAfterStart()
    {
        yield return null;
        InitializeStatements();
    }
}
