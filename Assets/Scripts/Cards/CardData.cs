using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Scriptable Objects/CardData")]
public class CardData : ScriptableObject
{
    public int power;
    public string cardName;
    public Sprite artwork;
    public Color color;
}
