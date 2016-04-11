using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogManager : MonoBehaviour
{
	public static TextType debugLog = TextType.None;
	public static bool writeToText = false;
	public static LogChannel channel = LogChannel.Default;
	public static bool logToConsole = false;
	public static LogType logType = LogType.Log;
	public static bool includeUp = false;

	public bool _includeUp = false;
	public bool _writeToText = false;
	public bool _logToConsole = false;
	public TextType _debugLog = TextType.None;
	public LogChannel _channel = LogChannel.Default;
	public LogType _logType = LogType.Log;
	public bool resetAtStart = false;
	public string fileName;
	[HideInInspector]
	public bool
		colorful = false;  //TODO
	static int lineNum = 1;

	static Dictionary<LogColor,string> colorMap = new Dictionary<LogColor,string> ();

	void Start ()
	{
		AddColorMap ();
		debugLog = _debugLog;
		writeToText = _writeToText;
		channel = _channel;
		logToConsole = _logToConsole;
		logType = _logType;
		includeUp = _includeUp;
		if (resetAtStart)
			Reset (_debugLog, fileName, colorful);
	}	


	void AddColorMap ()
	{
		colorMap.Add (LogColor.red, "<color=red>{0}</color>");
		colorMap.Add (LogColor.blue, "<color=blue>{0}</color>");
		colorMap.Add (LogColor.yellow, "<color=yellow>{0}</color>");
		colorMap.Add (LogColor.black, "<color=black>{0}</color>");
		colorMap.Add (LogColor.purple, "<color=purple>{0}</color>");
		colorMap.Add (LogColor.orange, "<color=orange>{0}</color>");
		colorMap.Add (LogColor.cyan, "<color=cyan>{0}</color>");
		colorMap.Add (LogColor.teal, "<color=teal>{0}</color>");
		colorMap.Add (LogColor.green, "<color=green>{0}</color>");
		colorMap.Add (LogColor.grey, "<color=grey>{0}</color>");
		colorMap.Add (LogColor.magenta, "<color=magenta>{0}</color>");
		colorMap.Add (LogColor.white, "<color=white>{0}</color>");
		colorMap.Add (LogColor.maroon, "<color=maroon>{0}</color>");
	}

	void Update ()
	{
		if (debugLog != _debugLog) {
			debugLog = _debugLog; 
		}
		
		if (writeToText != _writeToText) {
			writeToText = _writeToText;
		}

		if (channel != _channel) {
			channel = _channel;
		}

		if (logToConsole != _logToConsole) {
			logToConsole = _logToConsole;
		}

		if (logType != _logType) {
			logType = _logType;
		}

		if (includeUp != _includeUp) {
			includeUp = _includeUp;
		}
	}
	
	public static void Reset (TextType debugLog = TextType.None, string fileName = "", bool colorful = false)
	{
		if (debugLog != TextType.None) {
			TextToFile.Reset (debugLog, fileName, colorful);
		}	
	}

	void OnDestroy ()
	{
		CloseDegbug ();
	}
	
	public static void CloseDegbug ()
	{
		TextToFile.CloseDebug ();
	}

	public static void Log (string log, LogChannel channel, bool includeUp)
	{
		if (logToConsole) {
			if (includeUp && ((int)LogManager.channel >= (int)channel)) {
				Debug.Log (log);
			} else if (LogManager.channel == channel) {
				Debug.Log (log);
			}
		}
		
		if (writeToText) {
			if (includeUp && ((int)LogManager.channel >= (int)channel)) {
				TextToFile.WriteToFile (log, debugLog);
			} else if (LogManager.channel == channel) {
				TextToFile.WriteToFile (log, debugLog);
			}
		}
	}

	public static void Log (string log, LogChannel channel, bool includeUp, LogType logType)
	{
		if (logToConsole) {
			if ((includeUp && ((int)LogManager.channel >= (int)channel)) || LogManager.channel == channel) {
				if (logType == LogType.Log)
					Debug.Log (log);
				else if (logType == LogType.Error)
					Debug.LogError (log);
				else
					Debug.LogWarning (log);
			}
		}
		
		if (writeToText) {
			if (includeUp && ((int)LogManager.channel >= (int)channel)) {
				TextToFile.WriteToFile (log, debugLog);
			} else if (LogManager.channel == channel) {
				TextToFile.WriteToFile (log, debugLog);
			}
		}
	}

	public static void Log (string log, LogChannel logChannel = LogChannel.Default)
	{
		bool includeUp = false;
		bool controlByInside = true;
		bool isWriteToText = true;
		bool isLogToConsole = false;

		Log (log, logChannel, includeUp, logType, controlByInside, isWriteToText, isLogToConsole);

	}

	public static void Log (string log, LogColor logColor, LogChannel logChannel = LogChannel.Default)
	{
		bool includeUp = false;
		bool controlByInside = true;
		bool isWriteToText = true;
		bool isLogToConsole = false;
		
		Log (log, logChannel, includeUp, logType, controlByInside, isWriteToText, isLogToConsole, logColor);
	}

	public static void Log (string log, LogColor logColor)
	{
		bool includeUp = false;
		bool controlByInside = true;
		bool isWriteToText = true;
		bool isLogToConsole = false;

		Log (log, channel, includeUp, logType, controlByInside, isWriteToText, isLogToConsole, logColor);
	}

	public static void Log (string log, LogType logType)
	{
		LogChannel logChannel = LogChannel.Default;
		bool includeUp = false;
		bool controlByInside = true;
		bool isWriteToText = true;
		bool isLogToConsole = false;
		
		Log (log, logChannel, includeUp, logType, controlByInside, isWriteToText, isLogToConsole);
	}

	public static void Log (string log, LogChannel channel, bool includeUp, LogType logType, bool controlByInside, bool isWriteToText, bool isLogToConsole, LogColor logColor = LogColor.none)
	{
		
		if (controlByInside) {
			isWriteToText = writeToText;
			isLogToConsole = logToConsole;
			includeUp = LogManager.includeUp;
		}

		
		if (isLogToConsole) {
			if ((includeUp && ((int)LogManager.channel >= (int)channel)) || LogManager.channel == channel) {
				if (logColor != LogColor.none)
					log = string.Format (colorMap [logColor], log);
				if (logType == LogType.Log)
					Debug.Log (log);
				else if (logType == LogType.Error)
					Debug.LogError (log);
				else
					Debug.LogWarning (log);
			}
		}
			
		if (writeToText) {
			log = string.Format ("{0}. {1}", lineNum, log);
			lineNum++;
			if (includeUp && ((int)LogManager.channel >= (int)channel)) {
				TextToFile.WriteToFile (log, debugLog);
			} else if (LogManager.channel == channel) {
				TextToFile.WriteToFile (log, debugLog);
			}
		}
	}
}
