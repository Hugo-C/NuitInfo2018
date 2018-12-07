using UnityEngine;
using System.Collections;

public class MyCameraMenu : MonoBehaviour {
    
	private GameObject _player;
	private Vector3 _offset;
	
	public float zoomSpeed;
	public float zoomMin;
	public float zoomMax;
     
	private float zoom;

	// Use this for initialization
	void Start() {
		_player = GameObject.Find("RocketSmile");
		_offset = new Vector3(0, 0, -10f);
		transform.position = _player.transform.position + _offset;
		zoom = Camera.main.orthographicSize;
	}

	public void ZoomIn() {
		zoom = zoomMax;
	}
	
	public void ZoomOut() {
		zoom = zoomMin;
	}

	// LateUpdate is called after Update each frame
	private void LateUpdate() {
		if (_player != null) {
			//transform.position = Vector3.Lerp(transform.position, _player.transform.position + _offset, Smoothing);  // smooth transition (but appear laggy)
			transform.position =_player.transform.position + _offset;
		} else {
			Debug.LogWarning("camera can't find the player");
			_player = GameObject.Find("Player");
		}
		Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, zoom, Time.deltaTime * zoomSpeed);
	}
}