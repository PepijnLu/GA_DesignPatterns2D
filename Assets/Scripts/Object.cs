using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Object : Observer
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private string startingType;
    public Stack<MoveUnitCommand> usedMoveCommands = new();
    public bool movedThisTurn;
    private int xPos, yPos;
    public List<ObjectProperty> objectProperties = new();
    public ObjectType objectType;
    public SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputHandler.objectsInScene.Add(this);
        Debug.Log($"{name} added to objects list");
        InitializeState();
    }

    void InitializeState()
    {
        if(startingType != null)
        {
            stateMachine.SetType(ObjectType.objectTypes[startingType], this);
        }
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
        foreach(ObjectProperty _property in objectProperties)
        {
            if(_property.handlesCollision)
            {
                return _property.HandleCollision(_direction, this);
            }
        }

        return true;
    }

    public bool GetPushed(Vector2 _direction, bool _justCheck)
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
    }
}
