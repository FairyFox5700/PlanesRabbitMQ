using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using PlanesRabbitMQ.BL.Models;
using PlanesRabbitMQ.BL.Services.Impl;

namespace PlanesRabbitMQ.BL.Services.Contracts
{
    public class XmlPlanesParser:IXmlPlanesParser
    {
        private readonly ILogger<XmlPlanesParser> _logger;
        private static string SchemaUri = "Resources/planes.xsd";
        private static string XmlPath="Resources/planesxml.xml";
        private const string TargetNamespace = "http://www.mydomain/planes";

        public XmlPlanesParser(ILogger<XmlPlanesParser> logger)
        {
            _logger = logger;
        }
        public List<Models.Plane> ParsePlanes()
        {

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(TargetNamespace, SchemaUri);
            
            var settings = new XmlReaderSettings
            {
                Schemas = schemas,
                ValidationType = ValidationType.Schema,
                ValidationFlags =
                    XmlSchemaValidationFlags.ProcessIdentityConstraints |
                    XmlSchemaValidationFlags.ReportValidationWarnings
            };
            settings.ValidationEventHandler += new ValidationEventHandler(SchemaValidationEventHandler);
            using var input = new StreamReader(XmlPath);
            using XmlReader reader = XmlReader.Create(input, settings);
            XmlSerializer serializer = new XmlSerializer(typeof(Planes));
            Planes planes = (Planes) serializer.Deserialize(reader);
            if (planes != null && planes.Plane.Any())
            {
                var planesList = planes.Plane.ToList(); 
                planesList.Sort();
                return planesList.ToList();
            }

            return new List<Models.Plane>();

        }

        private void SchemaValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                   _logger.LogError($"\nError: {e.Message}");
                    throw new Exception(e.Message);
                case XmlSeverityType.Warning:
                    _logger.LogError($"\nWarning: { e.Message}");
                    throw new Exception(e.Message);
            }
        }
    }
}