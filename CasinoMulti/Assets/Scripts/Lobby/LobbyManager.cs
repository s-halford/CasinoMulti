using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [Header("---UI Screens---")]
    public GameObject roomUI;
    public GameObject connectUI;

    [Header("---UI Text---")]
    public Text statusText;
    public Text connectingText;

    [Header("---UI InputFields---")]
    public GameObject createRoom;
    public GameObject joinRoom;

    private void Awake()
    {
        
    }

}
