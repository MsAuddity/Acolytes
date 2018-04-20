using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

	public Tile selected;
	public List<Tile> tiles = new List<Tile>(0);

	public Action act = null;

	public int food = 100;
	public int mFood = 100;
	public int people;
	public int wood = 100;
	public int mWood = 100;

	public float updateDelay = 1;
	public float timer;

	public Text text1, text2, text3;
	public Text fText, pText, wText;

	public GameObject button;
	public bool actionsDisplayed = false;
	public List<GameObject> buttonCollect = new List<GameObject>(0);

	void Start() {
		Tile[] tilesGet = FindObjectsOfType<Tile>();
		foreach (Tile t in tilesGet) {
			tiles.Add(t);
		}
		timer = updateDelay;
	}

	void Update () {
		// Timed events
		timer -= Time.deltaTime;
		if (timer < 0) {
			timer = updateDelay;
			int ppl = 0;
			foreach (Tile t in tiles) {
				ppl+=t.people;
				t.PassiveEffect();
			}
			people = ppl;
			food -= people-1;
			if (food < 0) {
				for (int i = -food; i > 0; i--) {
					bool killedPeople = false;
					int t = Random.Range(0,tiles.Count);
					while (!killedPeople) {
						if (tiles[t].people > 0) {
							tiles[t].people--;
							killedPeople = true;
						}
						else {
							t++;
							if (t == tiles.Count) t = 0;
						}
					}
				}
				food = 0;
			}
		}
		// Update status display
		fText.text = "Food: "+food+"/"+mFood;
		pText.text = "Population: "+people;
		wText.text = "Wood: "+wood+"/"+mWood;
		// Controls
		// Click to select something
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(r, out hit)) {
				if (hit.collider.gameObject.CompareTag("Tile")) {
					if (act == null) {
						selected = hit.collider.gameObject.GetComponent<Tile>();
						actionsDisplayed = false;
					} else {
						act.TakeAction(hit.collider.gameObject);
					}
				} else {
					selected = null;
				}
			} else if (!EventSystem.current.IsPointerOverGameObject()) {
				selected = null;
			}
		}
		// Display information and commands
		if (selected != null) {
			text1.text = selected.GetTileType().ToString();
			text2.text = "People: " +selected.people.ToString();
			text3.text = selected.GetInfo();
			if (!actionsDisplayed) {
				foreach (GameObject g in buttonCollect) {
					Destroy(g);
				}
				buttonCollect = new List<GameObject>(0);
				for (int i = 0; i < selected.actions.Length; i++) {
					int tmp = i;
					GameObject b = Instantiate(button);
					b.GetComponentInChildren<Text>().text = selected.actions[tmp].actionName;
					b.transform.SetParent(GameObject.FindGameObjectWithTag("UI").transform, false);
					b.GetComponent<RectTransform>().localPosition += new Vector3(70*i, 0);
					b.GetComponent<Button>().onClick.AddListener(() => selected.actions[tmp].TakeAction(selected.gameObject));
					b.GetComponent<Button>().onClick.AddListener(Deselect);
					buttonCollect.Add(b);
				}
				actionsDisplayed = true;
			}
		} else {
			foreach (GameObject g in buttonCollect) {
				Destroy(g);
			}
			buttonCollect = new List<GameObject>(0);
			actionsDisplayed = false;
			text1.text = string.Empty;
			text2.text = string.Empty;
			text3.text = string.Empty;
		}
	}

	public void Deselect() { selected = null; }
}
