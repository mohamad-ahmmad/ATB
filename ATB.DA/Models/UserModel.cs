using ATB.DA.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATB.DA.Models
{
    public record UserModel
        (
        ulong UserId,
        string FName,
        string MName,
        string LName,
        string Email,
        string Password,
        AccessLevelEnum AccessLevel
        )
    {

        public static UserModel FromCSV(string CSVData)
        {
            string[] fields = CSVData.Split(",");
            return new UserModel(
                ulong.Parse(fields[0]),
                fields[1],
                fields[2],
                fields[3],
                fields[4],
                fields[5],
                (AccessLevelEnum)int.Parse(fields[6])
                );
        }
        public string ToCSV() => $"{UserId},{FName},{MName},{LName},{Email},{Password},{(int)AccessLevel}";
    };
    
    

}
