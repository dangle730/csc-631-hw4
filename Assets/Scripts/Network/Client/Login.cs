using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;

public class Login : MonoBehaviour {
	
	private GameObject mainObject;
	// Window Properties
	private float width = 280;
	private float height = 200;
	// Other
	public Texture background;
	private string user_id = "";
	private string password = "";
	private Rect windowRect;
	private bool isHidden;
	private MessageQueue msgQueue;
	private ConnectionManager cManager;
	
	void Awake() {
		mainObject = GameObject.Find("MainObject");
		cManager = mainObject.GetComponent<ConnectionManager>();
		msgQueue = mainObject.GetComponent<MessageQueue> ();
		msgQueue.AddCallback(Constants.SMSG_AUTH, ResponseLogin);
		msgQueue.AddCallback(Constants.SMSG_PLAYERS, responsePlayers);
		msgQueue.AddCallback(Constants.SMSG_TEST, responseTest);
        msgQueue.AddCallback(Constants.SMSG_REGISTER, responseRegister);
    }
	
	// Use this for initialization
	void Start() {

	}
	
	void OnGUI() {
		// Background
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		// Client Version Label
		
		// Login Interface
		if (!isHidden) {
			windowRect = new Rect ((Screen.width - width) / 2, Screen.height / 2 - height, width, height);
			windowRect = GUILayout.Window((int) Constants.GUI_ID.Login, windowRect, MakeWindow, "Login");
			if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) {
				Submit();
			}
		}
	}
	
	void MakeWindow(int id) {
		GUILayout.Label("User ID");
		GUI.SetNextControlName("username_field");
		user_id = GUI.TextField(new Rect(10, 45, windowRect.width - 20, 25), user_id, 25);
		GUILayout.Space(30);
		GUILayout.Label("Password");
		GUI.SetNextControlName("password_field");
		password = GUI.PasswordField(new Rect(10, 100, windowRect.width - 20, 25), password, "*"[0], 25);
		GUILayout.Space(75);
		if (GUI.Button(new Rect(windowRect.width / 2 - 50, 135, 100, 30), "Log In"))
        {
			Submit();
		}
        if (GUI.Button(new Rect(windowRect.width / 2 - 50, 165, 100, 30), "Register"))
            Register();
	}
	
	public void Submit() {
		user_id = user_id.Trim();
		password = password.Trim();
		if (user_id.Length == 0) {
			Debug.Log("User ID Required");
			GUI.FocusControl("username_field");
		} else if (password.Length == 0) {
			Debug.Log("Password Required");
			GUI.FocusControl("password_field");
		} else {
			cManager.send(requestLogin(user_id, password));
		}
	}

    public void Register()
    {
        user_id = user_id.Trim();
        password = password.Trim();
        if (user_id.Length == 0)
        {
            Debug.Log("User ID Required");
            GUI.FocusControl("username_field");
        }
        else if (password.Length == 0)
        {
            Debug.Log("Password Required");
            GUI.FocusControl("password_field");
        }
        else
            cManager.send(requestRegister(user_id, password));
    }
	
	public RequestLogin requestLogin(string username, string password) {
		RequestLogin request = new RequestLogin();
		request.send(username, password);
		return request;
	}
	
	public void ResponseLogin(ExtendedEventArgs eventArgs) {
		ResponseLoginEventArgs args = eventArgs as ResponseLoginEventArgs;
		if (args.status == 0) {
			Constants.USER_ID = args.user_id;
			Debug.Log ("Successful Login response : " + args.ToString());
			EditorUtility.DisplayDialog ("Login Successful", "You have successfully logged in.\nClick Ok to continue execution and see responses on console", "Ok");
            SceneManager.LoadScene("SampleScene");
		} else {
			Debug.Log("Login Failed");
		}
	}
	
	public RequestPlayers requestPlayers() {
		RequestPlayers request = new RequestPlayers();
		request.send ();
		return request;
	}

	public void responsePlayers(ExtendedEventArgs eventArgs) {
		ResponsePlayersEventArgs args = eventArgs as ResponsePlayersEventArgs;
		int numActivePlayers = args.numActivePlayers;
		Debug.Log ("Number of Connected Players : " + numActivePlayers);
	}

	public RequestTest requestTest(string arithmeticOperator, int testNum) {
		RequestTest requestTest = new RequestTest ();
		requestTest.send (arithmeticOperator, testNum);
		return requestTest;
	}
	
	public void responseTest(ExtendedEventArgs eventArgs) {
		ResponseTestEventArgs args = eventArgs as ResponseTestEventArgs;
		Debug.Log ("newTestVar updated on server!!!");
	}

    public RequestRegister requestRegister(string username, string password) {
        RequestRegister request = new RequestRegister();
        request.send(username, password);
        return request;
    }

    public void responseRegister(ExtendedEventArgs eventArgs) {
        ResponseRegisterEventArgs args = eventArgs as ResponseRegisterEventArgs;
        if (args.status == 0)
        {
            Constants.USER_ID = args.user_id;
            Debug.Log("Successful registration response: " + args.ToString());
            EditorUtility.DisplayDialog("Registration successful", "You have successfully registered.\nClick Ok to continue execution and see responses on console", "Ok");
        }
        else
            Debug.Log("Registration failed: Username is taken.");
    }

	public void Show() {
		isHidden = false;
	}
	
	public void Hide() {
		isHidden = true;
	}
	
	// Update is called once per frame
	void Update() {
	}
}
