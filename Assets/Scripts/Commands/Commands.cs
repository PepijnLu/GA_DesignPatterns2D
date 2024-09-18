using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class NullCommand : Command
{
    public override void Execute(Object obj) {Debug.Log($"{obj.name}: null command");}
}

public class MoveUnitCommand : Command
{
    public Vector2 direction;
    //public bool playerActivated;
    private bool movementAllowed;
    public MoveUnitCommand(Vector2 _direction, bool _playerActivated) : base() 
    {
        direction = new Vector2(_direction.x, _direction.y);

        movementAllowed = true;

        //playerActivated = _playerActivated;
    }

    public override bool IsExecutable(Object obj)
    {
        //Check if the movement is allowed
        //...

        //For now return true
        return true;
    }
    public override void Execute(Object obj)
    {   
        if(movementAllowed /*&& playerActivated*/)
        {
            executed = true;

            Vector2 currentPosition = obj.transform.position;
            Vector2 newPosition = currentPosition + direction;

            Notify("ObjectMoved", newPosition, direction);

            obj.transform.position = newPosition;
        }
    }

    public override void Undo(Object obj)
    {
        Debug.Log($"Direction of {obj.name}: {direction.x} , {direction.y}");
        direction.x *= -1;
        direction.y *= -1;
        Debug.Log($"Direction of {obj.name} after *-1: {direction.x} , {direction.y}");
        Execute(obj);
    }
}

public class QCommand : Command
{
    public override void Execute(Object obj)
    {
        Debug.Log($"{obj.name}: Pressed Q");
    }
}

public class WCommand : Command
{
    public override void Execute(Object obj)
    {
        Debug.Log($"{obj.name}: Pressed W");
    }
}

public class ECommand : Command
{
    public override void Execute(Object obj)
    {
        Debug.Log($"{obj.name}: Pressed E");
    }
}

public class RCommand : Command
{
    public override void Execute(Object obj)
    {
        Debug.Log($"{obj.name}: Pressed R");
    }
}
