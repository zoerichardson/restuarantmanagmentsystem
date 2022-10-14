namespace restuarantmanagmentsystem.Models
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }
       

        public string Name { get; set; } = string.Empty;
        

        public float Price { get; set; }
        public int CategoryType { get; set; }
    }
}
