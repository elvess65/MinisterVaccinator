using CoreFramework;
using FrameworkPackages.MinMax;
using MinisterVaccinator.Data.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace MinisterVaccinator.Gameplay
{
    public class PersonFactory
    {
        private List<EnumsCollection.RolesFlags> m_CorrectRolesBuffer = new List<EnumsCollection.RolesFlags>();
        private List<EnumsCollection.RolesFlags> m_AllRolesBuffer = new List<EnumsCollection.RolesFlags>();
        private List<EnumsCollection.RolesFlags> m_WrongRolesBuffer = new List<EnumsCollection.RolesFlags>();

        public EntityData_Person GetCorrectPerson(EntityData_Task task)
        {
            //Age
            int age = GetCorrectPersonAge(task);

            //Role
            EnumsCollection.RolesFlags role = GetCorrectPersonRole(task);

            //Create data
            EntityData_Person person = new EntityData_Person()
            {
                Age = age,
                Role = role
            };

            return person;
        }

        public EntityData_Person GetWrongPerson(EntityData_Mode mode, EntityData_Task task)
        {
            bool wrongAge = false;
            bool wrongRole = false;

            //If there is no age - only Role should be considered
            if (task.Cure.HasNoAge)
                wrongRole = true;

            RandomizeRoleAndAge(ref wrongAge, ref wrongRole);

            int age = GetWrongPersonAge(mode, task, wrongAge);
            EnumsCollection.RolesFlags role = GetWrongPersonRole(mode, task, wrongRole);

            //Create data
            EntityData_Person person = new EntityData_Person()
            {
                Age = age,
                Role = role
            };

            return person;
        }

        #region Tools

        private int GetCorrectPersonAge(EntityData_Task task)
        {
            int age = -1;

            //Pick random age
            if (!task.Cure.HasNoAge)
            {
                int rndIndex = Random.Range(0, task.Cure.AgesToApply.Length);
                MinMax rndAgeBound = task.Cure.AgesToApply[rndIndex];

                age = (int)rndAgeBound.RandomValue;
            }

            return age;
        }

        private EnumsCollection.RolesFlags GetCorrectPersonRole(EntityData_Task task)
        {
            //Fill buffer with Role types
            EnumsCollection.RetrieveRolesToBuffer(task.RolesToVaccinate, ref m_CorrectRolesBuffer);

            //Pick random role
            int rndIndex = Random.Range(0, m_CorrectRolesBuffer.Count);
            EnumsCollection.RolesFlags role = m_CorrectRolesBuffer[rndIndex];

            return role;
        }

        private int GetWrongPersonAge(EntityData_Mode mode, EntityData_Task task, bool wrongAge)
        {
            //Pick random age withing specified bounds
            int age = -1;

            if (!task.Cure.HasNoAge)
            {
                if (wrongAge)
                {
                    while (IsInRange(task.Cure.AgesToApply, age))
                        age = (int)Random.Range(mode.AgeToShow.Min, mode.AgeToShow.Max);
                }
                else
                    age = (int)Random.Range(mode.AgeToShow.Min, mode.AgeToShow.Max);
            }

            return age;
        }

        private EnumsCollection.RolesFlags GetWrongPersonRole(EntityData_Mode mode, EntityData_Task task, bool wrongRole)
        {
            //Fill correct Role types buffer
            EnumsCollection.RetrieveRolesToBuffer(task.RolesToVaccinate, ref m_CorrectRolesBuffer);

            //Fill all Role types buffer
            EnumsCollection.RetrieveRolesToBuffer(mode.RolesToShow, ref m_AllRolesBuffer);

            //Prepare wrong roles buffer
            m_WrongRolesBuffer.Clear();

            //Fill wrong roles buffer
            foreach (EnumsCollection.RolesFlags allRole in m_AllRolesBuffer)
            {
                if (wrongRole && !m_CorrectRolesBuffer.Contains(allRole) || !wrongRole)
                {
                    m_WrongRolesBuffer.Add(allRole);
                }
            }

            //Pick random role from buffer
            int rndIndex = Random.Range(0, m_WrongRolesBuffer.Count);
            EnumsCollection.RolesFlags role = m_WrongRolesBuffer[rndIndex];
            return role;
        }

        private void RandomizeRoleAndAge(ref bool wrongAge, ref bool wrongRole)
        {
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

        #endregion
    }
}
