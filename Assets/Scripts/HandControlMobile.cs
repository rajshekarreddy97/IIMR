using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using UnityEngine;


public class HandControlMobile : MonoBehaviour 
{
	public GameObject rightHandPrefab,leftHandPrefab;

	private Thread receiveThread;
	private UdpClient client;

	[SerializeField]
	private int port = 1999; 

	private string[] DataArray = new string[100];

	private GameObject currentRightHand, currentLeftHand;

	private GameObject currentLeftThumb;

	private bool shouldUpdateHands = false;
	private bool shouldDestroyHands = false;

	private bool rightHandExists = false;	
	private bool leftHandExists = false;

	private Vector3 position1, position2;
	private Quaternion rotation1, rotation2;

	private Vector3[] positionArray = new Vector3[10];
	private Quaternion[] rotationArray = new Quaternion[10];

	[SerializeField]

	public void Start()
	{
		Application.targetFrameRate = 60;

		receiveThread = new Thread(new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start();
	}


	void Update()
	{
		if (shouldDestroyHands) 
		{
			shouldDestroyHands = false;
			if (transform.childCount > 0) 
			{
				DestroyHands ();
			}
		}

		if (shouldUpdateHands) 
		{
			shouldUpdateHands = false;
			UpdateHands ();
		}
	}


	private void UpdateHands()
	{
		rightHandExists = false;
		leftHandExists = false;
	
		if (DataArray.Length > 1) 
		{
			DataArray[1] = DataArray [1].TrimStart ('(');
			DataArray [3] = DataArray [3].TrimEnd (')');
			DataArray [4] = DataArray [4].TrimStart ('(');
			DataArray [7] = DataArray [7].TrimEnd (')');
			
			position1 = new Vector3(float.Parse(DataArray[1]),float.Parse(DataArray[2]),float.Parse(DataArray[3]));
			rotation1 = new Quaternion(float.Parse(DataArray[4]),float.Parse(DataArray[5]),float.Parse(DataArray[6]),float.Parse(DataArray[7]));

			if (DataArray [0] == "l") 
			{
				leftHandExists = true;
				if (currentLeftHand == null) 
				{
					currentLeftHand = Instantiate (leftHandPrefab, this.transform);
				}

				currentLeftHand.transform.transform.localPosition = position1;
				currentLeftHand.transform.transform.localRotation = rotation1;
			} 

			else if (DataArray [0] == "r") 
			{
				rightHandExists = true;
				if (currentRightHand == null) {
					currentRightHand = Instantiate (rightHandPrefab, this.transform);
				}

				currentRightHand.transform.transform.localPosition = position1;
				currentRightHand.transform.transform.localRotation = rotation1;
			}

		}

		if (DataArray.Length > 9) 
		{
			DataArray[9] = DataArray [9].TrimStart ('(');
			DataArray [11] = DataArray [11].TrimEnd (')');
			DataArray [12] = DataArray [12].TrimStart ('(');
			DataArray [15] = DataArray [15].TrimEnd (')');

			position2 = new Vector3(float.Parse(DataArray[9]),float.Parse(DataArray[10]),float.Parse(DataArray[11]));
			rotation2 = new Quaternion(float.Parse(DataArray[12]),float.Parse(DataArray[13]),float.Parse(DataArray[14]),float.Parse(DataArray[15]));

			if (DataArray [8] == "l") 
			{
				leftHandExists = true;
				if (currentLeftHand == null) {
					currentLeftHand = Instantiate (leftHandPrefab, this.transform);
				}

				currentLeftHand.transform.transform.localPosition = position2;
				currentLeftHand.transform.transform.localRotation = rotation2;
			} 
			else if (DataArray [8] == "r") 
			{
				rightHandExists = true;
				if (currentRightHand == null) {
					currentRightHand = Instantiate (rightHandPrefab, this.transform);
				}

				currentRightHand.transform.transform.localPosition = position2;
				currentRightHand.transform.transform.localRotation = rotation2;
			}
		}	

		if (!leftHandExists) 
		{
			Destroy (currentLeftHand);
		}

		if (!rightHandExists) 
		{
			Destroy (currentRightHand);
		}
	}


	void DestroyHands()
	{
		foreach (Transform child in gameObject.transform) 
		{

			Destroy (child.gameObject);
		}
	}


	// receive thread
	void ReceiveData()
	{
		client = new UdpClient(port);
		IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);

		while (true)
		{
			byte[] data = client.Receive(ref anyIP);

			string text = Encoding.UTF8.GetString(data);

			// split the items by comma
			DataArray = text.Split(',');

			if (DataArray[0] != "nothing")
			{
				shouldUpdateHands =  true;
			} 
			else 
			{
				shouldDestroyHands = true;
			}
		}
	}
}
