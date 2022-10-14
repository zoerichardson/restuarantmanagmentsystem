namespace restuarantmanagmentsystem.Models
{
    public class DayRevenue
    {
        [Key]
        public int ID { get; set; }

        public string Day { get; set; } = string.Empty;
        public float DayTotal { get; set; }

    }
}
