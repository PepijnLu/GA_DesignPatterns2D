using System.Collections.Generic;
using UnityEngine;

//Command pattern
//This script holds the commands that can be executed 


//Command that does nothing
public class NullCommand : Command
{
    public override bool Execute(Object _obj, bool _isUndo) {return true;}
}

//Move a unit
public class MoveUnitCommand : Command
{
    public Vector2 direction;
    public bool justCheck;

    //Set passed through variables
    public MoveUnitCommand(Vector2 _direction, bool _justCheck) : base() 
    {
        direction = new Vector2(_direction.x, _direction.y);
        justCheck = _justCheck;
    }
    
    //Execute the command
    public override bool Execute(Object _obj, bool _isUndo)
    {   
        bool moveIsAllowed = true;

        Vector2 currentPosition = _obj.transform.position;
        Vector2 newPosition = currentPosition + direction;

        if(moveIsAllowed && !_isUndo) 
        {
            //Notify other objects where this one moved and in what direction,
            //so they can get pushed if needed
            moveIsAllowed = Notify("ObjectMoved", newPosition, direction, _obj, justCheck);
        }
        //Check if the object can and is supposed to move (as opposed to just checking if they can move)
        if(moveIsAllowed && !justCheck) 
        {
            _obj.transform.position = newPosition;

            //If a word moved, clear the properties of this object
            //if(_obj.objectType is Word) _obj.objectProperties.Clear();
        }

        //Return if theyre allowed to move
        return moveIsAllowed;
    }

    //Inverse the movement and execute the command again to undo
    public override void Undo(Object _obj)
    {
        direction.x *= -1;
        direction.y *= -1;
        Execute(_obj, true);
    }

    //Overload OnNotify method for handling collision
    protected override bool Notify(string _myEvent, Vector2 _newPosition, Vector2 _direction, Object _otherObject, bool _justCheck)
    {
        bool moveIsAllowed;

        foreach(Observer observer in observers)
        {
            moveIsAllowed = observer.OnNotify(_myEvent, _newPosition, _direction, _otherObject, _justCheck);
            if(!moveIsAllowed) return false;
        }
        
        return true;
    }
}

//The undo command
public class UndoCommand : Command
{
    public UndoCommand(List<Object> _objectsInScene) : base()
    {
        foreach(Object _obj in _objectsInScene)
        {
            if(_obj.usedMoveCommands.Count > 0) 
            {
                //Pop the latest move on the stack and undo it
                _obj.usedMoveCommands.Pop().Undo(_obj);
            }
            else Debug.Log($"{_obj.name} has nothing to undo");
        }
    }
}

//Check the statements that are made by creating sentences with the word objects.
//Full disclosure: this isn't *full* functional but hey we don't get graded on functionality so don't be mad
public class StatementsCheck : Command
{
    public StatementsCheck(Vector2 _newPosition, Object _obj, List<Object> _objectsInScene) : base()
    {
        Word thisObjectComponent = _obj.ReturnWordType(_obj);
        if(thisObjectComponent == null) return; 

        List<Word> leftObjectsComponents = new();
        List<Word> upObjectsComponents = new();
        int amountsOfObjects = _objectsInScene.Count;

        for(int i = 1; i < amountsOfObjects; i++)
        {
            Word leftObjectComponent = Notify("StatementCheck", _newPosition + new Vector2(-i, 0), _obj);
            Word upObjectComponent = Notify("StatementCheck", _newPosition + new Vector2(0, i), _obj);
            
            if(leftObjectComponent != null) leftObjectsComponents.Add(leftObjectComponent); 
            if(upObjectComponent != null) upObjectsComponents.Add(upObjectComponent);

            if(leftObjectComponent == null && upObjectComponent == null) break;
        }

        if(leftObjectsComponents.Count == 0 && upObjectsComponents.Count == 0) return;

        if(leftObjectsComponents.Count > 1) ActivateStatement(leftObjectsComponents, thisObjectComponent, _objectsInScene);
        if(upObjectsComponents.Count > 1) ActivateStatement(upObjectsComponents, thisObjectComponent, _objectsInScene);
    }

    private void ActivateStatement(List<Word> _list, Object _objToCheck, List<Object> _objectsInScene)
    {

        Word _thisTextObj;
        if(_objToCheck is Word textObj) _thisTextObj = textObj;
        else return;

        ObjectType typeToAffect;
        ObjectType typeToChange  = null;
        ObjectProperty propertyToChange = null;

        //Check if the first word in the sentence is a direct word
        if( (_thisTextObj.wordType != WordType.DirectOrSubjectWord) && (_thisTextObj.wordType != WordType.DirectWord)) return;

        //Check if the second word is 'Is'
        if(_list[0].wordType != WordType.OperatorWord) return;

        //Check if the third word is a subject word
        if( (_list[1].wordType != WordType.DirectOrSubjectWord) && (_list[1].wordType != WordType.SubjectWord)) return;

        //Get the type that needs to be affected and the property that needs to be changes
        typeToAffect = ObjectType.objectTypes[_list[1].typeToAffect];

        if(_thisTextObj.wordType == WordType.DirectWord) 
        {
            propertyToChange = ObjectProperty.objectProperties[_thisTextObj.propertyToChange];
        }
        else
        {
            typeToChange = ObjectType.objectTypes[_thisTextObj.typeToAffect];
        }

        //If the propertyToChange is a direct word, just change the property 
        if(propertyToChange != null)
        {
            foreach(Object _obj in _objectsInScene)
            {
                if(_obj.objectType == typeToAffect) 
                { 
                    Debug.Log($"Property Changed: {_obj.name} , {propertyToChange.GetType().Name}");
                    _obj.objectProperties.Add(propertyToChange);
                    //Debug.Log($"Properties Cleared And Changed: {_obj.name} , {propertyToChange.GetType().Name}");    
                }
            }   
        }
        //If the propertyToChange can be a subject word, change the type
        else if (typeToChange != null)
        {
            foreach(Object _obj in _objectsInScene)
            {
                if(_obj.objectType == typeToAffect) 
                {
                    _obj.stateMachine.SetType(typeToChange, _obj);
                    //Debug.Log($"Properties Cleared And Changed: {_obj.name} , {propertyToChange.GetType().Name}");    
                }
            }
        }
    }

    //Overload OnNotify method for handling checking statements
    protected override Word Notify(string _myEvent, Vector2 _newPosition, Object _otherObject)
    {
        foreach(Observer observer in observers)
        {
            Word textObject = observer.OnNotify(_myEvent, _newPosition, _otherObject);
            if(textObject != null) return textObject;
        }
        return null;
    }   

    
}
