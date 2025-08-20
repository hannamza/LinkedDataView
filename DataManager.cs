using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedDataView
{
    class DataManager
    {
        private static DataManager instance;

        private DataManager() 
        {
            CircuitDic = new SortedDictionary<string, Circuit>();
            InputTypeDic = new SortedDictionary<string, List<Circuit>>();
            OutputTypeDic = new SortedDictionary<string, List<Circuit>>();
            EquipmentDic = new SortedDictionary<string, List<Circuit>>();
            OutputContentDic = new SortedDictionary<string, List<Circuit>>();
            PatternDic = new Dictionary<string, Pattern>();
        }

        ~DataManager()
        {
            ClearAllData();
        }

        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager();
                }

                return instance;
            }
        }

        // 회로번호 SortedDictionary
        private SortedDictionary<string, Circuit> CircuitDic;

        // 입력타입 SortedDictionary
        private SortedDictionary<string, List<Circuit>> InputTypeDic;

        // 출력타입 SortedDictionary
        private SortedDictionary<string, List<Circuit>> OutputTypeDic;

        // 설비명 Dictionary
        private SortedDictionary<string, List<Circuit>> EquipmentDic;

        // 출력내용 Dictionary
        private SortedDictionary<string, List<Circuit>> OutputContentDic;

        // 패턴 Dictionary, key: 패턴 번호 
        private Dictionary<string, Pattern> PatternDic;

        // 패턴 추가
        public void AddToPatternDic(int patternID, Pattern pattern)
        {
            // key값을 "P"와 Pattern ID를 붙여서 만들어서 검색 시 바로 매칭할 수 있도록 함
            string strPatternID = "P" + patternID.ToString();
            PatternDic.Add(strPatternID, pattern);
        }

        // 전체 패턴 가져오기
        public Dictionary<string, Pattern> GetPatternDic()
        {
            return PatternDic;
        }

        // 회로번호 추가
        public void AddToCircuitDic(string strCircuitNum, Circuit circuit)
        {
            CircuitDic.Add(strCircuitNum, circuit);
        }

        // 회로번호로 회로정보 가져오기
        public Circuit GetCircuitInfoByCircuitNumber(string strCircuitNum)
        {
            if(CircuitDic.TryGetValue(strCircuitNum, out Circuit circuit))
            {
                return circuit;
            }

            return null;
        }

        // 전체 회로정보 가져오기
        public SortedDictionary<string, Circuit> GetCircuitDic()
        {
            return CircuitDic;
        }

        // 입력타입 추가
        public void AddToInputTypeDic(string strInputType, Circuit circuit)
        {
            if(InputTypeDic.TryGetValue(strInputType, out List<Circuit> circuitList))
            {
                circuitList.Add(circuit);
            }
            else
            {
                List<Circuit> list = new List<Circuit>();
                list.Add(circuit); 
                InputTypeDic.Add(strInputType, list);
            }
        }

        // 전체 입력타입 가져오기
        public SortedDictionary<string, List<Circuit>> GetInputTypeDic()
        {
            return InputTypeDic;
        }

        // 출력타입 추가
        public void AddToOutputTypeDic(string strOutputType, Circuit circuit)
        {
            // 출력타입이 공백이면 -로 대체
            if(strOutputType.Equals(string.Empty))
            {
                strOutputType = "-";
            }

            if(OutputTypeDic.TryGetValue(strOutputType, out List<Circuit> circuitList))
            {
                circuitList.Add(circuit);
            }
            else
            {
                List<Circuit> list = new List<Circuit>();
                list.Add(circuit);
                OutputTypeDic.Add(strOutputType, list);
            }
        }

        // 전체 출력타입 가져오기
        public SortedDictionary<string, List<Circuit>> GetOutputTypeDic()
        {
            return OutputTypeDic;
        }

        // 설비명 추가
        public void AddToEquipmentNameDic(string strEquipmentName, Circuit circuit)
        {
            // 설비명이 공백이면 -로 대체
            if (strEquipmentName.Equals(string.Empty))
            {
                strEquipmentName = "-";
            }

            if(EquipmentDic.TryGetValue(strEquipmentName, out List<Circuit> circuitList))
            {
                circuitList.Add(circuit);
            }
            else
            {
                List<Circuit> list = new List<Circuit>();
                list.Add(circuit);
                EquipmentDic.Add(strEquipmentName, list);
            }
        }

        // 전체 설비명 가져오기
        public SortedDictionary<string, List<Circuit>> GetEquipmentNameDic() 
        { 
            return EquipmentDic; 
        }

        // 출력내용 추가
        public void AddToOutputContentDic(string strOutputContent, Circuit circuit)
        {
            // 출력내용이 공백이면 -로 대체
            if (strOutputContent.Equals(string.Empty))
            {
                strOutputContent = "-";
            }

            if (OutputContentDic.TryGetValue(strOutputContent, out List<Circuit> circuitList))
            {
                circuitList.Add(circuit);
            }
            else
            {
                List<Circuit> list = new List<Circuit>();
                list.Add(circuit);
                OutputContentDic.Add(strOutputContent, list);   
            }
        }

        // 전체 출력내용 가져오기
        public SortedDictionary<string, List<Circuit>> GetOutputContentDic()
        {
            return OutputContentDic;
        }

        public void ClearAllData()
        {
            if ((CircuitDic != null) && (CircuitDic.Count > 0))
            {
                CircuitDic.Clear();
            }

            if ((InputTypeDic != null) && (InputTypeDic.Count > 0))
            {
                foreach (KeyValuePair<string, List<Circuit>> kvp in InputTypeDic)
                {
                    if (kvp.Value.Count > 0)
                    {
                        kvp.Value.Clear();
                    }
                }

                InputTypeDic.Clear();
            }

            if ((OutputTypeDic != null) && (OutputTypeDic.Count > 0))
            {
                foreach(KeyValuePair<string, List<Circuit>> kvp in OutputTypeDic)
                {
                    if (kvp.Value.Count > 0)
                    {
                        kvp.Value.Clear();
                    }
                }

                OutputTypeDic.Clear();
            }

            if ((EquipmentDic != null) && (EquipmentDic.Count > 0))
            {
                foreach (KeyValuePair<string, List<Circuit>> kvp in EquipmentDic)
                {
                    if (kvp.Value.Count > 0)
                    {
                        kvp.Value.Clear();
                    }
                }

                EquipmentDic.Clear();
            }

            if ((OutputContentDic != null) && (OutputContentDic.Count > 0))
            {
                foreach (KeyValuePair<string, List<Circuit>> kvp in OutputContentDic)
                {
                    if (kvp.Value.Count > 0)
                    {
                        kvp.Value.Clear();
                    }
                }

                OutputContentDic.Clear();
            }

            if ((PatternDic != null) && (PatternDic.Count > 0))
            {
                foreach (KeyValuePair<string , Pattern> kvp in PatternDic)
                {
                    if(kvp.Value.Items.Count > 0) 
                    { 
                        kvp.Value.Items.Clear();
                    }
                }

                PatternDic.Clear();
            }
        }
    }
}
