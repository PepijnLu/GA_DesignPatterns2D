using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Stop,
    Push,
    You,
    Null
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
    public override bool OnNotify(string _myEvent, Vector2 _newPosition, Vector2 _direction, Object _obj, bool _justCheck)
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
                    return HandleCollision(_direction, _obj);
                }
            break;
        }

        return true;
    }

    bool HandleCollision(Vector2 _direction, Object _obj)
    {
        if(type == ObjectType.Push) 
        {
            if (GetPushed(_direction, false)) return true;
            else return false;
        }
        else if (type == ObjectType.Stop)
        {
            return false;
        }
        else if (type == ObjectType.You)
        {
            //Check if the player object can move
            if (GetPushed(_direction, true)) return true;
            return false;
        }
        return true;
    }

    private bool GetPushed(Vector2 _direction, bool justCheck)
    {
        MoveUnitCommand newMoveCommand = new MoveUnitCommand(_direction, false, justCheck);
        if(inputHandler.ActivateCommand(newMoveCommand, this))
        {
            movedThisTurn = true;
            return true;
        }
        return false;
    }

    public void FillEmptyMove()
    {
        MoveUnitCommand newMoveCommand = new MoveUnitCommand(new Vector2(0, 0), false, false);
        usedMoveCommands.Push(newMoveCommand);
        Debug.Log($"Stack Add {name}: [{newMoveCommand.direction.x} , {newMoveCommand.direction.y}]");
    }
}
