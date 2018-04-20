using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : Tile {

	public int amnt = 100;
	public int maxAmnt = 100;
	GameManager gm;

	public override string GetInfo() {
		return "Wood: "+amnt.ToString()+"/"+maxAmnt.ToString();
	}

	void Awake() {
		actions = new Action[1];
		actions[0] = new TransferPeople();
	}

	public override void PassiveEffect() {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		} 
		if (gm != null) {
			for (int i = 0; i < this.people; i++) {
				if (amnt > 0 && gm.wood < gm.mWood) {
					gm.wood++;
					amnt--;
				}
			}
		}
		amnt+=3;
		if (amnt > maxAmnt) amnt = maxAmnt;
		if (amnt < 0) amnt = 0;
	}
}
