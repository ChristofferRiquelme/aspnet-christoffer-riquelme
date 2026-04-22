using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GymPortal.Web.TagHelpers;

[HtmlTargetElement("a", Attributes = "asp-controller, asp-action")]

public class ActiveLinkTagHelper : TagHelper

{

    [HtmlAttributeName("asp-controller")]

    public string? Controller { get; set; }

    [HtmlAttributeName("asp-action")]

    public string? Action { get; set; }

    [ViewContext]

    [HtmlAttributeNotBound]

    public ViewContext ViewContext { get; set; } = default!;

    public override void Process(TagHelperContext context, TagHelperOutput output)

    {

        var currentController = ViewContext.RouteData.Values["controller"]?.ToString();

        var currentAction = ViewContext.RouteData.Values["action"]?.ToString();

        var isActive =

            string.Equals(currentController, Controller, StringComparison.OrdinalIgnoreCase) &&

            string.Equals(currentAction, Action, StringComparison.OrdinalIgnoreCase);

        var existingClasses = output.Attributes["class"]?.Value?.ToString() ?? "";

        if (isActive)

        {

            existingClasses = $"{existingClasses} text-[#D6EB31] font-medium";

        }

        output.Attributes.SetAttribute("class", existingClasses.Trim());

    }

}
