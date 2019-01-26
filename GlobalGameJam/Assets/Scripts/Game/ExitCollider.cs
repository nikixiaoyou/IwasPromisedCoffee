using ggj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCollider : MonoBehaviour
{
    private bool _loading = false;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        GoToNextScene();
    }

    public void GoToNextScene()
    {
        if (_loading)
        {
            return;
        }

        _loading = true;
        GameManager.Instance.StartCoroutine(GoToNextSceneRoutine());
    }

    private IEnumerator GoToNextSceneRoutine()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        var player = GameManager.Instance.Services.Get<PlayerController>();

        for (float t = 0; t < 1.0; t += Time.deltaTime)
        {
            player.transform.position += new Vector3(300 * Time.deltaTime, 0, 0);
            yield return null;
        }
        GameManager.Instance.Services.UnregisterAll();
        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        var gos = SceneManager.GetSceneByBuildIndex(index-1).GetRootGameObjects();
        Camera fromCamera = null;
        foreach(var go in gos)
        {
            go.transform.position -= Camera.main.ScreenToWorldPoint(new Vector3(2*1920, 0, 0));
            if(fromCamera == null)
            {
                fromCamera = go.GetComponent<Camera>();
            }
        }

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

        for (float t = 0; t < 1.0f; t += Time.deltaTime)
        {
            fromCamera.rect = new Rect(-t, 0, 1, 1);
            toCamera.rect = new Rect(1-t, 0, 1, 1);
            yield return null;
        }

        yield return SceneManager.UnloadSceneAsync(index - 1);
    }
}
