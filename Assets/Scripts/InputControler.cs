using UnityEngine;
using System.Collections;

public class InputControler : MonoBehaviour
{
	
	public float cameraTiggerWidth = 10.0f;
	public float cameraSpeed = 12.0f;
	public GUITexture tower1;
	public Texture2D tower1_enable;
	public GUITexture tower1_disable;
	public GUITexture tower2;
	public Texture2D tower2_enable;
	public GUITexture tower2_disable;
	public GUITexture tower3;
	public Texture2D tower3_enable;
	public GUITexture tower3_disable;
	Camera mainCamera;
	Cursor cursor;
	GameControl gameControl;
	
	// Use this for initialization
	void Start ()
	{
		mainCamera = GameObject.Find ("Camera").camera;	
		cursor = GameObject.Find ("Cursor").GetComponent<Cursor> ();
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		
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
		Vector3 normalizedPoint = new Vector3(cursor.mousePosition.x ,Screen.height - cursor.mousePosition.y, 0);

		GUILayer gui = mainCamera.GetComponent<GUILayer>();
		if(gui.HitTest(normalizedPoint) != null){
			//Debug.Log(gui.HitTest(normalizedPoint).name);
			string name = gui.HitTest(normalizedPoint).name;

			if (name=="Tower1"&&Input.GetMouseButtonUp(0)){
				Debug.Log(name+" is clicked");
				tower1.texture = tower1_enable;
			}
			else if (name=="Tower2"&&Input.GetMouseButtonUp(0)){
				Debug.Log(name+" is clicked");
				tower2.texture = tower2_enable;
			}
			else if(name=="Tower3"&&Input.GetMouseButtonUp(0)) {
				Debug.Log(name+" is clicked");
				tower3.texture = tower3_enable;
			}
		}else{
			Ray ray = mainCamera.ScreenPointToRay(normalizedPoint);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){

				if(hit.collider.gameObject.tag.Equals("Clickable")){
					hit.collider.gameObject.GetComponent<Character>().Highlight();
					if(Input.GetMouseButtonUp(0)){
						gameControl.selectedObject = hit.collider.gameObject;
					}
				} else{
					if(Input.GetMouseButtonUp(0)){
						Debug.Log("Button up");
						Debug.DrawLine(new Vector3(hit.point.x - 1, hit.point.y + 0.1f, hit.point.z), new Vector3(hit.point.x + 1, hit.point.y + 0.1f, hit.point.z),Color.red, 1);
						Debug.DrawLine(new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z - 1), new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z + 1),Color.red, 1);
		
						CharacterControl charter = GameObject.Find("Sphere").GetComponent<CharacterControl>();
						GameObject target = GameObject.Find("Target");
						target.transform.position = hit.point;
						charter.move(target.transform);
					}
				}
			}
		}
	}
}