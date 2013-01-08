using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Configuration;
using System.Web;

namespace RBAC.Core.Repository
{
    public abstract class Repository<M> : IRepository<M>
    {
        protected readonly String fileName;
        protected readonly String app_Data;
        protected readonly XDocument document;

        public Repository(String fileName)
        {
            this.fileName = fileName;

            if (HttpContext.Current != null)
            {
                this.app_Data = HttpContext.Current.Server.MapPath("~/Bin/App_Data/");
            }
            else
            {
                String projectName = "RBAC.Core.Tests";
                String pathTemp = Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf(projectName) + projectName.Length + 1);
                app_Data = pathTemp + "App_Data\\";
            }

            this.document = XDocument.Load(this.app_Data + this.fileName);
        }

        public abstract List<M> LoadAll();

    }
}
