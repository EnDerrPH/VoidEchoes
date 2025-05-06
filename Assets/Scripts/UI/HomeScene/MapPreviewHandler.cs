using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapPreviewHandler : MonoBehaviour , IPointerClickHandler
{
   [SerializeField] Image _image;
   string _mapName;
   MapData _mapData;
   UIHomeSceneHandler _UIHomeScreenHandler;
   AudioManager _audioManager;

   public Image MapImage {get => _image ; set => _image = value;}
   public MapData MapData {get => _mapData ; set => _mapData = value;}

   void Start()
   {
      _audioManager = AudioManager.instance;
   }

   public void OnPointerClick(PointerEventData eventData)
   {
      _UIHomeScreenHandler.SetMapPreviewHandler(this);
      _UIHomeScreenHandler.SetMapData(_mapData);
      _audioManager.PlaySound(_audioManager.GetAudioClipData().MainMenuButtonSFX);
   }

   public void SetMapData(MapData mapData)
   {
      _mapData  = mapData;
      _image.sprite = _mapData.Sprite;
      _mapName = _mapData.MapName;
   }

   public void SetUIHomeScreenHandler(UIHomeSceneHandler uiHomeScreenHandler)
   {
      _UIHomeScreenHandler = uiHomeScreenHandler;
   }

   public void SetMapPreviewAlpha(float alphaRange)
    {
      Color color = _image.color;
      color.a = Mathf.Clamp01(alphaRange); // Ensure alpha stays between 0 and 1
      _image.color = color;
    }
}
