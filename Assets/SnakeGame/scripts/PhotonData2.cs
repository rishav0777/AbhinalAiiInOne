using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PhotonData2 : MonoBehaviourPunCallbacks
{
    public SnakeLadder snakeLadder;
    public Message messanger;
    // Start is called before the first frame update
    [PunRPC]
    void PhotonStep(int step)
    {
        snakeLadder.PStep = step;
        Debug.Log(")))))))))))))Pstep" + step); 
    }
    [PunRPC]
    void PhotonPlayer(int step)
    {
        //rolling.SetPhotonPLayer(step);
        Debug.Log(")))))))))Pplayer" + step);
    }
    [PunRPC]
    void PhotonPlayerPass(int step)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != step)
        {
            //rolling.SetPass(true);
            Debug.Log(")))))))))Pplayerpass" + PhotonNetwork.LocalPlayer.ActorNumber + " " + step);
        }
    }
    [PunRPC]
    void PhotonSmile(int s)
    {
        Debug.Log("Photon smilee ");
        messanger.ReceiveMessages(s);
    }
}
