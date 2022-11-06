using Microsoft.EntityFrameworkCore;
using System;

namespace Entity
{
    //Klasa do mapowania w bazie
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
    //Mapowanie w bazie
    class ProductContext:DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //String Builder do łączenia się z bazą
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-L2JMNQT;Initial catalog=DBTest; Integrated Security=true");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using (ProductContext db = new ProductContext())
            {
                Product p1 = new Product { Name = "TV", Price = 3000 };
                //Dodawanie obiektu do bazy
                db.Products.Add(p1);

                //Iterowanie po obiektach w bazie
                foreach(var item in db.Products)
                {
                    Console.WriteLine(item.Name);

                    //Modyfikacja obiektu w bazie
                    if (item.Name == "Phone")
                    {
                        item.Name = "Mobile-Phone";
                        db.Products.Update(item);
                    }

                }
   
                //Zapisywanie obiektu w bazie
                if(db.SaveChanges()>0)
                {
                    Console.WriteLine("Product added successfully!");
                }
                else
                    Console.WriteLine("Fail to add product!");
            }
                Console.WriteLine("Hello World!");
        }
    }
}
