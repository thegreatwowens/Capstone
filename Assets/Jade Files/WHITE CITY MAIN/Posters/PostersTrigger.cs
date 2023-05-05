
using UnityEngine;
[CreateAssetMenu(fileName = "TriviasCreator", menuName = "Posters/Contents/Trivia", order = 1)]
public class PostersTrigger : ScriptableObject

{
    public string _title;
    [TextArea(15, 25)]
    public string _content;
    public Sprite _image;
    public CanvasGroup canvasGroup;
    
}
