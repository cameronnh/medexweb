using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace medexnet.Models
{
    public class DALtoMedex
    {
        public static PatientPrescriptions DMPatientPrescriptions(DataLibrary.Models.PatientPrescriptions temp)
        {
            return new PatientPrescriptions
            {
                id = temp.id,
                patientId = temp.patientId,
                doctorId = temp.doctorFID,
                prescriptionId = temp.prescriptionFID,
                deliveryId = temp.deliveryFID,
                name = temp.name,
                dosage = temp.dosage,
                pillcount = temp.pillCount,
                numberofrefills = temp.numberofRefills,
                useBefore = temp.useBefore,
                description = temp.description,
                datePrescribed = temp.datePrescribed
            };
        }

        public static Delivery DMDeliveries(DataLibrary.Models.Delivery temp)
        {
            return new Delivery
            {
                Id = temp.Id,
                shippedDate = temp.shippedDate,
                arrivalDate = temp.arrivalDate,
                prescriptionName = temp.prescriptionName,
                prescriptionDosage = temp.prescriptionDosage
            };
        }

        public static UserModel DMDoctorData(DataLibrary.Models.UserModel temp)
        {
            return new UserModel
            {
                Id = temp.Id,
                fName = temp.fName,
                lName = temp.lName,
                officeHours = temp.officeHours,
                email = temp.email,
                phoneNumber = temp.phoneNumber,
                city = temp.city,
                state = temp.state,
                zipcode = temp.zipcode,
                streetAddress = temp.streetAddress
            };
        }
    }
}