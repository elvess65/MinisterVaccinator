using CoreFramework;
using FrameworkPackages.MinMax;
using MinisterVaccinator.Data.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace MinisterVaccinator.Gameplay
{
    public class PersonFactory
    {
        private List<EnumsCollection.Roles> m_CorrectRolesBuffer = new List<EnumsCollection.Roles>();
        private List<EnumsCollection.Roles> m_AllRolesBuffer = new List<EnumsCollection.Roles>();
        private List<EnumsCollection.Roles> m_WrongRolesBuffer = new List<EnumsCollection.Roles>();

        public EntityData_Person GetCorrectPerson(EntityData_Task task)
        {
            //Age

            //Pick random age
            int rndIndex = Random.Range(0, task.Cure.AgesToApply.Length);
            MinMax rndAgeBound = task.Cure.AgesToApply[rndIndex];
 
            //Fill buffer with Role types
            EnumsCollection.RetrieveRolesToBuffer(task.RolesToVaccinate, ref m_CorrectRolesBuffer);

            //Pick random role
            rndIndex = Random.Range(0, m_CorrectRolesBuffer.Count);
            EnumsCollection.Roles role = m_CorrectRolesBuffer[rndIndex];

            //Create data
            EntityData_Person person = new EntityData_Person()
            {
                Age = (int)rndAgeBound.RandomValue,
                Role = role
            };

            return person;
        }

        public EntityData_Person GetWrongPerson(EntityData_Mode mode, EntityData_Task task)
        {
            bool wrongAge = false;
            bool wrongRole = false;

            //Ensure always is something wrong
            while (!wrongRole && !wrongAge)
            {
                float rndValue = Random.Range(0, 100);
                if (rndValue > 50)
                    wrongAge = true;

                rndValue = Random.Range(0, 100);
                if (rndValue > 50)
                    wrongRole = true;
            }

            //Pick random age withing specified bounds
            int age = (int)task.Cure.AgesToApply[0].Min;
            if (wrongAge)
            {    
                while (IsInRange(task.Cure.AgesToApply, age))
                    age = (int)Random.Range(mode.AgeToShow.Min, mode.AgeToShow.Max);
            }
            else
                age = (int)Random.Range(mode.AgeToShow.Min, mode.AgeToShow.Max);

            //Fill correct Role types buffer
            EnumsCollection.RetrieveRolesToBuffer(task.RolesToVaccinate, ref m_CorrectRolesBuffer);

            //Fill all Role types buffer
            EnumsCollection.RetrieveRolesToBuffer(mode.RolesToShow, ref m_AllRolesBuffer);

            //Prepare wrong roles buffer
            m_WrongRolesBuffer.Clear();

            //Fill wrong roles buffer
            foreach (EnumsCollection.Roles allRole in m_AllRolesBuffer)
            {
                if (wrongRole && !m_CorrectRolesBuffer.Contains(allRole) || !wrongRole)
                {
                    m_WrongRolesBuffer.Add(allRole);
                }
            }

            //Pick random role from buffer
            int rndIndex = Random.Range(0, m_WrongRolesBuffer.Count);
            EnumsCollection.Roles role = m_WrongRolesBuffer[rndIndex];

            //Create data
            EntityData_Person person = new EntityData_Person()
            {
                Age = age,
                Role = role
            };

            return person;
        }


        private bool IsInRange(MinMax[] ageToApply, float val)
        {
            bool isInRange = true;
            for (int i = 0; i < ageToApply.Length; i++)
            {
                if (!ageToApply[i].IsInRange(val))
                    return false;
            }

            return isInRange;
        }

    }
}
