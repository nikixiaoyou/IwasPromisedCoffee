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
        ggj.GameManager.Instance.StartCoroutine(GoToNextSceneRoutine());
    }

    private IEnumerator GoToNextSceneRoutine()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    }
}
