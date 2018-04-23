using System.Collections;
using System.Linq;
using DevExpressMvcApplication1;
using DevExpress.Web.Mvc;
using DevExpressMvcApplication1.Models;
using System.Collections.Generic;
using System.Drawing;
using System;
using System.Web;
using System.Web.Mvc.Html;
using DevExpress.XtraScheduler;
using DevExpress.Web;
using DevExpress.XtraScheduler.Xml;

namespace DevExpressMvcApplication1.Helpers {
    public static class SchedulerDataHelper {
        public static List<CustomResource> GetResources() {
            List<CustomResource> resources = new List<CustomResource>();
            resources.Add(CustomResource.CreateCustomResource(1, "Max Fowler", Color.Yellow.ToArgb()));
            resources.Add(CustomResource.CreateCustomResource(2, "Nancy Drewmore", Color.Green.ToArgb()));
            resources.Add(CustomResource.CreateCustomResource(3, "Pak Jang", Color.LightPink.ToArgb()));
            return resources;
        }

        static Random myRand = new Random();
        public static List<CustomAppointment> GetAppointments(List<CustomResource> resources) {
            List<CustomAppointment> appointments = new List<CustomAppointment>();
            foreach(CustomResource item in resources) {
                string subjPrefix = item.Name + "'s ";
                appointments.Add(CustomAppointment.CreateCustomAppointment(subjPrefix + "meeting", item.ResID, 2, 5, lastInsertedID++));
                appointments.Add(CustomAppointment.CreateCustomAppointment(subjPrefix + "travel", item.ResID, 3, 6, lastInsertedID++));
                appointments.Add(CustomAppointment.CreateCustomAppointment(subjPrefix + "phone call", item.ResID, 0, 10, lastInsertedID++));
            }
            return appointments;
        }


        public static List<Company> GetCompanies() {
            if(HttpContext.Current.Session["CompaniesList"] == null) {
                List<Company> returnedResult = new List<Company>();
                for(int i = 0; i < 10; i++) {
                    returnedResult.Add(new Company() { CompanyID = i, CompanyName = "Company " + i.ToString() });
                }
                HttpContext.Current.Session["CompaniesList"] = returnedResult;
            }
            return HttpContext.Current.Session["CompaniesList"] as List<Company>;
        }

        public static List<CompanyContact> GetCompanyContacts(int companyID) {
            if(HttpContext.Current.Session["ContactsList"] == null) {
                List<CompanyContact> returnedResult = new List<CompanyContact>();
                List<Company> companies = HttpContext.Current.Session["CompaniesList"] as List<Company>;

                int uniqueContactID = 0;
                for(int i = 0; i < companies.Count; i++) {
                    for(int j = 0; j < 5; j++) {
                        returnedResult.Add(new CompanyContact() { CompanyID = i, ContactName = "Contact " + j + ", Company " + i, ContactID = uniqueContactID });
                        uniqueContactID++;
                    }
                }

                HttpContext.Current.Session["ContactsList"] = returnedResult;
            }

            List<CompanyContact> contacts = HttpContext.Current.Session["ContactsList"] as List<CompanyContact>;
            return contacts.Where(cont => cont.CompanyID.Equals(companyID)).ToList<CompanyContact>(); ;
        }



        public static SchedulerDataObject DataObject {
            get {
                SchedulerDataObject dataObject = new SchedulerDataObject();


                if(HttpContext.Current.Session["ResourcesList"] == null) {
                    HttpContext.Current.Session["ResourcesList"] = GetResources();
                }
                dataObject.Resources = HttpContext.Current.Session["ResourcesList"] as List<CustomResource>;

                if(HttpContext.Current.Session["AppointmentsList"] == null) {
                    HttpContext.Current.Session["AppointmentsList"] = GetAppointments(dataObject.Resources);
                }
                dataObject.Appointments = HttpContext.Current.Session["AppointmentsList"] as List<CustomAppointment>;
                return dataObject;
            }
        }

        static MVCxAppointmentStorage defaultAppointmentStorage;
        public static MVCxAppointmentStorage DefaultAppointmentStorage {
            get {
                if(defaultAppointmentStorage == null) {
                    defaultAppointmentStorage = CreateDefaultAppointmentStorage();
                }
                return defaultAppointmentStorage;

            }
        }

        static MVCxAppointmentStorage CreateDefaultAppointmentStorage() {
            MVCxAppointmentStorage appointmentStorage = new MVCxAppointmentStorage();
            appointmentStorage.AutoRetrieveId = true;
            appointmentStorage.Mappings.AppointmentId = "ID";
            appointmentStorage.Mappings.Start = "StartTime";
            appointmentStorage.Mappings.End = "EndTime";
            appointmentStorage.Mappings.Subject = "Subject";
            appointmentStorage.Mappings.AllDay = "AllDay";
            appointmentStorage.Mappings.Description = "Description";
            appointmentStorage.Mappings.Label = "Label";
            appointmentStorage.Mappings.Location = "Location";
            appointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo";
            appointmentStorage.Mappings.ReminderInfo = "ReminderInfo";
            appointmentStorage.Mappings.ResourceId = "OwnerId";
            appointmentStorage.Mappings.Status = "Status";
            appointmentStorage.Mappings.Type = "EventType";

            appointmentStorage.CustomFieldMappings.Add(new DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("AppointmentCustomField", "CustomInfo"));
            appointmentStorage.CustomFieldMappings.Add(new DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("TimeBeforeStart", "TimeBeforeStart"));

            appointmentStorage.CustomFieldMappings.Add(new DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("AppointmentCompany", "CompanyID"));
            appointmentStorage.CustomFieldMappings.Add(new DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("AppointmentContact", "ContactID"));

            return appointmentStorage;
        }

        static MVCxResourceStorage defaultResourceStorage;
        public static MVCxResourceStorage DefaultResourceStorage {
            get {
                if(defaultResourceStorage == null) {
                    defaultResourceStorage = CreateDefaultResourceStorage();
                }
                return defaultResourceStorage;

            }
        }

        static MVCxResourceStorage CreateDefaultResourceStorage() {
            MVCxResourceStorage resourceStorage = new MVCxResourceStorage();
            resourceStorage.Mappings.ResourceId = "ResID";
            resourceStorage.Mappings.Caption = "Name";
            resourceStorage.Mappings.Color = "Color";
            return resourceStorage;
        }

        public static SchedulerSettings GetSchedulerSettings() {
            return GetSchedulerSettings(null);
        }

        static AppointmentRecurrenceFormSettings CreateAppointmentRecurrenceFormSettings(DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer container) {
            return new AppointmentRecurrenceFormSettings {
                Name = "appointmentRecurrenceForm",
                Width = System.Web.UI.WebControls.Unit.Percentage(100),
                IsRecurring = container.Appointment.IsRecurring,
                DayNumber = container.RecurrenceDayNumber,
                End = container.RecurrenceEnd,
                Month = container.RecurrenceMonth,
                OccurrenceCount = container.RecurrenceOccurrenceCount,
                Periodicity = container.RecurrencePeriodicity,
                RecurrenceRange = container.RecurrenceRange,
                Start = container.Start,
                WeekDays = container.RecurrenceWeekDays,
                WeekOfMonth = container.RecurrenceWeekOfMonth,
                RecurrenceType = container.RecurrenceType,
                IsFormRecreated = container.IsFormRecreated
            };
        }

        public static SchedulerSettings GetSchedulerSettings(this System.Web.Mvc.HtmlHelper customHtml) {
            SchedulerSettings settings = new SchedulerSettings();
            settings.Name = "scheduler";

            settings.InitClientAppointment = (sched, evargs) => {
                evargs.Properties.Add(DevExpress.Web.ASPxScheduler.ClientSideAppointmentFieldNames.AppointmentType, evargs.Appointment.Type);
                evargs.Properties.Add(DevExpress.Web.ASPxScheduler.ClientSideAppointmentFieldNames.Subject, evargs.Appointment.Subject);
            };

            settings.PopupMenuShowing = (sched, evargs) => {
                if(evargs.Menu.MenuId == SchedulerMenuItemId.AppointmentMenu) {
                    evargs.Menu.ClientSideEvents.PopUp = "OnAppointmentMenuPopup";
                }
            };

            settings.CallbackRouteValues = new { Controller = "Home", Action = "SchedulerPartial" };
            settings.EditAppointmentRouteValues = new { Controller = "Home", Action = "EditAppointment" };
            settings.CustomActionRouteValues = new { Controller = "Home", Action = "CustomCallBackAction" };

            settings.Storage.Appointments.Assign(SchedulerDataHelper.DefaultAppointmentStorage);
            settings.Storage.Resources.Assign(SchedulerDataHelper.DefaultResourceStorage);

            settings.Storage.EnableReminders = true;
            settings.GroupType = SchedulerGroupType.Resource;
            settings.Views.DayView.Styles.ScrollAreaHeight = 400;
            settings.Start = DateTime.Now;

            settings.AppointmentFormShowing = (sender, e) => {
                var scheduler = sender as MVCxScheduler;
                if(scheduler != null)
                    e.Container = new CustomAppointmentTemplateContainer(scheduler);
            };

            settings.OptionsForms.RecurrenceFormName = "appointmentRecurrenceForm";

            settings.OptionsForms.SetAppointmentFormTemplateContent(c => {
                var container = (CustomAppointmentTemplateContainer)c;
                ModelAppointment modelAppointment = new ModelAppointment() {
                    ID = container.Appointment.Id == null ? -1 : (int)container.Appointment.Id,
                    Subject = container.Appointment.Subject,
                    Location = container.Appointment.Location,
                    StartTime = container.Appointment.Start,
                    EndTime = container.Appointment.End,
                    AllDay = container.Appointment.AllDay,
                    Description = container.Appointment.Description,
                    EventType = (int)container.Appointment.Type,
                    Status = Convert.ToInt32(container.Appointment.StatusKey),
                    Label = Convert.ToInt32(container.Appointment.LabelKey),
                    CustomInfo = container.CustomInfo,

                    CompanyID = container.CompanyID,
                    ContactID = container.ContactID,

                    HasReminder = container.Appointment.HasReminder,
                    Reminder = container.Appointment.Reminder,
                    OwnerId = Convert.ToInt32(container.Appointment.ResourceId)
                };

                customHtml.ViewBag.DeleteButtonEnabled = container.CanDeleteAppointment;

                (container.ResourceDataSource as ListEditItemCollection).RemoveAt(0);
                customHtml.ViewBag.ResourceDataSource = container.ResourceDataSource;
                customHtml.ViewBag.StatusDataSource = container.StatusDataSource;
                customHtml.ViewBag.LabelDataSource = container.LabelDataSource;
                customHtml.ViewBag.AppointmentRecurrenceFormSettings = CreateAppointmentRecurrenceFormSettings(container);
                customHtml.ViewBag.ReminderDataSource = container.ReminderDataSource;                
                customHtml.ViewBag.IsBaseAppointment = container.Appointment.Type == AppointmentType.Normal || container.Appointment.Type == AppointmentType.Pattern;

                customHtml.ViewBag.CompaniesDataSource = GetCompanies();
                customHtml.ViewBag.ContactsDataSource = GetCompanyContacts(container.CompanyID);

                customHtml.RenderPartial("CustomAppointmentFormPartial", modelAppointment);
            });
            return settings;
        }

        static int lastInsertedID = 0;

        public static void CorrectReminderInfo(CustomAppointment appt) {
            if(appt.ReminderInfo != "" && appt.TimeBeforeStart != "") {
                ReminderInfoCollection reminders = new ReminderInfoCollection();
                ReminderInfoCollectionXmlPersistenceHelper.ObjectFromXml(reminders, appt.ReminderInfo);

                for(int i = 0; i < reminders.Count; i++) {
                    reminders[i].TimeBeforeStart = TimeSpan.Parse(appt.TimeBeforeStart);
                    reminders[i].AlertTime = appt.StartTime.Subtract(reminders[i].TimeBeforeStart);
                }
                ReminderInfoCollectionXmlPersistenceHelper helper = new ReminderInfoCollectionXmlPersistenceHelper(reminders);
                appt.ReminderInfo = helper.ToXml();
            }
        }

        // CRUD operations implementation
        public static void InsertAppointments(CustomAppointment[] appts) {
            if(appts.Length == 0)
                return;

            List<CustomAppointment> appointmnets = HttpContext.Current.Session["AppointmentsList"] as List<CustomAppointment>;
            for(int i = 0; i < appts.Length; i++) {
                if(appts[i] != null) {
                    appts[i].ID = lastInsertedID++;
                    CorrectReminderInfo(appts[i]);
                    appointmnets.Add(appts[i]);
                }
            }
        }

        public static void UpdateAppointments(CustomAppointment[] appts) {
            if(appts.Length == 0)
                return;

            List<CustomAppointment> appointmnets = System.Web.HttpContext.Current.Session["AppointmentsList"] as List<CustomAppointment>;
            for(int i = 0; i < appts.Length; i++) {
                CustomAppointment sourceObject = appointmnets.First<CustomAppointment>(apt => apt.ID == appts[i].ID);
                appts[i].ID = sourceObject.ID;
                appointmnets.Remove(sourceObject);

                CorrectReminderInfo(appts[i]);
                appointmnets.Add(appts[i]);
            }
        }

        public static void RemoveAppointments(CustomAppointment[] appts) {
            if(appts.Length == 0)
                return;

            List<CustomAppointment> appointmnets = HttpContext.Current.Session["AppointmentsList"] as List<CustomAppointment>;
            for(int i = 0; i < appts.Length; i++) {
                CustomAppointment sourceObject = appointmnets.First<CustomAppointment>(apt => apt.ID == appts[i].ID);
                appointmnets.Remove(sourceObject);
            }
        }
    }
}