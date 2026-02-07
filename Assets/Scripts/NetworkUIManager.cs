using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetworkUIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField ip_Input;
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        Debug.Log("Network Host Started");
    }
    public void StartClient()
    {
        string ip_ad = ip_Input.text;
        UnityTransport unityTransport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        unityTransport.ConnectionData.Address = ip_ad;
        NetworkManager.Singleton.StartClient();
        Debug.Log("Network Client started with IP Address: "+ip_ad);
    }
    public void Shutdown()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
