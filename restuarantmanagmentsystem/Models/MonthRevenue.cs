namespace restuarantmanagmentsystem.Models
{
    public class MonthRevenue
    {

        [Key]
        public int ID { get; set; }

        public int Month { get; set; } 
        public float MonthTotal { get; set; }
        
    }
}
