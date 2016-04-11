using System;
using System.IO;
using System.Text;
using UnityEngine;

public class TextToFile
{
	public static  string FILE_PATH = @"..\Game\";
	//public static  string FILE_PATH = @"..\Game\EffectsLog.txt";
	public static  string FILE_PATH_Discrete = @"..\Game\EffectLog\EffectsLog";
	private static string  timestamp = "";
	public static string path = @"..\Game\EffectLog\EffectsLog1.txt";
	public static string folderName = "EffectLog";
	public static string SERVER_LOG_PATH = @"..\Game\S_Battle_PrintAttr_0x213.txt";
	public static string SpriteLogPath = Application.dataPath + "/../SpriteLog.txt";
	public static StreamWriter sr = null;

	public static void Reset (TextType debugLog = TextType.None, string fileName = "", bool colorful = false)
	{
#if UNITY_EDITOR
		if (sr != null)
			return;
		Debug.Log ("3--->colorful:" + colorful);
		if (debugLog == TextType.Continuours) {
			FILE_PATH = Application.dataPath + "/../EffectsLog_" + fileName + (colorful ? ".rtf" : ".txt");
			if (!File.Exists (FILE_PATH)) {
				UnityEngine.Debug.Log (FILE_PATH);
				sr = File.CreateText (FILE_PATH);
			} else {
				sr = File.AppendText (FILE_PATH);
			}
		} else if (debugLog == TextType.Discrete) {
			timestamp = System.DateTime.Now.Day.ToString () + "_" + System.DateTime.Now.Hour.ToString ()
				+ "_" + System.DateTime.Now.Minute.ToString () + "_" + System.DateTime.Now.Second.ToString ();
			path = Application.dataPath + "/../EffectLog/EffectsLog_" + fileName + timestamp + (colorful ? ".rtf" : ".txt");
			string folderPath = Application.dataPath + "/../" + folderName;
			if (!Directory.Exists (folderPath)) {
				Directory.CreateDirectory (folderPath);
			}
			if (!File.Exists (path)) {
				sr = File.CreateText (path); 
			} else {
				sr = File.AppendText (path);
			}
		}	
#endif
	}

	public static void CloseDebug ()
	{
		if (sr != null)
			sr.Close ();
		sr = null;
	}

	public static void SpriteOperationLogToFile (string text)
	{
		#if UNITY_EDITOR
		if (!File.Exists (SpriteLogPath)) {
			UnityEngine.Debug.Log (SpriteLogPath);
			StreamWriter sw = File.CreateText (SpriteLogPath);
			sw.WriteLine (text);
			sw.Close (); 
		} else {
			using (StreamWriter sw = File.AppendText(SpriteLogPath)) {
				sw.WriteLine (text);
			}
		}   
#endif
	}

	public static void WriteToFile (string text, TextType debugLog = TextType.None)
	{
		#if UNITY_EDITOR
		if (sr == null) {
			Reset (debugLog);
		}
		if (debugLog == TextType.Continuours) {
			sr.WriteLine (text);            	
		} else if (debugLog == TextType.Discrete) {
			sr.WriteLine (text);
		} 
#endif
	}
}