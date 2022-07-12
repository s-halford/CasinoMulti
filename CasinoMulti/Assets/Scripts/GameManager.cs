using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Siccity.GLTFUtility;
using ReadyPlayerMe;
using Cinemachine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform playerSpawnPosition;

    [SerializeField]
    private string avatarUrl = "https://d1a370nemizbjq.cloudfront.net/5e4baea4-916a-4277-9f5d-a98816ae2480.glb";

    [SerializeField]
    private CinemachineVirtualCamera cam;

    [SerializeField]
    private MonoBehaviour[] camScripts;

    private GameObject playerClone;
    private Animator anim;
    private PhotonView photonView;

    private void Start()
    {
        playerClone = PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnPosition.position, playerSpawnPosition.rotation);
        anim = playerClone.GetComponent<Animator>();
        PhotonCheck();
        LoadAvatar();
    }

    private void PhotonCheck()
    {
        photonView = playerClone.GetComponent<PhotonView>();

        foreach (MonoBehaviour script in camScripts)
        {
            script.enabled = photonView.IsMine;
        }
    }

    private void LoadAvatar()
    {
        Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");

        var avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += (sender, args) =>
        {
            Player player = playerClone.GetComponent<Player>();
            cam.Follow = player.playerCameraRoot;
            player.PhotonCheck();

            Debug.Log($"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]");
            Debug.Log($"{args.Avatar.gameObject} is imported!");

            var avatar = args.Avatar.gameObject;
            avatar.transform.SetParent(playerClone.transform);
            avatar.transform.localPosition = Vector3.zero;

            var animator = avatar.GetComponent<Animator>();
            if (animator) Destroy(animator);

            if (anim)
            {
                anim.Rebind();
                anim.Update(0f);
            }
        };

        avatarLoader.OnFailed += (sender, args) =>
        {
            print("FAIL");
            Debug.Log(args.Type);
        };

        avatarLoader.LoadAvatar(avatarUrl);
    }

}
