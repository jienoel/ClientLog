using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugTest : MonoBehaviour
{
	public Button btnShowLogPath;

	public LogColor logColor;
	public LogChannel logChannel;
	public bool printLog;
	// Use this for initialization
	void Start ()
	{
	
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
			LogManager.Log ("test!", logColor, logChannel);
			printLog = false;
		}
	}
}
