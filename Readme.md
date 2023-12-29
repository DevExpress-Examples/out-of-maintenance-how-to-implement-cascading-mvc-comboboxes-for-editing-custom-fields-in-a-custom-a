<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/DevExpressMvcApplication1/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/DevExpressMvcApplication1/Controllers/HomeController.vb))
* [CustomClasses.cs](./CS/DevExpressMvcApplication1/Models/CustomClasses.cs) (VB: [CustomClasses.vb](./VB/DevExpressMvcApplication1/Models/CustomClasses.vb))
* [CustomFormHelper.cs](./CS/DevExpressMvcApplication1/Models/CustomFormHelper.cs) (VB: [CustomFormHelper.vb](./VB/DevExpressMvcApplication1/Models/CustomFormHelper.vb))
* [SchedulerDataHelper.cs](./CS/DevExpressMvcApplication1/Models/SchedulerDataHelper.cs) (VB: [SchedulerDataHelper.vb](./VB/DevExpressMvcApplication1/Models/SchedulerDataHelper.vb))
* [ComboboxCompanyPartial.cshtml](./CS/DevExpressMvcApplication1/Views/Home/ComboboxCompanyPartial.cshtml)
* [ComboboxContactPartial.cshtml](./CS/DevExpressMvcApplication1/Views/Home/ComboboxContactPartial.cshtml)
* [CustomAppointmentFormPartial.cshtml](./CS/DevExpressMvcApplication1/Views/Home/CustomAppointmentFormPartial.cshtml)
* [SchedulerPartial.cshtml](./CS/DevExpressMvcApplication1/Views/Home/SchedulerPartial.cshtml)
<!-- default file list end -->
# How to implement cascading MVC ComboBoxes for editing custom fields in a custom Appointment Edit form


This example illustrates a server-side technique of editing hierarchical custom appointment fields using cascading MVC ComboBoxes.<br>The main idea of implementing cascading MCX ComboBoxes was demonstrated in the following examples:<br><a href="https://www.devexpress.com/Support/Center/p/T155879">GridView - A simple implementation of cascading comboboxes in Batch Edit mode</a><br><a href="https://www.devexpress.com/Support/Center/p/E2844">MVC ComboBox Extension - Cascading Combo Boxes</a><br>In this example, we added two custom fields for appointments (<strong>CompanyID</strong> and <strong>ContactID</strong>)<strong> </strong>and two corresponding MVC ComboBoxes onto a custom Appointment Edit form. <br>An approach for customizing the Appointment Edit Form for working with custom fields was described here:<br><a href="https://www.devexpress.com/Support/Center/p/t153478">How to create a custom Appointment form to work with custom fields</a><br>Changing a value of the "<strong>CompanyID</strong>" combobox results in filtering data in the "<strong>ContactID</strong>" combobox.

<br/>


