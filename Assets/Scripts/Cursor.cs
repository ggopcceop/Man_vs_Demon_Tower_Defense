using UnityEngine;
using System.Collections;

public class Cursor : MonoBehaviour
{
	
	public Vector2 mousePosition;
	public float mouseSensitivity = 15.0f;
	public Texture virtualCursor;
	bool wasLocked = false;
	
	// Use this for initialization
	void Start ()
	{
		mousePosition.x = Screen.width / 2;
		mousePosition.y = Screen.height / 2;
		
		Screen.lockCursor = true;
		Screen.showCursor = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		//lock the cursor on the screen
		LockCursor ();
		
		//if the cursor is locked, move virtual cursor by mouse input
		if(wasLocked){		
			float mDeltaX = Input.GetAxis ("Mouse X");
			float mDeltaY = Input.GetAxis ("Mouse Y");
			
			//update mouse position
			mousePosition += new Vector2 (mDeltaX, -mDeltaY) * mouseSensitivity;
			
			//limit the mouse inside the screen
			if (mousePosition.x < 0) {
				mousePosition.x = 0;
			} else if (mousePosition.x > Screen.width) {
				mousePosition.x = Screen.width;
			}
			
			if (mousePosition.y < 0) {
				mousePosition.y = 0;
			} else if (mousePosition.y > Screen.height) {
				mousePosition.y = Screen.height;
			}
		}
	}
	
	//draw the virtual cursor on the screen
	void OnGUI ()
	{
		GUI.Label (new Rect (mousePosition.x, mousePosition.y, 35, 35), virtualCursor);
	}
	
	void LockCursor ()
	{
		//if esc key pressed, release the lock
		if (Input.GetKeyDown ("escape")){
			Screen.lockCursor = false;
			Screen.showCursor = true;
			wasLocked = false;
		//or hold the lock
		}else if(Input.anyKeyDown){
			Screen.lockCursor = true;
			Screen.showCursor = false;
			wasLocked = true;
		}
	}
}
