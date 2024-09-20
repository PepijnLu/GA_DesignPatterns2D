using UnityEngine;

//Here live all the object types
//Base class for a word type (can create statements)

public class Word : ObjectType
{
    public Word() : base() 
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);

        if(ObjectProperty.objectProperties.ContainsKey(GetType().Name + "Property"))
        {
            properties.Add(ObjectProperty.objectProperties[GetType().Name + "Property"]);
        }
        else Debug.Log($"Dictionary: {GetType().Name + "Property"} not found");
    }
}

//Types of different objects that are not text
public class Face : ObjectType {}
public class Wall : ObjectType {}
public class Crate : ObjectType {}
public class Flag : ObjectType {}

//Types of different objects that are text
public class IsText : Word {}
public class FaceText : Word {}
public class YouText : Word {}
public class CrateText : Word {}
public class StopText : Word {}
public class PushText : Word {}
public class WallText : Word {}
public class WinText : Word {}
public class FlagText : Word {}



