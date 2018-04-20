using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Tile {

	public bool built = true;
	public float buildTime = 0;
	public int amnt = 100;
	public int maxAmnt = 100;
	GameManager gm;

	void Awake() {
		actions = new Action[1];
		actions[0] = new TransferPeople();
	}

	public override string GetInfo() {
		if (built) return "Food: "+amnt.ToString()+"/"+maxAmnt.ToString();
		else return "Build Progress: "+buildTime.ToString();
	}

	public override void PassiveEffect() {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
		} 
		if (gm != null) {
			for (int i = 0; i < this.people; i++) {
				if (amnt > 0 && gm.food < gm.mFood) {
					gm.food++;
					amnt--;
				}
			}
		}
		amnt+=3;
		if (amnt > maxAmnt) amnt = maxAmnt;
		if (amnt < 0) amnt = 0;
	}
}
