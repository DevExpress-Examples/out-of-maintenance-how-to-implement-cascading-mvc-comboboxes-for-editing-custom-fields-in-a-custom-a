Imports System
Imports System.Collections.Generic
Imports System.Web.Mvc
Imports DevExpress.Web.Mvc
Imports DevExpressMvcApplication1.Models
Imports DevExpressMvcApplication1.Helpers

Namespace DevExpressMvcApplication1.Controllers

    Public Class HomeController
        Inherits Controller

        '
        ' GET: /Home/
        Public Function Index() As ActionResult
            Return View(SchedulerDataHelper.DataObject)
        End Function

        Public Function SchedulerPartial() As ActionResult
            Return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject)
        End Function

        Public Function EditAppointment() As ActionResult
            Call UpdateAppointment()
            Return PartialView("SchedulerPartial", SchedulerDataHelper.DataObject)
        End Function

        Public Function ContactPartial(ByVal CompanyID As Integer) As ActionResult
            ViewBag.ContactsDataSource = SchedulerDataHelper.GetCompanyContacts(CompanyID)
            Return PartialView("ComboboxContactPartial")
        End Function

        Public Function CompanyPartial() As ActionResult
            Return PartialView("ComboboxCompanyPartial")
        End Function

        Private Shared Sub UpdateAppointment()
            Dim appointmnets As List(Of CustomAppointment) = TryCast(Web.HttpContext.Current.Session("AppointmentsList"), List(Of CustomAppointment))
            Dim resources As List(Of CustomResource) = TryCast(Web.HttpContext.Current.Session("ResourcesList"), List(Of CustomResource))
            Dim insertedAppts As CustomAppointment() = SchedulerExtension.GetAppointmentsToInsert(Of CustomAppointment)(SchedulerDataHelper.GetSchedulerSettings(), appointmnets, resources)
            Call SchedulerDataHelper.InsertAppointments(insertedAppts)
            Dim updatedAppts As CustomAppointment() = SchedulerExtension.GetAppointmentsToUpdate(Of CustomAppointment)(SchedulerDataHelper.GetSchedulerSettings(), appointmnets, resources)
            Call SchedulerDataHelper.UpdateAppointments(updatedAppts)
            Dim removedAppts As CustomAppointment() = SchedulerExtension.GetAppointmentsToRemove(Of CustomAppointment)(SchedulerDataHelper.GetSchedulerSettings(), appointmnets, resources)
            Call SchedulerDataHelper.RemoveAppointments(removedAppts)
        End Sub
    End Class
End Namespace
