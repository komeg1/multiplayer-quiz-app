using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiplayerQuizGame.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace MultiplayerQuizGame.Shared.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
        : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionChoice> Question_Choices { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
    }
}