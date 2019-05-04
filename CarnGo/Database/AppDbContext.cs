using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CarnGo.Database
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mowinckel.database.windows.net,1433;Initial Catalog = CarnGo; Persist Security Info=False;User ID = ProjectDB@mowinckel;Password=Vores1.sødedatabase;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30");

        } 
        private DbSet<CarEquipment> CarEquipment { get; set; }
        private DbSet<User> Users { get; set; }
        private DbSet<Message> Messages { get; set; }
        private DbSet<CarProfile> CarProfiles { get; set; }
        private DbSet<DayThatIsRented> DaysThatIsRented { get; set; }
        private DbSet<PossibleToRentDay> PossibleToRentDays { get; set; }

        //reposetory pattern, CRUD

        public void AddDayThatIsRentedList(List<DayThatIsRented> list)
        {
            DaysThatIsRented.AddRange(list);
            SaveChanges();
        }

        //Get
        public async Task<User> GetUser(string email, SecureString password)
        {
            var user = await Users.FindAsync(email);
            user.AuthorizationString = Guid.NewGuid();
            if (user.Password != password)
                throw new AuthenticationFailedException();
            return user;
        }


        public async Task<User> GetUser(string email, Guid authorization)
        {
            var user = await Users.FindAsync(email);
            if(user == null)
                throw new AuthenticationFailedException();
            if (user.AuthorizationString != authorization)
                throw new AuthorizationFailedException();
            return user;
        }

        public async Task<List<Message>> GetMessages(User user)
        {
            var messages = await Messages
                .Include(msg => msg.MessagesWithUsers
                    .Where(mwu => mwu.User == user))
                .ToListAsync();
            
            return messages;
        }

        //Update
        public void UpdateCarEquipment(CarEquipment carEquipment)
        {
            var result = CarEquipment.Single(b => b.CarEquipmentID == carEquipment.CarEquipmentID);

            if (result == null) return;
            result = carEquipment;
            SaveChanges();
        }

        public void UpdateUser(User user)
        {
            var result = Users.Single(b => b.Email == user.Email);

            if (result == null) return;
            result = user;
            SaveChanges();
        }

        public async Task UpdateMessage(Message message)
        {
            var result = Messages.Single(b => b.MessageID == message.MessageID);

            if (result == null) return;
            result = message;
            await SaveChangesAsync();
        }

        public void UpdateCarProfile(CarProfile carProfile)
        {
            var result = CarProfiles.Single(b => b.RegNr == carProfile.RegNr);

            if (result == null) return;
            result = carProfile;
            SaveChanges();
        }

        public void UpdateDayThatIsRented(DayThatIsRented dayThatIsRented)
        {
            var result = DaysThatIsRented.Single(b => b.Date == dayThatIsRented.Date);

            if (result == null) return;
            result = dayThatIsRented;
            SaveChanges();
        }

        public void UpdatePossibleToRentDay(PossibleToRentDay possibleToRentDay)
        {
            var result = PossibleToRentDays.Single(b => b.Date == possibleToRentDay.Date);

            if (result == null) return;
            result = possibleToRentDay;
            SaveChanges();
        }

        //Delete
        public void RemoveCarEquipment(int ID)
        {
            var carEquipment = new CarEquipment { CarEquipmentID = ID };

            Attach(carEquipment);
            Remove(carEquipment);
            SaveChanges();
        }

        public void RemoveUser(string ID)
        {
            var user = new User { Email = ID };

            Attach(user);
            Remove(user);
            SaveChanges();
        }

        public void RemoveMessage(int ID)
        {
            var message = new Message { MessageID = ID };

            Attach(message);
            Remove(message);
            SaveChanges();
        }

        public void RemoveCarProfile(string ID)
        {
            var carProfile = new CarProfile { RegNr = ID };

            Attach(carProfile);
            Remove(carProfile);
            SaveChanges();
        }

        public void RemoveDayThatIsRented(DateTime ID)
        {
            var dayThatIsRented = new DayThatIsRented { Date = ID };

            Attach(dayThatIsRented);
            Remove(dayThatIsRented);
            SaveChanges();
        }
        
        public void RemovePossibleToRentDay(DateTime ID)
        {
            var possibleToRentDay = new PossibleToRentDay { Date = ID };

            Attach(possibleToRentDay);
            Remove(possibleToRentDay);
            SaveChanges();
        }


        //Create

        public void AddCarEquipment(CarEquipment carEquipment)
        {
            CarEquipment.Add(carEquipment);
            SaveChanges();
        }

        public async Task AddUser(User user)
        {
            await Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
            SaveChanges();
        }

        public void AddCarProfile(CarProfile carProfile)
        {
            CarProfiles.Add(carProfile);
            SaveChanges();
        }

        public void AddDaysThatIsRented(DayThatIsRented dayThatIsRented)
        {
            DaysThatIsRented.Add(dayThatIsRented);
            SaveChanges();
        }

        public void AddPossibleToRentDay(PossibleToRentDay possibleToRentDay)
        {
            PossibleToRentDays.Add(possibleToRentDay);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PossibleToRentDay>()
                .HasOne(p => p.CarProfile)
                .WithMany(b => b.PossibleToRentDays);

            modelBuilder.Entity<DayThatIsRented>()
                .HasOne(p => p.CarProfile)
                .WithMany(b => b.DaysThatIsRented);

            modelBuilder.Entity<CarProfile>()
                .HasOne(p => p.CarEquipment)
                .WithOne(b => b.CarProfile);

            modelBuilder.Entity<CarProfile>()
                .HasOne(p => p.User)
                .WithMany(b => b.Cars);

            modelBuilder.Entity<User>()
                .HasMany(p => p.MessagesWithUsers)
                .WithOne(b => b.User);

            modelBuilder.Entity<Message>()
                .HasMany(p => p.MessagesWithUsers)
                .WithOne(b => b.Message);

        }
    }

}
    