using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProperty
{
    public static Dictionary<string, ObjectProperty> objectProperties = new Dictionary<string, ObjectProperty>();
    public List<ObjectProperty> excludedProperties = new();
    public bool handlesCollision;
    public ObjectProperty()
    {
        if(!objectProperties.ContainsKey(GetType().Name))
        {
            objectProperties.Add(GetType().Name, this);
            Debug.Log($"Dictionary: {GetType().Name} added");
        }
    }

    public virtual bool HandleCollision(Vector2 _direction, Object _obj) {throw new Exception("Base Handle Collision Called");}
    
}
public class You : ObjectProperty
{
    public You() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj)
    {
        if (_obj.GetPushed(_direction, true)) return true;
        return false;
    }
}

public class Push : ObjectProperty
{
    public Push() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj)
    {
        if (_obj.GetPushed(_direction, false)) return true;
        else return false;
    }
}

public class Stop : ObjectProperty
{
    public Stop() : base()
    {
        handlesCollision = true;
    }
    public override bool HandleCollision(Vector2 _direction, Object _obj)
    {
        return false; 
    }
}
