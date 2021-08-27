<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128553385/16.1.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T479780)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
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
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t479780/)**
<!-- run online end -->


This example illustrates a server-side technique of editing hierarchical custom appointment fields using cascading MVC ComboBoxes.<br>The main idea of implementing cascading MCX ComboBoxes was demonstrated in the following examples:<br><a href="https://www.devexpress.com/Support/Center/p/T155879">GridView - A simple implementation of cascading comboboxes in Batch Edit mode</a><br><a href="https://www.devexpress.com/Support/Center/p/E2844">MVC ComboBox Extension - Cascading Combo Boxes</a><br>In this example, we added two custom fields for appointments (<strong>CompanyID</strong> and <strong>ContactID</strong>)<strong> </strong>and two corresponding MVC ComboBoxes onto a custom Appointment Edit form. <br>An approach for customizing the Appointment Edit Form for working with custom fields was described here:<br><a href="https://www.devexpress.com/Support/Center/p/T156298">How to create a custom Appointment form to work with custom fields (based on T153478)</a><br>Changing a value of the "<strong>CompanyID</strong>" combobox results in filtering data in the "<strong>ContactID</strong>" combobox.

<br/>


