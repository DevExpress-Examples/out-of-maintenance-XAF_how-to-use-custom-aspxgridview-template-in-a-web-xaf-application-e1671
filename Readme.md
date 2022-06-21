<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128594313/21.2.8%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E1671)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* **[ChangeColumnTemplateController.cs](./CS/WebExample.Module.Web/ChangeColumnTemplateController.cs) (VB: [ChangeColumnTemplateController.vb](./VB/WebExample.Module.Web/ChangeColumnTemplateController.vb))**
<!-- default file list end -->
# How to use custom ASPxGridView template in a Web XAF application
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e1671/)**
<!-- run online end -->


<p>The ASPxGridView, which is used as the grid list editor in XAF Web applications, provides the capability to customize its style via templates. The result of such a customization is shown in the ASPxGridView Demo.</p><p>You can achieve the same result in XAF. The difference is that you will need to create controls dynamically.</p><p>In this example, a custom DataItemTemplate - UpDownButtonsTemplate - is implemented to display the Up and Down buttons in the grid column. These buttons are intended to change the position of grid rows. This is implemented via grid callbacks - when the button is clicked, a grid callback is performed on the client side, and the server-side code changes the position of the grid rows by changing the Index property, by which the grid is sorted.</p><p><strong>See also:</strong><br />
<a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewGridViewDataColumn_DataItemTemplatetopic"><u>ASPxGridView.Templates Property</u></a><br />
<a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewGridViewDataColumn_DataItemTemplatetopic"><u>GridViewDataColumn.DataItemTemplate Property</u></a><br />
<a href="https://www.devexpress.com/Support/Center/p/E4610">How to implement multi-row editing in the ASP.NET ListView</a></p>

<br/>


