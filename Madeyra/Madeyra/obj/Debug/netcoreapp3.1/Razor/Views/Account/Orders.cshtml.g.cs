#pragma checksum "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bf9a4226f02f17ddb75c446174825e9dc2986065"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Orders), @"mvc.1.0.view", @"/Views/Account/Orders.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\_ViewImports.cshtml"
using Madeyra;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\_ViewImports.cshtml"
using Madeyra.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\_ViewImports.cshtml"
using Madeyra.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\_ViewImports.cshtml"
using Madeyra.Services;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bf9a4226f02f17ddb75c446174825e9dc2986065", @"/Views/Account/Orders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ddc0ea38c2fbf656db6134b9cff8708f444fbeca", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Orders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Order>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:150px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "profilemenu", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "account", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("button"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-warning"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
  
    ViewData["Title"] = "Orders";
    int count = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("    <main>\r\n        <div class=\"hesab-class\">\r\n            <div class=\"container\">\r\n                <div class=\"row\">\r\n                    <div class=\"col-xl-2 col-lg-3 col-md-3 col-sm-12 col-12\">\r\n                        ");
#nullable restore
#line 13 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                   Write(await Html.PartialAsync("_AccountmenuPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                    </div>
                    <div class=""col-xl-10 col-lg-9 col-md-9 col-sm-12 col-12"">
                        <div class=""melumatlarim"">
                            <div class=""row"">
                                <h1>Sifariş tarixçəsi</h1>
                                <div class=""container"">
                                </div>
                            </div>

");
#nullable restore
#line 23 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                             if (Model.Count == 0 || Model == null)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"row\">\r\n                                    <span>Sizin hər hansı bir sifarişiniz mövcud deyil!</span>\r\n                                </div>\r\n");
#nullable restore
#line 28 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"accordion\" id=\"accordionExample\">\r\n\r\n");
#nullable restore
#line 33 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                             foreach (var order in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"accordion-item\" >\r\n                                    <h2 class=\"accordion-header\"");
            BeginWriteAttribute("id", " id=\"", 1432, "\"", 1454, 2);
            WriteAttributeValue("", 1437, "heading-", 1437, 8, true);
#nullable restore
#line 36 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
WriteAttributeValue("", 1445, order.Id, 1445, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                        <button class=\"accordion-button collapsed\" style=\"box-shadow:none !important\" type=\"button\" data-bs-toggle=\"collapse\" data-bs-target=\"#collapse-");
#nullable restore
#line 37 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                                                                                                                                                                   Write(order.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" aria-expanded=\"false\"");
            BeginWriteAttribute("aria-controls", " aria-controls=\"", 1674, "\"", 1708, 2);
            WriteAttributeValue("", 1690, "collapse-", 1690, 9, true);
#nullable restore
#line 37 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
WriteAttributeValue("", 1699, order.Id, 1699, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                            ");
#nullable restore
#line 38 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                       Write(order.CreatedAt);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </button>\r\n                                    </h2>\r\n                                    <div");
            BeginWriteAttribute("id", " id=\"", 1908, "\"", 1931, 2);
            WriteAttributeValue("", 1913, "collapse-", 1913, 9, true);
#nullable restore
#line 41 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
WriteAttributeValue("", 1922, order.Id, 1922, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"accordion-collapse collapse\"");
            BeginWriteAttribute("aria-labelledby", " aria-labelledby=\"", 1968, "\"", 2003, 2);
            WriteAttributeValue("", 1986, "heading-", 1986, 8, true);
#nullable restore
#line 41 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
WriteAttributeValue("", 1994, order.Id, 1994, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-bs-parent=\"#accordionExample\">\r\n                                        <div class=\"accordion-body\">\r\n");
#nullable restore
#line 43 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                             foreach (var item in order.OrderItems)
                                            {
                                                
                                                decimal total = (item.SalePrice - (item.SalePrice * item.DiscountPercent) / 100) * item.Count;


#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <p>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bf9a4226f02f17ddb75c446174825e9dc298606511137", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 2517, "~/uploads/product/", 2517, 18, true);
#nullable restore
#line 48 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
AddHtmlAttributeValue("", 2535, item.Product.ProductImages.FirstOrDefault(x=>x.IsPoster==true)?.Image, 2535, 70, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" <strong>Məhsulun Adı:</strong>");
#nullable restore
#line 48 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                                                                                                                                                                                   Write(item.ProdName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <strong>Cəmi:</strong> ");
#nullable restore
#line 48 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                                                                                                                                                                                                                         Write(total.ToString("#.##"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" <strong>Sayı:</strong> ");
#nullable restore
#line 48 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                                                                                                                                                                                                                                                                        Write(item.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <strong>Status:</strong>");
#nullable restore
#line 48 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                                                                                                                                                                                                                                                                                                            Write(order.Status);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <strong>Məhsul statusu:</strong>");
#nullable restore
#line 48 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                                                                                                                                                                                                                                                                                                                                                          Write(order.DeliveryStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 49 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        </div>\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 53 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n");
#nullable restore
#line 56 "C:\Users\heyde\source\repos\Madeyra\Madeyra\Views\Account\Orders.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"row\" style=\"margin-top:1rem\">\r\n                                <div class=\"col-xl-12\">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bf9a4226f02f17ddb75c446174825e9dc298606516346", async() => {
                WriteLiteral("Davam et");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n        </div>\r\n    </main>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591
