using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Stop,
    Push,
    You
}
public class Object : Observer
{

    [SerializeField] private InputHandler inputHandler;
    public Stack<MoveUnitCommand> usedMoveCommands = new();
    public ObjectType type;
    public bool movedThisTurn;

    private int xPos, yPos;

    protected override void Start()
    {
        base.Start();
        inputHandler.objectsInScene.Add(this);
        Debug.Log($"{name} added to objects list");
    }
    public override bool OnNotify(string _myEvent, Vector2 _newPosition, Vector2 _direction, ObjectType _otherType)
    {
        //Debug.Log("Event: event called");
        switch(_myEvent)
        {
            case "ObjectMoved":
                //Debug.Log($"Event: {name} got notified");
                xPos = (int)transform.position.x;
                yPos = (int)transform.position.y;
                if((_newPosition.x == xPos) && (_newPosition.y == yPos)) 
                {
                    return HandleCollision(_direction, _otherType);
                }
            break;
        }

        return true;
    }

    bool HandleCollision(Vector2 _direction, ObjectType _otherType)
    {
        if(type == ObjectType.Push) 
        {
            if (GetPushed(_direction)) return true;
            else return false;
        }
        else if (type == ObjectType.Stop)
        {
            return false;
        }
        else if (type == ObjectType.You)
        {
            return false;
        }
        return true;
    }

    private bool GetPushed(Vector2 _direction)
    {
        MoveUnitCommand newMoveCommand = new MoveUnitCommand(_direction, false);
        if(inputHandler.ActivateCommand(newMoveCommand, this))
        {
            movedThisTurn = true;
            return true;
        }
        return false;
    }

    public void FillEmptyMove()
    {
        MoveUnitCommand newMoveCommand = new MoveUnitCommand(new Vector2(0, 0), false);
        usedMoveCommands.Push(newMoveCommand);
        Debug.Log($"Stack Add {name}: [{newMoveCommand.direction.x} , {newMoveCommand.direction.y}]");
    }
}
