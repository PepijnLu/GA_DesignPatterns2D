using System.Collections.Generic;
using UnityEngine;

//State Machine pattern
//The types an object can be (e.g. Crate, Flag, Face etc.)
public class ObjectType
{
    public static Dictionary<string, ObjectType> objectTypes = new Dictionary<string, ObjectType>();
    private Sprite sprite;
    public ObjectType()
    {
        //Load the sprite from the resources folder
        sprite = Resources.Load<Sprite>(GetType().Name);

        //Add the instantiated class to the dictionary if it's not in it yet
        if(!objectTypes.ContainsKey(GetType().Name))
        {
            objectTypes.Add(GetType().Name, this);
            Debug.Log($"Dictionary: {GetType().Name} added");
        }
    }

    //Logic for entering a state
    public void OnStateEnter(Object _obj)
    {
        _obj.spriteRenderer.sprite = sprite;
    }

    //Logic for exiting a state
    public void OnStateExit(Object _obj)
    {
        _obj.objectProperties.Clear();
    }
}

//Types of different objects that are not text
public class Face : ObjectType {}
public class Wall : ObjectType {}
public class Crate : ObjectType {}
public class Flag : ObjectType {}
public class WordObjectType : ObjectType {}


