namespace restuarantmanagmentsystem.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        
        public int TableNumber { get; set; }
        

        public bool IsAvailable { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
