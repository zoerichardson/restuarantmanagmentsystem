namespace restuarantmanagmentsystem.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public int Passcode { get; set; }

        public ICollection<Table>? Tables { get; set; }  
    }
}
