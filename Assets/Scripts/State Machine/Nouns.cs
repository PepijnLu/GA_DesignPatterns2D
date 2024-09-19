using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noun : ObjectType
{
    public Noun(Object _obj) : base(_obj) {}
}

public class StartChar : Noun
{
    public StartChar(Object _obj) : base(_obj)
    {
        properties.Add(ObjectProperty.objectProperties["You"]);
    }
}
public class Wall : Noun
{
    public Wall(Object _obj) : base(_obj)
    {
        properties.Add(ObjectProperty.objectProperties["Stop"]);
    }
}

public class Crate : Noun
{
    public Crate(Object _obj) : base(_obj)
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);
    }
}



