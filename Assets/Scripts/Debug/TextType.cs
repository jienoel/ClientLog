using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public enum TextType
{
	None,
	Continuours,
	Discrete
}

public enum LogChannel
{
	Warning = 0,
	Error = 1,
	Tcp = 2,
	Udp = 4,
	Default = 8,
	Specify = 16,
	UdpSt = 17,
	Statistic = 32,
	All = 300,
}

public class LogString
{

	public class LogFormat
	{
		public int index{ get; set; }
		public string prefix;
		public string postfix;
		public LogChannel channel;

		public LogFormat ()
		{
			index = 0;
			prefix = string.Empty;
			postfix = string.Empty;
			channel = LogChannel.Default;
		}

		public LogFormat (string prefix, string postfix, LogChannel channel = LogChannel.Default)
		{
			index = 0;
			this.prefix = prefix;
			this.postfix = postfix;
			this.channel = channel;
		}

		public string ConcatString (string text)
		{
			index++;
			return string.Format ("{4} -- {0} -- {1}{2}{3}", index, prefix, text, postfix, channel);
		}
	}

	public static Dictionary<LogChannel,LogFormat> lineNumbers = new Dictionary<LogChannel, LogFormat> ();

	public static void Reset (bool showInfo = false)
	{
		lineNumbers.Clear ();
		foreach (LogChannel channel in Enum.GetValues(typeof(LogChannel))) {
			lineNumbers.Add (channel, new LogFormat ());
		}
		if (showInfo) {
			ShowInfo ();
		}
	}

	public static void ShowInfo ()
	{
		foreach (KeyValuePair<LogChannel,LogFormat> c in lineNumbers) {
			LogManager.Log (c.Value.ConcatString (c.Key.ToString ()));
		}
	}

	public static string ArrayString (ArrayList array, string title, LogChannel channel = LogChannel.Default)
	{
		StringBuilder builder = new StringBuilder ();
		builder.AppendLine ("===============" + title + " count: " + array.Count.ToString () + "  type:" + array.GetType ().ToString () + "  start=================");
		foreach (var a in array) {
			Debug.Log (a.ToString ());
		}
		builder.AppendLine ("===============" + title + "  end=================End");
		return builder.ToString ();
	}

	public static string GetLogString (string text, LogChannel channel = LogChannel.Default)
	{
		return	string.Concat (lineNumbers [channel].ConcatString (text));
	}
}
