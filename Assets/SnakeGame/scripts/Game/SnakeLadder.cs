using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SnakeLadder : MonoBehaviourPunCallbacks
{
    bool[] Profileflag = { false, false, false, false };
    private int myTurn = 0;
    public GameObject[] OppDice = new GameObject[4];
    [SerializeField] private Sprite[] dice_sprites;
    [SerializeField] private Sprite _sprites;
    [SerializeField] private GameObject[] Goti=new GameObject[4];
    [SerializeField] private GameObject[] timerr = new GameObject[4];
    public float wait = 0.25f;
    public float _rotate = 5f;
    private int[] diceVal = { 0, 5, 0, 2, 0, 3, 4, 0, 1, 0, 0 };

    private bool Roll_Flag = false;

    private int step = 0;
    private Image diceValue;
    bool[] flag = { true, false, false, false };
    public int totalPlayer;


    public bool IsOnline = false;
    public int PStep { get; set; }


    public GameObject arrow;


    // Start is called before the first frame update
    void Start()
    {
        //DataSaver.Instance.SetMode(1);
        PStep = -1;
        if (DataSaver2.Instance.GetMode() == 1) { IsOnline = true;  }
        if (IsOnline && PhotonNetwork.IsMasterClient) { myTurn = 0;Setflag(myTurn); }
        else if (IsOnline && !PhotonNetwork.IsMasterClient) { arrow.SetActive(false); myTurn = 1; Setflag(myTurn); }
        else if (!IsOnline) { myTurn = 0; Setflag(myTurn); }
        InitializeGoti(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Profileflag[myTurn]) OppDice[myTurn].transform.Rotate(new Vector3(0f, 0f, _rotate * Time.deltaTime));
        CheckForTurn();
    }


    public void BotRolling()
    {
        Debug.Log("Bot roll ");
        if(!IsOnline) step = diceVal[Random.Range(0, 10)];
        Debug.Log("step" + step);
        StartCoroutine(Rolling(step, true));
    }

    public void RollDice()
    {
        Debug.Log("user roll");
        if (myTurn != 0 || !flag[myTurn]) { Debug.Log("return roll "+myTurn+ flag[myTurn]); return; }
        flag[myTurn] = false;
        step = diceVal[Random.Range(0, 10)];
        if(IsOnline) photonView.RPC("PhotonStep", RpcTarget.Others, step);
        StartCoroutine(Rolling(step, true));
    }




    IEnumerator Rolling(int step, bool fl)
    {
        Debug.Log("rolling");
        timerr[myTurn].SetActive(false);
        diceValue = OppDice[myTurn].GetComponent<Image>();

        diceValue.sprite = _sprites;
        Profileflag[myTurn] = true;
        yield return new WaitForSeconds(2.0f);
        Profileflag[myTurn] = false;
        OppDice[myTurn].transform.rotation = Quaternion.identity;
        OppDice[myTurn].GetComponent<Image>().sprite = dice_sprites[step];
        

        bool possible = CheckMovement(step + 1);
        if (!possible)
        {
            SetTurn(); Setflag(myTurn);
        }
        else ChooseGotiForMovement();
    }

    public void ChooseGotiForMovement()
    {
        Goti[myTurn].GetComponent<Movement>().MakeMovement(step+1);
    }




    public void CheckForTurn()
    {
        if (myTurn != 0 && flag[myTurn])
        {
            timerr[myTurn].SetActive(true);
            arrow.SetActive(false);
            float w = Random.Range(0.3f, 4.0f);
            if (!IsOnline)
            {
                flag[myTurn] = false;
                Invoke("BotRolling", w);
            }
            else if (IsOnline && PStep != -1)
            {
                flag[myTurn] = false;
                step = PStep; PStep = -1;
                Invoke("BotRolling", 0.0f);
            }
        }
        else if (flag[myTurn] && myTurn==0)
        {
            timerr[myTurn].SetActive(true);
            arrow.SetActive(true);

        }
        

    }

    public void InitializeGoti(int val)
    {
        Debug.Log("INitilizer");
        totalPlayer = val;
        for(int j = 0; j < val; j++)
        {
            Goti[j].SetActive(true);
        }
    }

    public bool CheckMovement(int steps)
    {
        bool isHome= Goti[myTurn].GetComponent<Movement>().GetHomestatus();
        if (isHome && steps != 1) return false;
        if ((Goti[myTurn].GetComponent<Movement>().GetPawnPosition() + steps) >= 100) return false;
        return true;
    }


    public void SetTurn()
    {
        
        myTurn -= 1;
        if (myTurn >= totalPlayer)
        {
            myTurn = 0;
        }
        if (myTurn < 0)
        {
            myTurn = totalPlayer - 1;
        }
        Debug.Log("Turn " + myTurn);
    }
    public void Setflag(int i)
    {
        Debug.Log("setflag " + i);
        flag[0] = false; flag[1] = false; flag[2] = false; flag[3] = false;
        flag[i] = true;
    }
    public int GetMyTurn()
    {
        return myTurn;
    }


    public GameObject winnerWindow;
    public void WinStatus(int player)
    {
        winnerWindow.SetActive(true);
    }
    public void ExaustedTime()
    {
        SetTurn();
        Setflag(myTurn);
    }


    
   

}
