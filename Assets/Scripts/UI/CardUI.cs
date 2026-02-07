using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public Image art;
    public TMP_Text nameText;
    public TMP_Text powerText;

    int cardIndex;

    public void Init(CardData data, int index)
    {
        cardIndex = index;
        nameText.text = data.cardName;
        powerText.text = data.power.ToString();
        art.color = data.color;
        //art.sprite = data.artwork;
    }

    public void OnClick()
    {
        GameStateManager.Instance.SelectCardRpc(cardIndex);
    }
}
