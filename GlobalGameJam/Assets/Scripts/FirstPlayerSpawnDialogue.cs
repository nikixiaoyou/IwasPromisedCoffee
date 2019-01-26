using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerSpawnDialogue : MonoBehaviour
{

	[SerializeField]
	private float _timeWaitBeforeSpawnDialogue = 0f;

	[SerializeField]
	private GameObject _dialogueTonInstantiate;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DialogueCoroutine());
	}



	private IEnumerator DialogueCoroutine()
	{

		yield return new WaitForSeconds(_timeWaitBeforeSpawnDialogue);

		GameObject.Instantiate(_dialogueTonInstantiate, this.transform);
	}

}
