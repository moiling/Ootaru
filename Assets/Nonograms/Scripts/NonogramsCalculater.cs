using System;
using System.Collections;
using UnityEngine;

namespace Nonograms.Scripts {
    public class NonogramsCalculater : MonoBehaviour {
        public NonogramsCreator Creator;
        public GameObject PassText;
        public string NonogramsStr;

        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void Update() {
            if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) { // 不点击就不去计算
                return;
            }

            var leftMapList = new ArrayList();
            var bottomMapList = new ArrayList();

            // left
            for (var i = 0; i < Creator.Height; i++) {
                var count = 1;
                var type = NonogramsType.Clear;
                var leftNumbers = new ArrayList();

                for (var j = 0; j < Creator.Width; j++) {
                    var currentType = ((GameObject) Creator.Blocks[Creator.Width * i + j])
                        .GetComponent<NonogramsBlock>().CurrentType;

                    if (currentType == type) {
                        count++;
                    } else if (type != NonogramsType.Clear) {
                        leftNumbers.Add(new NonogramsNumber(count, type));
                        type = currentType;
                        count = 1;
                    } else {
                        type = currentType;
                        count = 1;
                    }
                }

                if (leftNumbers.Count == 0) {
                    leftNumbers.Add(new NonogramsNumber(0, NonogramsType.Black));
                }

                leftMapList.Add(leftNumbers);
            }

            // bottom
            for (var i = 0; i < Creator.Width; i++) {
                var count = 1;
                var type = NonogramsType.Clear;
                var bottomNumbers = new ArrayList();

                for (var j = 0; j < Creator.Height; j++) {
                    var currentType = ((GameObject) Creator.Blocks[Creator.Width * j + i])
                        .GetComponent<NonogramsBlock>().CurrentType;

                    if (currentType == type) {
                        count++;
                    } else if (type != NonogramsType.Clear) {
                        bottomNumbers.Add(new NonogramsNumber(count, type));
                        type = currentType;
                        count = 1;
                    } else {
                        type = currentType;
                        count = 1;
                    }
                }

                if (bottomNumbers.Count == 0) {
                    bottomNumbers.Add(new NonogramsNumber(0, NonogramsType.Black));
                }

                bottomMapList.Add(bottomNumbers);
            }

            PrintMap(leftMapList, bottomMapList);
            Pass();
        }

        // 过关
        private void Pass() {
            if (NonogramsStr == Creator.NumberMap) {
                PassText.SetActive(true);
            }
        }

        private void PrintMap(ArrayList leftMapList, ArrayList bottomMapList) {
            var output = "";
            // left
            output += "{[";
            var i = 0;

            foreach (ArrayList leftNumbers in leftMapList) {
                i++;
                output += "[";
                var j = 0;

                foreach (NonogramsNumber number in leftNumbers) {
                    j++;
                    output += "{" + number.Count + "|" + Convert.ToInt32(number.Type) + "}";

                    if (j < leftNumbers.Count) {
                        output += ",";
                    }
                }

                output += "]";

                if (i < leftMapList.Count) {
                    output += ",";
                }
            }

            output += "],";

            // bottom
            output += "[";
            i = 0;

            foreach (ArrayList bottomNumbers in bottomMapList) {
                i++;
                output += "[";
                var j = 0;

                foreach (NonogramsNumber number in bottomNumbers) {
                    j++;
                    output += "{" + number.Count + "|" + Convert.ToInt32(number.Type) + "}";

                    if (j < bottomNumbers.Count) {
                        output += ",";
                    }
                }

                output += "]";

                if (i < leftMapList.Count) {
                    output += ",";
                }
            }

            output += "]}";

            NonogramsStr = output;
            //Debug.Log(output);
        }
    }
}