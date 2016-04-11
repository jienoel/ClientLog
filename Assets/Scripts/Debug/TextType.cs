using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public enum TextType
{
	None,  //不写入file
	Continuours, //写入同一个file中
	Discrete	//
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

public enum LogColor
{
	none = 0,
	red = 4,
	blue = 5,
	yellow = 6,
	black = 7,
	purple = 8,
	orange = 9,
	cyan = 10,
	gray = 11,
	green = 12,
	grey = 14,
	magenta = 15,
	white = 16
}


