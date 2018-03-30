using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Tile {

	public int amnt = 100;
	public int maxAmnt = 100;

	public override string GetInfo() {
		return "Food: "+amnt.ToString()+"/"+maxAmnt.ToString();
	}
}
