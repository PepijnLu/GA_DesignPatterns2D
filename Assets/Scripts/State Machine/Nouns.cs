using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noun : ObjectType
{
    public Noun() : base() {}
}

public class StartChar : Noun
{
    public StartChar() : base()
    {
        properties.Add(ObjectProperty.objectProperties["You"]);
    }
}
public class Wall : Noun
{
    public Wall() : base()
    {
        properties.Add(ObjectProperty.objectProperties["Stop"]);
    }
}

public class Crate : Noun
{
    public Crate() : base()
    {
        properties.Add(ObjectProperty.objectProperties["Push"]);
    }
}



