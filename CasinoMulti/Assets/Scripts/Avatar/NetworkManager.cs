using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] behaviors;

    [SerializeField]
    private PhotonView playerPhotonView;

    private void Start()
    {
        //print(playerPhotonView.IsMine);
        //if (playerPhotonView.IsMine) return;

        foreach (var behavior in behaviors)
            behavior.enabled = playerPhotonView.IsMine;


        //thirdPersonController.enabled = false;
    }
}
