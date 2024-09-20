//Here live all the properties that make it so words can create statements
public enum WordType
{
    //Can be either the subject or the direct in a sentence
    DirectOrSubjectWord,
    //Can only be the subject
    SubjectWord,
    //Can only be the direct
    DirectWord,
    //Is an operator
    OperatorWord
}
public class TextObject : ObjectProperty
{
    public WordType wordType;
    public string typeToAffect;
    public string propertyToChange;

    public TextObject() : base() 
    {
        string thisName = GetType().Name;
        if(thisName.Contains("TextProperty"))
        {
            typeToAffect = thisName.Replace("TextProperty", "");
            propertyToChange = thisName.Replace("TextProperty", "");
        }
    }
}

public class FaceTextProperty : TextObject
{
    public FaceTextProperty() : base()
    {
        wordType = WordType.DirectOrSubjectWord;
    }
}
public class CrateProperty : TextObject
{
    public CrateProperty() : base()
    {
        wordType = WordType.DirectOrSubjectWord;
    }
}
public class IsTextProperty : TextObject
{
    public IsTextProperty() : base()
    {
        wordType = WordType.OperatorWord;
    }
}

public class YouTextProperty : TextObject
{
    public YouTextProperty() : base()
    {
        wordType = WordType.DirectWord;
    }
}

public class CrateTextProperty : TextObject
{
    public CrateTextProperty() : base()
    {
        wordType = WordType.DirectOrSubjectWord;
    }
}

public class StopTextProperty : TextObject
{
    public StopTextProperty() : base()
    {
        wordType = WordType.DirectWord;
    }
}

public class PushTextProperty : TextObject
{
    public PushTextProperty() : base()
    {
        wordType = WordType.DirectWord;
    }
}

public class WallTextProperty : TextObject
{
    public WallTextProperty() : base()
    {
        wordType = WordType.DirectOrSubjectWord;
    }
}

public class FlagTextProperty : TextObject
{
    public FlagTextProperty() : base()
    {
        wordType = WordType.DirectOrSubjectWord;
    }
}

public class WinTextProperty : TextObject
{
    public WinTextProperty() : base()
    {
        wordType = WordType.DirectWord;
    }
}





