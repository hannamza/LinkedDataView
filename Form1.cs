using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static LinkedDataView.ImageManager;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LinkedDataView
{
    public partial class Form1 : Form
    {
        public readonly string PROGRAM_VERSION = "1.2";
        public readonly string[] TYPE_TEXT = { "Unknown Type", "패턴", "비상방송", "펌프", "수신기 접점" };
        private ColumnHeaderSorting lvOutputPtnSorter;
        private ColumnHeaderSorting lvOutputCircuitSorter;
        private ColumnHeaderSorting lvPtnItemListSorter;
        public readonly string CIRCUIT_INFO_CAN_NOT_FOUND_MSG = "(연동표에서 회로 정보를 찾을 수 없음)";
        private bool fileOpened;
        private readonly Dictionary<int, TreeNode[]> _treeCache = new Dictionary<int, TreeNode[]>();
        private int _currentSortType = -1;
        private bool _suppressTreeEvents = false;
        private readonly string HELP_FILE_NAME = "사용 설명서.pdf";

        public bool FileOpened
        {
            get
            {
                return this.fileOpened;
            }

            set 
            { 
                this.fileOpened = value;               
            }
        }

        enum TYPE_TEXT_INDEX
        {
            UNKNOWN_TYPE = 0,
            PATTERN,
            EB,
            PUMP,
            FACP_CONTACT
        }

        enum INPUT_SORT{
            NO_TYPE = -1,
            CIRCUIT_NUM,
            INPUT_TYPE,
            OUTPUT_TYPE,
            EQUIPMENT_NAME,
            OUTPUT_CONTENT
        }

        public Form1()
        {
            InitializeComponent();

            this.Text = "연동데이터 VIEW 프로그램" + " (ver" + PROGRAM_VERSION + ")";

            //최초 입력 타입 정렬과 검색 기능은 비활성화
            comboBoxSort.Enabled = false;
            textBoxSearch.Enabled = false;
            btnSearch.Enabled = false;

            // ListView에 기본 정렬자 연결
            lvOutputPtnSorter = new ColumnHeaderSorting();
            listViewOutputPtn.ListViewItemSorter = lvOutputPtnSorter;
            lvOutputCircuitSorter = new ColumnHeaderSorting();
            listViewOutputCircuit.ListViewItemSorter = lvOutputCircuitSorter;
            lvPtnItemListSorter = new ColumnHeaderSorting();
            listViewPtnItemList.ListViewItemSorter = lvPtnItemListSorter;

            // Input TreeView 이미지 설정
            SetImagesInInputCircuitTreeView();

            // ListView(패턴,비상방송,펌프 등 표시 리스트 + 출력회로 리스트)에 이미지 설정
            SetImagesInListView();

            // 패턴 검색 TreeView 이미지 설정
            SetImagesInSearchTreeView();

            fileOpened = false;
        }

        /// <summary>
        /// 파일 이름에서 숫자를 추출하고 중복 및 수신기 번호의 연속성 여부를 검사
        /// </summary>
        private bool ValidateFileNumbers(string[] filePaths, out Dictionary<string, string> facpNumDic, out string errorMessage)
        {
            facpNumDic = new Dictionary<string, string>();
            HashSet<string> numbersSet = new HashSet<string>();

            // "(연동표)"로 시작하고 끝에 두 자리 숫자가 있어야 함
            Regex numberPattern = new Regex(@"^\(연동표\).*?(\d{2})\.xlsx$", RegexOptions.IgnoreCase);

            foreach (string path in filePaths)
            {
                string fileName = Path.GetFileName(path);
                Match match = numberPattern.Match(fileName);
                if (!match.Success)
                {
                    errorMessage = $"파일 이름 형식이 올바르지 않습니다.: {fileName}\r\n파일 이름은 (연동표)로 시작해야 하며 .xlsx(확장자)앞에는 두 자리 숫자(수신기 번호)여야 합니다.";
                    return false;
                }

                string numberPart = match.Groups[1].Value;

                if (!numbersSet.Add(numberPart))
                {
                    errorMessage = $"중복된 수신기 번호가 발견되었습니다.: {numberPart}";
                    return false;
                }

                facpNumDic.Add(path, numberPart);
            }

            // 추가 검사: 수신기 번호가 0부터 시작하는 연속된 정수인지 확인
            var sortedNumbers = numbersSet
                .Select(n => int.Parse(n))
                .OrderBy(n => n)
                .ToList();

            for (int i = 0; i < sortedNumbers.Count; i++)
            {
                if (sortedNumbers[i] != i)
                {
                    errorMessage = $"수신기 번호 리스트는 파일이 하나면 0,\r\n파일이 여러 개면 0부터 시작하는 연속된 번호여야 합니다.\r\n선택된 수신기 번호 리스트: {string.Join(", ", sortedNumbers)}";
                    return false;
                }
            }

            errorMessage = null;
            return true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (fileOpened)
            {
                MessageBox.Show("현재 연동표가 열려있습니다. 먼저 [연동표 파일 닫기]를 눌러주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "연동표 파일 선택",
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                Multiselect = true,
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] selectedFiles = openFileDialog.FileNames;

                //유효성 검사
                if (ValidateFileNumbers(selectedFiles, out Dictionary<string, string> facpNumDic, out string errorMessage))
                {
                    MessageBox.Show("연동표 파일 선택 완료:\n" + string.Join("\n", selectedFiles),
                                    "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 여기서 이후 로직 수행 (예: 파일 처리)

                    //
                    ExcelLoadingForm excelLoadingForm = new ExcelLoadingForm();

                    excelLoadingForm.ShownOnce += () =>
                    {
                        Thread thread = new Thread(() =>
                        {
                            ExcelManager excelManager = ExcelManager.Instance;
                            try
                            {
                                excelManager.GetLinkedData(selectedFiles, facpNumDic);
                                fileOpened = true;
                            }
                            catch (IOException ex)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    MessageBox.Show("Excel 파일이 이미 열려 있거나 접근할 수 없습니다.\n\n" + ex.Message,
                                                    "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }));    
                            }

                            excelLoadingForm.Invoke(new Action(() =>
                            {
                                excelLoadingForm.DialogResult = DialogResult.OK;
                                excelLoadingForm.Close();
                            }));
                        });

                        thread.Start();
                    };
                    //

                    excelLoadingForm.StartPosition = FormStartPosition.CenterParent;
                    excelLoadingForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(errorMessage, "연동표 파일 유효성 검사 실패",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // 리턴하기 전에 메모리 정리, 앞선 번호의 수신기 정보가 있을 수 있음
                    DataManager dataManager = DataManager.Instance;
                    dataManager.ClearAllData();

                    return;
                }

                if (fileOpened)
                {
                    MessageBox.Show("연동표 파일 열기를 성공했습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //연동표 엑셀 파일 파싱이 모두 완료되면 UI Control을 초기화
                    InitControls();
                }
                else 
                {
                    MessageBox.Show("연동표 파일 열기를 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // 리턴하기 전에 메모리 정리, 앞선 번호의 수신기 정보가 있을 수 있음
                    DataManager dataManager = DataManager.Instance;
                    dataManager.ClearAllData();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            fileOpened = false;
            _treeCache.Clear();
            InitControls();
        }

        private void InitControls()
        {
            if (fileOpened)
            {
                //연동표가 열렸으므로 입력회로 정렬, 검색 기능 활성화
                comboBoxSort.Enabled = true;
                textBoxSearch.Enabled = true;
                btnSearch.Enabled = true;

                int inputTreeViewOrder = (int)INPUT_SORT.CIRCUIT_NUM;
                comboBoxSort.SelectedIndex = inputTreeViewOrder;        // Combobox index를 적용하는 것만으로 comboBoxSort_SelectedIndexChanged 매서드가 호출되어 따로 DisplayInputTree를 호출하지 않음
            }
            else
            {
                //모든 데이터 초기화
                DataManager dataManager = DataManager.Instance;
                dataManager.ClearAllData();

                //입력 타입 정렬과 검색 기능은 비활성화
                comboBoxSort.Enabled = false;
                textBoxSearch.Enabled = false;
                btnSearch.Enabled = false;

                //모든 UI 컨트롤 내용 Clear

                //입력 회로 트리 - 정렬 여부에 따라 다른 이미지를 로드하므로 연동표를 닫았을 경우 clear
                treeViewInput.Nodes.Clear();

                //입력 회로 명
                richTextBoxIputCircuit.Clear();

                // 출력 패턴 리스트 clear
                listViewOutputPtn.Items.Clear();

                //출력 회로 리스트 clear
                listViewOutputCircuit.Items.Clear();

                //패턴 검색어 텍스트박스
                textBoxSearch.Clear();

                //패턴 검색 트리
                treeViewPtnSearch.Nodes.Clear();

                //패턴 명
                richTextBoxPtnName.Clear();

                //패턴 리스트
                listViewPtnItemList.Items.Clear();

                //연동표가 닫힌 후 새로 연동표를 열었을 때 comboBoxSort_SelectedIndexChanged를 호출하게 해서 입력 회로를 그리게 해야 하므로 입력 회로 정렬 선택 인덱스를 -1로 세팅
                comboBoxSort.SelectedIndex = (int)INPUT_SORT.NO_TYPE;
            }
        }

        private void SetInputCircuitTreeOrderByCircuitNum(TreeNodeCollection dummy)
        {
            ImageManager imageManager = ImageManager.Instance;

            // 회로 입력 가지고 오기
            SortedDictionary<string, Circuit> CircuitDic = DataManager.Instance.GetCircuitDic();
            foreach (KeyValuePair<string, Circuit> kvp in CircuitDic)
            {
                string strFullCircuitNumber = kvp.Key;

                // 계층별 이름 생성
                string strFacp = $"FACP_{int.Parse(strFullCircuitNumber.Substring(0, 2)):D2}";
                string strUnit = $"Unit_{int.Parse(strFullCircuitNumber.Substring(0, 2)):D2}{int.Parse(strFullCircuitNumber.Substring(2, 2)):D2}";
                string strChannel = $"Channel_{int.Parse(strFullCircuitNumber.Substring(0, 2)):D2}{int.Parse(strFullCircuitNumber.Substring(2, 2)):D2}-{int.Parse(strFullCircuitNumber.Substring(5, 1)):D1}";
                string strCircuit = kvp.Value.InputFullName; // 예: [0000-0001] [연식아나로그] 클럽자이안1 B1F MDF실 [AN연기]

                // TreeView 최상위 (FACP) 노드 찾거나 생성, ImageKey는 파일 이름
                TreeNode facpNode = FindOrCreateNode(dummy, strFacp, imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.FACP]);

                // Unit 노드 찾거나 생성, ImageKey는 파일 이름
                TreeNode unitNode = FindOrCreateNode(facpNode.Nodes, strUnit, imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.UNIT]);

                // Channel 노드 찾거나 생성, ImageKey는 파일 이름
                TreeNode channelNode = FindOrCreateNode(unitNode.Nodes, strChannel, imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.LOOP]);

                // Circuit 노드 추가 (회로는 중복이 없으므로 바로 추가), ImageKey는 파일 이름
                TreeNode circuitNode = new TreeNode(strCircuit)
                {
                    ImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                    SelectedImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                    Tag = kvp.Value                 // ✅ 말단 노드에 Circuit 객체 연결
                };

                channelNode.Nodes.Add(circuitNode);
            }
        }

        private void SetInputCircuitTreeOrderByInputType(TreeNodeCollection dummy)
        {
            ImageManager imageManager = ImageManager.Instance;

            // 입력 타입 가져오기
            SortedDictionary<string, List<Circuit>> inputTypeDic = DataManager.Instance.GetInputTypeDic();
            foreach (KeyValuePair<string, List<Circuit>> kvp in inputTypeDic)
            {
                string strInputType = kvp.Key;

                foreach (Circuit circuit in kvp.Value)
                {
                    string strCircuit = circuit.InputFullName;

                    // TreeView 최상위 (InputType) 노드 찾거나 생성
                    TreeNode InputTypeNode = FindOrCreateNode(dummy, strInputType, imageManager.TREE_IMAGES_NAME[(int)ImageManager.ENUM_TREE_IMAGES.INPUT_TYPE]);

                    // Circuit 노드 추가 (회로는 중복이 없으므로 바로 추가)
                    TreeNode circuitNode = new TreeNode(strCircuit)
                    {
                        ImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        SelectedImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        Tag = circuit                 // ✅ 말단 노드에 Circuit 객체 연결
                    };

                    InputTypeNode.Nodes.Add(circuitNode);
                }
            }
        }

        private void SetInputCircuitTreeOrderByOutputType(TreeNodeCollection dummy)
        {
            ImageManager imageManager = ImageManager.Instance;

            // 출력 타입 가져오기
            SortedDictionary<string, List<Circuit>> outputTypeDic = DataManager.Instance.GetOutputTypeDic();
            foreach (KeyValuePair<string, List<Circuit>> kvp in outputTypeDic)
            {
                string strOutputType = kvp.Key;

                foreach (Circuit circuit in kvp.Value)
                {
                    string strCircuit = circuit.InputFullName;

                    // TreeView 최상위 (outputType) 노드 찾거나 생성
                    TreeNode outputTypeNode = FindOrCreateNode(dummy, strOutputType, imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.OUTPUT_TYPE]);

                    // Circuit 노드 추가 (회로는 중복이 없으므로 바로 추가)
                    TreeNode circuitNode = new TreeNode(strCircuit)
                    {
                        ImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        SelectedImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        Tag = circuit                 // ✅ 말단 노드에 Circuit 객체 연결
                    };

                    outputTypeNode.Nodes.Add(circuitNode);
                }
            }
        }

        private void SetInputCircuitTreeOrderByEquipmentName(TreeNodeCollection dummy)
        {
            ImageManager imageManager = ImageManager.Instance;  

            // 출력 타입 가져오기
            SortedDictionary<string, List<Circuit>> equipmentNameDic = DataManager.Instance.GetEquipmentNameDic();
            foreach (KeyValuePair<string, List<Circuit>> kvp in equipmentNameDic)
            {
                string strEquipment = kvp.Key;

                foreach (Circuit circuit in kvp.Value)
                {
                    string strCircuit = circuit.InputFullName;

                    // TreeView 최상위 (equipmentName) 노드 찾거나 생성
                    TreeNode equipmentNameNode = FindOrCreateNode(dummy, strEquipment, imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.EQUIPMENT_NAME]);

                    // Circuit 노드 추가 (회로는 중복이 없으므로 바로 추가)
                    TreeNode circuitNode = new TreeNode(strCircuit)
                    {
                        ImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        SelectedImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        Tag = circuit                // ✅ 말단 노드에 Circuit 객체 연결
                    };

                    equipmentNameNode.Nodes.Add(circuitNode);
                }
            }
        }

        private void SetInputCircuitTreeOrderByOutputContent(TreeNodeCollection dummy)
        {
            ImageManager imageManager = ImageManager.Instance;

            // 출력 타입 가져오기
            SortedDictionary<string, List<Circuit>> outputContentDic = DataManager.Instance.GetOutputContentDic();
            foreach (KeyValuePair<string, List<Circuit>> kvp in outputContentDic)
            {
                string strOutputType = kvp.Key;

                foreach (Circuit circuit in kvp.Value)
                {
                    string strCircuit = circuit.InputFullName;

                    // TreeView 최상위 (outputContent) 노드 찾거나 생성
                    TreeNode outputContentNode = FindOrCreateNode(dummy, strOutputType, imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.OUTPUT_CONTENT]);

                    // Circuit 노드 추가 (회로는 중복이 없으므로 바로 추가)
                    TreeNode circuitNode = new TreeNode(strCircuit)
                    {
                        ImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        SelectedImageKey = imageManager.TREE_IMAGES_NAME[(int)ENUM_TREE_IMAGES.CIRCUIT],
                        Tag = circuit                 // ✅ 말단 노드에 Circuit 객체 연결
                    };

                    outputContentNode.Nodes.Add(circuitNode);
                }
            }
        }

        // 특정 컬렉션에서 동일한 이름의 노드를 찾거나 없으면 새로 추가
        TreeNode FindOrCreateNode(TreeNodeCollection nodes, string nodeName, string imageKey)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Text == nodeName)
                    return node;
            }

            TreeNode newNode = new TreeNode(nodeName)
            {
                ImageKey = imageKey,
                SelectedImageKey = imageKey
            };
            nodes.Add(newNode);
            return newNode;
        }

        private static TreeNode CloneTreeNodeDeep(TreeNode src)
        {
            var dst = new TreeNode(src.Text)
            {
                // 필요 속성 복제
                Name = src.Name,                // 쓰고 있으면
                Tag = src.Tag,                  // 참조 복제 (원본 객체 공유 OK)

                // src의 이미지는 key값을 기반으로 값을 가지고 있는데 Index까지 복사하면 src에서는 Index를 설정하지 않은 상황에서 컨트롤에서 표현 시
                // Index가 Key값에 우선하기 때문에 Index 0번이 f.png로 보이는 현상이 생김
                ImageKey = src.ImageKey,
                SelectedImageKey = src.SelectedImageKey,
                //ImageIndex = src.ImageIndex,
                //SelectedImageIndex = src.SelectedImageIndex,

                StateImageIndex = src.StateImageIndex,
                ToolTipText = src.ToolTipText
            };

            // children
            foreach (TreeNode child in src.Nodes)
                dst.Nodes.Add(CloneTreeNodeDeep(child));

            return dst;
        }

        private static TreeNode[] CloneRange(TreeNode[] nodes)
        {
            var cloned = new TreeNode[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
                cloned[i] = CloneTreeNodeDeep(nodes[i]);
            return cloned;
        }

        // 실제 트리가 아니라 메모리에 트리 내용을 먼저 채움
        private TreeNode[] BuildTreeNodes(int sortType)
        {
            var dummy = new System.Windows.Forms.TreeView();

            switch (sortType)
            {
                case (int)INPUT_SORT.CIRCUIT_NUM:
                    SetInputCircuitTreeOrderByCircuitNum(dummy.Nodes);
                    break;
                case (int)INPUT_SORT.INPUT_TYPE:
                    SetInputCircuitTreeOrderByInputType(dummy.Nodes);
                    break;
                case (int)INPUT_SORT.OUTPUT_TYPE:
                    SetInputCircuitTreeOrderByOutputType(dummy.Nodes);
                    break;
                case (int)INPUT_SORT.EQUIPMENT_NAME:
                    SetInputCircuitTreeOrderByEquipmentName(dummy.Nodes);
                    break;
                case (int)INPUT_SORT.OUTPUT_CONTENT:
                    SetInputCircuitTreeOrderByOutputContent(dummy.Nodes);
                    break;
            }

            return dummy.Nodes.Cast<TreeNode>().ToArray();
        }

        // 정렬 기준에 따라 트리 구성, 데이터가 많아서 처리가 느릴 경우에 대비해 실제 UI와 정렬 기준에 따라 달리 구성되는 데이터를 분리
        private void DisplayInputTree(int sortType)
        {
            _suppressTreeEvents = true;
            treeViewInput.BeginUpdate();

            try
            {
                // 현재 화면 내용은 그냥 지움 (복제본으로 붙인 것이므로 원본 캐시는 안전)
                treeViewInput.Nodes.Clear();

                // 캐시에 없으면 빌드하여 "템플릿"으로 저장
                if (!_treeCache.TryGetValue(sortType, out var templateNodes))
                {
                    templateNodes = BuildTreeNodes(sortType); // 아래 ③
                    _treeCache[sortType] = templateNodes;
                }

                // 붙일 때는 항상 새 복제본으로!
                var clones = CloneRange(templateNodes);
                treeViewInput.Nodes.AddRange(clones);

                _currentSortType = sortType;
            }
            finally
            {
                treeViewInput.EndUpdate();
                _suppressTreeEvents = false;
            }
        }

        private void comboBoxSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nOrderType = comboBoxSort.SelectedIndex;
            if(nOrderType != (int)INPUT_SORT.NO_TYPE)
            {
                DisplayInputTree(nOrderType);
            }
        }

        private string MakeOutputPtnItemType(string item)
        {
            string strRet = string.Empty;
            string strType = string.Empty;

            // 첫번째 문자가 타입
            strType = item.Substring(0, 1);
            switch(strType)
            {
                case "P":
                    strRet = TYPE_TEXT[(int)TYPE_TEXT_INDEX.PATTERN];
                    break;
                case "A":
                    strRet = TYPE_TEXT[(int)TYPE_TEXT_INDEX.EB];
                    break;
                case "M":
                    strRet = TYPE_TEXT[(int)TYPE_TEXT_INDEX.PUMP];
                    break;
                case "R":
                    strRet = TYPE_TEXT[(int)TYPE_TEXT_INDEX.FACP_CONTACT];
                    break;
                default:
                    strRet = TYPE_TEXT[(int)TYPE_TEXT_INDEX.UNKNOWN_TYPE];
                    break;
            }            

            return strRet;
        }

        private void DisplayOutputLists(Circuit circuit)
        {
            DataManager dataManager = DataManager.Instance;
            ImageManager imageManager = ImageManager.Instance;  
            Dictionary<string, Pattern> patternDic = dataManager.GetPatternDic();
            Dictionary<string, EB> ebDic = dataManager.GetEBDic();
            Dictionary<string, Pump> pumpDic = dataManager.GetPumpDic();
            Dictionary<string, Contact> contactDic = dataManager.GetContactDic();
            SortedDictionary<string, Circuit> circuitDic = dataManager.GetCircuitDic();

            // 출력 패턴 리스트 clear
            listViewOutputPtn.Items.Clear();

            // 출력 회로 리스트 clear
            listViewOutputCircuit.Items.Clear();

            listViewOutputPtn.BeginUpdate();
            // 선택된 입력 회로의 출력을 리스트에 삽입
            List<string> listContactControls = circuit.ContactControls;
            if (listContactControls.Count > 0)
            {
                foreach (string output in listContactControls)
                {
                    string strType = MakeOutputPtnItemType(output);

                    // 이름 찾기
                    string strName = string.Empty; 
                    if (strType == TYPE_TEXT[(int)TYPE_TEXT_INDEX.PATTERN])
                    {
                        if (patternDic.TryGetValue(output, out Pattern pattern))
                        {
                            strName = pattern.Name;
                        }
                    }
                    else if (strType == TYPE_TEXT[(int)TYPE_TEXT_INDEX.EB])
                    {
                        if(ebDic.TryGetValue(output, out EB eb))
                        {
                            // SLP4에서 표현하는 방식과 같은 방식
                            strName = eb.Remarks + " (" + eb.CommContent + ")";
                        }
                    }
                    else if (strType == TYPE_TEXT[(int)TYPE_TEXT_INDEX.PUMP])
                    {
                        if(pumpDic.TryGetValue(output, out Pump pump))
                        {
                            strName = pump.PumpName;
                        }
                    }
                    else if (strType == TYPE_TEXT[(int) TYPE_TEXT_INDEX.FACP_CONTACT])
                    {
                        if(contactDic.TryGetValue(output, out Contact contact))
                        {
                            strName = contact.Name;
                        }
                    }

                    listViewOutputPtn.Items.Add(new ListViewItem(new string[] { output, strName, strType}));
                }
            }
            listViewOutputPtn.EndUpdate();

            listViewOutputCircuit.BeginUpdate();
            // 선택된 입력 회로의 출력을 리스트에 삽입
            List<string> listRepeaterControls = circuit.RepeaterControls;
            if(listRepeaterControls.Count > 0)
            {
                int nNumber = 1;
                foreach(string output in listRepeaterControls)
                {
                    string strNumber = $"{nNumber:D3}";
                    string strFullOutputName = string.Empty;

                    // 이미지 아이템 인스턴스 생성
                    ListViewItem circuitRow = new ListViewItem();

                    if (circuitDic.TryGetValue(output, out Circuit outputCircuit))
                    {
                        // 회로번호(output)을 보고 입력 회로 Dictionary에서 출력 Full Name을 얻어서 추가
                        strFullOutputName = outputCircuit.OutputFullName;

                        // 회로번호를 기준으로 출력 내용을 얻음
                        string strOutputContent = outputCircuit.OutputContent;

                        // 이미지 키 값을 얻음
                        string strImageKey = imageManager.GetImageKey(strOutputContent);
                        if (strImageKey != string.Empty)
                        {
                            circuitRow.ImageKey = strImageKey;

                        }
                        else
                        {
                            circuitRow.ImageKey = imageManager.DEFAULT_IMAGE_NAME;
                            //circuitRow.ImageIndex = -1;     // 이미지 없음
                        }
                    }
                    else
                    {
                        strFullOutputName = $"[{output}] {CIRCUIT_INFO_CAN_NOT_FOUND_MSG}";
                        circuitRow.ImageKey = imageManager.DEFAULT_IMAGE_NAME;
                    }

                    circuitRow.SubItems.Add(strNumber);
                    circuitRow.SubItems.Add(strFullOutputName);

                    listViewOutputCircuit.Items.Add(circuitRow);

                    nNumber++;
                }
            }
            listViewOutputCircuit.EndUpdate();
        }

        private void treeViewInput_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_suppressTreeEvents) 
                return;

            TreeNode selectedNode = e.Node;

            //말단 노드(자식이 없는 노드)만 처리
            if (selectedNode.Nodes.Count == 0)
            {
                Circuit circuit;
                circuit = selectedNode.Tag as Circuit;

                //입력 회로 이름 갱신
                richTextBoxIputCircuit.Text = circuit.InputFullName;
                richTextBoxIputCircuit.SelectionAlignment = HorizontalAlignment.Center;

                //새 입력 회로의 출력으로 표시
                DisplayOutputLists(circuit);
            }
        }

        private void listViewOutputPtn_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvOutputPtnSorter.Column == e.Column)
            {
                // 이미 정렬한 컬럼이면 방향 토글
                lvOutputPtnSorter.Order = (lvOutputPtnSorter.Order == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // 새 컬럼 클릭 → 오름차순 시작
                lvOutputPtnSorter.Column = e.Column;
                lvOutputPtnSorter.Order = SortOrder.Ascending;
            }

            listViewOutputPtn.Sort(); // 정렬 수행
        }

        private void DisplayListViewPtnItemList(string patternNumber, bool bSearch)
        {
            // 출력 패턴 리스트 clear
            listViewPtnItemList.Items.Clear();
            //

            string type = MakeOutputPtnItemType(patternNumber);

            // 패턴이면
            if (type == TYPE_TEXT[(int)TYPE_TEXT_INDEX.PATTERN])
            {
                ImageManager imageManager = ImageManager.Instance;
                DataManager dataManager = DataManager.Instance;
                Dictionary<string, Pattern> patternDic = dataManager.GetPatternDic();
                Pattern pattern = patternDic[patternNumber];
                SortedDictionary<string, Circuit> circuitDic = dataManager.GetCircuitDic();

                // 패턴 명
                string strPatternName = patternNumber + " - " + pattern.Name;
                richTextBoxPtnName.Text = strPatternName;
                richTextBoxPtnName.SelectAll();
                richTextBoxPtnName.SelectionAlignment = HorizontalAlignment.Center;

                listViewPtnItemList.BeginUpdate();
                // 패턴 아이템 리스트 표시
                foreach (string item in pattern.Items)
                {
                    string strFullOutputName = string.Empty;

                    // 이미지 아이템 인스턴스 생성
                    ListViewItem itemRow = new ListViewItem();

                    if (circuitDic.TryGetValue(item, out Circuit circuit))
                    {
                        strFullOutputName = circuit.OutputFullName;

                        // 회로번호를 기준으로 출력 내용을 얻음
                        string strOutputContent = circuit.OutputContent;

                        // 이미지 키 값을 얻음
                        string strImageKey = imageManager.GetImageKey(strOutputContent);
                        if (strImageKey != string.Empty)
                        {
                            itemRow.ImageKey = strImageKey;

                        }
                        else
                        {
                            itemRow.ImageKey = imageManager.DEFAULT_IMAGE_NAME;
                            //circuitRow.ImageIndex = -1;     // 이미지 없음
                        }
                    }
                    else
                    {
                        strFullOutputName = $"[{item}] {CIRCUIT_INFO_CAN_NOT_FOUND_MSG}";
                        itemRow.ImageKey = imageManager.DEFAULT_IMAGE_NAME;
                    }

                    // 검색 결과로써 패턴 아이템 리스트를 보여줄 때는 검색어와 매칭되는 row는 하이라이트 처리
                    if (bSearch)
                    {
                        string strSearch = textBoxSearch.Text;

                        if (strSearch != string.Empty)
                        {
                            if (strFullOutputName.Contains(strSearch))
                            {
                                itemRow.BackColor = Color.Blue;
                                itemRow.ForeColor = Color.White;
                            }
                        }
                    }

                    itemRow.SubItems.Add(strFullOutputName);
                    listViewPtnItemList.Items.Add(itemRow);

                    /*
                    Circuit circuit = circuitDic[item];

                    string strFullOutputName = circuit.OutputFullName;

                    // 회로번호를 기준으로 출력 내용을 얻음
                    string strOutputContent = circuit.OutputContent;

                    // 이미지 아이템 인스턴스 생성
                    ListViewItem itemRow = new ListViewItem();

                    // 이미지 키 값을 얻음
                    string strImageKey = imageManager.GetImageKey(strOutputContent);
                    if (strImageKey != string.Empty)
                    {
                        itemRow.ImageKey = strImageKey;

                    }
                    else
                    {
                        itemRow.ImageKey = imageManager.DEFAULT_IMAGE_NAME;
                        //circuitRow.ImageIndex = -1;     // 이미지 없음
                    }
                    itemRow.SubItems.Add(strFullOutputName);

                    // 검색 결과로써 패턴 아이템 리스트를 보여줄 때는 검색어와 매칭되는 row는 하이라이트 처리
                    if(bSearch)
                    {
                        string strSearch = textBoxSearch.Text;
                        if(strFullOutputName.Contains(strSearch))
                        {
                            itemRow.BackColor = Color.Blue;
                            itemRow.ForeColor = Color.White;
                        }
                    }

                    listViewPtnItemList.Items.Add(itemRow);
                    */
                }
                listViewPtnItemList.EndUpdate();
            }
        }

        private void listViewOutputPtn_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                // 패턴 이름 초기화
                richTextBoxPtnName.Text = string.Empty;

                // 패턴 아이템 리스트 갱신
                string key = e.Item.SubItems[0].Text;
                DisplayListViewPtnItemList(key, false);
            }
        }

        private void listViewOutputCircuit_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvOutputCircuitSorter.Column == e.Column)
            {
                // 이미 정렬한 컬럼이면 방향 토글
                lvOutputCircuitSorter.Order = (lvOutputCircuitSorter.Order == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // 새 컬럼 클릭 → 오름차순 시작
                lvOutputCircuitSorter.Column = e.Column;
                lvOutputCircuitSorter.Order = SortOrder.Ascending;
            }

            listViewOutputCircuit.Sort(); // 정렬 수행
        }

        private void listViewPtnItemList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lvPtnItemListSorter.Column == e.Column)
            {
                // 이미 정렬한 컬럼이면 방향 토글
                lvPtnItemListSorter.Order = (lvPtnItemListSorter.Order == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // 새 컬럼 클릭 → 오름차순 시작
                lvPtnItemListSorter.Column = e.Column;
                lvPtnItemListSorter.Order = SortOrder.Ascending;
            }

            listViewPtnItemList.Sort(); // 정렬 수행
        }

        private void DisplayPtnSearchTree(List<string> matchedPtnList)
        {
            DataManager dataManager = DataManager.Instance;
            Dictionary<string, Pattern> patternDic = dataManager.GetPatternDic();

            treeViewPtnSearch.BeginUpdate();
            foreach(string patternNumber in matchedPtnList)
            {
                Pattern pattern = patternDic[patternNumber];
                string name = patternNumber + " - " + pattern.Name;
                TreeNode nameNode = new TreeNode(name);
                nameNode.Tag = pattern;
                treeViewPtnSearch.Nodes.Add(nameNode);
            }
            treeViewPtnSearch.EndUpdate();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 기존에 남아있는 검색 내용을 Clear
            treeViewPtnSearch.Nodes.Clear();

            // 검색어 문자열을 가져옴
            string strSearch = textBoxSearch.Text;

            if (strSearch == string.Empty)
            {
                MessageBox.Show("검색어를 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if(strSearch.Length < 2)
            {
                MessageBox.Show("검색어는 두 글자 이상이어야 합니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 패턴 Dictionary를 순회하면서 패턴 아이템을 순회, 회로번호를 기반으로 회로 Dictionary에서 Full Output Name과 비교 매칭
            // 조건에 만족하는 회로가 발견되면 패턴을 리스트에 추가하고 해당 패턴 내의 패턴 아이템 순회를 중단하고 다음 패턴으로 넘어감 (ver1.0)
            // 검색 대상을 전체 패턴 Dictionary에서 현재 선택된 입력 회로에 대한 출력 중 패턴들로 변경 (ver1.1)

            List<string> ptnList = GetPatternNameListFromListViewOutputPtn();
            if(ptnList.Count > 0)
            {
                List<string> matchedPtnList = new List<string>();
                DataManager dataManager = DataManager.Instance;
                Dictionary<string, Pattern> patternDic = dataManager.GetPatternDic();
                SortedDictionary<string, Circuit> circuitDic = dataManager.GetCircuitDic();

                foreach(string patternName in ptnList)
                {
                    if(patternDic.TryGetValue(patternName, out Pattern pattern))
                    {
                        bool bMatched = false;
                        List<string> patternItem = pattern.Items;

                        foreach (string item in patternItem)
                        {
                            string outputFullName = string.Empty;

                            // 회로 정보에서 찾으면 검색어와 비교, 못 찾으면 단순히 회로 번호만 검색어와 일치하는 지 검사
                            if (circuitDic.TryGetValue(item, out Circuit circuit))
                            {
                                outputFullName = circuit.OutputFullName;
                            }
                            else
                            {
                                outputFullName = item + " " + CIRCUIT_INFO_CAN_NOT_FOUND_MSG;
                            }

                            if (outputFullName.Contains(strSearch))
                            {
                                bMatched = true;
                                break;
                            }
                        }

                        if (bMatched)
                        {
                            matchedPtnList.Add(patternName);
                        }
                    }
                }

                if (matchedPtnList.Count > 0)
                {
                    DisplayPtnSearchTree(matchedPtnList);
                }
                else
                {
                    MessageBox.Show("검색어와 매칭되는 출력 회로가 포함된 패턴이 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("현재 입력 회로에 해당하는 패턴이 리스트에 없습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 원래 Afterselect로 처리했으나 검색된 노드가 1개일때 클릭 이벤트가 발생하지 않아서 이 매서드로 변경, 대신 선택 상태의 정보를 줘야 함
        private void treeViewPtnSearch_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            treeViewPtnSearch.SelectedNode = selectedNode;
            Pattern pattern = selectedNode.Tag as Pattern;
            string patternNumber = $"P{pattern.Number}";
            DisplayListViewPtnItemList(patternNumber, true);
        }

        private void btnOpen_MouseHover(object sender, EventArgs e)
        {
            this.toolTipOpen.ToolTipTitle = "연동표";
            this.toolTipOpen.IsBalloon = true;
            this.toolTipOpen.SetToolTip(this.btnOpen, "열기");
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            this.toolTipClose.ToolTipTitle = "연동표";
            this.toolTipClose.IsBalloon = true;
            this.toolTipClose.SetToolTip(this.btnClose, "닫기");
        }

        private void SetImagesInInputCircuitTreeView()
        {
            ImageManager imageManager = ImageManager.Instance;

            // exe파일이 실행 중인 현재 경로
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            
            // 새로 쓰일 이미지 리스트
            ImageList imageList = new ImageList()
            {
                ImageSize = new Size(24, 24),
                ColorDepth = ColorDepth.Depth32Bit
            };

            int nImageNameList = imageManager.TREE_IMAGES_NAME.Length;
            for (int i = 0; i < nImageNameList; i++)
            {
                // 이미지 파일 이름을 이미지 리스트의 키 값으로 넣음
                string imageName = imageManager.TREE_IMAGES_NAME[i];
                string imagePath = Path.Combine(baseDir, imageManager.IMAGES_FOLDER, imageName);
                imageList.Images.Add(imageName, Image.FromFile(imagePath));
            }

            treeViewInput.ImageList = imageList;    
        }
        private void SetImagesInListView()
        {
            ImageManager imageManager = ImageManager.Instance;
            List<string> outputContentList = imageManager.GetOutputContentList();
            ImageList imageListSmall = new ImageList();
            ImageList imageListLarge = new ImageList();
            imageListSmall.ImageSize = new Size(48, 48);
            imageListLarge.ImageSize = new Size(128, 128);

            foreach (string outputContent in outputContentList)
            {
                string imagePath = imageManager.GetOutputContentImagePath(outputContent);
                imageListSmall.Images.Add(outputContent, Image.FromFile(imagePath));
                imageListLarge.Images.Add(outputContent, Image.FromFile(imagePath));
            }

            listViewOutputCircuit.SmallImageList = imageListSmall;
            listViewOutputCircuit.LargeImageList = imageListLarge;

            listViewPtnItemList.SmallImageList = imageListSmall;
            listViewPtnItemList.LargeImageList = imageListLarge;
        }

        private void SetImagesInSearchTreeView()
        {
            ImageManager imageManager = ImageManager.Instance;

            // exe파일이 실행 중인 현재 경로
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(baseDir, imageManager.IMAGES_FOLDER, imageManager.TREE_IMAGES_NAME[(int)ImageManager.ENUM_TREE_IMAGES.PATTERN]);

            // 새로 쓰일 이미지 리스트
            ImageList imageList = new ImageList()
            {
                ImageSize = new Size(24, 24),
                ColorDepth = ColorDepth.Depth32Bit
            };
            imageList.Images.Add("pattern", Image.FromFile(imagePath));

            treeViewPtnSearch.ImageList = imageList;
        }

        private List<string> GetPatternNameListFromListViewOutputPtn()
        {
            List<string> patternNameList = new List<string>();
            if (listViewOutputPtn.Items.Count > 0)
            {
                foreach(ListViewItem item in listViewOutputPtn.Items)
                {
                    string type = item.SubItems[2].Text;
                    if(type == TYPE_TEXT[(int)TYPE_TEXT_INDEX.PATTERN])
                    {
                        string patternName = item.SubItems[0].Text;
                        patternNameList.Add(patternName);
                    }
                }
            }

            return patternNameList;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            // 실행 중인 exe의 경로
            string exePath = AppDomain.CurrentDomain.BaseDirectory;

            // 사용 설명서 경로
            string helpFilePath = Path.Combine(exePath, HELP_FILE_NAME);

            if (!File.Exists(helpFilePath))
            {
                MessageBox.Show("사용 설명서 파일이 존재하지 않습니다.\n" + helpFilePath, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = helpFilePath,
                    UseShellExecute = true
                };

                var proc = Process.Start(psi);

                if (proc == null)
                {
                    MessageBox.Show("PDF 뷰어 실행에 실패했습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("사용 설명서 파일을 여는 중 오류가 발생했습니다.\n" + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHelp_MouseHover(object sender, EventArgs e)
        {
            this.toolTipHelp.ToolTipTitle = "사용 설명서";
            this.toolTipHelp.IsBalloon = true;
            this.toolTipHelp.SetToolTip(this.btnHelp, "보기");
        }
    }
}
