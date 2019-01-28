using System;
using CardMode.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {
    public GameObject DialogPanel;
    public Transform LeftFacePosition;
    public Transform RightFacePosition;
    public GameObject ChuFace;
    public GameObject JeanFace;
    public GameObject LeftDialog;
    public GameObject RightDialog;
    public GameObject LeftName;
    public GameObject RightName;
    public GameObject Option;
    public Transform OptionPanel;
    public PlayerController Player;

    // TODO 保存多个runner 执行不同的文本
    private DialogRunner _runner;
    private bool _canControlDialog = false;
    private bool _canControlOption = false;

    private bool _isStart = false;

    #region Singleton

    private static DialogController _instance;

    public static DialogController GetInstance() {
        return _instance;
    }

    public void Awake() {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    private void Update() {
        if (_runner == null) {
            return;
        }
        
        if (_canControlDialog) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                _runner.Next();
            }
        }
    }

    public void StartDialog(string text) {
        _runner = new DialogRunner(text, this);
        Player.DoNotMove = true;
        _runner.Next();
    }

    public void ShowDialogPanel() {
        DialogPanel.SetActive(true);
    }

    public void HideDialog() {
        LeftDialog.SetActive(false);
        RightDialog.SetActive(false);
    }

    public void ShowDialog() {
        // TODO 我觉得没用
        //LeftDialog.SetActive(true);
        //RightDialog.SetActive(true);
    }

    public void SwitchDialogModel(SpeakerPosition position) {
        switch (position) {

            case SpeakerPosition.Left:
                LeftDialog.SetActive(true);
                RightDialog.SetActive(false);
                LeftName.SetActive(true);
                RightName.SetActive(false);

                LeftDialog.transform.SetSiblingIndex(2);
                LeftFacePosition.transform.SetSiblingIndex(3);
                RightFacePosition.transform.SetSiblingIndex(1);
                break;
            case SpeakerPosition.Right:
                LeftDialog.SetActive(false);
                RightDialog.SetActive(true);
                LeftName.SetActive(false);
                RightName.SetActive(true);

                RightDialog.transform.SetSiblingIndex(2);
                RightFacePosition.transform.SetSiblingIndex(3);
                LeftFacePosition.transform.SetSiblingIndex(1);
                break;
            case SpeakerPosition.Both:

                // TODO 两边
                break;
        }
    }

    public void End() {
        _canControlDialog = false;
        _canControlOption = false;
        DialogPanel.SetActive(false);
        Player.DoNotMove = false;
    }

    public void SetName(string showName) {
        RightName.GetComponentInChildren<Text>().text = showName;
        LeftName.GetComponentInChildren<Text>().text = showName;
    }

    public void AddContent(string content) {
        RightDialog.GetComponentInChildren<Text>().text += content;
        LeftDialog.GetComponentInChildren<Text>().text += content;
        _canControlDialog = true;
    }

    public void ClearContent() {
        RightDialog.GetComponentInChildren<Text>().text = "";
        LeftDialog.GetComponentInChildren<Text>().text = "";
    }

    public void SetFace(string faceId, bool isRight = false) {
        // TODO 临时测试的人物，而且每边只能设置一个
        if (isRight) {
            var face = Instantiate(faceId.StartsWith("Jean") ? JeanFace : ChuFace);

            if (face == null) return;

            for (var i = RightFacePosition.transform.childCount - 1; i >= 0; i--) {
                Destroy(RightFacePosition.transform.GetChild(i).gameObject);
            }

            face.transform.SetParent(RightFacePosition.transform);
            face.transform.localScale = Vector3.one;
            face.transform.localPosition = Vector3.zero;
            face.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        } else {
            var face = Instantiate(faceId.StartsWith("Jean") ? JeanFace : ChuFace);

            if (face == null) return;

            for (var i = LeftFacePosition.transform.childCount - 1; i >= 0; i--) {
                Destroy(LeftFacePosition.transform.GetChild(i).gameObject);
            }

            face.transform.SetParent(LeftFacePosition.transform);
            face.transform.localScale = Vector3.one;
            face.transform.localPosition = Vector3.zero;
            face.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }

    public void SelectOption(int id) {
        _runner.Select(id);
        _canControlOption = false;
        _canControlDialog = true;
        OptionPanel.gameObject.SetActive(false);
    }

    public void ShowOption(string[] options) {
        _canControlOption = true;
        _canControlDialog = false;
        OptionPanel.gameObject.SetActive(true);
        
        for (var i = OptionPanel.transform.childCount - 1; i >= 0; i--) {
            Destroy(OptionPanel.transform.GetChild(i).gameObject);
        }
        
        for (var i = 0; i < options.Length; i++) {
            var optionItem = Instantiate(Option);
            optionItem.transform.SetParent(OptionPanel.transform);
            optionItem.transform.localScale = Vector3.one;

            optionItem.GetComponent<OptionController>().TextStr = options[i];
            optionItem.GetComponent<OptionController>().optionId = i;
        }
    }
}