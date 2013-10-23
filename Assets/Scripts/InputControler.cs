using UnityEngine;
using System.Collections;

public class InputControler : MonoBehaviour {
	
	public float cameraTiggerWidth = 10.0f;
	public float cameraSpeed = 12.0f;
	
	private GameObject mainCamera;
	private Cursor cursor;
	
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Camera");	
		cursor = GameObject.Find("Cursor").GetComponent<Cursor>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float mPosX = cursor.mousePosition.x;
		float mPosY = cursor.mousePosition.y;
		
		Vector3 cameraMoveDirection = Vector3.zero;
		if((mPosX < cameraTiggerWidth) || (mPosX > Screen.width - cameraTiggerWidth)
			||(mPosY < cameraTiggerWidth) ||(mPosY > Screen.height - cameraTiggerWidth)){
			cameraMoveDirection = new Vector3(mPosX / Screen.width - 0.5f, 0, -(mPosY / Screen.height - 0.5f));
			
		}
		
		mainCamera.transform.Translate(cameraMoveDirection * Time.deltaTime * cameraSpeed,Space.World);
		
	}
}
