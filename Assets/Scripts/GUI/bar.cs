using UnityEngine;
using System.Collections;

public class bar : MonoBehaviour {
	private UISlider slider;
	private float maxWidth;
	public UILabel label;
	private bool displayText = false;
	void Awake(){
		slider = GetComponent<UISlider>();
		if (slider == null){
			Debug.LogError("UISlider cannot found");
			return;
		}
		maxWidth = slider.foreground.localScale.x;
		
		DisplayText = displayText;
	}
	
	void Start(){
		//UpdateDisplay(.5f);
	}
	
	public void UpdateDisplay(float x){
		if (x<0){
			x=0;
		}
		else if (x>1){
			x=1;
		}
		slider.foreground.localScale = new Vector3(maxWidth*x,slider.foreground.localScale.y,slider.foreground.localScale.z);
		DisplayText = false;
	}
	public void UpdateDisplay(float x, string str){
		UpdateDisplay(x);
		if (str!="")
			label.text = str;
	}
	public bool DisplayText{
		get{return displayText;}
		set{
			displayText = value;
			if(!displayText){
				label.text = "";
			}
		}
	}
}
