﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiplayerQuizGame.Shared.Data;

#nullable disable

namespace MultiplayerQuizGame.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.QuestionChoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ChoiceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsTrue")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("QuestionChoice");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("HostConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<int?>("QuizId")
                        .HasColumnType("int");

                    b.Property<string>("RoomCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.UserQuizStamp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameMode")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("UserQuizStamp");
                });

            modelBuilder.Entity("QuestionQuiz", b =>
                {
                    b.Property<int>("QuestionsId")
                        .HasColumnType("int");

                    b.Property<int>("QuizzesId")
                        .HasColumnType("int");

                    b.HasKey("QuestionsId", "QuizzesId");

                    b.HasIndex("QuizzesId");

                    b.ToTable("QuestionQuiz");
                });

            modelBuilder.Entity("RoomUser", b =>
                {
                    b.Property<int>("RoomsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RoomsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoomUser");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Guest", b =>
                {
                    b.HasOne("MultiplayerQuizGame.Shared.Models.Room", null)
                        .WithMany("Guests")
                        .HasForeignKey("RoomId");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.QuestionChoice", b =>
                {
                    b.HasOne("MultiplayerQuizGame.Shared.Models.Question", "Question")
                        .WithMany("QuestionChoices")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Room", b =>
                {
                    b.HasOne("MultiplayerQuizGame.Shared.Models.Quiz", "Quiz")
                        .WithMany("Rooms")
                        .HasForeignKey("QuizId");

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.UserQuizStamp", b =>
                {
                    b.HasOne("MultiplayerQuizGame.Shared.Models.Quiz", "Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiplayerQuizGame.Shared.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");

                    b.Navigation("User");
                });

            modelBuilder.Entity("QuestionQuiz", b =>
                {
                    b.HasOne("MultiplayerQuizGame.Shared.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiplayerQuizGame.Shared.Models.Quiz", null)
                        .WithMany()
                        .HasForeignKey("QuizzesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoomUser", b =>
                {
                    b.HasOne("MultiplayerQuizGame.Shared.Models.Room", null)
                        .WithMany()
                        .HasForeignKey("RoomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiplayerQuizGame.Shared.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Question", b =>
                {
                    b.Navigation("QuestionChoices");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Quiz", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("MultiplayerQuizGame.Shared.Models.Room", b =>
                {
                    b.Navigation("Guests");
                });
#pragma warning restore 612, 618
        }
    }
}
