using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _dialogueRender;


	[SerializeField]
	private Transform _arrow;

	[SerializeField]
	private Transform _dialogue;

	[SerializeField]
	private Transform _bubble;

	[SerializeField]
	private float _timeShowBuble;

	[SerializeField]
	private float _timeFade;

	[System.Serializable]
	public struct DialogueData
	{
		public Sprite sprite;
		public float timeStay;
	}



	[SerializeField]
	List<DialogueData> dialogueData = new List<DialogueData>();


	const float toLerpXBuble = 2;
	const float toLerpYBuble = 3;

	private int currentIndex = 0;


	// Start is called before the first frame update
	void Awake()
    {
		_dialogueRender.sprite = null;

		_bubble.gameObject.SetActive(false);
		_dialogue.gameObject.SetActive(false);
		_arrow.gameObject.SetActive(false);

		LaunchDialogue();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private IEnumerator DialogueCoroutine()
	{

		float deltaTime = 0f;

		while (currentIndex != dialogueData.Count)
		{

			while (_bubble.localScale.x != toLerpXBuble)
			{
				deltaTime += 0.05f;
				float xScale = Mathf.Lerp(0, toLerpXBuble, deltaTime / _timeShowBuble);
				float yScale = Mathf.Lerp(0, toLerpYBuble, deltaTime / _timeShowBuble);

				_bubble.localScale = new Vector3(xScale, yScale, 1f);

				yield return new WaitForSeconds(0.05f);
			}

			deltaTime = 0.0f;

			_dialogueRender.sprite = dialogueData[currentIndex].sprite;


			while (_dialogueRender.color.a != 1)
			{
				deltaTime += 0.05f;

				float alpha = Mathf.Lerp(0, 1, deltaTime / _timeFade);
				_dialogueRender.color = new Color(1, 1, 1, alpha);

				yield return new WaitForSeconds(0.05f);
			}


			yield return new WaitForSeconds(dialogueData[currentIndex].timeStay);

			deltaTime = 0.0f;


			while (_dialogueRender.color.a != 0)
			{
				deltaTime += 0.05f;

				float alpha = Mathf.Lerp(1, 0, deltaTime / _timeFade);
				_dialogueRender.color = new Color(1, 1, 1, alpha);

				yield return new WaitForSeconds(0.05f);
			}

			_dialogueRender.sprite = null;
			deltaTime = 0.0f;

			while (_bubble.localScale.x != 0)
			{
				deltaTime += 0.05f;
				float xScale = Mathf.Lerp(toLerpXBuble, 0, deltaTime / _timeShowBuble);
				float yScale = Mathf.Lerp(toLerpYBuble, 0, deltaTime / _timeShowBuble);

				_bubble.localScale = new Vector3(xScale, yScale, 1f);

				yield return new WaitForSeconds(0.05f);
			}

			currentIndex++; 
			deltaTime = 0.0f;
		}


		_bubble.gameObject.SetActive(false);
		_dialogue.gameObject.SetActive(false);
		_arrow.gameObject.SetActive(false);

	}



	



	public void LaunchDialogue()
	{
		_arrow.gameObject.SetActive(true);
		_dialogue.gameObject.SetActive(true);

		_bubble.localScale = Vector3.zero;
		_bubble.gameObject.SetActive(true);


		currentIndex = 0;
		_dialogueRender.color = new Color(1, 1, 1, 0);

		StartCoroutine(DialogueCoroutine());
	}

}
