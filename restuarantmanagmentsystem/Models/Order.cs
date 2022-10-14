namespace restuarantmanagmentsystem.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }

        public string Items { get; set; } = string.Empty;

        public float Total { get; set; }
          
        public Staff? Staff { get; set; }
        public int? StaffID { get; set; }

        
        public Table? Table { get; set; }
        public int? TableNumber { get; set; }
    }
}
