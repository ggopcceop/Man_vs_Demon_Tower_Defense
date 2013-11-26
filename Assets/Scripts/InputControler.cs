using UnityEngine;
using System.Collections;

public class InputControler : MonoBehaviour
{
	
	public float cameraTiggerWidth = 10.0f;
	public float cameraSpeed = 12.0f;
	public GUITexture tower1;
	public Texture2D tower1_enable;
	public Texture2D tower1_disable;
	public GUITexture tower2;
	public Texture2D tower2_enable;
	public Texture2D tower2_disable;
	public GUITexture tower3;
	public Texture2D tower3_enable;
	public Texture2D tower3_disable;

	public GameObject Tower1Object;
	public GameObject Tower2Object;
	public GameObject Tower3Object;

	int enableButton = 0;

	Camera mainCamera;
	Cursor cursor;
	GameControl gameControl;
	public GameObject buildingTowner;
	private GamePlayGUI gamePlayGUI;
	
	// Use this for initialization
	void Start ()
	{
		mainCamera = GameObject.Find ("Camera").camera;	
		cursor = GameObject.Find ("Cursor").GetComponent<Cursor> ();
		gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
		gamePlayGUI = GameObject.Find("Panel").GetComponent<GamePlayGUI>();
		
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

		if(gameControl.currentPlayerState == GameControl.PlayerState.Building){
			Ray ray = mainCamera.ScreenPointToRay(normalizedPoint);
			RaycastHit hit;
			LayerMask mask = (1 << 9);
			if(Physics.Raycast(ray, out hit, 100, mask)){
				Vector3 offset = buildingTowner.transform.position 
					- buildingTowner.transform.FindChild("TowerBase").position;
				buildingTowner.transform.position = hit.point + offset;

				if(Input.GetMouseButtonUp(0)){
					gameControl.currentPlayerState = GameControl.PlayerState.Normal;
					buildingTowner.collider.enabled = true;
					buildingTowner.GetComponent<AITower>().enable = true;
					buildingTowner.renderer.material.SetColor("_Color", Color.white);
					buildingTowner = null;

					switch(enableButton){
					case 1:
						tower1.texture = tower1_disable;
						break;
					case 2:
						tower2.texture = tower2_disable;
						break;
					case 3:
						tower3.texture = tower3_disable;
						break;
					}
					enableButton = 0;
				}

			}
		} else {		
			GUILayer gui = mainCamera.GetComponent<GUILayer>();
			//test if player clicked on gui
			if(gui.HitTest(normalizedPoint) != null){
				//Debug.Log(gui.HitTest(normalizedPoint).name);
				if(Input.GetMouseButtonUp(0)){
					string name = gui.HitTest(normalizedPoint).name;
					//if player clicked start button, start the game. enemy waves
					if (name == "Start_button"){
						Debug.Log(name+"is clicked");
						gameControl.currentGameState = GameControl.GameState.Playing;
					}
					//if player clicked tower1 button, build tower 1 object
					else if (name=="Tower1"){
						Debug.Log(name+" is clicked");
						tower1.texture = tower1_enable;
						if (gamePlayGUI.cashCount>=10){
							buildTower(Tower1Object);
							enableButton = 1;
							//gamePlayGUI.cashCount -= gamePlayGUI.tower1Cost;
							gamePlayGUI.cashCount -= 10;
						}
					}
					//if player clicked tower2 button, build tower 2 object
					else if (name=="Tower2"){
						Debug.Log(name+" is clicked");
						tower2.texture = tower2_enable;
						if (gamePlayGUI.cashCount>=20){
							buildTower(Tower2Object);
							enableButton = 2;
							//gamePlayGUI.cashCount -= gamePlayGUI.tower2Cost;
							gamePlayGUI.cashCount -= 20;
						}
					}
					//if player clicked tower3 button, build tower 3 object
					else if(name=="Tower3") {
						Debug.Log(name+" is clicked");
						tower3.texture = tower3_enable;
						if (gamePlayGUI.cashCount>=30){
							buildTower(Tower3Object);
							enableButton = 3;
							//gamePlayGUI.cashCount -= gamePlayGUI.tower3Cost;
							gamePlayGUI.cashCount -= 30;
						}
					}
					//if player clicked menu button, shows menu
					else if (name=="Menu_button"){
						Debug.Log(name+"is clicked");
						Application.LoadLevel(0);

					}
					//retry this level
					else if (name=="Retry"){
						Debug.Log(name +"is clicked");
						//reload currect level
						print (Application.loadedLevelName);
						if(Application.loadedLevelName=="Level1"){
							print (Application.loadedLevelName);
							Application.LoadLevel(3);
						} 
						else if (Application.loadedLevelName=="Level2"){
							print (Application.loadedLevelName);
							Application.LoadLevel(4);
						}
						else if (Application.loadedLevelName=="Level3"){
							print (Application.loadedLevelName);
							Application.LoadLevel(5);
						}
					}
					//entry next level
					else if (name=="Next"){
						Debug.Log(name+"is clicked");
						//load Next level
						if(Application.loadedLevelName=="Level1"){
							print (Application.loadedLevelName);
							Application.LoadLevel(4);
						} 
						else if (Application.loadedLevelName=="Level2"){
							print (Application.loadedLevelName);
							Application.LoadLevel(5);
						}
						else if (Application.loadedLevelName=="Level3"){
							print (Application.loadedLevelName);
							Application.LoadLevel(7);
						}
					}

				}
			//test player clicked ingame objects
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
			
							CharacterControl character = gameControl.player.GetComponent<CharacterControl>();
							GameObject target = GameObject.Find("Target");
							target.transform.position = hit.point;
							character.move(target.transform);
						}
					}
				}
			}
		}
	}

	void buildTower(GameObject tower){
		gameControl.currentPlayerState = GameControl.PlayerState.Building;
		buildingTowner = Instantiate (tower, Vector3.zero, tower.transform.rotation) as GameObject;
		buildingTowner.collider.enabled = false;
		buildingTowner.GetComponent<AITower>().enable = false;
		buildingTowner.renderer.material.SetColor("_Color", Color.green);
	}
}