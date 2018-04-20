using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public enum TileType {
		Empty,
		Farm,
		Forest
	}

	public TileType tileType = TileType.Empty;
	public int people = 0;

	public virtual void PassiveEffect() { }

	public TileType GetTileType() { return tileType; }

	public virtual string GetInfo() { return string.Empty; }

	public Action[] actions;

	protected void Awake() {
		actions = new Action[2];
		actions[0] = new TransferPeople();
		actions[1] = new BuildFarm();
	}
}
