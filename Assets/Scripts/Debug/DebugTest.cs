using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugTest : MonoBehaviour
{
	public Text pathFolder;
	public Button btnShowLogPath;
	public bool printLog;
	public uint id1;
	public uint id2;
	// Use this for initialization
	void Start ()
	{
	
	}

	public void ShowLogPath ()
	{
		pathFolder.text = TextToFile.path;
	}

	public void ResetLogPath ()
	{
		TextToFile.Reset (TextType.Discrete);
	}

	// Update is called once per frame
	void Update ()
	{
		if (printLog) {
//			Debug.Log (LogStringUtil.ConvertId1ToString (1));
//			Debug.Log (LogStringUtil.ConvertId2ToString (1, 5));
//			Debug.Log (LogStringUtil.ConvertId1ToString (2));
//			Debug.Log (LogStringUtil.ConvertId2ToString (2, 201));
			printLog = false;
		}
	}
}
