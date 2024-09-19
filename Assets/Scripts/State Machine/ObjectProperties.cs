using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State Machine pattern
//The different kind of properties an object can have (e.g. Push, Stop, Win etc.)
public class ObjectProperty : ObjectComponent
{
    public static Dictionary<string, ObjectProperty> objectProperties = new Dictionary<string, ObjectProperty>();
    public List<ObjectProperty> excludedProperties = new();

    //This bool checks if the property is one that handles the collision of the object
    public bool handlesCollision;
    public ObjectProperty()
    {
        //Add the instantiated class to the dictionary if it's not in it yet
        if(!objectProperties.ContainsKey(GetType().Name))
        {
            objectProperties.Add(GetType().Name, this);
            Debug.Log($"Dictionary: {GetType().Name} added");
        }
    }

    //Function for handling collision between this and another object
    public virtual bool HandleCollision(Vector2 _direction, Object _obj, Object _otherObject) {throw new Exception("Base Handle Collision Called");}
    
}

//You control objects with this property
public class You : ObjectProperty
{
    public You() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj, Object _otherObject)
    {
        if (_obj.GetPushed(_direction, true)) return true;
        return false;
    }
}

//You can push this object; it doesn't stop you
public class Push : ObjectProperty
{
    public Push() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj, Object _otherObject)
    {
        if (_obj.GetPushed(_direction, false)) return true;
        else return false;
    }
}

//This object can't be pushed
public class Stop : ObjectProperty
{
    public Stop() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj, Object _otherObject)
    {
        return false; 
    }
}

//If an object with "You" touches this, you win
public class Win : ObjectProperty
{
    public Win() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj, Object _otherObject)
    {
        if(_otherObject.objectProperties.Contains(objectProperties["You"]))
        {
            Debug.Log("YOU WIN");
        }
        return true; 
    }
}

