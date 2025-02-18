using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class ContestScript : MonoBehaviour
{
    public string contestUrl = "https://ludo-project-backend.vercel.app/api/v1/user/allContest";

    public GameObject contestPrefab;
    public GameObject contestParent;

    private void Start()
    {
        GetContest(contestUrl);
    }


    [Serializable]
    public class Contest
    {
        public string _id { get; set; }
        public string contestId { get; set; }
        public List<object> users { get; set; } // You can replace 'object' with the appropriate user class if needed.
        public int firstPrize { get; set; }
        public int secondPrize { get; set; }
        public int thirdPrize { get; set; } // Nullable because it's not always present
        public int entryFee { get; set; }
        public int noOfuser { get; set; } // Rename to 'NoOfUsers' to follow C# naming conventions
        public int joined { get; set; }
        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public int __v { get; set; }
    }
    ContestData val;
    public class ContestData
    {
        public string message { get; set; }
        public int status { get; set; }
        public List<Contest> data { get; set; }
    }

    public void GetContest(string url)
    {
        StartCoroutine(Registrations(url));
    }

    IEnumerator Registrations(string url)
    {
        //yield return www; Debug.Log(www.text +"jkj");
        // Root val = JsonConvert.DeserializeObject<Root>(www.text);
        //Debug.Log(www.text + val.data.otp);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            /* byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

             request.uploadHandler = new UploadHandlerRaw(bodyRaw);*/
             request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            var response = request.result;
            try
            {
                if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                else if (request.result == UnityWebRequest.Result.Success)
                {
                    print("Successfully registered ");
                    var json = request.downloadHandler.text;
                    Debug.Log(json.ToString());

                     val = JsonConvert.DeserializeObject<ContestData>(json.ToString());
                    ShowContest();
                    Debug.Log("val.data.otp" + val.message);
                }
            }
            catch (Exception e)
            {
                print("exception "+e);
            }
            finally
            {

            }
        }

    }


    public Transform contestTransform;
    public void ShowContest()
    {
        for (int j = 0; j < val.data.Count; j++)
        {
            GameObject contestButton = Instantiate(contestPrefab);
            contestButton.transform.SetParent(contestParent.transform);
            contestButton.transform.localScale = new Vector3(1, 1, 1);
            contestButton.GetComponent<ContestPage>().contestId = val.data[j].contestId;
            contestButton.GetComponent<ContestPage>().Id = val.data[j]._id;
            contestButton.GetComponent<ContestPage>()._firstPrize = val.data[j].firstPrize;
            contestButton.GetComponent<ContestPage>()._secondPrize = val.data[j].secondPrize;
            contestButton.GetComponent<ContestPage>()._thirdPrize = val.data[j].thirdPrize;
            contestButton.GetComponent<ContestPage>()._entryFee = val.data[j].entryFee;
            contestButton.GetComponent<ContestPage>().noOfuser = val.data[j].noOfuser;
            contestButton.GetComponent<ContestPage>().joined = val.data[j].joined;
            contestButton.GetComponent<ContestPage>().createdAt = val.data[j].createdAt;
            contestButton.GetComponent<ContestPage>().updatedAt = val.data[j].updatedAt;
            contestButton.GetComponent<ContestPage>().SetPrize(val.data[j].firstPrize);
            contestButton.GetComponent<ContestPage>().SetFee(val.data[j].entryFee);
            contestButton.GetComponent<ContestPage>().SetPlayerJOined();
        }
    }
}
