using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class GameStateManager : NetworkBehaviour
{
    [SerializeField] GameObject networkUIPanel,gamePlayUIPanel;
    public static GameStateManager Instance;

    public List<CardData> deck;

    public NetworkVariable<GamePhase> Phase = new(GamePhase.WaitingForPlayers);
    public NetworkVariable<float> timer = new();

    public NetworkList<int> p1Hand = new();
    public NetworkList<int> p2Hand = new();
    public NetworkList<int> p1Selected = new();
    public NetworkList<int> p2Selected = new();

    float turnTime = 15f;

    void Awake() => Instance = this;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
            NetworkManager.Singleton.OnClientConnectedCallback += OnPlayerConnected;
    }

    void OnPhaseChanged(GamePhase oldP, GamePhase newP)
    {
        if (!IsClient) return;

        var ui = FindObjectOfType<UIStateManager>();

        if (newP == GamePhase.Selecting)
            ui.SetState(UIState.Gameplay);
        else if (newP == GamePhase.Revealing)
            ui.SetState(UIState.Revealing);
        else if (newP == GamePhase.RoundEnd)
            ui.SetState(UIState.Result);
    }
    void OnPlayerConnected(ulong id)
    {
        if (NetworkManager.Singleton.ConnectedClients.Count == 2)
            StartGame();
    }

    void StartGame()
    {
        Phase.Value = GamePhase.Dealing;
        DealCards();
        StartTurn();
    }

    void DealCards()
    {
        p1Hand.Clear(); p2Hand.Clear();
        for(int i=0;i<5;i++)
        {
            p1Hand.Add(Random.Range(0, deck.Count));
            p2Hand.Add(Random.Range(0, deck.Count));
        }
    }

    void StartTurn()
    {
        p1Selected.Clear();
        p2Selected.Clear();
        timer.Value = turnTime;
        Phase.Value = GamePhase.Selecting;
    }

    void Update()
    {
        if (!IsServer) return;

        if (Phase.Value == GamePhase.Selecting)
        {
            timer.Value -= Time.deltaTime;
            if (timer.Value <= 0)
            {
                Phase.Value = GamePhase.Revealing;
                ResolveRound();
            }
        }
    }

    void ResolveRound()
    {
        int p1Wins = 0, p2Wins = 0;

        for(int i=0;i<3;i++)
        {
            int a = p1Selected[i];
            int b = p2Selected[i];

            if (deck[a].power > deck[b].power) p1Wins++;
            else if (deck[b].power > deck[a].power) p2Wins++;
        }

        Debug.Log($"Round Result P1:{p1Wins} P2:{p2Wins}");

        Phase.Value = GamePhase.RoundEnd;
        Invoke(nameof(StartTurn), 3f);
    }

    // Player selects card
    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    public void SelectCardRpc(int cardIndex, RpcParams rpcParams = default)
    {
        ulong id = rpcParams.Receive.SenderClientId;

        if (id == 0 && p1Selected.Count < 3)
            p1Selected.Add(cardIndex);
        else if (id == 1 && p2Selected.Count < 3)
            p2Selected.Add(cardIndex);
    }
}
public enum GamePhase
{
    WaitingForPlayers,
    Dealing,
    Selecting,
    Revealing,
    RoundEnd
}