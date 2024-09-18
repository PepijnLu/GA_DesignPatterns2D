using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NullCommand : Command
{
    public override bool Execute(Object obj, bool isUndo) {return true;}
}

public class MoveUnitCommand : Command
{
    public Vector2 direction;
    //public bool playerActivated;
    private bool movementAllowed, playerActivated;
    public MoveUnitCommand(Vector2 _direction, bool _playerActivated) : base() 
    {
        direction = new Vector2(_direction.x, _direction.y);

        movementAllowed = true;

        playerActivated = _playerActivated;
    }

    public override bool IsExecutable(Object obj)
    {
        //Check if the movement is allowed
        //...

        //For now return true
        return true;
    }
    public override bool Execute(Object obj, bool isUndo)
    {   
        bool moveIsAllowed = true;
        if(movementAllowed /*&& playerActivated*/)
        {
            executed = true;

            Vector2 currentPosition = obj.transform.position;
            Vector2 newPosition = currentPosition + direction;

            if(!isUndo) 
            {
                if(moveIsAllowed) moveIsAllowed = Notify("ObjectMoved", newPosition, direction, obj.type);
            }

            if(moveIsAllowed) obj.transform.position = newPosition;
        }
        return moveIsAllowed;
    }

    public override void Undo(Object obj)
    {
        direction.x *= -1;
        direction.y *= -1;
        Execute(obj, true);
    }
}
