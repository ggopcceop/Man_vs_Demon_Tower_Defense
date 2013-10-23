using UnityEngine;
using System.Collections;

public class InputControler : MonoBehaviour
{
	
	public float cameraTiggerWidth = 10.0f;
	public float cameraSpeed = 12.0f;
	
	private Camera mainCamera;
	private Cursor cursor;
	
	// Use this for initialization
	void Start ()
	{
		mainCamera = GameObject.Find ("Camera").camera;	
		cursor = GameObject.Find ("Cursor").GetComponent<Cursor> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//move camera when the cursor is at the edge of the screen
		moveCameraWhenEdge ();		
		
		MouseEvent();
		
	}
	
	//move camera when the cursor is at the edge of the screen
	void moveCameraWhenEdge ()
	{
		//get the mosuse position
		float mPosX = cursor.mousePosition.x;
		float mPosY = cursor.mousePosition.y;
		
		//set camera moving direction
		Vector3 cameraMoveDirection = Vector3.zero;
		if ((mPosX < cameraTiggerWidth) || (mPosX > Screen.width - cameraTiggerWidth)
			|| (mPosY < cameraTiggerWidth) || (mPosY > Screen.height - cameraTiggerWidth)) {
			cameraMoveDirection = new Vector3 (mPosX / Screen.width - 0.5f, 0, -(mPosY / Screen.height - 0.5f));
			
		}
		
		//camera will move if direction is not zero
		mainCamera.transform.Translate (cameraMoveDirection * Time.deltaTime * cameraSpeed, Space.World);
	}
	
	//function for mouse click events
	void MouseEvent ()
	{
		if (Input.GetMouseButton (0)) {
			Vector3 normalizedPoint = new Vector3(cursor.mousePosition.x ,Screen.height - cursor.mousePosition.y, 0);
			Ray ray = mainCamera.ScreenPointToRay(normalizedPoint);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				
			}
		}

	}
}