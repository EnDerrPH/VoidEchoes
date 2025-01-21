using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeyserTrapHandler : MonoBehaviour
{
    [SerializeField] List<GameObject> _geyserList = new List<GameObject>();
    [SerializeField] List<Transform> _CurrentPhasePositionList = new List<Transform>();
    [SerializeField] List<Transform> _phase1PositionList = new List<Transform>();
    [SerializeField] List<Transform> _phase2PositionList = new List<Transform>();
    [SerializeField] List<Transform> _phase3PositionList = new List<Transform>();
    [SerializeField] List<Transform> _phase4PositionList = new List<Transform>();
    [SerializeField] List<Transform> _phase5PositionList = new List<Transform>();
    [SerializeField] float _changePositionTreshhold;
    [SerializeField] float _geyserRandomiseTimer;
    [SerializeField] List<int> _possibleNumbers = new List<int>();
    [SerializeField] List<int> _chosenNumbers = new List<int>();
    [SerializeField] GeyserPhase _geyserPhase;
    public GeyserPhase GeyserPhase {get => _geyserPhase; set => _geyserPhase = value;}

    void Update()
    {
        RandomiseGeyserPosition();
    }

    private void RandomiseGeyserPosition()
    {
        if(_geyserPhase == GeyserPhase.Default)
        {
            return;
        }
        _geyserRandomiseTimer += 1 * Time.deltaTime;

        if (_geyserRandomiseTimer < _changePositionTreshhold)
        {
            return;
        }

        foreach(GameObject gameObject in _geyserList)
        {
            int positionNumber = (GetRandomExcluding(_possibleNumbers, _chosenNumbers));
            _chosenNumbers.Add(positionNumber);
        }
        SetGeyserPosition();
        _chosenNumbers.Clear();

        _geyserRandomiseTimer = 0f;
    }

   private int GetRandomExcluding(List<int> _possibleNumbers, List<int> _chosenNumbers)
    {
        List<int> remainingNumbers = _possibleNumbers.Where(x => !_chosenNumbers.Contains(x)).ToList();
        int randomNumber = remainingNumbers[Random.Range(0, remainingNumbers.Count)];
        return randomNumber;
    }

    private void SetGeyserPosition()
    {
        for(int i = 0 ; i < _geyserList.Count ; i++)
        {
            _geyserList[i].transform.position = new Vector3(_CurrentPhasePositionList[_chosenNumbers[i]].transform.position.x, _geyserList[i].transform.position.y , _CurrentPhasePositionList[_chosenNumbers[i]].transform.position.z);
            _geyserList[i].gameObject.SetActive(false);
            _geyserList[i].gameObject.SetActive(true);
        }
    }

    private void DeactivateGeyser()
    {
        for(int i = 0 ; i < _geyserList.Count ; i++)
        {
           _geyserList[i].gameObject.SetActive(false);
        }
    }

    public void SetGeyserPhase(GeyserPhase geyserPhase)
    {
        _geyserPhase = geyserPhase;
        switch(_geyserPhase)
        {
            case GeyserPhase.One:
                _CurrentPhasePositionList = _phase1PositionList;
            break;
            case GeyserPhase.Two:
                _CurrentPhasePositionList = _phase2PositionList;
            break;
            case GeyserPhase.Three:
                _CurrentPhasePositionList = _phase3PositionList;
            break;
            case GeyserPhase.Four:
                _CurrentPhasePositionList = _phase4PositionList;
            break;
            case GeyserPhase.Five:
                _CurrentPhasePositionList = _phase5PositionList;
            break;
                case GeyserPhase.Done:
                DeactivateGeyser();
                this.gameObject.SetActive(false);
            break;
            default:
            break;
        }
        _possibleNumbers = Enumerable.Range(0, _CurrentPhasePositionList.Count).ToList();
    }
}
