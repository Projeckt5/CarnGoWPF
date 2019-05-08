using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CarnGo.Database
{
    public class AppDbContext : DbContext, IAppDbContext
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
        private DbSet<MessagesWithUsers> MessagesWithUsersJunction { get; set; }

        //reposetory pattern, CRUD

        public void AddDayThatIsRentedList(List<DayThatIsRented> list)
        {
            DaysThatIsRented.AddRange(list);
       
        }

        //Get
        public async Task<User> GetUser(string email, string password)
        {
            var user = await Users.FindAsync(email);
            if (user == null || user.Password != password)
                throw new AuthenticationFailedException();
            Update(user);
            user.AuthorizationString = Guid.NewGuid();
            await SaveChangesAsync();
            return user;
        }

        public async Task<CarProfile> GetCarProfile(string regNr)
        {
            var carProfile = await CarProfiles.FindAsync(regNr);
            return carProfile;
        }

        public async Task<List<CarProfile>> GetAllCars(User user)
        {
            var carProfiles = await CarProfiles
                .Where(c => c.User == user)
                .ToListAsync();
            return carProfiles;
        }


        public async Task<User> GetUser(string email, Guid authorization)
        {
            var user = await Users.FindAsync(email);
            if (user == null)
                throw new AuthenticationFailedException($"No user found for the email: {email}");
            if (user.AuthorizationString != authorization)
                throw new AuthorizationFailedException();
            return user;
        }

        public async Task<List<Message>> GetMessages(User user)
        {
            var userWithMessage = await Users
                .Include(u => u.MessagesWithUsers)
                .ThenInclude(mwu=>mwu.Message)
                .ThenInclude(m => m.CarProfile)
                .SingleAsync(u => u == user);

            return userWithMessage.MessagesWithUsers.Select(mwu => mwu.Message).ToList();
        }
        
        public async Task<List<Message>> GetAllMessages()
        {
            var messages = await Messages
                .ToListAsync();
            return messages;
        }
        
        public async Task<List<User>> GetAllUsers()
        {
            var users = await Users
                .ToListAsync();
            return users;
        }
        
        
        public async Task<List<CarEquipment>> GetAllCarEquipment()
        {
            var carEquipments = await CarEquipment
                .ToListAsync();
            return carEquipments;
        }
        
        
        public async Task<List<CarProfile>> GetAllCarProfiles()
        {
            var carProfiles = await CarProfiles
                .ToListAsync();
            foreach (var carProfile in carProfiles)
            {
                await Entry(carProfile).Reference(c => c.Owner).LoadAsync();
            }
            return carProfiles;
        }
        
        public async Task<List<DayThatIsRented>> GetAllDayThatIsRented()
        {
            var daysRented = await DaysThatIsRented
                .ToListAsync();
            return daysRented;
        }

        
        public async Task<List<PossibleToRentDay>> GetAllPossibleToRentDay()
        {
            var possibleToRentDays = await PossibleToRentDays
                .ToListAsync();
            return possibleToRentDays;
        }


        public CarProfile GetCarProfileForSendRequestView(string regnr)
        {
            var carprofile =new CarProfile();
            carprofile=CarProfiles.Include(d => d.DaysThatIsRented)
                .Include(p => p.PossibleToRentDays)
                .Include(u => u.Owner)
                .Single(e => e.RegNr == regnr);
            return carprofile;
        }

        public User GetUser(string email)
        {
            return Users.Single(u => u.Email == email);
        }

        public void AddMessageToLessor(Message message)
        {
            Messages.Add(message);
        }

        //Update
        public async Task UpdateCarEquipment(CarEquipment carEquipment)
        {
            var result = await CarEquipment.SingleOrDefaultAsync(b => b.CarEquipmentID == carEquipment.CarEquipmentID);

            if (result == default(CarEquipment)) return;
            Update(result);
            result = carEquipment;
            await SaveChangesAsync();
        }

        public async Task UpdateUserInformation(User user)
        {
            var result = await Users.SingleOrDefaultAsync(b => b.Email == user.Email);

            if (result == default(User))
                throw new AuthenticationFailedException($"No user found for the email: {user.Email}");

            if (result.AuthorizationString != user.AuthorizationString)
                throw new AuthorizationFailedException();
            Update(result);
            result.Email = user.Email;
            result.FirstName = user.FirstName;
            result.LastName = user.LastName;
            result.Address = user.Address;
            result.AuthorizationString = user.AuthorizationString;
            result.UserType = user.UserType;
            //TODO: SAVE THE PICTURE
            await SaveChangesAsync();
        }

        public async Task UpdateUserPassword(User user, string password)
        {

            var result = await Users.SingleOrDefaultAsync(b => b.Email == user.Email);

            if (result == default(User))
                throw new AuthenticationFailedException($"No user found for the email: {user.Email}");

            if (result.AuthorizationString != user.AuthorizationString)
                throw new AuthorizationFailedException();
            Update(result);
            result.Email = user.Email;
            result.Password = password;

            await SaveChangesAsync();
        }

        public async Task UpdateMessage(Message message)
        {
            var result = Messages.SingleOrDefault(b => b.MessageID == message.MessageID);

            if (result == default(Message)) return;
            Update(result);
            result = message;
            await SaveChangesAsync();
        }

        public async Task UpdateCarProfile(CarProfile carProfile)
        {
            var result = await CarProfiles.SingleOrDefaultAsync(b => b.RegNr == carProfile.RegNr);

            if (result == default(CarProfile)) return;
            result = carProfile;
            SaveChanges();
        }

        public async Task UpdateDayThatIsRented(DayThatIsRented dayThatIsRented)
        {
            var result = await DaysThatIsRented.SingleOrDefaultAsync(b => b.Date == dayThatIsRented.Date);

            if (result == default(DayThatIsRented)) return;
            result = dayThatIsRented;
            SaveChanges();
        }

        public async Task UpdatePossibleToRentDay(PossibleToRentDay possibleToRentDay)
        {
            var result = await PossibleToRentDays.SingleOrDefaultAsync(b => b.Date == possibleToRentDay.Date);

            if (result == default(PossibleToRentDay)) return;
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
            if(Users.Find(user.Email) != null)
                throw new AuthenticationFailedException("The user already exists");
            await Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public void AddMessage(Message message)
        {

            MessagesWithUsers newConnection = new MessagesWithUsers();
            if (message.MsgType == 1)
            {
                User theuser = GetUser(message.RenterEmail);

                newConnection.Message = message;
                newConnection.MessageId = message.MessageID;
                newConnection.User = theuser;
                newConnection.UserEmail = theuser.Email;
            }
            else
            {
                User theuser = GetUser(message.LessorEmail);

                newConnection.Message = message;
                newConnection.MessageId = message.MessageID;
                newConnection.User = theuser;
                newConnection.UserEmail = theuser.Email;
            }
            message.MessagesWithUsers.Add(newConnection);


            MessagesWithUsersJunction.Add(newConnection);
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
                .WithOne(b => b.CarProfile)
                .HasForeignKey<CarEquipment>();

            modelBuilder.Entity<CarProfile>()
                .HasOne(p => p.Owner)
                .WithMany(b => b.Cars)
                .HasForeignKey(p=>p.OwnerEmail);

            modelBuilder.Entity<MessagesWithUsers>().HasKey(k => new {k.MessageId, k.UserEmail});

            modelBuilder.Entity<User>()
                .HasMany(p => p.MessagesWithUsers)
                .WithOne(b => b.User)
                .HasForeignKey(p => p.UserEmail);

            modelBuilder.Entity<Message>()
                .HasMany(p => p.MessagesWithUsers)
                .WithOne(b => b.Message)
                .HasForeignKey(p => p.MessageId);

            modelBuilder.Entity<Message>()
                .HasOne(p => p.CarProfile)
                .WithMany(b => b.MessagesCarOccursIn)
                .HasForeignKey(p => p.CarProfileRegNr);
        }
    }

}
    