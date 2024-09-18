using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ObjectType
{
    Flag,
}

public enum ObjectState
{
    Stop,
    Push,
    You,
    Null,
}
public class Object : Observer
{

    [SerializeField] private InputHandler inputHandler;
    public Stack<MoveUnitCommand> usedMoveCommands = new();
    public ObjectState state;
    public bool movedThisTurn;
    private int xPos, yPos;

    protected override void Start()
    {
        base.Start();
        inputHandler.objectsInScene.Add(this);
        Debug.Log($"{name} added to objects list");
    }
    public override bool OnNotify(string _myEvent, Vector2 _newPosition, Vector2 _direction, bool _justCheck)
    {
        switch(_myEvent)
        {
            case "ObjectMoved":
                xPos = (int)transform.position.x;
                yPos = (int)transform.position.y;
                if((_newPosition.x == xPos) && (_newPosition.y == yPos)) 
                {
                    return HandleCollision(_direction);
                }
            break;
        }

        return true;
    }

    bool HandleCollision(Vector2 _direction)
    {
        if(state == ObjectState.Push) 
        {
            if (GetPushed(_direction, false)) return true;
            else return false;
        }
        else if (state == ObjectState.Stop)
        {
            return false;
        }
        else if (state == ObjectState.You)
        {
            //Check if the player object can move
            if (GetPushed(_direction, true)) return true;
            return false;
        }
        return true;
    }

    private bool GetPushed(Vector2 _direction, bool _justCheck)
    {
        MoveUnitCommand newMoveCommand = new MoveUnitCommand(_direction, _justCheck);
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
