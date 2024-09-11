using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyboardManager : MonoBehaviour
{
    public GameObject keyboard;
    public TMP_InputField inputField;

    public Button capslockButton;
    public bool isCapslock;
    private Color normalColor = Color.white;
    private Color capsLockColor = Color.red;

    // string theInputText = "";
    void Start()
    {
        isCapslock = false;
        SetCapslockButtonColor();  
        

        inputField.onSelect.AddListener(delegate { showKeyboard(); });
    }

    public void CapsLockButton(){
        isCapslock = !isCapslock;
        SetCapslockButtonColor();
    } 

    private void SetCapslockButtonColor()
    {
        ColorBlock cb = capslockButton.colors; 

        if (isCapslock)
        {
            cb.normalColor = capsLockColor;
        }
        else
        {
            cb.normalColor = normalColor;
        }

        capslockButton.colors = cb; 
    }

    public void key(string theKey){


        if(isCapslock){
            // kalo capslock 
            inputField.text = inputField.text + theKey.ToUpper();
        }else{
            // kalo gak capslock 
            inputField.text = inputField.text + theKey.ToLower();
        }
    } 

    public void BackspaceKey()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }
    }

    public void ClearAll()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = "";
        }
    }

    public void SpaceKey()
    {
        inputField.text = inputField.text + " "; 
    }

    public void showKeyboard(){
        keyboard.SetActive(true);
    } 

    public void escapeKey(){
        keyboard.SetActive(false);
    } 

   
}
