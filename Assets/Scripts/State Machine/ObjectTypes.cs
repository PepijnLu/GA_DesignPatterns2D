using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectType
{
    public static Dictionary<string, ObjectType> objectTypes = new Dictionary<string, ObjectType>();
    protected Object obj;
    public List<ObjectProperty> properties = new();
    public Sprite sprite;
    public ObjectType(Object _obj)
    {
        obj = _obj;
        sprite = Resources.Load<Sprite>(GetType().Name);

        if(!objectTypes.ContainsKey(GetType().Name))
        {
            objectTypes.Add(GetType().Name, this);
            Debug.Log($"Dictionary: {GetType().Name} added");
        }
    }

    public void OnStateEnter(Object _obj)
    {
        _obj.spriteRenderer.sprite = sprite;

        foreach(ObjectProperty _property in properties)
        {
            _obj.objectProperties.Add(_property);
            Debug.Log("property added " + _property.GetType().Name);
        }
    }

    public void OnStateExit(Object _obj)
    {
        _obj.objectProperties.Clear();
    }
}


