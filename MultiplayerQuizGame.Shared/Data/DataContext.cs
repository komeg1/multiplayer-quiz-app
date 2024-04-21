using MultiplayerQuizGame.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiplayerQuizGame.Shared.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
        : base(options){}

        public DbSet<User> User { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionChoice> QuestionChoice { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<UserQuizStamp> UserQuizStamp { get; set;}

        
    }
}