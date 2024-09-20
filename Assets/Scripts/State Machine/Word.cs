using UnityEngine;

//Here live all the object types
//Base class for a word type (can create statements)
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
public class Word : Object
{
    public WordType wordType;
    [SerializeField] private string textField;
    public string typeToAffect;
    [HideInInspector] public string propertyToChange;
    
    protected override void Start() 
    {
        base.Start();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(textField + "Text");
        typeToAffect = textField;
        propertyToChange = textField;

        objectProperties.Add(ObjectProperty.objectProperties["Push"]);

        if(ObjectProperty.objectProperties.ContainsKey(GetType().Name + "Property"))
        {
            objectProperties.Add(ObjectProperty.objectProperties[GetType().Name + "Property"]);
        }
        else Debug.Log($"Dictionary: {GetType().Name + "Property"} not found");

        string thisName = GetType().Name;
        if(thisName.Contains("Text"))
        {
            typeToAffect = thisName.Replace("Text", "");
            propertyToChange = thisName.Replace("Text", "");
        }
    }
}

//Types of different objects that are not text
public class Face : ObjectType { /*public Face() : base() {properties.Add(ObjectProperty.objectProperties["You"]); } */}
public class Wall : ObjectType {}
public class Crate : ObjectType {}
public class Flag : ObjectType {}
public class WordObjectType : ObjectType {}

// //Types of different objects that are text
// public class IsText : Word {public IsText() : base() {wordType = WordType.OperatorWord;}}
// public class FaceText : Word {public FaceText() : base() {wordType = WordType.DirectOrSubjectWord;}}
// public class YouText : Word {public YouText() : base() {wordType = WordType.DirectWord;}}
// public class CrateText : Word {public CrateText() : base() {wordType = WordType.DirectOrSubjectWord;}}
// public class StopText : Word {public StopText() : base() {wordType = WordType.DirectWord;}}
// public class PushText : Word {public PushText() : base() {wordType = WordType.DirectWord;}}
// public class WallText : Word {public WallText() : base() {wordType = WordType.DirectOrSubjectWord;}}
// public class WinText : Word {public WinText() : base() {wordType = WordType.DirectWord;}}
// public class FlagText : Word {public FlagText() : base() {wordType = WordType.DirectOrSubjectWord;}}



