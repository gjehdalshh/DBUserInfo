using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DBUserInfo
{
    public partial class form1 : Form
    {
        private bool loadCompleted = false;
        private int studentDelCheck = 0;
        private int studentNullCheck = 0;

        public form1()
        {
            InitializeComponent();
            InitVariables();
            //studentCount();
            //loadCheck();
        }

        private void loadCheck() { // 학생 정보를 자동으로 불러오기 위한 함수 form1 실행 시 바로 함수가 실행됨
            StreamReader sr = new StreamReader(new FileStream("studentLoad.dat", FileMode.Open));

            String str = sr.ReadLine(); // 파일을 읽어 string에 저장
            sr.Close();

            if (str.Equals("체크 완료")) { // 읽은 str이 체크 완료라면
                autoStudentLoad.Checked = true; // 체크 상태로 변경
                loadCompleted = true;
                LoadStudent(); // 불러오기 함수
            }      
        }

        private void studentCount() { // 현재 파일에 저장된 학생의 수를 세는 함수
            BinaryReader br = new BinaryReader(new FileStream("student.stu", FileMode.Open));
            int count = 0;

            while (br.PeekChar() != -1) { // 파일의 끝까지 while
                String str = br.ReadString(); // 파일에서 읽어 string에 저장
                if(str.Contains("학생 정보")) { // 파일에서 받은 str에 학생 정보가 있다면
                    count++;
                }
            }
            loadStudentCount.Text = "현재 저장된 학생수 : " + count;

            br.Close();
        }

        private void InitVariables()
        {
            comboBoxStudent2Gender.Items.Clear();
            comboBoxStudent2Gender.Items.Add("남자");
            comboBoxStudent2Gender.Items.Add("여자");

            String[] genderData = { "남자", "여자"};
            comboBoxStudent3Gender.Items.Clear();
            comboBoxStudent3Gender.Items.AddRange(genderData);

           // comboBoxStudent1Gender.SelectedIndex = 0; // selected
           // comboBoxStudent2Gender.SelectedIndex = 1; 
        }

        private void buttonLoadStudentInfo_Click(object sender, EventArgs e)
        {
            LoadStudent();
            loadCompleted = true;
        }

        private void buttonStudent2Save_Click(object sender, EventArgs e)
        {
            SaveStudent();
        }

        private void buttonStudent3Save_Click(object sender, EventArgs e)
        {
            SaveStudent();
        }

        #region Student 1 Event handlers

        private void buttonStudent1Save_Click(object sender, EventArgs e)
        {
            SaveStudent();
        }
        #endregion

        private void SaveStudent() // 학생 정보를 저장하는 함수
        {
            /*
            if(loadCompleted == false) { // 불러오기를 하지 않았다면 return
                return;
            }
            */

            StudentInfo studentInfo = new StudentInfo();

                //studentInfo.SetName(textBoxStudent1Name.Text);
                studentInfo.name_ = textBoxStudent1Name.Text;
                studentInfo.gender_ = comboBoxStudent1Gender.Text;
                studentInfo.sid_ = textBoxStudent1ID.Text;
            

            // 직렬화
            Stream ws = new FileStream("student.stu", FileMode.Create);
            BinaryFormatter serializer = new BinaryFormatter();

            serializer.Serialize(ws, studentInfo);
            ws.Close();


            StreamWriter sw = new StreamWriter(new FileStream("studentInfo.stu", FileMode.Create));


            /*
            BinaryWriter bw = new BinaryWriter(new FileStream("student.stu", FileMode.Create));

            if (textBoxStudent1Name.Text.Equals("")) { // 저장 시 name에 값이 없다면
                studentNullCheck = 1; 
            } else { // 값이 존재한다면
                studentNullCheck = 0;
            }
            if (studentDelCheck != 1 && studentNullCheck == 0) { // 학생 정보 삭제를 누르지 않고 값이 존재한다면 값 삽입
                bw.Write("1번 학생 정보");
                bw.Write(textBoxStudent1Name.Text);
                bw.Write(textBoxStudent1ID.Text);
                bw.Write(comboBoxStudent1Gender.Text);
                bw.Write(textBoxStudent1Info.Text);
                Console.WriteLine("a");
            }
            if(studentDelCheck == 1) { // 학생 정보 삭제를 눌렀다면 text를 빈 값으로 초기화
                textBoxStudent1Name.Text = "";
                textBoxStudent1ID.Text = "";
                comboBoxStudent1Gender.Text = "";
                textBoxStudent1Info.Text = "";
            }

            // 학생 정보 1과 같음
            if (textBoxStudent2Name.Text.Equals("")) { 
                studentNullCheck = 1;
            } else {
                studentNullCheck = 0;
            }
            if (studentDelCheck != 2 && studentNullCheck == 0) {
                bw.Write("2번 학생 정보");
                bw.Write(textBoxStudent2Name.Text);
                bw.Write(textBoxStudent2ID.Text);
                bw.Write(comboBoxStudent2Gender.Text);
                bw.Write(textBoxStudent2Info.Text);
                Console.WriteLine("b");
            }
            if (studentDelCheck == 2) {
                textBoxStudent2Name.Text = "";
                textBoxStudent2ID.Text = "";
                comboBoxStudent2Gender.Text = "";
                textBoxStudent2Info.Text = "";
            }


            if (textBoxStudent3Name.Text.Equals("")) {
                studentNullCheck = 1;
            } else {
                studentNullCheck = 0;
            }
            if (studentDelCheck != 3 && studentNullCheck == 0) {
                bw.Write("3번 학생 정보");
                bw.Write(textBoxStudent3Name.Text);
                bw.Write(textBoxStudent3ID.Text);
                bw.Write(comboBoxStudent3Gender.Text);
                bw.Write(textBoxStudent3Info.Text);
                Console.WriteLine("c");
            }
            if (studentDelCheck == 3) {
                textBoxStudent3Name.Text = "";
                textBoxStudent3ID.Text = "";
                comboBoxStudent3Gender.Text = "";
                textBoxStudent3Info.Text = "";
            }

            // 저장이 끝난 후 초기화
            studentDelCheck = 0;
            studentNullCheck = 0;
            bw.Close();

            */

        }

        private void LoadStudent()
        {
            // 역 직렬화
            Stream rs = new FileStream("student.stu", FileMode.Open);
            BinaryFormatter deserializer = new BinaryFormatter();

            StudentInfo studentInfo = (StudentInfo)deserializer.Deserialize(rs);

            rs.Close();

            textBoxStudent1Name.Text = studentInfo.name_;
            textBoxStudent1ID.Text = studentInfo.sid_;
            comboBoxStudent1Gender.Text = studentInfo.gender_;

            /*
            BinaryReader br = new BinaryReader(new FileStream("student.stu", FileMode.Open));
            String str;
            
            while(br.PeekChar() != -1) // 파일의 끝까지 반복
            {
                str = br.ReadString();

                // str에 담긴 학생 정보 읽기
                if(str.Equals("1번 학생 정보")) { 
                    textBoxStudent1Name.Text = br.ReadString();
                    textBoxStudent1ID.Text = br.ReadString();
                    comboBoxStudent1Gender.Text = br.ReadString();
                    textBoxStudent1Info.Text = br.ReadString();
                    Console.WriteLine("1번");
                }
                if(str.Equals("2번 학생 정보")){
                    textBoxStudent2Name.Text = br.ReadString();
                    textBoxStudent2ID.Text = br.ReadString();
                    comboBoxStudent2Gender.Text = br.ReadString();
                    textBoxStudent2Info.Text = br.ReadString();
                    Console.WriteLine("2번");
                }
                    if(str.Equals("3번 학생 정보")) {
                    textBoxStudent3Name.Text = br.ReadString();
                    textBoxStudent3ID.Text = br.ReadString();
                    comboBoxStudent3Gender.Text = br.ReadString();
                    textBoxStudent3Info.Text = br.ReadString();
                    Console.WriteLine("3번");
                }
            }

            br.Close();  
            */
        }

        private void buttonStudent1Delete_Click(object sender, EventArgs e)
        {
            studentDelCheck = 1;
            SaveStudent();
        }

        private void buttonStudent2Delete_Click(object sender, EventArgs e)
        {
            studentDelCheck = 2;
            SaveStudent();
        }

        private void buttonStudent3Delete_Click(object sender, EventArgs e)
        {
            studentDelCheck = 3;
            SaveStudent();
        }


        private void autoStudentLoad_CheckedChanged(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(new FileStream("studentLoad.dat", FileMode.Create));

            if(autoStudentLoad.Checked) { // 자동 저장이 체크 되어 있다면 파일에 체크 완료 write
                sw.WriteLine("체크 완료");
            }
            else {
                sw.WriteLine("체크 준비");
            }

            sw.Close();
        }
    }
}
