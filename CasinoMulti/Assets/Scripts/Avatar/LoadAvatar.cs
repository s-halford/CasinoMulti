using UnityEngine;
using Siccity.GLTFUtility;
using ReadyPlayerMe;

public class LoadAvatar : MonoBehaviour
{
    [SerializeField]
    private string avatarUrl = "https://d1a370nemizbjq.cloudfront.net/5e4baea4-916a-4277-9f5d-a98816ae2480.glb";

    [SerializeField]
    private Transform avatarParent;

    [SerializeField]
    private Animator anim;

    private void Start()
    {
        Debug.Log($"Started loading avatar. [{Time.timeSinceLevelLoad:F2}]");

        var avatarLoader = new AvatarLoader();
        avatarLoader.OnCompleted += (sender, args) =>
        {
            Debug.Log($"Loaded avatar. [{Time.timeSinceLevelLoad:F2}]");
            Debug.Log($"{args.Avatar.gameObject} is imported!");

            var avatar = args.Avatar.gameObject;

            avatar.transform.SetParent(avatarParent);

            var animator = avatar.GetComponent<Animator>();
            if(animator) Destroy(animator);

            if(anim)
            {
                anim.Rebind();
                anim.Update(0f);
            }

        };
        avatarLoader.OnFailed += (sender, args) =>
        {
            Debug.Log(args.Type);
        };

        avatarLoader.LoadAvatar(avatarUrl);
    }
}
