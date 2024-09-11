using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;


public class SceneNavigation : MonoBehaviour
{
    private string principalId;
    public TMP_InputField inputField;
    public Button enter;
    public Button playButton;
    public Button useExistingWalletBtn;
    public GameObject myalert;

    private string balance_of = "https://vrxperience-api.vercel.app/balance_of/";

    void Start()
    {
        playButton.interactable = false;
        enter.interactable = false;
        inputField.onValueChanged.AddListener(delegate { ValidateInput(); });
    }

    void Update(){
        if (!string.IsNullOrEmpty(inputField.text))
        {
            useExistingWalletBtn.interactable = false;
        }else{
            useExistingWalletBtn.interactable = true;
        }
    }

    public void setPrincipalId()
    {
        string _principalId = inputField.text.ToString();
        if (_principalId != null)
        {
            principalId = _principalId;
            StartCoroutine(CheckAddress()); 
        }
    }

    public void UseExistingWallet(){
       inputField.text = "2xhdh-vezie-3qxqn-ubkrb-kvpwi-orb6x-dhevv-bmwu3-i2rgd-c4yru-lqe";
    }

    IEnumerator CheckAddress()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(balance_of + principalId))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                myalert.SetActive(true);
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                playButton.interactable = true;
            }
        }
    }

    public void ValidateInput()
    {
        // Aktifkan tombol play jika input field tidak kosong
        if (!string.IsNullOrEmpty(inputField.text))
        {
            enter.interactable = true;
        }
        else
        {
            enter.interactable = false;
        }
    }

    public void LoadScene(string sceneName)
    {
        PlayerPrefs.SetString("principalId", principalId);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName);

    }

    public void CloseAlert()
    {
        myalert.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
