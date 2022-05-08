using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FINALEXAM_2013829
{
    public class Student
    {
        public static string collectionName = "Students";

        [JsonIgnore]
        public string Uid { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }

        public async Task<bool> Save()
        {
            try
            {
                if (this.Uid == null)  // If Uid is null = new student
                {
                    var response = await DataSource.db.Child(Student.collectionName).PostAsync(this);

                    if (response.Key != null)
                        this.Uid = response.Key;
                }
                else    // If Uid is !null = update existing student
                {
                    await DataSource.db.Child(Student.collectionName).Child(this.Uid).PutAsync(this);
                }
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }
        }

        public async Task<bool> Delete()
        {
            if (this.Uid == null)
                return false;

            try
            {
                await DataSource.db.Child(Student.collectionName).Child(this.Uid).DeleteAsync();
                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                return false;
            }
        }

        public static void StartObserving(ObservableCollection<Student> listToUpdate)
        {
            DataSource.db.Child(Student.collectionName)
                .AsObservable<Student>()
                .Subscribe((dbEvent) =>
                {
                    if (dbEvent.Object != null)
                    {
                        dbEvent.Object.Uid = dbEvent.Key;
                        for (int i = 0; i < listToUpdate.Count; i++)
                        {
                            if (listToUpdate[i].Uid.Equals(dbEvent.Object.Uid)) //record Found
                            {
                                if (dbEvent.EventType == Firebase.Database.Streaming.FirebaseEventType.InsertOrUpdate)
                                {
                                    listToUpdate[i] = dbEvent.Object;
                                }
                                else //delete
                                {
                                    listToUpdate.RemoveAt(i);
                                }
                                return;
                            }
                        }

                        listToUpdate.Add(dbEvent.Object);
                    }
                });
        }
    }
}