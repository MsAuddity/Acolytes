using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {

	protected GameManager gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
	public virtual void TakeAction(GameObject input) {}
	public virtual void ResetInput() { gm.act = null; }
	public string actionName;
}
