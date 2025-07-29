using Microsoft.EntityFrameworkCore;

namespace Clinic_Management.Models
{
    public class myContext : DbContext
    {
        public myContext(DbContextOptions<myContext> options) :base(options)
        {
            
        }

        public DbSet<Admin> tbl_admins { get; set; }
        public DbSet<User> tbl_users { get; set; }
        public DbSet<Apparatus> tbl_apparatus { get; set; }
        public DbSet<Medicines> tbl_medicines { get; set; }
        public DbSet<Staff> tbl_staff { get; set; }
        public DbSet<Contact_us> tbl_contact_us { get; set; }
        public DbSet<Feedback> tbl_feedback { get; set; }
        public DbSet<Cart> tbl_cart { get; set; }
        public DbSet<Order> tbl_order { get; set; }
        public DbSet<FAQS> tbl_faqs { get; set; }
        public DbSet<Services> tbl_services { get; set; }
        public DbSet<Department> tbl_department { get; set; }
        public DbSet<Booking> tbl_bookings { get; set; }

    }
}
