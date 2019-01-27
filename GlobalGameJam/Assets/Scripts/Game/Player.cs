using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject DeadVFX;
	public GameObject Shell;

	private Vector2 _startingPosition;
    // Start is called before the first frame update
    private void Start()
    {
        _startingPosition = this.transform.position;
    }

	public void Reset()
	{
		PlaySmoke();

		this.transform.position = _startingPosition;

		GameObject goEnd = Instantiate(DeadVFX, new Vector3(this.transform.position.x, this.transform.position.y, -5f), Quaternion.identity);
		Destroy(goEnd, 2f);


		var rigidBody = this.GetComponent<Rigidbody2D>();
		if (rigidBody != null)
		{
			rigidBody.velocity = Vector2.zero;
		}
	}

	public void PlaySmoke()
	{
		GameObject go = Instantiate(DeadVFX, new Vector3(this.transform.position.x, this.transform.position.y, -5f), Quaternion.identity);
		go.GetComponent<AudioSource>().Play();
		Destroy(go, 2f);
	}

	public void StartDamagedAnimation()
	{
		StartCoroutine("AnimDamage");
	}

	private IEnumerator AnimDamage()
	{
		float totalTime = 0;
		SpriteRenderer render = Shell.GetComponent<SpriteRenderer>();
		Color color = Color.white;
		float c = 1;
		while (totalTime < 1)
		{
			c = Mathf.Cos(totalTime * 2 * Mathf.PI);
			c = c * .5f + 0.5f;
			color.g = c;
			color.b = c;
			render.color = color;
			totalTime += Time.deltaTime;
			yield return null;
		}
		color.g = 1;
		color.b = 1;
		render.color = color;
	}
}
