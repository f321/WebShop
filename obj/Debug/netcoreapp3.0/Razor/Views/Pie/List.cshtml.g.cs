#pragma checksum "C:\Users\fical\source\repos\BethanysPieShop\Views\Pie\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "419a24a05431aabeb0048a47cd7d161bc5c983c7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Pie_List), @"mvc.1.0.view", @"/Views/Pie/List.cshtml")]
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
#line 1 "C:\Users\fical\source\repos\BethanysPieShop\Views\_ViewImports.cshtml"
using BethanysPieShop.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\fical\source\repos\BethanysPieShop\Views\_ViewImports.cshtml"
using BethanysPieShop.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"419a24a05431aabeb0048a47cd7d161bc5c983c7", @"/Views/Pie/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"25174df3599e4a3cf69b5ced54924f3276c51095", @"/Views/_ViewImports.cshtml")]
    public class Views_Pie_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div id=\"pieDiv\">\n\n\n</div>\n\n");
            DefineSection("scripts", async() => {
                WriteLiteral(@"

    <script>

        $(document).ready(function () {
            LoadMorePies();
        });

        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                LoadMorePies();
            }
        });

        function LoadMorePies() {

            $.ajax({
                type: 'GET',
                url: '/api/PieData',
                dataType: 'json',
                success: function (jsonData) {
                    if (jsonData == null) {
                        alert('no data returned');
                        return;
                    }

                    $.each(jsonData, function (index, pie) {

                        var PieSummarString = '<div class=""col-sm-4 col-lg-4 col-md-4""> ' +
                                              '  <div class=""thumbnail"">' +
                                              '     <img src=""' + pie.imageThumbnailUrl + '"" alt="""">' +
                                              '      <di");
                WriteLiteral(@"v class=""caption"">' +
                                              '         <h3 class=""pull-right"">' + pie.price + '</h3>' +
                                              '         <h3>' +
                                              '             <a href=/Pie/Details/' + pie.pieId + '>' + pie.name + '</a>' +
                                              '         </h3>' +
                                              '         <p>' + pie.shortDescription + '</p>' +
                                              '    </div>' +
                                              '    <div class=""addToCart"">' +
                                              '        <p class=""button"">' +
                                              '             <a class=""btn btn-primary"" href=/ShoppingCart/AddToShoppingCart?pieId=' + pie.pieId + '>Add to cart</a>' +
                                              '         </p>' +
                                              '     </div>' +
                                         ");
                WriteLiteral(@"     '  </div>' +
                                              '</div>';

                        $('#pieDiv').append(PieSummarString);
                    });
                },
                error: function (ex) {
                    alert(ex);
                }
            });
            return false;
        }
    </script>

");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
