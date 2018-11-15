<!-- default file list -->
*Files to look at*:

* **[ChangeColumnTemplateController.cs](./CS/WebExample.Module.Web/ChangeColumnTemplateController.cs) (VB: [ChangeColumnTemplateController.vb](./VB/WebExample.Module.Web/ChangeColumnTemplateController.vb))**
<!-- default file list end -->
# How to use custom ASPxGridView template in a Web XAF application


<p>The ASPxGridView, which is used as the grid list editor in XAF Web applications, provides the capability to customize its style via templates. The result of such a customization is shown in the ASPxGridView Demo.</p><p>You can achieve the same result in XAF. The difference is that you will need to create controls dynamically.</p><p>In this example, a custom DataItemTemplate - UpDownButtonsTemplate - is implemented to display the Up and Down buttons in the grid column. These buttons are intended to change the position of grid rows. This is implemented via grid callbacks - when the button is clicked, a grid callback is performed on the client side, and the server-side code changes the position of the grid rows by changing the Index property, by which the grid is sorted.</p><p><strong>See also:</strong><br />
<a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewGridViewDataColumn_DataItemTemplatetopic"><u>ASPxGridView.Templates Property</u></a><br />
<a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewGridViewDataColumn_DataItemTemplatetopic"><u>GridViewDataColumn.DataItemTemplate Property</u></a><br />
<a href="https://www.devexpress.com/Support/Center/p/E4610">How to implement multi-row editing in the ASP.NET ListView</a></p>

<br/>


