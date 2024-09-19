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

    public TextObject(/*WordType _wordType, ObjectComponent _component*/) : base() 
    {
        //wordType = _wordType;
    }
}

public class FaceProperty : TextObject
{
    public FaceProperty() : base()
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
public class IsProperty : TextObject
{
    public IsProperty() : base()
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




