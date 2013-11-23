using UnityEngine;
using System.Collections;

public class bar : MonoBehaviour {
	private UISlider slider;
	public float maxWidth;
	public float currentHealth;
	private float maxWidthBackground;
	public UILabel label;
	private bool displayText = false;
	public GameObject target;
	//private GameObject barName;
	private GameObject parent;
	
	void Awake(){
		slider = GetComponent<UISlider>();
		if (slider == null){
			Debug.LogError("UISlider cannot found");
			return;
		}
		//slider.
		maxWidth = slider.foreground.localScale.x;
		//maxWidthBackground = transform.GetChild ().localScale.x;
		parent=transform.parent.gameObject.transform.parent.gameObject;
		maxWidth = parent.GetComponent<Character>().maxHealth;
		DisplayText = displayText;
	}
	
	void Start(){
		target = GameObject.Find ("Camera");
		
		if(transform.parent){
			Debug.Log(parent.name);
		}
		else{
			Debug.Log("Not found parent");
		}
		currentHealth = parent.GetComponent<Character>().currentHealth;
		
		//UpdateDisplay(currentHealth/maxHealth);
		//barName = this.gameObject;
	}
	void FixedUpdate(){
		UpdateDisplay(currentHealth/maxWidth*maxWidthBackground);
		transform.LookAt(target.transform);
		
	}
	//check for valid update health
	public void UpdateDisplay(float x){
		if (x<0){
			x=0;
		}
		else if (x>1){
			x=1;
		}
		//change slider (Health bar) value
		slider.foreground.localScale = new Vector3(maxWidth*x,slider.foreground.localScale.y,slider.foreground.localScale.z);
		DisplayText = false;
	}
	//update health bar & number
	public void UpdateDisplay(float x, string str){
		UpdateDisplay(x);
		if (str!="")
			label.text = str;
	}
	
	public bool DisplayText{
		get{
			return displayText;
		}
		set{
			displayText = value;
			if(!displayText){
				label.text = "";
			}
		}
	}
}
