﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class beMobileMaze : MonoBehaviour {

	public float MazeWidthInMeter = 6f;
	public float MazeLengthInMeter = 10f;
	public float RoomHigthInMeter = 2;
	
	public Vector3 RoomDimension = new Vector3(1.3f, 2, 1.3f);
	public Vector2 EdgeDimension = new Vector2(0.1f, 0.1f);

	public float WallThicknessInMeter = 0.1f;

	public int Rows;
	public int Columns;

	public List<GameObject> Walls;
	public List<GameObject> Edges;
	public List<GameObject> Waypoints;

	[HideInInspector]
	public Vector3 MarkerPosition;

	public List<MazeUnit> Units = new List<MazeUnit>();

	 
	private Vector3 origin;
	[HideInInspector]
	public bool drawEditingHelper = true;

	void OnEnable() 
	{
		origin = gameObject.transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnDrawGizmos()
	{
		drawFloorGrid();
 
		// Draw marker position
		if (drawEditingHelper) {  
			Gizmos.color = Color.red;    
			Gizmos.DrawWireCube(this.MarkerPosition + new Vector3(0,RoomHigthInMeter / 2,0), new Vector3(RoomDimension.x, RoomHigthInMeter, RoomDimension.z) * 1.1f);
		}
	}

	private void drawFloorGrid() 
	{
		// store map width, height and position
		var mapWidth = MazeWidthInMeter;
		var mapHeight = MazeLengthInMeter;
		var position = this.transform.position;

		// draw layer border
		Gizmos.color = Color.white;
		Gizmos.DrawLine(position, position + new Vector3(mapWidth, 0, 0));
		Gizmos.DrawLine(position, position + new Vector3(0, 0, mapHeight));
		Gizmos.DrawLine(position + new Vector3(mapWidth, 0, 0), position + new Vector3(mapWidth, 0, mapHeight));
		Gizmos.DrawLine(position + new Vector3(0, 0, mapHeight), position + new Vector3(mapWidth, 0, mapHeight));

		Columns = Mathf.FloorToInt(MazeWidthInMeter / RoomDimension.x);
		Rows = Mathf.FloorToInt(MazeLengthInMeter / RoomDimension.z);

		// draw tile cells
		Gizmos.color = Color.grey;
		for (float i = 1; i < Columns + 1; i++)
		{
			Gizmos.DrawLine(position + new Vector3(i * RoomDimension.x, 0, 0), position + new Vector3(i * RoomDimension.x, 0, mapHeight));
		}

		for (float i = 1; i < Rows + 1; i++)
		{
			Gizmos.DrawLine(position + new Vector3(0, 0, i * RoomDimension.z), position + new Vector3(mapWidth, 0, i * RoomDimension.z));
		}
	}
}
