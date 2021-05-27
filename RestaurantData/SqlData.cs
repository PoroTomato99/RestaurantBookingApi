using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantBookingApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace RestaurantBookingApi.RestaurantData
{
    public class SqlData : IRestaurant, IAddress, IAspRoleData, ICustomerData, ITable, IBooking, IBookingDetails,
        IStatus, ITimeStatus, ITimeSlot, IUserProfile, IEmployee, INewResForm
    {
        private readonly RestaurantBookingDbContext _dbcontext;
        private readonly IConfiguration _config;
        private readonly ILogger<SqlData> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public SqlData(RestaurantBookingDbContext DbContext, IConfiguration configuration, ILogger<SqlData> logger, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = DbContext;
            _config = configuration;
            _logger = logger;
            _userManager = userManager;
        }

        /*
         RESTAURANT
         */

        public Restaurant AddRestaurant(NewRestaurantForm restaurant)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            //check if the restaurant name ady existed
            if (IsRestaurantExist(restaurant.RestaurantName))
            {
                throw new DuplicateNameException();
            }

            Restaurant newRestaurant = new()
            {
                Name = restaurant.RestaurantName,
                Description = restaurant.Description
            };

            if (restaurant.Approval != 1)
            {
                throw new DbUpdateException("Approval is needed to register new restaurant");
            }

            _dbcontext.Restaurants.Add(newRestaurant);
            try
            {
                _dbcontext.SaveChanges();

                var res = _dbcontext.Restaurants.Where(x => x.Name == newRestaurant.Name).FirstOrDefault();
                if (res != null)
                {
                    UserProfile p = new()
                    {
                        Id = restaurant.UserId,
                        DateJoined = DateTime.UtcNow.Date,
                        RestaurantId = res.Id,
                        FirstName = "",
                        LastName = ""
                    };



                    Address a = new()
                    {
                        RestaurantId = res.Id,
                        BuildingNo = restaurant.BuildingNo,
                        Address1 = restaurant.Address1,
                        Address2 = restaurant.Address2,
                        City = restaurant.City,
                        State = restaurant.State,
                        PostCode = restaurant.PostCode,
                        Country = restaurant.Country
                    };

                    _dbcontext.UserProfiles.Add(p);
                    _dbcontext.SaveChanges();

                    _dbcontext.Addresses.Add(a);
                    _dbcontext.SaveChanges();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"Error Type : {ex.GetType()}, Error Message : {ex.Message}");
            }
            return newRestaurant;
        }
        public void DeleteRestaurant(Restaurant restaurant)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.Restaurants.Remove(restaurant);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"Exception Type : {ex.GetType()}\t\tException Message : {ex.Message}");
            }
        }
        public Restaurant EditRestaurant(Restaurant restaurant)
        {
            var rest = _dbcontext.Restaurants.Find(restaurant.Id);
            if (rest != null)
            {
                rest.Name = restaurant.Name;
                rest.Description = restaurant.Description;
                _dbcontext.Restaurants.Update(rest);
                _dbcontext.SaveChanges();
            }
            return restaurant;
        }
        public Restaurant GetRestaurant(int? id)
        {
            var rest = _dbcontext.Restaurants.Where(x => x.Id == id).Include(z => z.Address)
                .Include(y => y.TimeSlots).Include(t => t.Tables).FirstOrDefault();
            return rest;
        }
        public List<Restaurant> GetRestaurants()
        {
            return _dbcontext.Restaurants.Include(x => x.Address).Include(y => y.TimeSlots).Include(z => z.Tables).ToList();
        }
        public bool IsRestaurantExist(string restaurantName)
        {
            var ifExist = _dbcontext.Restaurants.Any(x => x.Name == restaurantName);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }

        /*
         Address
         */
        public Address AddAddress(Address address)
        {
            var transaction = _dbcontext.Database.BeginTransaction();

            _dbcontext.Addresses.Add(address);

            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return address;
        }
        public Address EditAddress(Address address)
        {
            var restAdd = _dbcontext.Addresses.Find(address.RestaurantId);
            if (restAdd != null)
            {
                restAdd.Address1 = address.Address1;
                restAdd.Address2 = address.Address2;
                restAdd.BuildingNo = address.BuildingNo;
                restAdd.State = address.State;
                restAdd.PostCode = address.PostCode;
                restAdd.City = address.City;
                restAdd.Country = address.Country;
                _dbcontext.Addresses.Update(restAdd);
                _dbcontext.SaveChanges();
            }
            return address;
        }
        public void DeleteAddress(Address address)
        {
            _dbcontext.Addresses.Remove(address);
            _dbcontext.SaveChanges();
        }
        public Address GetAddress(int? restaurantId)
        {
            var address = _dbcontext.Addresses.Find(restaurantId);
            return address;
        }
        public List<Address> GetAddresses()
        {
            return _dbcontext.Addresses.Include(r => r.Restaurant).ToList();
        }


        /*
         Announcement
         */
        public List<Annoucement> GetAnnoucements()
        {
            return _dbcontext.Annoucements.ToList();
        }
        public Annoucement GetAnnoucement(int? id)
        {
            var x = _dbcontext.Annoucements.Find(id);
            return x;
        }
        public Annoucement Post(Annoucement annoucement)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.Annoucements.Add(annoucement);

            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return annoucement;

        }
        public void DeleteAnnouncement(Annoucement annoucement)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            try
            {
                _dbcontext.Annoucements.Remove(annoucement);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
        }
        public Annoucement EditAnnouncement(Annoucement announcement)
        {
            var transaction = _dbcontext.Database.BeginTransaction();

            var existAnnounce = _dbcontext.Annoucements.Find(announcement.Id);
            if (existAnnounce != null)
            {
                existAnnounce.Date = DateTime.Now;
                existAnnounce.Type = announcement.Type;
                existAnnounce.Message = announcement.Message;
                _dbcontext.Annoucements.Update(existAnnounce);
                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.Annoucements.Find(announcement.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }



        /*
         Asp Net Core User Role
         */

        //Get the roles into list
        public List<AspNetRole> GetRoles()
        {
            return _dbcontext.AspNetRoles.Include(r => r.AspNetUserRoles).ToList();
        }
        //Get user role but user's id
        public AspNetUserRole GetRoleByCustId(string cusNo)
        {
            var role = _dbcontext.AspNetUserRoles.Where(x => x.UserId == cusNo).Include(i => i.Role).FirstOrDefault();
            return role;
        }

        /*
            Booking Data Logic
         */
        public List<Booking> GetBookings()
        {
            return _dbcontext.Bookings.Include(s => s.Statu)
                .Include(x => x.BookingDetails).ToList();
        }
        public Booking GetBooking(int? id)
        {
            //var x = _dbcontext.Bookings.Find(id);
            var x = _dbcontext.Bookings.Where(y => y.Id == id)
                .Include(s => s.Statu)
                .Include(z => z.BookingDetails)
                .FirstOrDefault();
            return x;
        }
        public Booking AddBooking(Booking booking, int? restaurantId)
        {
            if (booking == null)
            {
                throw new NullReferenceException();
            }
            var transaction = _dbcontext.Database.BeginTransaction();

            var timeSlot = _dbcontext.TimeSlots.Where(x => x.RestaurantId == restaurantId
                                                        && x.AvailableTime == booking.ReservedTime)
                                                        .FirstOrDefault();

            var count = _dbcontext.BookingDetails.Where(x => x.RestaurantId == restaurantId)
                                                        .Include(y => y.Booking)
                                                        .Where(i => i.Booking.ReservedDate == booking.ReservedDate
                                                        && i.Booking.ReservedTime == booking.ReservedTime
                                                        && i.Booking.StatuId != 3)
                                                        .Where(i => i.Booking.ReservedDate == booking.ReservedDate
                                                        && i.Booking.ReservedTime == booking.ReservedTime
                                                        && i.Booking.StatuId != 4).Count();
            if (timeSlot.Vacancy <= count)
            {
                //var t = _dbcontext.TimeSlots.Find(timeSlot.Id);
                //t.TimeStatusId = 1;
                //_dbcontext.TimeSlots.Update(t);
                //_dbcontext.SaveChanges();
                //transaction.Commit();
                throw new FullyBookedException(booking.ReservedTime.ToString(), booking.ReservedDate.ToString("yyyy-MM-dd"));
            }

            _dbcontext.Bookings.Add(booking);
            try
            {
                _dbcontext.SaveChanges();
                //transaction.Commit();
                try
                {
                    var currentBookingId = _dbcontext.Bookings.Where(x => x.UserId == booking.UserId
                                            && x.BookingDate == booking.BookingDate
                                            && x.ReservedDate == booking.ReservedDate
                                            && x.ReservedTime == booking.ReservedTime).FirstOrDefault().Id;
                    BookingDetail details = new()
                    {
                        BookingId = currentBookingId,
                        RestaurantId = restaurantId
                    };
                    _dbcontext.BookingDetails.Add(details);
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }

            return (_dbcontext.Bookings.Find(booking.Id));
        }
        public bool DeleteBooking(Booking booking)
        {
            //if deleted return true;
            //else if not deleted return false;

            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.Bookings.Remove(booking);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public Booking EditBooking(Booking booking)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.Bookings.Find(booking.Id);
            if (x != null)
            {
                x.BookingDate = booking.BookingDate;
                x.StatuId = booking.StatuId;
                x.UserId = booking.UserId;
                x.ReservedDate = booking.ReservedDate;
                x.ReservedTime = booking.ReservedTime;
                _dbcontext.Bookings.Update(x);


                //var restaurantId = _dbcontext.BookingDetails.Where(x => x.BookingId == booking.Id).FirstOrDefault().RestaurantId;
                //var timeSlot = _dbcontext.TimeSlots.Where(x => x.RestaurantId == restaurantId
                //                            && x.AvailableTime == booking.ReservedTime)
                //                            .FirstOrDefault();

                //var count = _dbcontext.BookingDetails.Where(x => x.RestaurantId == restaurantId)
                //                                            .Include(y => y.Booking)
                //                                            .Where(i => i.Booking.ReservedDate == booking.ReservedDate
                //                                            && i.Booking.ReservedTime == booking.ReservedTime
                //                                            && i.Booking.StatuId != 3)
                //                                            .Where(i => i.Booking.ReservedDate == booking.ReservedDate
                //                                            && i.Booking.ReservedTime == booking.ReservedTime
                //                                            && i.Booking.StatuId != 4).Count();

                //if(timeSlot.Vacancy > count)
                //{
                //    timeSlot.TimeStatusId = 0; ;
                //    _dbcontext.TimeSlots.Update(timeSlot);
                //}

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.Bookings.Find(booking.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsBookingExist(Booking booking)
        {
            var exist = _dbcontext.Bookings.Where(x => x.ReservedDate == booking.ReservedDate && x.UserId == booking.UserId && x.ReservedTime == booking.ReservedTime
                        && x.StatuId != 3).
                        Where(x => x.ReservedDate == booking.ReservedDate && x.UserId == booking.UserId && x.ReservedTime == booking.ReservedTime
                        && x.StatuId != 4).FirstOrDefault();
            if (exist == null)
            {
                return false;
            }
            return true;
        }

        /*
         BOOKING DETAILS LOGIC
         */
        public List<BookingDetail> GetBookingDetails(int? id)
        {
            return _dbcontext.BookingDetails
                .Where(r => r.RestaurantId == id).Include(x => x.Booking).Include(s => s.Booking.Statu)
                .Include(y => y.Restaurant).ToList();
        }

        public List<BookingDetail> GetUserBooking(string userId)
        {
            //return _dbcontext.BookingDetails.Include(x => x.Booking).Include(r => r.Restaurant).Where(u => u.Booking.UserId == userId).ToList();
            return _dbcontext.BookingDetails.Where(u => u.Booking.UserId == userId)
                .Include(r => r.Restaurant)
                .Include(x => x.Booking)
                .Include(y => y.Booking.Statu)
                .ToList();
        }
        public BookingDetail GetBookingDetail(int? id)
        {
            var x = _dbcontext.BookingDetails
                .Include(x => x.Booking)
                .Include(y => y.Restaurant)
                .Where(z => z.Id == id).FirstOrDefault();
            return x;
        }
        public BookingDetail AddBookingDetail(BookingDetail bookingDetail)
        {
            if (bookingDetail == null)
            {
                throw new NullReferenceException();
            }
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.BookingDetails.Add(bookingDetail);

            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }

            // var x = _dbcontext.BookingDetails.Where(x => x.BookingId == bookingDetail.BookingId && x.RestaurantId == bookingDetail.RestaurantId).FirstOrDefault();
            var x = _dbcontext.BookingDetails.Find(bookingDetail.Id);
            return (x);
        }
        public bool DeleteBookingDetail(BookingDetail bookingDetail)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.BookingDetails.Remove(bookingDetail);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public BookingDetail EditBookingDetail(BookingDetail bookingDetail)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.BookingDetails.Find(bookingDetail.Id);
            if (x != null)
            {
                x.BookingId = bookingDetail.BookingId;
                x.RestaurantId = bookingDetail.RestaurantId;
                _dbcontext.BookingDetails.Update(x);

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.BookingDetails.Find(bookingDetail.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsBookingDetailExist(BookingDetail bookingDetail)
        {
            var exist = _dbcontext.BookingDetails.Any(x => x.BookingId == bookingDetail.BookingId && x.RestaurantId == bookingDetail.RestaurantId);
            if (!exist)
            {
                return false;
            }
            return true;
        }



        /*
           Table Data Logic
         */
        public List<Table> GetTables()
        {
            return _dbcontext.Tables.Include(x => x.Restaurant).ToList();
        }
        public Table GetTable(int? id)
        {
            if (id == null)
            {
                throw new Exception("Null Id provided");
            }
            var x = _dbcontext.Tables.Find(id);
            return x;
        }
        public Table AddTable(Table table)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            if (IsTableExist(table))
            {
                throw new DuplicateNameException();
            }
            _dbcontext.Tables.Add(table);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return _dbcontext.Tables.Find(table.Id);
        }
        public void DeleteTable(Table table)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.Tables.Remove(table);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
        }
        public Table EditTable(Table table)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.Tables.Find(table.Id);
            if (x != null)
            {
                x.TableNum = table.TableNum;
                _dbcontext.Tables.Update(x);
                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.Tables.Find(table.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsTableExist(Table table)
        {
            var ifExist = _dbcontext.Tables.Any(x => x.TableNum == table.TableNum && x.RestaurantId == table.RestaurantId);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }


        /*
         * STATUS LOGIC
         */
        public List<Status> GetStatuses()
        {
            return _dbcontext.Statuses.Include(x => x.Bookings).ToList();
        }
        public Status GetStatus(int? id)
        {
            var x = _dbcontext.Statuses.Find(id);
            return x;
        }
        public Status AddStatus(Status status)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            if (IsStatusExist(status))
            {
                throw new DuplicateNameException();
            }
            _dbcontext.Statuses.Add(status);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return _dbcontext.Statuses.Find(status.Id);
        }
        public bool DeleteStatus(Status status)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.Statuses.Remove(status);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public Status EditStatus(Status status)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.Statuses.Find(status.Id);
            if (x != null)
            {
                x.Status1 = status.Status1;
                _dbcontext.Statuses.Update(x);

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.Statuses.Find(status.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsStatusExist(Status status)
        {
            var ifExist = _dbcontext.Statuses.Any(x => x.Status1 == status.Status1);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }


        /*
         * TIME STATUS LOGIC
         */
        public List<TimeStatus> GetTimeStatuses()
        {
            return _dbcontext.TimeStatuses.Include(x => x.TimeSlots).ToList();
        }
        public TimeStatus GetTimeStatus(int? id)
        {
            var x = _dbcontext.TimeStatuses.Find(id);
            return x;
        }
        public TimeStatus AddTimeStatus(TimeStatus status)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            if (IsTimeStatusExist(status))
            {
                throw new DuplicateNameException();
            }
            _dbcontext.TimeStatuses.Add(status);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return _dbcontext.TimeStatuses.Find(status.Id);
        }
        public bool DeleteTimeStatus(TimeStatus status)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.TimeStatuses.Remove(status);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public TimeStatus EditTimeStatus(TimeStatus status)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.TimeStatuses.Find(status.Id);
            if (x != null)
            {
                x.TimeStatus1 = status.TimeStatus1;
                _dbcontext.TimeStatuses.Update(x);

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.TimeStatuses.Find(status.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsTimeStatusExist(TimeStatus status)
        {
            var ifExist = _dbcontext.TimeStatuses.Any(x => x.TimeStatus1 == status.TimeStatus1);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }


        /*
         * TIME SLOT LOGIC
         */
        public List<TimeSlot> GetAllTimeSlot()
        {
            return _dbcontext.TimeSlots.Include(x => x.Restaurant).ToList();
        }
        public List<TimeSlot> GetTimeSlots(int? restaurantId)
        {
            var timeList = _dbcontext.TimeSlots.Where(x => x.RestaurantId == restaurantId).Include(r => r.Restaurant).ToList();
            //return _dbcontext.TimeSlots.Include(x => x.Restaurant).ToList();
            return timeList;
        }
        public TimeSlot GetTimeSlot(int? id)
        {
            var x = _dbcontext.TimeSlots.Include(x => x.Restaurant).Where(y => y.Id == id).FirstOrDefault();
            return x;
        }
        public TimeSlot AddTimeSlot(TimeSlot timeSlot)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            if (IsTimeSlotExist(timeSlot))
            {
                throw new DuplicateNameException();
            }
            _dbcontext.TimeSlots.Add(timeSlot);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return _dbcontext.TimeSlots.Find(timeSlot.Id);
        }
        public bool DeleteTimeSlot(TimeSlot timeSlot)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.TimeSlots.Remove(timeSlot);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public TimeSlot EditTimeSlot(TimeSlot timeSlot)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.TimeSlots.Find(timeSlot.Id);
            if (x != null)
            {
                x.AvailableTime = timeSlot.AvailableTime;
                x.TimeStatusId = timeSlot.TimeStatusId;
                x.RestaurantId = timeSlot.RestaurantId;
                x.Vacancy = timeSlot.Vacancy;
                _dbcontext.TimeSlots.Update(x);

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.TimeSlots.Find(timeSlot.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsTimeSlotExist(TimeSlot timeSlot)
        {
            var ifExist = _dbcontext.TimeSlots.Any(x => x.AvailableTime == timeSlot.AvailableTime && x.RestaurantId == timeSlot.RestaurantId);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }

        /*
         * RESTAURANT EMPLOYEE USER PROFILE LOGIC
         */
        public List<UserProfile> GetUserProfiles(int? restaurantId)
        {
            return _dbcontext.UserProfiles.Where(x => x.RestaurantId == restaurantId).ToList();
        }
        public UserProfile GetUserProfile(string userId)
        {
            return _dbcontext.UserProfiles.Find(userId);
        }
        public UserProfile AddUserProfile(UserProfile profile)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            if (IsUserProfileExist(profile))
            {
                throw new DuplicateNameException();
            }
            profile.DateJoined = DateTime.UtcNow.Date;
            _dbcontext.UserProfiles.Add(profile);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return _dbcontext.UserProfiles.Find(profile.Id);
        }
        public bool DeleteUserProfile(UserProfile profile)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.UserProfiles.Remove(profile);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public UserProfile EditUserProfile(UserProfile profile)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.UserProfiles.Find(profile.Id);
            if (x != null)
            {
                x.FirstName = profile.FirstName;
                x.LastName = profile.LastName;
                x.DateJoined = profile.DateJoined;
                x.RestaurantId = profile.RestaurantId;
                _dbcontext.UserProfiles.Update(x);

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.UserProfiles.Find(profile.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsUserProfileExist(UserProfile profile)
        {
            var ifExist = _dbcontext.UserProfiles.Any(x => x.Id == profile.Id);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }

        /*
        Customer data logic
        */
        public List<AspNetUser> GetCustomers()
        {
            return _dbcontext.AspNetUsers.Include(x => x.AspNetUserRoles).ToList();
        }
        public AspNetUser GetCustomer(string cusNo)
        {
            var customer = _dbcontext.AspNetUsers.Where(c => c.Id == cusNo)
                .Include(ur => ur.AspNetUserRoles)
                .Include(p => p.UserProfile)
                .FirstOrDefault();
            return customer;
        }
        public AspNetUser GetCustomerByUsername(string userName)
        {
            var customer = _dbcontext.AspNetUsers.Where(c => c.UserName == userName).FirstOrDefault();
            return customer;
        }
        public async Task<RegisterModel> AddCustomer(RegisterModel x)
        {
            var userExists = await _userManager.FindByNameAsync(x.Username);
            ApplicationUser user = new ApplicationUser()
            {
                Email = x.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = x.Username
            };
            if (userExists != null)
            {
                await _userManager.CreateAsync(user, x.Password);
            }

            return x;
        }
        public void DeleteCustomer(AspNetUser customer)
        {
            _dbcontext.AspNetUsers.Remove(customer);
            _dbcontext.SaveChanges();
        }
        public AspNetUser EditCustomer(AspNetUser customer)
        {
            var existCustomer = _dbcontext.AspNetUsers.Find(customer.Id);
            if (existCustomer != null)
            {
                existCustomer.PhoneNumber = customer.PhoneNumber;
                _dbcontext.AspNetUsers.Update(existCustomer);
                _dbcontext.SaveChanges();
            }
            return customer;
        }

        /*
         * EMPLOYEE LOGIC
         */
        public List<AspNetUser> GetEmployees(int? restaurantId)
        {
            return _dbcontext.AspNetUsers.Include(x => x.UserProfile)
                .Where(r => r.UserProfile.RestaurantId == restaurantId)
                .ToList();
        }
        public AspNetUser GetEmployee(string employeeId)
        {
            //return _dbcontext.AspNetUsers
            //    .Include(x => x.UserProfile)
            //    .Where(e => e.Id == employeeId).FirstOrDefault();

            var customer = _dbcontext.AspNetUsers.Include(x => x.UserProfile).Where(u => u.Id == employeeId).FirstOrDefault();
            return customer;
        }
        public AspNetUser GetEmployeeByUsername(string userName)
        {
            //return _dbcontext.AspNetUsers.Where(e => e.UserName == userName)
            //    .Include(p => p.UserProfile).FirstOrDefault();
            var employee = _dbcontext.AspNetUsers.Where(c => c.UserName == userName)
                .Include(p => p.UserProfile).FirstOrDefault();
            return employee;
        }
        public bool DeleteEmployee(AspNetUser employee)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.AspNetUsers.Remove(employee);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }


        /*
         * New Restaurant Application 
         */
        public List<NewRestaurantForm> GetFroms()
        {
            return _dbcontext.NewRestaurantForms.ToList();
        }
        public NewRestaurantForm GetForm(int? id)
        {
            return _dbcontext.NewRestaurantForms.Find(id);
        }
        public NewRestaurantForm AddForm(NewRestaurantForm x)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            if (IsFormExist(x))
            {
                throw new DuplicateNameException();
            }
            _dbcontext.NewRestaurantForms.Add(x);
            try
            {
                _dbcontext.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
            }
            return x;
        }
        public bool DeleteForm(NewRestaurantForm table)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            _dbcontext.NewRestaurantForms.Remove(table);
            try
            {
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError($"{ex.GetType()} : {ex.Message}");
                return false;
            }
        }
        public NewRestaurantForm EditForm(NewRestaurantForm table)
        {
            var transaction = _dbcontext.Database.BeginTransaction();
            var x = _dbcontext.NewRestaurantForms.Find(table.Id);
            if (x != null)
            {
                x.RestaurantName = table.RestaurantName;
                x.Description = table.Description;
                x.UserId = table.UserId;
                x.Approval = 1;
                _dbcontext.NewRestaurantForms.Update(x);

                try
                {
                    _dbcontext.SaveChanges();
                    transaction.Commit();
                    return _dbcontext.NewRestaurantForms.Find(table.Id);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError($"{ex.GetType()} : {ex.Message}");
                }
            }
            return null;
        }
        public bool IsFormExist(NewRestaurantForm y)
        {
            var ifExist = _dbcontext.NewRestaurantForms.Any(x => x.RestaurantName == y.RestaurantName);
            if (!ifExist)
            {
                return false;
            }
            return true;
        }
    }
}
