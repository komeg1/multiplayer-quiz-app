using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiplayerQuizGame.Components.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
        : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Question_choices> Question_Choices { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
    }
}