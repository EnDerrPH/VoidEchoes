using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class UIHomeSceneHandler : InitializeManager
{
    [SerializeField] private TMP_Text _mapName;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] MapPreviewHandler _mapPreviewPrefab;
    [SerializeField] Transform _mapScrollViewContent;
    [SerializeField] PlanetData _currentPlanetData;
    [SerializeField] Button _exploreButton;
    MapPreviewHandler _currentMapPreviewHandler;
    MapData _currentMapData;
    const float _yOffset = 50f;

    public override void AddListener()
    {
        base.AddListener();
        _exploreButton.onClick.AddListener(OnExplore);
    }

    public void SetCurrentPlanetData(PlanetData planetData)
    {
        _currentPlanetData = planetData;
        if(_currentPlanetData.MapDataList.Count <= 0)
        {
            _mapName.text = _currentPlanetData.PlanetName;
            return;
        }
        _mapName.text = _currentPlanetData.PlanetName + " " + _currentPlanetData.MapDataList[0].MapName;
    }

    public void SetMapList()
    {
        if(_currentPlanetData == null)
        {
            return;
        }
        List<MapData> _mapDataList = _currentPlanetData.MapDataList;
        if(_mapDataList.Count <= 0)
        {
            if(_mapScrollViewContent.childCount != 0)
            {
                foreach(Transform child in _mapScrollViewContent)
                {
                    child.gameObject.SetActive(false);
                }
            }
            return;
        }

        if(_mapDataList.Count == _mapScrollViewContent.childCount)
        {
            for(int i = 0 ; i < _mapDataList.Count ; i++)
            {
                _mapScrollViewContent.GetChild(i).gameObject.SetActive(true);
  
            }
            return;
        }

        int mapCount = (_mapDataList.Count - _mapScrollViewContent.childCount);

        for(int i = 0 ; i < _mapDataList.Count ; i++)
        {
            if(i > mapCount)
            {
                _mapScrollViewContent.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            MapPreviewHandler mapPreview = Instantiate(_mapPreviewPrefab, _mapPreviewPrefab.transform.position, Quaternion.identity);
            mapPreview.transform.SetParent(_mapScrollViewContent , false);
            mapPreview.SetMapData(_mapDataList[i]);
            mapPreview.SetUIHomeScreenHandler(this);
            mapPreview.gameObject.SetActive(true);
            if(i == 0)
            {
                _currentMapPreviewHandler = mapPreview;
                continue;
            }
            mapPreview.SetMapPreviewAlpha(.3f);
        }
        _currentMapPreviewHandler.transform.position += Vector3.up * _yOffset;
    }
    
    public void SetMapData(MapData mapData)
    {   
        _currentMapData = mapData;
        _mapName.text = _currentPlanetData.PlanetName + " : " +_currentMapData.MapName;
    }

    public void SetMapPreviewHandler(MapPreviewHandler mapPreviewHandler)
    {
        Image image = _currentMapPreviewHandler.MapImage;
        _currentMapPreviewHandler.SetMapPreviewAlpha(.3f);
        _currentMapPreviewHandler = mapPreviewHandler; 
        _currentMapPreviewHandler.SetMapPreviewAlpha(1f);
        Image newImage = _currentMapPreviewHandler.MapImage;
    }

    private void OnExplore()
    {
        _gameManager.SetMapSO(_currentMapPreviewHandler.MapData);
        _loadingSceneManager.LoadScene("GameScene");
    }
}
