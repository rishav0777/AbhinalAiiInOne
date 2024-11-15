using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Message : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject[] character = new GameObject[9];
    public void SendMessages(int s)
    {
        photonView.RPC("PhotonSmile", RpcTarget.Others, s);
        MyMessage(character[s].transform.GetComponent<Image>().sprite);
    }
    public void ReceiveMessages(int s)
    {
        BotMessage(character[s].transform.GetComponent<Image>().sprite);
    }


    public GameObject myMessanger;
    public GameObject botMessanger;
    public void MyMessage(Sprite s)
    {
        myMessanger.SetActive(false);
        myMessanger.SetActive(true);
        myMessanger.GetComponent<Image>().sprite = s;
    }
    public void BotMessage(Sprite s)
    {
        botMessanger.SetActive(false);
        botMessanger.SetActive(true);
        botMessanger.GetComponent<Image>().sprite = s;
    }
}
