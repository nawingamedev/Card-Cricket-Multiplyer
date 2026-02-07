using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    public NetworkVariable<int> turnCost = new NetworkVariable<int>(3);
    public NetworkVariable<int> score = new NetworkVariable<int>(0);
    public override void OnNetworkSpawn()
    {
        Debug.Log("Player Spawned: " + OwnerClientId);
    }
}
