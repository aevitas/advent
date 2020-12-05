using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advent
{
    internal static class Day4
    {
        public static async Task PartOneAsync()
        {
            using var sr = new StreamReader("Day4.txt");
            var passports = (await sr.ReadToEndAsync()).Split($"{Environment.NewLine}{Environment.NewLine}");

            Console.WriteLine(passports.Count(ContainsRequiredFields));
        }

        public static async Task PartTwoAsync()
        {
            using var sr = new StreamReader("Day4.txt");
            var passports = (await sr.ReadToEndAsync()).Split($"{Environment.NewLine}{Environment.NewLine}");

            Console.WriteLine(passports.Count(p =>
            {
                var oneLine = p.Replace(Environment.NewLine, " ");

                return ContainsRequiredFields(oneLine) && IsValid(oneLine);
            }));

            static bool IsValid(string p)
            {
                if (!int.TryParse(GetField("byr"), out var birthYear))
                    return false;

                if (birthYear < 1920 || birthYear > 2002)
                    return false;

                if (!int.TryParse(GetField("iyr"), out var issueYear))
                    return false;

                if (issueYear < 2010 || issueYear > 2020)
                    return false;

                if (!int.TryParse(GetField("eyr"), out var expirationYear))
                    return false;

                if (expirationYear < 2020 || expirationYear > 2030)
                    return false;

                var height = GetField("hgt");
                if (height.EndsWith("in"))
                {
                    var s = height.Substring(0, height.IndexOf("in", StringComparison.Ordinal));
                    var h = int.Parse(s);

                    if (h < 59 || h > 76)
                        return false;
                }

                if (height.EndsWith("cm"))
                {
                    var s = height.Substring(0, height.IndexOf("cm", StringComparison.Ordinal));
                    var h = int.Parse(s);

                    if (h < 150 || h > 193)
                        return false;
                }

                if (!height.EndsWith("in") && !height.EndsWith("cm"))
                    return false;

                var hairColor = GetField("hcl");

                if (!hairColor.StartsWith('#'))
                    return false;

                var hairColorHex = hairColor[1..];
                if (hairColorHex.Length != 6)
                    return false;

                if (!int.TryParse(hairColorHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _))
                    return false;

                var eyeColor = GetField("ecl");

                switch (eyeColor)
                {
                    case "amb":
                    case "blu":
                    case "brn":
                    case "gry":
                    case "grn":
                    case "hzl":
                    case "oth":
                        break;
                    default:
                        return false;
                }

                var passportId = GetField("pid");

                if (!int.TryParse(passportId, out _))
                    return false;

                if (passportId.Length != 9)
                    return false;

                return true;

                string GetField(string fieldName) =>
                    p.Split(' ').FirstOrDefault(s => s.StartsWith(fieldName))?.Split(':')[1] ??
                    throw new ArgumentException($"Could not find field {fieldName}");
            }
        }

        private static bool ContainsRequiredFields(string p)
        {
            return p.Contains("byr:") && p.Contains("iyr:") && p.Contains("eyr:") && p.Contains("hgt:") &&
                   p.Contains("hcl:") && p.Contains("ecl:") && p.Contains("pid:");
        }
    }
}
