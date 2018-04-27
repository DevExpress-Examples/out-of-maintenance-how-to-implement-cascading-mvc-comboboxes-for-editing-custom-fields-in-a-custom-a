Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Linq
Imports System.Web

Namespace DevExpressMvcApplication1.Models
    Public Class ModelAppointment
        Public Sub New()
        End Sub

        <Required> _
        Public Property StartTime() As Date

        <Required> _
        Public Property EndTime() As Date

        <Required> _
        Public Property Subject() As String

        Public Property Status() As Integer
        Public Property Description() As String
        Public Property Label() As Integer
        Public Property Location() As String
        Public Property AllDay() As Boolean
        Public Property EventType() As Integer
        Public Property RecurrenceInfo() As String
        Public Property ReminderInfo() As String

        <Required, Display(Name := "Resource")> _
        Public Property OwnerId() As Integer
        Public Property CustomInfo() As String
        Public Property ID() As Integer

        Public Property HasReminder() As Boolean

        Public Property Reminder() As Reminder

        Public ReadOnly Property TimeBeforeStart() As TimeSpan
            Get
                Return If(Reminder Is Nothing, TimeSpan.FromMinutes(10), Reminder.TimeBeforeStart)
            End Get
        End Property

        Public Property CompanyID() As Integer
        Public Property ContactID() As Integer

        Public Sub New(ByVal source As CustomAppointment)
            If source IsNot Nothing Then
                StartTime = source.StartTime
                EndTime = source.EndTime
                Subject = source.Subject
                Status = source.Status
                Description = source.Description
                Label = source.Label
                Location = source.Location
                AllDay = source.AllDay
                EventType = source.EventType
                RecurrenceInfo = source.RecurrenceInfo
                ReminderInfo = source.ReminderInfo
                OwnerId = source.OwnerId
                CustomInfo = source.CustomInfo
                ID = source.ID
                HasReminder = source.ReminderInfo.Trim() <> ""
            End If
        End Sub
    End Class

    Public Class CustomAppointmentTemplateContainer
        Inherits AppointmentFormTemplateContainer

        Public Sub New(ByVal scheduler As MVCxScheduler)
            MyBase.New(scheduler)
        End Sub

        Public ReadOnly Property CustomInfo() As String
            Get
                Return If(Appointment.CustomFields("AppointmentCustomField") IsNot Nothing, Appointment.CustomFields("AppointmentCustomField").ToString(), "")
            End Get
        End Property

        Public ReadOnly Property CompanyID() As Integer
            Get
                Return If(Appointment.CustomFields("AppointmentCompany") IsNot Nothing, Convert.ToInt32(Appointment.CustomFields("AppointmentCompany")), 0)
            End Get
        End Property

        Public ReadOnly Property ContactID() As Integer
            Get
                Return If(Appointment.CustomFields("AppointmentContact") IsNot Nothing, Convert.ToInt32(Appointment.CustomFields("AppointmentContact")), 0)
            End Get
        End Property

    End Class
End Namespace