using System.Collections;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

[RequireComponent(typeof(HandController))]

public class HandControl : MonoBehaviour 
{
	[SerializeField]
	[TextArea(3,8)]
	public string strMessage;

	public static Vector3 offSetLeft = Vector3.zero;
	public static Vector3 offSetRight = Vector3.zero;

	private static int localPort;

	[SerializeField]
	private string smartphoneIPAddress = ""; // Smartphone's IP Address

	[SerializeField]
	private int port = 1999;  

	private IPEndPoint remoteEndPoint;
	private UdpClient client;

	[SerializeField]
	public HandController leapHandController;

	

	private void Awake() 
	{
		Application.runInBackground = true;
	}

	private void Start () 
	{	
		remoteEndPoint = new IPEndPoint(IPAddress.Parse(smartphoneIPAddress), port);
		client = new UdpClient();
		leapHandController = transform.GetComponent<HandController>();
		
		LoadCallibrationData();
		StartCoroutine (SendData ());
	}

	private void Update()
	{
		Debug.Log("Message: " + strMessage);
	}

	private void OnApplicationQuit()
	{
		SaveCallibrationData();
	}

	private void SaveCallibrationData()
	{
		//save offset x
		PlayerPrefs.SetFloat ("offsetLeftX", offSetLeft.x);
		PlayerPrefs.SetFloat ("offsetLeftY", offSetLeft.y);
		PlayerPrefs.SetFloat ("offsetLeftZ", offSetLeft.z);

		//save offset y
		PlayerPrefs.SetFloat ("offsetRightX", offSetRight.x);
		PlayerPrefs.SetFloat ("offsetRightY", offSetRight.y);
		PlayerPrefs.SetFloat ("offsetRightZ", offSetRight.z);
	}

	private void LoadCallibrationData()
	{
		if (PlayerPrefs.HasKey ("offsetLeftX")) {
			offSetLeft = new Vector3(PlayerPrefs.GetFloat("offsetLeftX"),PlayerPrefs.GetFloat("offsetLeftY"),PlayerPrefs.GetFloat("offsetLeftZ"));
		}
		if (PlayerPrefs.HasKey ("offsetRightX")) {
			offSetRight = new Vector3(PlayerPrefs.GetFloat("offsetRightX"),PlayerPrefs.GetFloat("offsetRightY"),PlayerPrefs.GetFloat("offsetRightZ"));
		}
	}

	public void ClearCallibrationData()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Callibration Data deleted.");
	}

	public void SetHeadMounted()
	{
		transform.eulerAngles = new Vector3(270.0f,180.0f,0.0f);
		leapHandController.isHeadMounted = true;

		Debug.Log("Head Mounted Enabled");
	}

	public void SetFlatSurface()
	{
		transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);
		leapHandController.isHeadMounted = false;

		Debug.Log("Flat Surface Enabled");
	}

	private IEnumerator SendData()
	{

		while (true) {

			if (transform.childCount > 0) 
			{
				
				strMessage = "";

				if (transform.Find ("CleanRobotFullLeftHand(Clone)") != null) 
				{

					Transform leftHand = transform.Find ("CleanRobotFullLeftHand(Clone)").transform.GetChild(2);

					Vector3 leftHandPosition = leftHand.position + offSetLeft;
					strMessage += "l," + leftHandPosition.ToString() + "," + leftHand.rotation.ToString() + ",";

				}
				if (transform.Find ("CleanRobotFullRightHand(Clone)") != null) 
				{
					Transform rightHand = transform.Find ("CleanRobotFullRightHand(Clone)").transform.GetChild(2);

					Vector3 rightHandPosition = rightHand.position + offSetRight;
					strMessage += "r," + rightHandPosition.ToString() + "," + rightHand.rotation.ToString() + ",";
				}

			} 
			else 
			{
				strMessage = "nothing";
			}	

			byte[] data = Encoding.UTF8.GetBytes (strMessage);

			var message = client.Send (data, data.Length, remoteEndPoint);
			yield return message;
		}
	}
 }
