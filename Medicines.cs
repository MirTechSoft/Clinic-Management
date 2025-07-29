namespace Clinic_Management.Models
{
    public class Medicines
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Medicine_Code { get; set; }

        public string? Medicine_Type { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public string? Image {  get; set; }
    }
}
