using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("---UI Screens---")]
    public GameObject roomUI;
    public GameObject connectUI;

    [Header("---UI Text---")]
    public TMP_Text statusText; 
    public TMP_Text connectingText;

    [Header("---UI InputFields---")]
    public TMP_InputField createRoom;
    public TMP_InputField joinRoom;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        connectingText.text = "Joining Lobby...";
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        connectUI.SetActive(false);
        roomUI.SetActive(true);
        statusText.text = "Joined Lobby";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectUI.SetActive(true);
        connectingText.text = "Disconnected... " + cause.ToString();
        roomUI.SetActive(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        int roomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 100;
        PhotonNetwork.CreateRoom(roomName.ToString(), roomOptions, TypedLobby.Default);
    }

    #region ButtonClicks
    public void OnClick_CreateBtn()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 100;
        PhotonNetwork.CreateRoom(createRoom.text, roomOptions, TypedLobby.Default);

    }

    public void OnClick_JoinBtn()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 100;
        PhotonNetwork.JoinOrCreateRoom(joinRoom.text, roomOptions, TypedLobby.Default);
    }

    public void OnClick_PlayNowBtn()
    {
        PhotonNetwork.JoinRandomRoom();
        statusText.text = "Creating Room.  Please wait...";
    }
    #endregion




}
