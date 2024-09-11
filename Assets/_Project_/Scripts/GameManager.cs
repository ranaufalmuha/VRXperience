// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.Networking;
// using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public float _mytime = 0.0f;
//     public float _myMenutesTime = 0.0f;
//     private float rewardTime = 30f;
//     public bool _timerOn = false;
//     private bool claimedReward = false;

//     // text 
//     public TMP_Text _menuteTMP;
//     public TMP_Text _seccondTMP;
//     public TMP_Text _semiColumn;
//     public TMP_Text _claimAlert;
//     public TMP_Text _walletAddress;
//     public TMP_Text _tokenValue;

//     // btn 
//     public Button claimBtn;

//     // web3 integration
//     string principalId;
//     private string balance_of = "https://vrxperience-api.vercel.app/balance_of/";
//     private string claim_token_url = "https://vrxperience-api.vercel.app/claim_token/";

//     // List untuk menyimpan principalID yang sudah klaim
//     private List<string> claimedPrincipalIDs;

//     void Start()
//     {
//         // Ambil principalID dari PlayerPrefs atau buat list baru jika tidak ada
//         claimedPrincipalIDs = GetClaimedPrincipalIDs();

//         principalId = PlayerPrefs.GetString("principalId", "no_value");
//         _timerOn = true;
//         _walletAddress.text = principalId;

//         Debug.Log(claimedPrincipalIDs);

//         // Cek apakah principalId sudah melakukan klaim
//         if (claimedPrincipalIDs.Contains(principalId))
//         {
//             // Jika sudah klaim, sembunyikan tombol dan tampilkan pesan
//             _claimAlert.gameObject.SetActive(true);
//             claimBtn.gameObject.SetActive(false);
//             _menuteTMP.gameObject.SetActive(false);
//             _seccondTMP.gameObject.SetActive(false);
//             _semiColumn.gameObject.SetActive(false);
//             claimedReward = true;
//         }
//         else
//         {
//             // Jika belum klaim, aktifkan timer dan klaim button
//             _claimAlert.gameObject.SetActive(false);
//             claimBtn.gameObject.SetActive(true);
//             _menuteTMP.gameObject.SetActive(true);
//             _seccondTMP.gameObject.SetActive(true);
//             _semiColumn.gameObject.SetActive(true);
//             claimBtn.interactable = false;
//         }

//         StartCoroutine(CallReadAll());
//     }

//     void Update()
//     {
//         if (!claimedReward)
//         {
//             TimerForReward();
//         }
//     }

//     void TimerForReward()
//     {
//         if (_timerOn)
//         {
//             _mytime += Time.deltaTime;
//             if (_mytime >= 60.0f)
//             {
//                 _myMenutesTime++;
//                 _mytime = 0;
//             }
//             _seccondTMP.text = _mytime > 9 ? ((int)_mytime).ToString() : "0" + ((int)_mytime).ToString();
//             _menuteTMP.text = _myMenutesTime > 9 ? ((int)_myMenutesTime).ToString() : "0" + ((int)_myMenutesTime).ToString();
//         }

//         if (_mytime >= rewardTime)
//         {
//             _timerOn = false;
//             claimBtn.interactable = true;
//             claimedReward = true;
//         }
//     }

//     IEnumerator CallReadAll()
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get(balance_of + principalId))
//         {
//             yield return webRequest.SendWebRequest();

//             if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
//                 webRequest.result == UnityWebRequest.Result.ProtocolError)
//             {
//                 Debug.LogError("Error: " + webRequest.error);
//             }
//             else
//             {
//                 string jsonResponse = webRequest.downloadHandler.text;
//                 ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

//                 if (responseData != null)
//                 {
//                     _tokenValue.text = responseData.balance.ToString() + " VRX";
//                     Debug.Log("Balance: " + responseData.balance);
//                 }
//                 else
//                 {
//                     Debug.LogError("Failed to parse JSON response.");
//                 }
//             }
//         }
//     }

//     public void StartClaimReward()
//     {
//         string key = "mykey_for_unity_user-claim_token"; 

//         // Simpan principalId setelah klaim
//         claimedPrincipalIDs.Add(principalId);
//         SaveClaimedPrincipalIDs(claimedPrincipalIDs);

//         StartCoroutine(ClaimReward(principalId, key));
//     }

//     IEnumerator ClaimReward(string _principalId, string key)
//     {
//         using (UnityWebRequest webRequest = UnityWebRequest.Get(claim_token_url + principalId + "/" + key))
//         {
//             string jsonResponse = webRequest.downloadHandler.text;
//             claimBtn.gameObject.SetActive(false);
//             _menuteTMP.gameObject.SetActive(false);
//             _seccondTMP.gameObject.SetActive(false);
//             _semiColumn.gameObject.SetActive(false);
//             claimedReward = true;

//             yield return webRequest.SendWebRequest();
//             StartCoroutine(CallReadAll());
//         }
//     }

//     // Method untuk mengambil daftar principalID yang sudah klaim dari PlayerPrefs
//     List<string> GetClaimedPrincipalIDs()
//     {
//         string jsonString = PlayerPrefs.GetString("claimedPrincipalIDs", "{\"principalIDs\":[]}");
//         PrincipalIDList idList = JsonUtility.FromJson<PrincipalIDList>(jsonString);
//         return idList.principalIDs;
//     }

//     // Method untuk menyimpan daftar principalID yang sudah klaim ke PlayerPrefs
//     void SaveClaimedPrincipalIDs(List<string> principalIDs)
//     {
//         PrincipalIDList idList = new PrincipalIDList { principalIDs = principalIDs };
//         string jsonString = JsonUtility.ToJson(idList);
//         PlayerPrefs.SetString("claimedPrincipalIDs", jsonString);
//     }

//     [System.Serializable]
//     public class PrincipalIDList
//     {
//         public List<string> principalIDs;
//     }
// }

// // Kelas untuk memetakan respons JSON
// [System.Serializable]
// public class ResponseData
// {
//     public string message;
//     public string balance;
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public float _mytime = 0.0f;
    public float _myMenutesTime = 0.0f;
    private float rewardTime = 30f;
    public bool _timerOn = false;
    private bool claimedReward = false;

    // text 
    public TMP_Text _menuteTMP;
    public TMP_Text _seccondTMP;
    public TMP_Text _semiColumn;
    public TMP_Text _claimAlert;
    public TMP_Text _walletAddress;
    public TMP_Text _tokenValue;

    // btn 
    public Button claimBtn;

    // web3 integration
    string principalId;
    private string balance_of = "https://vrxperience-api.vercel.app/balance_of/";
    private string claim_token_url = "https://vrxperience-api.vercel.app/claim_token/";

    // List untuk menyimpan data klaim berupa pasangan principalID dan tanggalClaim
    private List<ClaimData> claimDataList;

    void Start()
    {
        // Ambil principalID dan daftar klaim dari PlayerPrefs atau buat list baru jika tidak ada
        claimDataList = GetClaimDataList();
        principalId = PlayerPrefs.GetString("principalId", "no_value");
        _timerOn = true;
        _walletAddress.text = principalId;

        Debug.Log(claimDataList);

        // Cek apakah principalId sudah melakukan klaim pada hari ini
        if (HasClaimedToday(principalId))
        {
            // Jika sudah klaim, sembunyikan tombol dan tampilkan pesan
            _claimAlert.gameObject.SetActive(true);
            claimBtn.gameObject.SetActive(false);
            _menuteTMP.gameObject.SetActive(false);
            _seccondTMP.gameObject.SetActive(false);
            _semiColumn.gameObject.SetActive(false);
            claimedReward = true;
        }
        else
        {
            // Jika belum klaim, aktifkan timer dan klaim button
            _claimAlert.gameObject.SetActive(false);
            claimBtn.gameObject.SetActive(true);
            _menuteTMP.gameObject.SetActive(true);
            _seccondTMP.gameObject.SetActive(true);
            _semiColumn.gameObject.SetActive(true);
            claimBtn.interactable = false;
        }

        StartCoroutine(CallReadAll());
    }

    void Update()
    {
        if (!claimedReward)
        {
            TimerForReward();
        }
    }

    void TimerForReward()
    {
        if (_timerOn)
        {
            _mytime += Time.deltaTime;
            if (_mytime >= 60.0f)
            {
                _myMenutesTime++;
                _mytime = 0;
            }
            _seccondTMP.text = _mytime > 9 ? ((int)_mytime).ToString() : "0" + ((int)_mytime).ToString();
            _menuteTMP.text = _myMenutesTime > 9 ? ((int)_myMenutesTime).ToString() : "0" + ((int)_myMenutesTime).ToString();
        }

        if (_mytime >= rewardTime)
        {
            _timerOn = false;
            claimBtn.interactable = true;
            claimedReward = true;
        }
    }

    IEnumerator CallReadAll()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(balance_of + principalId))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string jsonResponse = webRequest.downloadHandler.text;
                ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

                if (responseData != null)
                {
                    _tokenValue.text = responseData.balance.ToString() + " VRX";
                    Debug.Log("Balance: " + responseData.balance);
                }
                else
                {
                    Debug.LogError("Failed to parse JSON response.");
                }
            }
        }
    }

    public void StartClaimReward()
    {
        string key = "mykey_for_unity_user-claim_token";

        // Simpan data klaim baru (principalId dan tanggal hari ini)
        claimDataList.Add(new ClaimData(principalId, DateTime.Now.ToString("yyyy-MM-dd")));
        SaveClaimDataList(claimDataList);

        StartCoroutine(ClaimReward(principalId, key));
    }

    IEnumerator ClaimReward(string _principalId, string key)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(claim_token_url + principalId + "/" + key))
        {
            string jsonResponse = webRequest.downloadHandler.text;
            claimBtn.gameObject.SetActive(false);
            _menuteTMP.gameObject.SetActive(false);
            _seccondTMP.gameObject.SetActive(false);
            _semiColumn.gameObject.SetActive(false);
            claimedReward = true;

            yield return webRequest.SendWebRequest();
            StartCoroutine(CallReadAll());
        }
    }

    // Method untuk mengecek apakah principalId sudah klaim hari ini
    bool HasClaimedToday(string _principalId)
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        // Cek apakah ada klaim dengan principalId dan tanggal hari ini
        foreach (ClaimData data in claimDataList)
        {
            if (data.principalId == _principalId && data.tanggalClaim == today)
            {
                return true; // Sudah klaim hari ini
            }
        }
        return false; // Belum klaim
    }

    // Method untuk mengambil daftar data klaim dari PlayerPrefs
    List<ClaimData> GetClaimDataList()
    {
        string jsonString = PlayerPrefs.GetString("claimDataList", "{\"claimDataList\":[]}");
        ClaimDataList dataList = JsonUtility.FromJson<ClaimDataList>(jsonString);
        return dataList.claimDataList;
    }

    // Method untuk menyimpan daftar data klaim ke PlayerPrefs
    void SaveClaimDataList(List<ClaimData> dataList)
    {
        ClaimDataList claimDataListObject = new ClaimDataList { claimDataList = dataList };
        string jsonString = JsonUtility.ToJson(claimDataListObject);
        PlayerPrefs.SetString("claimDataList", jsonString);
        PlayerPrefs.Save(); // Pastikan untuk menyimpan perubahan
    }

    [System.Serializable]
    public class ClaimData
    {
        public string principalId;
        public string tanggalClaim;

        public ClaimData(string _principalId, string _tanggalClaim)
        {
            principalId = _principalId;
            tanggalClaim = _tanggalClaim;
        }
    }

    [System.Serializable]
    public class ClaimDataList
    {
        public List<ClaimData> claimDataList;
    }
}

// Kelas untuk memetakan respons JSON
[System.Serializable]
public class ResponseData
{
    public string message;
    public string balance;
}
