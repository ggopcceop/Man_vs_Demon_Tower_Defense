using UnityEngine;
using System.Collections;

public class Driver : MonoBehaviour {
	public bar bar;
	public string str;
	public int curValue;
	public int maxValue;
	
	public bool displayText = false;
	
	void OnClick(){
		if (!displayText)
			bar.UpdateDisplay((float)curValue/maxValue);
		else
			bar.UpdateDisplay((float)curValue/maxValue,curValue+"/"+maxValue);
	} 
}
