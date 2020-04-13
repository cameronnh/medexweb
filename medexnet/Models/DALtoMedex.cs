using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLibrary;

namespace medexnet.Models
{
    public class DALtoMedex
    {
        public static UserModel GetUserData(DataLibrary.Models.UserModel temp)
        {
            return new UserModel
            {
                Id = temp.Id,
                fName = temp.fName,
                lName = temp.lName,
                email = temp.email,
                password = temp.password,
                phoneNumber = temp.phoneNumber,
                streetAddress = temp.streetAddress,
                city = temp.city,
                state = temp.state,
                zipcode = temp.zipcode,
                accountType = temp.accountType,
                officeHours = temp.officeHours,
            };
        }
        public static PatientPrescriptions DMPatientPrescriptions(DataLibrary.Models.PatientPrescriptions temp)
        {
            return new PatientPrescriptions
            {
                Id = temp.Id,
                patientFID = temp.patientFID,
                doctorFID = temp.doctorFID,
                prescriptionFID = temp.prescriptionFID,
                name = temp.name,
                dosage = temp.dosage,
                pillCount = temp.pillCount,
                numberofRefills = temp.numberofRefills,
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

        public static Appointment DMAppointmentData(DataLibrary.Models.Appointment temp)
        {
            return new Appointment
            {
                Id = temp.Id,
                PatientFID = temp.PatientFID,
                date = temp.date,
                desc = temp.desc,
                isConfirmed = temp.isConfirmed,
                doctor = DMDoctorData(temp.doctor)
            };
        }
        public static Chats DMChatData(DataLibrary.Models.Chats temp)
        {
            return new Chats
            {
                Id = temp.Id,
                doctorID = temp.doctorID,
                topic = temp.topic,
                patientID = temp.patientID,
                messageList = temp.messageList.ConvertAll(new Converter<DataLibrary.Models.Message, Message>(DMMessageData))
            };
        }
        public static Message DMMessageData(DataLibrary.Models.Message temp)
        {
            return new Message
            {
                Id = temp.Id,
                date = temp.date,
                user = temp.user,
                userID = temp.userID,
                text = temp.text,
                time = temp.time
            };
        }
    }
}