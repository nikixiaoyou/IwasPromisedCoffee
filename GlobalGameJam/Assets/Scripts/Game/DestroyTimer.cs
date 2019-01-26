using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
	public float Timer;

	public void Start()
	{
		Invoke("DestroyObject", Timer);
	}

	private void DestroyObject()
	{
		Destroy(gameObject);
	}
}