using System.Collections.Generic;
using UnityEngine;
public class Object : Observer
{
    [SerializeField] public InputHandler inputHandler;
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private string startingType;
    private int xPos, yPos;
    public Stack<MoveUnitCommand> usedMoveCommands = new();
    public List<ObjectProperty> objectProperties = new();
    public bool movedThisTurn;
    public ObjectType objectType;
    public SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        inputHandler.objectsInScene.Add(this);
        InitializeState();
    }

    void InitializeState()
    {
        //Initialize an object using the startingState you pass through in the inspector
        if(startingType != null)
        {
            ObjectType newState = ObjectType.objectTypes[startingType];
            stateMachine.SetType(newState, this);

            foreach(ObjectProperty _property in newState.properties)
            {
                objectProperties.Add(_property);
            }
        }
    }
    //Event listener for returning bool for possible movement
    public override bool OnNotify(string _myEvent, Vector2 _newPosition, Vector2 _direction, Object _otherObject, bool _justCheck)
    {
        switch(_myEvent)
        {
            case "ObjectMoved":
                xPos = (int)transform.position.x;
                yPos = (int)transform.position.y;
                if((_newPosition.x == xPos) && (_newPosition.y == yPos)) 
                {
                    return HandleCollision(_direction, _otherObject);
                }
                break;
        }

        return true;
    }

    //Event listener for returning the textobject in a statement
    public override TextObject OnNotify(string _myEvent, Vector2 _newPosition, Object _otherObject)
    {
        switch(_myEvent)
        {
            case "StatementCheck":
                xPos = (int)transform.position.x;
                yPos = (int)transform.position.y;
                if((_newPosition.x == xPos) && (_newPosition.y == yPos)) 
                {
                    return HandleStatementCheck(_otherObject);
                }
                break;
        }

        return null;
    }

    //Returns the object property that determines how it should behave in a statement
    public TextObject ReturnTextObject(Object _obj)
    {
        foreach(ObjectProperty _property in _obj.objectProperties)
        {
            if(_property is TextObject textObject) return textObject;
        }
        return null;
    }

    //Handles the logic for determining if the object is part of a statement 
    //Doesn't work yet so feel free to ignore
    private TextObject HandleStatementCheck(Object _otherObject)
    {
        Debug.Log("Handle Statement");

        TextObject thisObjectComponent = ReturnTextObject(this);
        return thisObjectComponent;

        TextObject _otherObjectComponent = ReturnTextObject(_otherObject);
        WordType thisObjectWordType, otherObjectWordType;

        if(thisObjectComponent == null || _otherObjectComponent == null) return null;

        thisObjectWordType = thisObjectComponent.wordType;
        otherObjectWordType = _otherObjectComponent.wordType;
        
        if(thisObjectWordType != WordType.OperatorWord)
        {
            if(otherObjectWordType == WordType.OperatorWord) return null;
            else
            {
                if(thisObjectWordType == WordType.SubjectWord) return null;
                return thisObjectComponent;
            }
        }
        else
        {
            if(otherObjectWordType == WordType.DirectWord) return null;
            return thisObjectComponent;
        }
    }

    //Function that handles collision and returns if it's possible to move
    bool HandleCollision(Vector2 _direction, Object _otherObject)
    {
        foreach(ObjectProperty _property in objectProperties)
        {
            if(_property.handlesCollision)
            {
                return _property.HandleCollision(_direction, this, _otherObject);
            }
        }

        return true;
    }


    //Function that pushes the object if another object goes to its location
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

    //Create a new move command with direction [0, 0] if it didn't move this turn but something else did
    //Makes it so all undo's between objects line up
    public void FillEmptyMove()
    {
        MoveUnitCommand newMoveCommand = new MoveUnitCommand(new Vector2(0, 0), false);
        usedMoveCommands.Push(newMoveCommand);
    }
}
