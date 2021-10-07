using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUserInfo
{
    [Serializable]
    class StudentInfo
    {
        /*
        private string name_; // 이름 
        private string sid_; // 학번
        private string gender_; // 성별

        public String GetName() {
            return name_;
        }
        public void SetName(String name) {
            this.name_ = name;
        }
        */
        /*
        private string name_;
        public string name { 
            get {
                return name_;
            }
            set {
                if(value == null) {
                    return;
                }
                name_ = value;
            }
        }
        */
        public string name_ { get; set; }
        public string sid_ { get; set; }
        public string gender_ { get; set; }
    }
}
