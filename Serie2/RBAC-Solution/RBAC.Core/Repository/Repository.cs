using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Configuration;

namespace RBAC.Core.Repository
{
    public abstract class Repository<M> : IRepository<M>
    {
        protected readonly String fileName;
        protected readonly XDocument document;

        public Repository(String fileName)
        {
            this.fileName = fileName;

            this.document = XDocument.Load(ConfigurationManager.AppSettings["App_Data_Url"] + "\\" + this.fileName);
        }

        public abstract List<M> LoadAll();

    }
}
