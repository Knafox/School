namespace StudentInfo
{
    class StudentInfo
    {
        private int? _StudID;
        private string _Name;
        private string _SSN;
        private string _EmailAddress;
        private string _HomePhone;
        private string _HomeAddr;
        private string _LocalAddr;
        private string _EmergencyContact;
        private int? _ProgramID;
        private string _PaymentID;
        private int? _AcademicStatus;

        public int? StudID { get => _StudID; set => _StudID = value; }
        public string Name { get => _Name; set => _Name = value; }
        public string SSN { get => _SSN; set => _SSN = value; }
        public string EmailAddress { get => _EmailAddress; set => _EmailAddress = value; }
        public string HomePhone { get => _HomePhone; set => _HomePhone = value; }
        public string HomeAddr { get => _HomeAddr; set => _HomeAddr = value; }
        public string LocalAddr { get => _LocalAddr; set => _LocalAddr = value; }
        public string EmergencyContact { get => _EmergencyContact; set => _EmergencyContact = value; }
        public int? ProgramID { get => _ProgramID; set => _ProgramID = value; }
        public string PaymentID { get => _PaymentID; set => _PaymentID = value; }
        public int? AcademicStatus { get => _AcademicStatus; set => _AcademicStatus = value; }

        public StudentInfo()
        {
            _StudID = null;
            _Name = null;
            _SSN = null;
            _EmailAddress = null;
            _HomePhone = null;
            _HomeAddr = null;
            _LocalAddr = null;
            _EmergencyContact = null;
            _ProgramID = null;
            _PaymentID = null;
            _AcademicStatus = null;
        }

        public StudentInfo(int StudentID, string Name, string SSN, string EmailAddress, 
            string HomePhone, string HomeAddress, string LocalAddress, string EmergencyContact, 
            int ProgramID, string PaymentID, int AcedemicStatus)
        {
            _StudID = StudentID;
            _Name = Name;
            _SSN = SSN;
            _EmailAddress = EmailAddress;
            _HomePhone = HomePhone;
            _HomeAddr = HomeAddress;
            _LocalAddr = LocalAddress;
            _EmergencyContact = EmergencyContact;
            _ProgramID = ProgramID;
            _PaymentID = PaymentID;
            _AcademicStatus = AcedemicStatus;
        }
    }
}
