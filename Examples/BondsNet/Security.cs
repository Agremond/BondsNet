using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Security
{
    public Security(string sec_code, int rank, double GoalACY_)
    {
        switch (rank)
        {
            case 0:
                Rank = Rank.AAA;
                break;
            case 1:
                Rank = Rank.AA;
                break;
            case 2:
                Rank = Rank.A;
                break;
            case 3:
                Rank = Rank.BBB;
                break;
            case 4:
                Rank = Rank.BB;
                break;
            case 5:
                Rank = Rank.B;
                break;
            case 6:
                Rank = Rank.CCC;
                break;
            case 7:
                Rank = Rank.CC;
                break;
            case 8:
                Rank = Rank.C;
                break;
            case 9:
                Rank = Rank.RD;
                break;
            case 10:
                Rank = Rank.SD;
                break;
            case 11:
                Rank = Rank.D;
                break;
            default:
                Rank = Rank.NONE;
                break;


        }
        SecCode = sec_code;
        goalACY = GoalACY_;

        ClassCode = "";


    }

    /// <summary>
    /// ISIN code
    /// </summary>
    public string SecCode;
    /// <summary>
    /// Goal ACY
    /// </summary>
    public double goalACY;
    /// <summary>
    /// Security class code
    /// </summary>
    public string ClassCode;
    /// <summary>
    /// Рейтинг инструмента
    /// </summary>
    public Rank Rank;
}
