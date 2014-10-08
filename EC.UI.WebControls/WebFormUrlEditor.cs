using System;
using System.Web.UI.Design;

namespace EC.UI.WebControls
{
    public class WebFormUrlEditor : UrlEditor
    {

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            //IWebApplication webApp =(IWebApplication)provider.GetService(typeof(IWebApplication));
            //IProjectItem item=webApp.GetProjectItemFromUrl(value.ToString());

            //Page page;
            //page = (context.Instance as Control).Page;

            //CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            //CompilerResults results;
            //results=codeProvider.CompileAssemblyFromFile(new System.CodeDom.Compiler.CompilerParameters(), new string[] { item.PhysicalPath+".cs" });

            //Object obj;
            //obj = PageParser.GetCompiledPageInstance(item.AppRelativeUrl,item.PhysicalPath,HttpContext.Current);
            //obj=BuildManager.CreateInstanceFromVirtualPath(item.AppRelativeUrl, typeof(Page));

            return base.EditValue(context, provider, value);
        }


        protected override string Filter
        {
            get
            {
                return "Web Forms (*.aspx)|*.aspx|All Files (*.*)|*.*";
            }
        }

    }
}
