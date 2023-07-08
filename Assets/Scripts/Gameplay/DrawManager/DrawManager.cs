using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public const float RESOLURION = .05f;

    [SerializeField] private Line _linePrefab;
    [SerializeField] private string[] _endTags;
    
    private Line _currentLine;
    public Path MovingPath { get; private set; }
    private Coroutine _coroutine;
    public bool interactible = true;
    
    public Action LineDrawed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!interactible)
            return;

        StopDrawing();
        _coroutine = StartCoroutine(Draw(eventData.enterEventCamera));

        _currentLine = Instantiate(_linePrefab, eventData.enterEventCamera.ScreenToWorldPoint(eventData.position), Quaternion.identity);
        MovingPath = _currentLine.GetComponent<Path>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!interactible)
            return;

        StopDrawing();

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            for(int i = 0; i < _endTags.Length; i++)
                if (eventData.pointerCurrentRaycast.gameObject.CompareTag(_endTags[i]))
                {
                    interactible = false;
                    LineDrawed?.Invoke();
                    return;
                }
        }
        
        Destroy(_currentLine.gameObject);
    }

    private void StopDrawing()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    public IEnumerator Draw(Camera camera)
    {
        while (true)
        {
            yield return null;
            var point = camera.ScreenToWorldPoint(Input.mousePosition);
            _currentLine.SetPosition(point);
            MovingPath.SetPosition(point);
        }
    }
}
