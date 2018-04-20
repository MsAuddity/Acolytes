using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFarm : Action {

	public GameObject[] inputs = new GameObject[1];
	public new string actionName = "Build Farm";


	public override void TakeAction (GameObject input) {
		if (gm.wood >= 50) {
			gm.wood -= 50;
			inputs[0] = input;
			Tile a = inputs[0].GetComponent<Tile>();
			Tile f = inputs[0].AddComponent<Farm>();
			f.people = a.people;
			f.tileType = Tile.TileType.Farm;
			gm.tiles.Remove(a);
			gm.tiles.Add(f);
			GameObject.Destroy(a);
			gm.selected = inputs[0].GetComponent<Tile>();
			ResetInput();
		}
	}

	public override void ResetInput() { 
		inputs = new GameObject[1]; 
		base.ResetInput();
	}
}
