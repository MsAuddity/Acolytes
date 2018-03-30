using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferPeople : Action {
	public GameObject[] inputs = new GameObject[2];

	public override void TakeAction (GameObject input) {
		if (inputs[0] == null) {
			if (input.GetComponent<Tile>().people > 0) {
				inputs[0] = input;
				gm.act = this;
			}
		} else {
			inputs[1] = input;
			inputs[0].GetComponent<Tile>().people--;
			inputs[1].GetComponent<Tile>().people++;
			gm.selected = inputs[1].GetComponent<Tile>();
			ResetInput();
		}
	}

	public override void ResetInput() { 
		inputs = new GameObject[2]; 
		base.ResetInput();
	}
}
