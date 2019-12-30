using FakeData;
using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {  

            //admin kullanıcı ekleme işlemi.
            EverNoteUser admin = new EverNoteUser()
            {
                Name = "Aydan",
                SurName = "Torun",
                Email = "aydann.15@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive=true,
                IsAdmin=true,
                UserName="aydann",
                Password="12345",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now.AddMinutes(5),
                ModifiedUserName="AydanT",
                ProfilImageFileName="userimg.jfif"

            };

            //standart kullanıcı ekleme.

            EverNoteUser standartUser = new EverNoteUser()
            {
                Name = "Maykıl",
                SurName = "Ceksın",
                Email = "ceksın@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "ceksın",
                Password = "54321",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "Maykılc",
                ProfilImageFileName = "userimg.jfif"

            };

            context.EverNoteUsers.Add(admin);
            context.EverNoteUsers.Add(standartUser);


            //fake kullanıcı ekleme.

            for (int i = 0; i < 8; i++)
            {
                EverNoteUser fakeuser = new EverNoteUser()
                {

                    Name = NameData.GetFirstName(),
                    SurName = NameData.GetSurname(),
                    Email = NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"user{i}",
                    Password = "123",
                    CreatedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2),DateTime.Now),
                    ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                    ModifiedUserName = $"user{i}",
                    ProfilImageFileName = "userimg.jfif"


                };
                context.EverNoteUsers.Add(fakeuser);
            }
            
            context.SaveChanges();

            //User List For using


            List<EverNoteUser> userList = context.EverNoteUsers.ToList();

            //Adding fake Categories
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title=PlaceData.GetStreetName(),
                    Description=PlaceData.GetAddress(),
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUserName="AydanT"
                };

                context.Categories.Add(cat);
                //Adding fake Notes

                for (int k = 0; k < NumberData.GetNumber(5,9); k++)
                {
                    EverNoteUser owner = userList[NumberData.GetNumber(0, userList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = TextData.GetAlphabetical(NumberData.GetNumber(5, 25)),
                        Text = TextData.GetSentences(NumberData.GetNumber(1,3)),
                        Category=  cat,
                        IsDraft=false,
                        LikeCount=NumberData.GetNumber(1,9),
                        Owner=owner ,
                        CreatedOn= DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                        ModifiedUserName = owner.UserName


                    };
                    cat.Notes.Add(note);

                    //Adding fake comments

                    for (int j = 0; j < NumberData.GetNumber(3,5); j++)
                    {
                        EverNoteUser comment_owner = userList[NumberData.GetNumber(0, userList.Count - 1)];
                        Comment comment =new Comment()
                        { 

                             Text = TextData.GetSentence(),
                             Owner=comment_owner,
                            CreatedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedOn = DateTimeData.GetDatetime(DateTime.Now.AddYears(-2), DateTime.Now),
                            ModifiedUserName = comment_owner.UserName,
                          //  Note=note
                    
                        };
                        note.Comments.Add(comment);

                        //Adding sahte beğeniler

                        for (int m = 0; m < note.LikeCount; m++)
                        {
                            Liked liked = new Liked()
                            {
                                LikedUser= userList[m]

                            };
                            note.Likes.Add(liked);
                        }
                    }
                }

                context.SaveChanges();


            }

        }
    }
}
