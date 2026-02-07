using UnityEngine;

public class UIStateManager : MonoBehaviour
{
    public GameObject panelNetwork;
    public GameObject panelGameplay;
    public GameObject panelReveal;
    public GameObject panelResult;

    UIState current;

    void Start()
    {
        SetState(UIState.NetworkSetup);
    }

    public void SetState(UIState state)
    {
        current = state;

        panelNetwork.SetActive(state == UIState.NetworkSetup);
        panelGameplay.SetActive(state == UIState.Gameplay);
        panelReveal.SetActive(state == UIState.Revealing);
        panelResult.SetActive(state == UIState.Result);
    }
}
public enum UIState
{
    NetworkSetup,
    Gameplay,
    Revealing,
    Result
}