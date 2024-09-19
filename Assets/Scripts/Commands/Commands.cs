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
//This currently doesnt work, so feel free to ignore it.
public class StatementsCheck : Command
{
    public StatementsCheck(Vector2 _newPosition, Object _otherObject, int amountsOfObjects) : base()
    {
        TextObject thisObjectComponent = _otherObject.ReturnTextObject(_otherObject);
        if(thisObjectComponent == null) return; 

        List<TextObject> leftObjectsComponents = new();
        List<TextObject> upObjectsComponents = new();

        for(int i = 1; i < amountsOfObjects; i++)
        {
            TextObject leftObjectComponent = Notify("StatementCheck", _newPosition + new Vector2(-i, 0), _otherObject);
            TextObject upObjectComponent = Notify("StatementCheck", _newPosition + new Vector2(0, i), _otherObject);
            
            Debug.Log("Statement Check left" + leftObjectComponent);
            Debug.Log("Statement Check right" + upObjectComponent);

            if(leftObjectComponent != null) leftObjectsComponents.Add(leftObjectComponent); 
            if(upObjectComponent != null) upObjectsComponents.Add(upObjectComponent);
            
            if(leftObjectComponent == null && upObjectComponent == null) break;
        }

        if(leftObjectsComponents.Count == 0 && upObjectsComponents.Count == 0) return;
        foreach(TextObject _leftObjectComponent in leftObjectsComponents)
        {
            Debug.Log("TextObject in list: " + _leftObjectComponent.GetType().Name);
        }
        foreach(TextObject _upObjectComponent in upObjectsComponents)
        {
            Debug.Log("TextObject in list: " + _upObjectComponent.GetType().Name);
        }
        
    }
}
