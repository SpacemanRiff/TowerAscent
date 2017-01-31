using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side {

	private GameObject sideGameObject;
	private GameObject parent;
	private Level myLevel;

	public Side(GameObject side) {
		this.sideGameObject = side;
	}

	public void SetLevel(Level level) {
		myLevel = level;
	}

	public void ScaleSide() {
		
	}

	public void GetTransform() {
	
	}


}
