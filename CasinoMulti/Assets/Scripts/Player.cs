using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public Transform playerCameraRoot;

    [SerializeField]
    private MonoBehaviour[] scripts;

    private PhotonView playerPhotonView;

    public void PhotonCheck()
    {
        playerPhotonView = GetComponent<PhotonView>();

        foreach (MonoBehaviour script in scripts)
            script.enabled = playerPhotonView.IsMine;
    }
}
