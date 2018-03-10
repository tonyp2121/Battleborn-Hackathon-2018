using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour {
	public GameObject username;
	public GameObject password;
    public Button login;
    public Button register;
	private string Username;
	private string Password;
	private String[] Lines;
	private string DecryptedPass;

    void Start()
    {
        login.onClick.AddListener(LoginButton);
        register.onClick.AddListener(RegisterButton);
    }

	public void LoginButton(){
		bool UN = false;
		bool PW = false;
		if (Username != ""){
			if(System.IO.File.Exists(@"C:/UnityTestFolder/"+Username+".txt")){
				UN = true;
				Lines = System.IO.File.ReadAllLines(@"C:/UnityTestFolder/"+Username+".txt");
			} else {
				Debug.LogWarning("Username Invaild");
			}
		} else {
			Debug.LogWarning("Username Field Empty");
		}
		if (Password != ""){
			if (System.IO.File.Exists(@"C:/UnityTestFolder/"+Username+".txt")){
				int i = 1;
				foreach(char c in Lines[2]){
					i++;
					char Decrypted = (char)(c / i);
					DecryptedPass += Decrypted.ToString();
				}
				if (Password == DecryptedPass){
					PW = true;
				} else {
					Debug.LogWarning("Password Is invalid");
				}
			} else {
				Debug.LogWarning("Password Is invalid");
			}
		} else {
			Debug.LogWarning("Password Field Empty");
		}
		if (UN == true&&PW == true || !Debug.isDebugBuild)
        {
			username.GetComponent<InputField>().text = "";
			password.GetComponent<InputField>().text = "";	
			print ("Login Sucessful");
            SceneManager.LoadScene(2);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			if (username.GetComponent<InputField>().isFocused){
				password.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			if (Password != ""&&Password != ""){
				LoginButton();
			}
		}
		Username = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;	

	}

    public void RegisterButton()
    {
        SceneManager.LoadScene(1);
    }
}
