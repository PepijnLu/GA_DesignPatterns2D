using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//State Machine pattern
//The types an object can be (e.g. Crate, Flag, Face etc.)
public class ObjectType : ObjectComponent
{
    protected Object obj;
    public List<ObjectProperty> properties = new();
    public static Dictionary<string, ObjectType> objectTypes = new Dictionary<string, ObjectType>();
    public Sprite sprite;
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

        foreach(ObjectProperty _property in properties)
        {
            _obj.objectProperties.Add(_property);
            Debug.Log("property added " + _property.GetType().Name);
        }
    }

    //Logic for exiting a state
    public void OnStateExit(Object _obj)
    {
        _obj.objectProperties.Clear();
    }
}


