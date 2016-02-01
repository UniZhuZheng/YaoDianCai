using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace UniDllMenu
{
    /// <summary>
    /// The Form class extends the HtmlForm HTML control by overriding its RenderAttributes()
    /// method and NOT emitting an action attribute.
    /// </summary>
    public class Form : System.Web.UI.HtmlControls.HtmlForm
    {
        /// <summary>
        /// The RenderAttributes method adds the attributes to the rendered &lt;form&gt; tag.
        /// We override this method so that the action attribute is not emitted.
        /// </summary>
        protected override void RenderAttributes(HtmlTextWriter writer)
        {
            // write the form's name
            writer.WriteAttribute("name", this.Name);
            base.Attributes.Remove("name");

            // write the form's method
            writer.WriteAttribute("method", this.Method);
            base.Attributes.Remove("method");

            // remove the action attribute
            base.Attributes.Remove("action");

            // finally write all other attributes
            this.Attributes.Render(writer);

            if (base.ID != null)
                writer.WriteAttribute("id", base.ClientID);
        }

    }

    //创建此类并对其进行编译之后，要在 ASP.NET Web 应用程序中使用它，应首先将其添加到 Web 应用程序的 References 文件夹中。
    //然后，要 使用它来代替 HtmlForm 类，做法是在 ASP.NET 网页的顶部添加以下内容：  
    //<%@ Register TagPrefix="af" Namespace="ActionlessForm" Assembly="ActionlessForm" %>  
}
