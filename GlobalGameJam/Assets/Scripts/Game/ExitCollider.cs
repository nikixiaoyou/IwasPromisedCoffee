using ggj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCollider : MonoBehaviour
{
    private const float _kTransitionTime = 1.0f;

	private AudioSource audioSource;

    private bool _loading = false;

	private void Awake()
	{
		audioSource = this.GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	protected void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        GoToNextScene();
    }

#if CHEAT
    protected void Update()
    {
        if(Input.GetKey(KeyCode.N))
        {
            GoToNextScene();
        }
    }
#endif

    public void GoToNextScene()
    {
        if (_loading)
        {
            return;
        }
		if (audioSource != null)
		{
			audioSource.Play();
		}

		_loading = true;
        GameManager.Instance.StartCoroutine(GoToNextSceneRoutine());
    }

    private IEnumerator GoToNextSceneRoutine()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        var player = GameManager.Instance.Services.Get<PlayerController>();

		player.GetComponent<CircleCollider2D>().enabled = false;
		player.GetComponent<AudioSource>().enabled = false;
		player.enabled = false;

		float currentPositionX = player.transform.position.x;


		while(player.transform.position.x < currentPositionX + 6f )
		{
			player.transform.position += new Vector3( 0.05f, 0, 0);
			yield return new WaitForSeconds(0.05f);
		}

		
        GameManager.Instance.Destroy();

        var gos = SceneManager.GetSceneByBuildIndex(index-1).GetRootGameObjects();
        Camera fromCamera = null;
        var newRoot = new GameObject();
        newRoot.transform.position = new Vector3(1000, 0, 0);
        foreach (var go in gos)
        {
            go.transform.SetParent(newRoot.transform, false);
            if (fromCamera == null)
            {
                fromCamera = go.GetComponent<Camera>();
            }
        }

        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);

        gos = SceneManager.GetSceneByBuildIndex(index).GetRootGameObjects();
        Camera toCamera = null;
        foreach (var go in gos)
        {
            if (toCamera == null)
            {
                toCamera = go.GetComponent<Camera>();
                break;
            }
        }

        fromCamera.depth = -10;
        for (float t = 0; t < _kTransitionTime; t += Time.deltaTime)
        {
            var ratio = t/_kTransitionTime;
            fromCamera.rect = new Rect(0, 0, 1- ratio / _kTransitionTime, 1);
            toCamera.rect = new Rect(1- ratio, 0, ratio, 1);
            yield return null;
        }
        toCamera.rect = new Rect(0, 0, 1, 1);
        yield return SceneManager.UnloadSceneAsync(index - 1);
    }
}
