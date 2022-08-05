Imports System.Collections
Imports System.Linq
Imports DevExpressMvcApplication1
Imports DevExpress.Web.Mvc
Imports DevExpressMvcApplication1.Models
Imports System.Collections.Generic
Imports System.Drawing
Imports System
Imports System.Web
Imports System.Web.Mvc.Html
Imports DevExpress.XtraScheduler
Imports DevExpress.Web
Imports DevExpress.XtraScheduler.Xml
Imports System.Runtime.CompilerServices

Namespace DevExpressMvcApplication1.Helpers

    Public Module SchedulerDataHelper

        Public Function GetResources() As List(Of DevExpressMvcApplication1.Models.CustomResource)
            Dim resources As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomResource) = New System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomResource)()
            resources.Add(DevExpressMvcApplication1.Models.CustomResource.CreateCustomResource(1, "Max Fowler", System.Drawing.Color.Yellow.ToArgb()))
            resources.Add(DevExpressMvcApplication1.Models.CustomResource.CreateCustomResource(2, "Nancy Drewmore", System.Drawing.Color.Green.ToArgb()))
            resources.Add(DevExpressMvcApplication1.Models.CustomResource.CreateCustomResource(3, "Pak Jang", System.Drawing.Color.LightPink.ToArgb()))
            Return resources
        End Function

        Private myRand As System.Random = New System.Random()

        Public Function GetAppointments(ByVal resources As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomResource)) As List(Of DevExpressMvcApplication1.Models.CustomAppointment)
            Dim appointments As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment) = New System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment)()
            For Each item As DevExpressMvcApplication1.Models.CustomResource In resources
                Dim subjPrefix As String = item.Name & "'s "
                appointments.Add(DevExpressMvcApplication1.Models.CustomAppointment.CreateCustomAppointment(subjPrefix & "meeting", item.ResID, 2, 5, System.Math.Min(System.Threading.Interlocked.Increment(DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID), DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID - 1)))
                appointments.Add(DevExpressMvcApplication1.Models.CustomAppointment.CreateCustomAppointment(subjPrefix & "travel", item.ResID, 3, 6, System.Math.Min(System.Threading.Interlocked.Increment(DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID), DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID - 1)))
                appointments.Add(DevExpressMvcApplication1.Models.CustomAppointment.CreateCustomAppointment(subjPrefix & "phone call", item.ResID, 0, 10, System.Math.Min(System.Threading.Interlocked.Increment(DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID), DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID - 1)))
            Next

            Return appointments
        End Function

        Public Function GetCompanies() As List(Of DevExpressMvcApplication1.Models.Company)
            If System.Web.HttpContext.Current.Session("CompaniesList") Is Nothing Then
                Dim returnedResult As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.Company) = New System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.Company)()
                For i As Integer = 0 To 10 - 1
                    returnedResult.Add(New DevExpressMvcApplication1.Models.Company() With {.CompanyID = i, .CompanyName = "Company " & i.ToString()})
                Next

                System.Web.HttpContext.Current.Session("CompaniesList") = returnedResult
            End If

            Return TryCast(System.Web.HttpContext.Current.Session("CompaniesList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.Company))
        End Function

        Public Function GetCompanyContacts(ByVal companyID As Integer) As List(Of DevExpressMvcApplication1.Models.CompanyContact)
            If System.Web.HttpContext.Current.Session("ContactsList") Is Nothing Then
                Dim returnedResult As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CompanyContact) = New System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CompanyContact)()
                Dim companies As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.Company) = TryCast(System.Web.HttpContext.Current.Session("CompaniesList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.Company))
                Dim uniqueContactID As Integer = 0
                For i As Integer = 0 To companies.Count - 1
                    For j As Integer = 0 To 5 - 1
                        returnedResult.Add(New DevExpressMvcApplication1.Models.CompanyContact() With {.CompanyID = i, .ContactName = "Contact " & j & ", Company " & i, .ContactID = uniqueContactID})
                        uniqueContactID += 1
                    Next
                Next

                System.Web.HttpContext.Current.Session("ContactsList") = returnedResult
            End If

            Dim contacts As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CompanyContact) = TryCast(System.Web.HttpContext.Current.Session("ContactsList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CompanyContact))
            Return contacts.Where(Function(cont) cont.CompanyID.Equals(companyID)).ToList(Of DevExpressMvcApplication1.Models.CompanyContact)()
        End Function

        Public ReadOnly Property DataObject As SchedulerDataObject
            Get
                Dim lDataObject As DevExpressMvcApplication1.Models.SchedulerDataObject = New DevExpressMvcApplication1.Models.SchedulerDataObject()
                If System.Web.HttpContext.Current.Session("ResourcesList") Is Nothing Then
                    System.Web.HttpContext.Current.Session("ResourcesList") = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.GetResources()
                End If

                lDataObject.Resources = TryCast(System.Web.HttpContext.Current.Session("ResourcesList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomResource))
                If System.Web.HttpContext.Current.Session("AppointmentsList") Is Nothing Then
                    System.Web.HttpContext.Current.Session("AppointmentsList") = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.GetAppointments(lDataObject.Resources)
                End If

                lDataObject.Appointments = TryCast(System.Web.HttpContext.Current.Session("AppointmentsList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment))
                Return lDataObject
            End Get
        End Property

        Private defaultAppointmentStorageField As DevExpress.Web.Mvc.MVCxAppointmentStorage

        Public ReadOnly Property DefaultAppointmentStorage As MVCxAppointmentStorage
            Get
                If DevExpressMvcApplication1.Helpers.SchedulerDataHelper.defaultAppointmentStorageField Is Nothing Then
                    DevExpressMvcApplication1.Helpers.SchedulerDataHelper.defaultAppointmentStorageField = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.CreateDefaultAppointmentStorage()
                End If

                Return DevExpressMvcApplication1.Helpers.SchedulerDataHelper.defaultAppointmentStorageField
            End Get
        End Property

        Private Function CreateDefaultAppointmentStorage() As MVCxAppointmentStorage
            Dim appointmentStorage As DevExpress.Web.Mvc.MVCxAppointmentStorage = New DevExpress.Web.Mvc.MVCxAppointmentStorage()
            appointmentStorage.AutoRetrieveId = True
            appointmentStorage.Mappings.AppointmentId = "ID"
            appointmentStorage.Mappings.Start = "StartTime"
            appointmentStorage.Mappings.[End] = "EndTime"
            appointmentStorage.Mappings.Subject = "Subject"
            appointmentStorage.Mappings.AllDay = "AllDay"
            appointmentStorage.Mappings.Description = "Description"
            appointmentStorage.Mappings.Label = "Label"
            appointmentStorage.Mappings.Location = "Location"
            appointmentStorage.Mappings.RecurrenceInfo = "RecurrenceInfo"
            appointmentStorage.Mappings.ReminderInfo = "ReminderInfo"
            appointmentStorage.Mappings.ResourceId = "OwnerId"
            appointmentStorage.Mappings.Status = "Status"
            appointmentStorage.Mappings.Type = "EventType"
            appointmentStorage.CustomFieldMappings.Add(New DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("AppointmentCustomField", "CustomInfo"))
            appointmentStorage.CustomFieldMappings.Add(New DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("TimeBeforeStart", "TimeBeforeStart"))
            appointmentStorage.CustomFieldMappings.Add(New DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("AppointmentCompany", "CompanyID"))
            appointmentStorage.CustomFieldMappings.Add(New DevExpress.Web.ASPxScheduler.ASPxAppointmentCustomFieldMapping("AppointmentContact", "ContactID"))
            Return appointmentStorage
        End Function

        Private defaultResourceStorageField As DevExpress.Web.Mvc.MVCxResourceStorage

        Public ReadOnly Property DefaultResourceStorage As MVCxResourceStorage
            Get
                If DevExpressMvcApplication1.Helpers.SchedulerDataHelper.defaultResourceStorageField Is Nothing Then
                    DevExpressMvcApplication1.Helpers.SchedulerDataHelper.defaultResourceStorageField = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.CreateDefaultResourceStorage()
                End If

                Return DevExpressMvcApplication1.Helpers.SchedulerDataHelper.defaultResourceStorageField
            End Get
        End Property

        Private Function CreateDefaultResourceStorage() As MVCxResourceStorage
            Dim resourceStorage As DevExpress.Web.Mvc.MVCxResourceStorage = New DevExpress.Web.Mvc.MVCxResourceStorage()
            resourceStorage.Mappings.ResourceId = "ResID"
            resourceStorage.Mappings.Caption = "Name"
            resourceStorage.Mappings.Color = "Color"
            Return resourceStorage
        End Function

        Public Function GetSchedulerSettings() As SchedulerSettings
            Return DevExpressMvcApplication1.Helpers.SchedulerDataHelper.GetSchedulerSettings(Nothing)
        End Function

        Private Function CreateAppointmentRecurrenceFormSettings(ByVal container As DevExpress.Web.ASPxScheduler.AppointmentFormTemplateContainer) As AppointmentRecurrenceFormSettings
            Return New DevExpress.Web.Mvc.AppointmentRecurrenceFormSettings With {.Name = "appointmentRecurrenceForm", .Width = System.Web.UI.WebControls.Unit.Percentage(100), .IsRecurring = container.Appointment.IsRecurring, .DayNumber = container.RecurrenceDayNumber, .[End] = container.RecurrenceEnd, .Month = container.RecurrenceMonth, .OccurrenceCount = container.RecurrenceOccurrenceCount, .Periodicity = container.RecurrencePeriodicity, .RecurrenceRange = container.RecurrenceRange, .Start = container.Start, .WeekDays = container.RecurrenceWeekDays, .WeekOfMonth = container.RecurrenceWeekOfMonth, .RecurrenceType = container.RecurrenceType, .IsFormRecreated = container.IsFormRecreated}
        End Function

        <Extension()>
        Public Function GetSchedulerSettings(ByVal customHtml As System.Web.Mvc.HtmlHelper) As SchedulerSettings
            Dim settings As DevExpress.Web.Mvc.SchedulerSettings = New DevExpress.Web.Mvc.SchedulerSettings()
            settings.Name = "scheduler"
            settings.InitClientAppointment = Sub(sched, evargs)
                evargs.Properties.Add(DevExpress.Web.ASPxScheduler.ClientSideAppointmentFieldNames.AppointmentType, evargs.Appointment.Type)
                evargs.Properties.Add(DevExpress.Web.ASPxScheduler.ClientSideAppointmentFieldNames.Subject, evargs.Appointment.Subject)
            End Sub
            settings.PopupMenuShowing = Sub(sched, evargs)
                If evargs.Menu.MenuId = DevExpress.XtraScheduler.SchedulerMenuItemId.AppointmentMenu Then
                    evargs.Menu.ClientSideEvents.PopUp = "OnAppointmentMenuPopup"
                End If
            End Sub
            settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "SchedulerPartial"}
            settings.EditAppointmentRouteValues = New With {.Controller = "Home", .Action = "EditAppointment"}
            settings.CustomActionRouteValues = New With {.Controller = "Home", .Action = "CustomCallBackAction"}
            settings.Storage.Appointments.Assign(DevExpressMvcApplication1.Helpers.SchedulerDataHelper.DefaultAppointmentStorage)
            settings.Storage.Resources.Assign(DevExpressMvcApplication1.Helpers.SchedulerDataHelper.DefaultResourceStorage)
            settings.Storage.EnableReminders = True
            settings.GroupType = DevExpress.XtraScheduler.SchedulerGroupType.Resource
            settings.Views.DayView.Styles.ScrollAreaHeight = 400
            settings.Start = System.DateTime.Now
            settings.AppointmentFormShowing = Sub(sender, e)
                Dim scheduler = TryCast(sender, DevExpress.Web.Mvc.MVCxScheduler)
                If scheduler IsNot Nothing Then e.Container = New DevExpressMvcApplication1.Models.CustomAppointmentTemplateContainer(scheduler)
            End Sub
            settings.OptionsForms.RecurrenceFormName = "appointmentRecurrenceForm"
            settings.OptionsForms.SetAppointmentFormTemplateContent(Sub(c)
                Dim container = CType(c, DevExpressMvcApplication1.Models.CustomAppointmentTemplateContainer)
                Dim modelAppointment As DevExpressMvcApplication1.Models.ModelAppointment = New DevExpressMvcApplication1.Models.ModelAppointment() With {.ID = If(container.Appointment.Id Is Nothing, -1, CInt(container.Appointment.Id)), .Subject = container.Appointment.Subject, .Location = container.Appointment.Location, .StartTime = container.Appointment.Start, .EndTime = container.Appointment.[End], .AllDay = container.Appointment.AllDay, .Description = container.Appointment.Description, .EventType = CInt(container.Appointment.Type), .Status = System.Convert.ToInt32(container.Appointment.StatusKey), .Label = System.Convert.ToInt32(container.Appointment.LabelKey), .CustomInfo = container.CustomInfo, .CompanyID = container.CompanyID, .ContactID = container.ContactID, .HasReminder = container.Appointment.HasReminder, .Reminder = container.Appointment.Reminder, .OwnerId = System.Convert.ToInt32(container.Appointment.ResourceId)}
                customHtml.ViewBag.DeleteButtonEnabled = container.CanDeleteAppointment
                TryCast(container.ResourceDataSource, DevExpress.Web.ListEditItemCollection).RemoveAt(0)
                customHtml.ViewBag.ResourceDataSource = container.ResourceDataSource
                customHtml.ViewBag.StatusDataSource = container.StatusDataSource
                customHtml.ViewBag.LabelDataSource = container.LabelDataSource
                customHtml.ViewBag.AppointmentRecurrenceFormSettings = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.CreateAppointmentRecurrenceFormSettings(container)
                customHtml.ViewBag.ReminderDataSource = container.ReminderDataSource
                customHtml.ViewBag.IsBaseAppointment = container.Appointment.Type = DevExpress.XtraScheduler.AppointmentType.Normal OrElse container.Appointment.Type = DevExpress.XtraScheduler.AppointmentType.Pattern
                customHtml.ViewBag.CompaniesDataSource = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.GetCompanies()
                customHtml.ViewBag.ContactsDataSource = DevExpressMvcApplication1.Helpers.SchedulerDataHelper.GetCompanyContacts(container.CompanyID)
                customHtml.RenderPartial("CustomAppointmentFormPartial", modelAppointment)
            End Sub)
            Return settings
        End Function

        Private lastInsertedID As Integer = 0

        Public Sub CorrectReminderInfo(ByVal appt As DevExpressMvcApplication1.Models.CustomAppointment)
            If Not Equals(appt.ReminderInfo, "") AndAlso Not Equals(appt.TimeBeforeStart, "") Then
                Dim reminders As DevExpress.XtraScheduler.Xml.ReminderInfoCollection = New DevExpress.XtraScheduler.Xml.ReminderInfoCollection()
                Call DevExpress.XtraScheduler.Xml.ReminderInfoCollectionXmlPersistenceHelper.ObjectFromXml(reminders, appt.ReminderInfo)
                For i As Integer = 0 To reminders.Count - 1
                    reminders(CInt((i))).TimeBeforeStart = System.TimeSpan.Parse(appt.TimeBeforeStart)
                    reminders(CInt((i))).AlertTime = appt.StartTime.Subtract(reminders(CInt((i))).TimeBeforeStart)
                Next

                Dim helper As DevExpress.XtraScheduler.Xml.ReminderInfoCollectionXmlPersistenceHelper = New DevExpress.XtraScheduler.Xml.ReminderInfoCollectionXmlPersistenceHelper(reminders)
                appt.ReminderInfo = helper.ToXml()
            End If
        End Sub

        ' CRUD operations implementation
        Public Sub InsertAppointments(ByVal appts As DevExpressMvcApplication1.Models.CustomAppointment())
            If appts.Length = 0 Then Return
            Dim appointmnets As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment) = TryCast(System.Web.HttpContext.Current.Session("AppointmentsList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment))
            For i As Integer = 0 To appts.Length - 1
                If appts(i) IsNot Nothing Then
                    appts(CInt((i))).ID = System.Math.Min(System.Threading.Interlocked.Increment(DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID), DevExpressMvcApplication1.Helpers.SchedulerDataHelper.lastInsertedID - 1)
                    Call DevExpressMvcApplication1.Helpers.SchedulerDataHelper.CorrectReminderInfo(appts(i))
                    appointmnets.Add(appts(i))
                End If
            Next
        End Sub

        Public Sub UpdateAppointments(ByVal appts As DevExpressMvcApplication1.Models.CustomAppointment())
            If appts.Length = 0 Then Return
            Dim appointmnets As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment) = TryCast(System.Web.HttpContext.Current.Session("AppointmentsList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment))
            For i As Integer = 0 To appts.Length - 1
                Dim sourceObject As DevExpressMvcApplication1.Models.CustomAppointment = appointmnets.First(Of DevExpressMvcApplication1.Models.CustomAppointment)(Function(apt) apt.ID = appts(CInt((i))).ID)
                appts(CInt((i))).ID = sourceObject.ID
                appointmnets.Remove(sourceObject)
                Call DevExpressMvcApplication1.Helpers.SchedulerDataHelper.CorrectReminderInfo(appts(i))
                appointmnets.Add(appts(i))
            Next
        End Sub

        Public Sub RemoveAppointments(ByVal appts As DevExpressMvcApplication1.Models.CustomAppointment())
            If appts.Length = 0 Then Return
            Dim appointmnets As System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment) = TryCast(System.Web.HttpContext.Current.Session("AppointmentsList"), System.Collections.Generic.List(Of DevExpressMvcApplication1.Models.CustomAppointment))
            For i As Integer = 0 To appts.Length - 1
                Dim sourceObject As DevExpressMvcApplication1.Models.CustomAppointment = appointmnets.First(Of DevExpressMvcApplication1.Models.CustomAppointment)(Function(apt) apt.ID = appts(CInt((i))).ID)
                appointmnets.Remove(sourceObject)
            Next
        End Sub
    End Module
End Namespace
