namespace School.Models
{
    public class Student
    {
        public Student(string name, string emailId, long phoneNo)
        {
            Name = name;
            EmailId = emailId;
            PhoneNo = phoneNo;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public long PhoneNo { get; set; }
        public string Cources { get; set; } = string.Empty;
    }
}
