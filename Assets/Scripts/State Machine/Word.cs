using UnityEngine;

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
    [SerializeField] private string textField;
    [HideInInspector] public string propertyToChange;
    public WordType wordType;
    public string typeOrProperty;
    
    protected override void Start() 
    {
        base.Start();

        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(textField + "Text");
        typeOrProperty = textField;

        //Make sure the text can be pushed 
        objectProperties.Add(ObjectProperty.objectProperties["Push"]);

        string thisName = GetType().Name;
        if(typeOrProperty.Contains("Text"))
        {
            typeOrProperty = thisName.Replace("Text", "");
        }
    }
}




