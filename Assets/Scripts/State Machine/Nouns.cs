using UnityEngine;

//Here live all the object types

//Base class for a word type (can create statements)
public class Word : ObjectType
{
<<<<<<< HEAD
    public Noun() : base() {}
=======
    public Word() : base() 
    {
        if(ObjectProperty.objectProperties.ContainsKey(GetType().Name + "Property"))
        {
            properties.Add(ObjectProperty.objectProperties[GetType().Name + "Property"]);
        }
        else
        {
            Debug.Log($"Dictionary: {GetType().Name + "Property"} not found");
        }
    }
>>>>>>> origin/main
}

public class StartChar : ObjectType
{
    public StartChar() : base()
    {
        properties.Add(ObjectProperty.objectProperties["You"]);
    }
}
public class Wall : ObjectType
{
    public Wall() : base()
    {
        properties.Add(ObjectProperty.objectProperties["Stop"]);
    }
}

public class Crate : ObjectType
{
    public Crate() : base()
<<<<<<< HEAD
=======
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);
    }
}

public class Flag : ObjectType
{
    public Flag() : base()
    {
        properties.Add(ObjectProperty.objectProperties["Win"]);
    }
}

public class Is : Word
{
    public Is() : base()
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);
    }
}

public class Face : Word
{
    public Face() : base()
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);
    }
}

public class YouText : Word
{
    public YouText() : base()
>>>>>>> origin/main
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);
    }
}



