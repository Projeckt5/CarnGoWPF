using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using CarnGo.Database.Models;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace CarnGo.Database
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext()
        {
            
        }

        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:carngo.database.windows.net,1433;Initial Catalog=CarnGo;Persist Security Info=False;User ID=carngo;Password=Aarhus123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
        public DbSet<CarEquipment> CarEquipment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CarProfile> CarProfiles { get; set; }
        public DbSet<DayThatIsRented> DaysThatIsRented { get; set; }
        public DbSet<MessagesWithUsers> MessagesWithUsersJunction { get; set; }
        public DbSet<PossibleToRentDay> PossibleToRentDays { get; set; }

        //reposetory pattern, CRUD

        public async Task AddDayThatIsRentedList(List<DayThatIsRented> list)
        {
            await DaysThatIsRented.AddRangeAsync(list);
            await SaveChangesAsync();
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
                .Where(c => c.Owner == user)
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

        public async Task<List<Message>> GetMessages(User user,List<Message> messageAlreadyRead, int amount)
        {
            var messages = await MessagesWithUsersJunction
                .Where(mwu => mwu.UserEmail == user.Email)
                .Select(mwu => mwu.Message)
                .Where(msg => !messageAlreadyRead.Contains(msg))
                .OrderByDescending(msg => msg.CreatedDate)
                .Include(msg => msg.MessagesWithUsers)
                .ThenInclude(mwu => mwu.User)
                .Include(msg => msg.CarProfile)
                .Take(amount)
                .ToListAsync();

            return messages;
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


        public async Task<List<CarProfile>> GetCarProfilesForSearchView(int pageIndex, int itemsPerPage)
        {
            var carProfiles = await CarProfiles
                .Skip(pageIndex*itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            foreach (var carProfile in carProfiles)
            {
                await Entry(carProfile).Reference(c => c.Owner).LoadAsync();
            }
            return carProfiles;
        }


        public async Task<int> GetCarProfilesCount()
        {
            return await CarProfiles.CountAsync();
        }


        public async Task<List<DayThatIsRented>> GetAllDayThatIsRented()
        {
            var daysRented = await DaysThatIsRented
                .ToListAsync();
            return daysRented;
        }

        public async Task<List<DayThatIsRented>> GetDaysThatIsRentedTask(string user, CarProfile carProfile)
        {
            return await DaysThatIsRented.Where(c => (c.CarProfile == carProfile && c.Renter.Email == user)).ToListAsync();
        }

        public async Task DeleteDaysThatIsRentedTask(List<DayThatIsRented> list)
        {
            DaysThatIsRented.RemoveRange(list);
            await SaveChangesAsync();
        }

        public async Task<List<PossibleToRentDay>> GetAllPossibleToRentDay()
        {
            var possibleToRentDays = await PossibleToRentDays
                .ToListAsync();
            return possibleToRentDays;
        }

        public async Task<List<DayThatIsRented>> GetCarProfileRentedDaysTask(string regnr)
        {
            var rentedDays =  await CarProfiles
                .Where(cp => cp.RegNr == regnr)
                .Select(cp => cp.DaysThatIsRented)
                .SingleOrDefaultAsync();
            return rentedDays;
        }

        public async Task<List<PossibleToRentDay>> GetCarProfilePossibleToRentDayTask(string regnr)
        {
            var possibleToRentDays = await CarProfiles
                .Where(cp => cp.RegNr == regnr)
                .Select(cp => cp.PossibleToRentDays)
                .SingleOrDefaultAsync();
            return possibleToRentDays;
        }

        public async Task<CarProfile> GetCarProfileWithDays(string regnr)
        {
            var carprofile =new CarProfile();
            carprofile= await CarProfiles.SingleAsync(e => e.RegNr == regnr);
            await Entry(carprofile).Collection(p => p.DaysThatIsRented).LoadAsync();
            await Entry(carprofile).Collection(p => p.PossibleToRentDays).LoadAsync();
            await Entry(carprofile).Reference(p => p.Owner).LoadAsync();
            return carprofile;
        }

        public async Task<User> GetUser(string email)
        {
         
            return await Users.FindAsync(email);
        }

        public async Task AddMessageToLessor(Message message)
        {
            await Messages.AddAsync(message);
            await SaveChangesAsync();
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
            result.CarProfile = message.CarProfile ?? result.CarProfile;
            result.ConfirmationStatus = message.ConfirmationStatus;
            result.CreatedDate = message.CreatedDate;
            result.LessorEmail = message.LessorEmail;
            result.MsgType = message.MsgType;
            result.ReceiverEmail = message.ReceiverEmail;
            result.RenterEmail = message.RenterEmail;
            result.SenderEmail = message.SenderEmail;
            result.HaveBeenSeen = message.HaveBeenSeen;
            result.MessagesWithUsers = message.MessagesWithUsers ?? result.MessagesWithUsers;
            result.TheMessage = message.TheMessage;
            await SaveChangesAsync();
        }

        public async Task UpdateCarProfile(CarProfile carProfile)
        {
            var result = await CarProfiles.SingleOrDefaultAsync(b => b.RegNr == carProfile.RegNr);
            if (result == default(CarProfile)) return;
            Update(result);
            result.CarPicture = carProfile.CarPicture ?? result.CarPicture;
            result.Age = carProfile.Age;
            result.Brand = carProfile.Brand ?? result.Brand;
            result.CarDescription = carProfile.CarDescription ?? result.CarDescription;
            //result.CarEquipment = carProfile.CarEquipment ?? result.CarEquipment;
            result.StartLeaseTime = carProfile.StartLeaseTime;
            result.EndLeaseTime = carProfile.EndLeaseTime;
            result.FuelType = carProfile.FuelType ?? result.FuelType;
            result.Model = carProfile.Model ?? result.Model;
            result.RentalPrice = carProfile.RentalPrice;
            result.RegNr = carProfile.RegNr ?? result.RegNr;
            result.Seats = carProfile.Seats;
            await SaveChangesAsync();
        }

        public async Task UpdateDayThatIsRented(DayThatIsRented dayThatIsRented)
        {
            var result = await DaysThatIsRented.SingleOrDefaultAsync(b => b.Date == dayThatIsRented.Date);

            if (result == default(DayThatIsRented)) return;
            result = dayThatIsRented;
            await SaveChangesAsync();
        }

        public async Task UpdatePossibleToRentDay(PossibleToRentDay possibleToRentDay)
        {
            var result = await PossibleToRentDays.SingleOrDefaultAsync(b => b.Date == possibleToRentDay.Date);

            if (result == default(PossibleToRentDay)) return;
            result = possibleToRentDay;
            await SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            var result = await Users.SingleOrDefaultAsync(u => u.Email == user.Email);

            if (result == default(User)) return;
            Users.Update(result);
            result = user;
            await SaveChangesAsync();

        }

        //Delete
        public async Task RemoveCarEquipment(int ID)
        {
            var carEquipment = new CarEquipment { CarEquipmentID = ID };

            Attach(carEquipment);
            Remove(carEquipment);
            await SaveChangesAsync();
        }

        public async Task RemoveUser(string ID)
        {
            var user = new User { Email = ID };
            
            Attach(user);
            Remove(user);
            await SaveChangesAsync();
        }

        public async Task RemoveMessage(int ID)
        {
            var message = new Message { MessageID = ID };

            Attach(message);
            Remove(message);
            await SaveChangesAsync();
        }

        public async Task RemoveCarProfile(string ID)
        {
            var carProfile = new CarProfile { RegNr = ID };

            Attach(carProfile);
            Remove(carProfile);
            await SaveChangesAsync();
        }

        public async Task RemoveDayThatIsRented(DateTime ID)
        {
            var dayThatIsRented = new DayThatIsRented { Date = ID };

            Attach(dayThatIsRented);
            Remove(dayThatIsRented);
            await SaveChangesAsync();
        }

        public async Task RemovePossibleToRentDay(DateTime ID)
        {
            var possibleToRentDay = new PossibleToRentDay { Date = ID };

            Attach(possibleToRentDay);
            Remove(possibleToRentDay);
            await SaveChangesAsync();
        }


        //Create

        public async Task AddCarEquipment(CarEquipment carEquipment)
        {
            CarEquipment.Add(carEquipment);
            await SaveChangesAsync();
        }

        public async Task AddUser(User user)
        {
            if(Users.Find(user.Email) != null)
                throw new AuthenticationFailedException("The user already exists");
            await Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task AddMessage(Message message)
        {
            var renter = await GetUser(message.RenterEmail);
            var lessor = await GetUser(message.LessorEmail);
            var junction = new List<MessagesWithUsers>()
            {
                new MessagesWithUsers()
                {
                    
                    Message = message,
                    MessageId = message.MessageID,
                    User = renter,
                    UserEmail = renter.Email
                },
                new MessagesWithUsers()
                {

                    Message = message,
                    MessageId = message.MessageID,
                    User = lessor,
                    UserEmail = lessor.Email
                }
            };


            await MessagesWithUsersJunction.AddRangeAsync(junction);
            await Messages.AddAsync(message);

            await SaveChangesAsync();
        }

        public async Task AddCarProfile(CarProfile carProfile)
        {
            if (Users.Find(carProfile.RegNr) != null)
                throw new AuthenticationFailedException("The car already exists");
            await CarProfiles.AddAsync(carProfile);
            await SaveChangesAsync();
        }

        public async Task AddDaysThatIsRented(DayThatIsRented dayThatIsRented)
        {
            await DaysThatIsRented.AddAsync(dayThatIsRented);
            await SaveChangesAsync();
        }

        public async Task AddPossibleToRentDay(PossibleToRentDay possibleToRentDay)
        {
            await PossibleToRentDays.AddAsync(possibleToRentDay);
            await SaveChangesAsync();
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
    