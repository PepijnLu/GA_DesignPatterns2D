using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NullCommand : Command
{
    public override void Execute(Object obj, bool isUndo) {Debug.Log($"{obj.name}: null command");}
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
    public override void Execute(Object obj, bool isUndo)
    {   
        if(movementAllowed /*&& playerActivated*/)
        {
            executed = true;

            Vector2 currentPosition = obj.transform.position;
            Vector2 newPosition = currentPosition + direction;

            if(!isUndo) Notify("ObjectMoved", newPosition, direction);

            obj.transform.position = newPosition;
        }
    }

    public override void Undo(Object obj)
    {
        Debug.Log($"Direction of {obj.name}: {direction.x} , {direction.y}");
        /*if (direction.x > 0)*/ direction.x *= -1;
        /*if (direction.y > 0)*/ direction.y *= -1;
        Debug.Log($"Direction of {obj.name} after *-1: {direction.x} , {direction.y}");
        Execute(obj, true);
    }
}
