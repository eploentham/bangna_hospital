using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class EpidemSendEPIDEM
    {
        public EpidemHospital hospital = new EpidemHospital();
        public EpidemPerson person = new EpidemPerson();
        public EpidemReport epidem_report = new EpidemReport();
        public EpidemLabReport lab_report = new EpidemLabReport();
        public EpidemVaccination[] vaccination = new EpidemVaccination[1];
    }
}
