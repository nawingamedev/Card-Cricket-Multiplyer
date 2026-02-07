using UnityEngine;
using Unity.Netcode;

public class HandUIManager : NetworkBehaviour
{
    public Transform playerPanel;
    public Transform enemyPanel;
    public GameObject cardPrefab;

    void Start()
    {
        GameStateManager.Instance.p1Hand.OnListChanged += RefreshUI;
        GameStateManager.Instance.p2Hand.OnListChanged += RefreshUI;
    }

    void RefreshUI(NetworkListEvent<int> e)
    {
        DrawHands();
    }

    void DrawHands()
    {
        foreach (Transform t in playerPanel) Destroy(t.gameObject);
        foreach (Transform t in enemyPanel) Destroy(t.gameObject);

        var game = GameStateManager.Instance;
        bool isP1 = NetworkManager.Singleton.LocalClientId == 0;

        var myHand = isP1 ? game.p1Hand : game.p2Hand;
        var enemyHand = isP1 ? game.p2Hand : game.p1Hand;

        for(int i=0;i<myHand.Count;i++)
        {
            var go = Instantiate(cardPrefab, playerPanel);
            go.GetComponent<CardUI>().Init(game.deck[myHand[i]], myHand[i]);
        }

        for(int i=0;i<enemyHand.Count;i++)
        {
            var go = Instantiate(cardPrefab, enemyPanel);
            // hide enemy card
            go.GetComponent<CardUI>().nameText.text = "???";
            go.GetComponent<CardUI>().powerText.text = "?";
        }
    }
}
