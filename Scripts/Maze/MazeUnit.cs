﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MazeUnit : MonoBehaviour {

	private const string NORTH = "North";
	private const string SOUTH = "South";
	private const string WEST = "West";
	private const string EAST = "East";

	public Vector2 GridID;
	public Waypoint Waypoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(Vector2 tilePos)
	{
		GridID = tilePos;
	}

	void OnDrawGizmos()
	{
		if (Waypoint != null && Waypoint.enabled)
		{
			Gizmos.DrawWireSphere(Waypoint.gameObject.transform.position, 0.2f);
		}
	}

	public void Open(string directionName)
	{
		transform.FindChild(directionName).gameObject.SetActive(false);
	}

	public void Close(string directionName)
	{
		transform.FindChild(directionName).gameObject.SetActive(true);
	}

	public static void Join(IEnumerable<MazeUnit> units)
	{
		System.Diagnostics.Debug.Assert(units.Any(), "should never called with empty enumerable");

		var unitsOrderedByColum = units.OrderBy((u) => u.GridID.x);
		var unitsOrderedByRow = units.OrderBy((u) => u.GridID.y);

		var columnStack = new Stack<MazeUnit>(unitsOrderedByColum);
		var rowStack = new Stack<MazeUnit>(unitsOrderedByRow);

		MazeUnit current = null;

		while (rowStack.Any())
		{

			if (current == null) {
				current = rowStack.Pop();
			}

			var lookAhead = rowStack.Peek();

			Debug.Log(string.Format("Current Grid ID {0} {1} vs LookAhead Grid ID {2} {3}",
				current.GridID.x, current.GridID.y, lookAhead.GridID.x, lookAhead.GridID.y));

			if (lookAhead.GridID.y == current.GridID.y + 1)
			{
				current.Open(NORTH);
				lookAhead.Open(SOUTH);
			}
			else if (lookAhead.GridID.y == current.GridID.y - 1)
			{
				current.Open(SOUTH);
				lookAhead.Open(NORTH);
			}

			current = rowStack.Pop(); 
		}

		current = null;

		while (columnStack.Any())
		{
			if (current == null)
			{
				current = columnStack.Pop();
			}

			var lookAhead = columnStack.Peek();

			if (lookAhead.GridID.x == current.GridID.x + 1)
			{
				current.Open(EAST);
				lookAhead.Open(WEST);
			}
			else if (lookAhead.GridID.x == current.GridID.x - 1)
			{
				lookAhead.Open(EAST);
				current.Open(WEST);
			}

			current = columnStack.Pop();
		}
	}

	public static void Split(IEnumerable<MazeUnit> units)
	{
		System.Diagnostics.Debug.Assert(units.Any(), "should never called with empty enumerable");

		
		var unitsOrderedByColum = units.OrderBy((u) => u.GridID.x);
		var unitsOrderedByRow = units.OrderBy((u) => u.GridID.y);

		var columnStack = new Stack<MazeUnit>(unitsOrderedByColum);
		var rowStack = new Stack<MazeUnit>(unitsOrderedByRow);

		MazeUnit current = null;

		while (rowStack.Any())
		{

			if (current == null) {
				current = rowStack.Pop();
			}

			var lookAhead = rowStack.Peek();

			Debug.Log(string.Format("Current Grid ID {0} {1} vs LookAhead Grid ID {2} {3}",
				current.GridID.x, current.GridID.y, lookAhead.GridID.x, lookAhead.GridID.y));

			if (lookAhead.GridID.y == current.GridID.y + 1)
			{
				current.Close(NORTH);
				lookAhead.Close(SOUTH);
			}
			else if (lookAhead.GridID.y == current.GridID.y - 1)
			{
				current.Close(SOUTH);
				lookAhead.Close(NORTH);
			}

			current = rowStack.Pop(); 
		}

		current = null;

		while (columnStack.Any())
		{
			if (current == null)
			{
				current = columnStack.Pop();
			}

			var lookAhead = columnStack.Peek();

			if (lookAhead.GridID.x == current.GridID.x + 1)
			{
				current.Close(EAST);
				lookAhead.Close(WEST);
			}
			else if (lookAhead.GridID.x == current.GridID.x - 1)
			{
				lookAhead.Close(EAST);
				current.Close(WEST);
			}

			current = columnStack.Pop();
		}
	
	}

}
