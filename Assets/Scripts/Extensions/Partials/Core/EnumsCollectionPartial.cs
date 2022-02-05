using System.Collections.Generic;

namespace CoreFramework
{
    public static partial class EnumsCollection
    {
        [System.Flags]
        public enum Roles
        {
            Medic = (1 << 0),
            Military = (1 << 1),
            Student = (1 << 2),
            Uneployed = (1 << 3),
            Teacher = (1 << 4),
        }

        public enum Cures
        {
            Cure1,
            Cure2
        }

        public enum Statuses
        {
            Infected,
            Healthy,
            Vaccinated
        }

        public static void RetrieveRoles(EnumsCollection.Roles roles, out List<Roles> buffer)
        {
            buffer = new List<Roles>();
            RetrieveRolesToBuffer(roles, ref buffer);
        }

        public static void RetrieveRolesToBuffer(Roles roles, ref List<Roles> buffer)
        {
            buffer.Clear();
             
            ulong flag = 1;
            foreach (Roles role in System.Enum.GetValues(typeof(Roles)))
            {
                ulong bits = System.Convert.ToUInt64(role);

                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && roles.HasFlag(role))
                {
                    buffer.Add(role);
                }
            }
        }
    }
}