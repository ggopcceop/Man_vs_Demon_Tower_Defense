using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour {
	
	public Vector2 mousePosition;
	public float mouseSensitivity = 15.0f;
	public Texture cursor;
	
	// Use this for initialization
	void Start () {
		mousePosition.x = Screen.width / 2;
		mousePosition.y = Screen.height / 2;
		
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Screen.lockCursor = true;
		
		float mDeltaX = Input.GetAxis ("Mouse X");
		float mDeltaY = Input.GetAxis ("Mouse Y");
		
		mousePosition += new Vector2(mDeltaX, -mDeltaY) * mouseSensitivity;
		
		if(mousePosition.x < 0){
			mousePosition.x = 0;
		}else if(mousePosition.x > Screen.width){
			mousePosition.x = Screen.width;
		}
		
		if(mousePosition.y < 0){
			mousePosition.y = 0;
		}else if(mousePosition.y > Screen.height){
			mousePosition.y = Screen.height;
		}
		
	}
	
	void OnGUI(){
		GUI.Label(new Rect(mousePosition.x, mousePosition.y, 35, 35), cursor);
	}
	
	void OnMouseDown() {
        Screen.lockCursor = true;
		Screen.showCursor = false;
    }
}
