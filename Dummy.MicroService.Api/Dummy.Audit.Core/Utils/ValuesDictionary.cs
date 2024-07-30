using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Utils
{
    public record struct ValuesDictionary
    {
        private Dictionary<string, string> _dictionary;
        public ValuesDictionary()
        {
            _dictionary = new Dictionary<string, string>
            {
                { "OrderTypeId", "Tipo Orden" },
                { "OrderColorCubeId", "Color de Cubo" },
                { "InsuranceCompanyId", "Seguro" },
                { "PhoneCountryId", "Caracteristica del Pais" },
                { "AgreementDateHour", "Fecha y hora del acuerdo" },
                { "OrderNumber", "Numero de orden" },
                { "AppointmentId", "Identificación de cita" },
                { "Mileage", "Kilometraje" },
                { "NextMileageService", "Servicio de próximo kilometraje" },
                { "TentativeNextDateService", "Servicio de próxima fecha tentativa" },
                { "NextDateService", "Servicio de próxima fecha" },
                { "NumberCube", "Numero de Cubo" },
                { "ClaimNumber", "Numero de reclamo" },
                { "Observations", "Observaciones" },
                { "Email", "Correo Electrónico" },
                { "OrderIdLinked", "ID de Orden Vinculada" },
                { "CustomerSignatureId", "ID de Firma del Cliente" },
                { "EmployeeSignatureId", "ID de Firma del Empleado" },
                { "PhoneAreaCode", "Código de Área del Teléfono" },
                { "PhoneNumber", "Número de Teléfono" },
                { "ProductBagId", "ID de Bolsa de Producto" },
                { "AgreementFileId", "ID de Archivo del Acuerdo" },
                { "AgreementPersonId", "ID de Persona del Acuerdo" },
                { "Approved", "Aprobado" },
                { "OrderParentId", "ID de Orden Principal" },
            };
        }


        public string FindTranslate(string key)
        {
            return _dictionary[key];
        }
    }
}
