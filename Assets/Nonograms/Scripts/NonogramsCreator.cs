using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Nonograms.Scripts {
    public class NonogramsCreator : MonoBehaviour {
        public int Width;
        public int Height;

        public string NumberMap;
        public GameObject Block;
        public GameObject ColumnNumbers;
        public GameObject RowNumbers;
        public GameObject Number;

        public GameObject CenterGrid;
        public GameObject RightList;
        public GameObject BottomList;

        public ArrayList Blocks = new ArrayList();

        // 解析完后的数组
        private ArrayList _rightMapList = new ArrayList();
        private ArrayList _bottomMapList = new ArrayList();

        // Use this for initialization
        void Start() {
            CreateNonograms();
        }

        public void CreateNonograms() {
            AnalyzeMap();
            CreateGrid();
            CreateNumber();
        }

        public void GenerateNumber() {
            AnalyzeMap();
            CreateNumber();
        }

        public void CreateGrid() {
            // 删除所有子对象
            for (var i = CenterGrid.transform.childCount - 1; i >= 0; i--) {
                Destroy(CenterGrid.transform.GetChild(i).gameObject);
            }

            Blocks.Clear();

            // Center Grid
            for (var i = 0; i < Width * Height; i++) {
                var block = Instantiate(Block);
                block.transform.SetParent(CenterGrid.transform);
                block.transform.localScale = Vector3.one;
                Blocks.Add(block);
            }
        }

        private void CreateNumber() {
            // 删除所有子对象
            for (var i = RightList.transform.childCount - 1; i >= 0; i--) {
                Destroy(RightList.transform.GetChild(i).gameObject);
            }

            for (var i = BottomList.transform.childCount - 1; i >= 0; i--) {
                Destroy(BottomList.transform.GetChild(i).gameObject);
            }

            // Right List			
            foreach (ArrayList rightNumbers in _rightMapList) {
                var columnNumbers = Instantiate(ColumnNumbers);
                columnNumbers.transform.SetParent(RightList.transform);
                columnNumbers.transform.localScale = Vector3.one;

                foreach (NonogramsNumber number in rightNumbers) {
                    var numberPrefab = Instantiate(Number);
                    numberPrefab.transform.SetParent(columnNumbers.transform);
                    numberPrefab.transform.localScale = Vector3.one;
                    SetNumber(numberPrefab, number);
                }
            }

            // Bottom List
            foreach (ArrayList bottomNumbers in _bottomMapList) {
                var rowNumbers = Instantiate(RowNumbers);
                rowNumbers.transform.SetParent(BottomList.transform);
                rowNumbers.transform.localScale = Vector3.one;

                foreach (NonogramsNumber number in bottomNumbers) {
                    var numberPrefab = Instantiate(Number);
                    numberPrefab.transform.SetParent(rowNumbers.transform);
                    numberPrefab.transform.localScale = Vector3.one;
                    SetNumber(numberPrefab, number);
                }
            }
        }

        private void SetNumber(GameObject numberPrefab, NonogramsNumber number) {
            numberPrefab.GetComponent<Text>().text = number.Count + "";

            switch (number.Type) {

                case NonogramsType.Clear:
                    numberPrefab.GetComponent<Text>().color = Color.white;

                    break;
                case NonogramsType.Black:
                    numberPrefab.GetComponent<Text>().color = Color.black;

                    break;
                /*
                case NonogramsType.Gray:
                    numberPrefab.GetComponent<Text>().color = Color.gray;
                    break;
                case NonogramsType.Red:
                    numberPrefab.GetComponent<Text>().color = Color.red;
                    break;
                case NonogramsType.Blue:
                    numberPrefab.GetComponent<Text>().color = Color.blue;
                    break;
                case NonogramsType.Green:
                    numberPrefab.GetComponent<Text>().color = Color.green;
                    break;
                case NonogramsType.Yellow:
                    numberPrefab.GetComponent<Text>().color = Color.yellow;
                    break;
                */
                //case NonogramsType.Cross:
                //	numberPrefab.GetComponent<Text>().color = Color.magenta;
                //	break;
                default:

                    throw new ArgumentOutOfRangeException();
            }
        }

        private void AnalyzeMap() {
            _rightMapList.Clear();
            _bottomMapList.Clear();

            var str = NumberMap.Substring(1, NumberMap.Length - 2); // 截去两边的{}

            var read = false;
            var tempNumberListStr = ""; // 一行的number的字符串
            var type = 0;               // 0->right; 1->bottom

            var i = 0;

            foreach (var s in str) {
                if (s == ']') { // 读完一个数组
                    read = false;

                    if (tempNumberListStr == "") { // 说明到了第一个数组的结尾，要换到bottom了
                        type++;
                    } else { // 普通读完一行，j变化

                        var tempNumbersStrList = tempNumberListStr.Split(',');

                        var numbers = new ArrayList();

                        foreach (var numberStr in tempNumbersStrList) { // 把每个解析出来的number类装进数组
                            numbers.Add(new NonogramsNumber(numberStr));
                        }

                        if (type == 0) { // 装入right
                            _rightMapList.Add(numbers);
                        } else {
                            _bottomMapList.Add(numbers);
                        }

                        tempNumberListStr = "";
                    }
                }

                if (read) {
                    tempNumberListStr += s;
                }

                if (s == '[' && str[i + 1] != '[') { // 开始读了
                    read = true;
                }

                i++;
            }
        }

        // Update is called once per frame
        void Update() {
            //Instantiate(Block);
        }
    }
}