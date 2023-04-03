using Photon.Pun;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerNickname : MonoBehaviour
{
    [SerializeField] private float distanceY;

    private RectTransform textTransform;
    private TextMeshProUGUI nickname;

    private void Start()
    {
        nickname = GetComponentInChildren<TextMeshProUGUI>();
        textTransform = nickname.GetComponent<RectTransform>();

        nickname.text = GetComponent<PhotonView>().Owner.NickName;
    }

    private void Update()
    {
        textTransform.SetPositionAndRotation(new Vector3(transform.position.x,
                                                         transform.position.y + distanceY,
                                                         textTransform.position.z),
                                                         Quaternion.Euler(Vector3.zero));
    }
}