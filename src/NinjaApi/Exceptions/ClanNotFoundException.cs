using NinjaApi.Models;

namespace NinjaApi
{
    public class ClanNotFoundException : NinjaApiException
    {
        public ClanNotFoundException(Clan clan)
            : this(clan.Name)
        {
        }

        public ClanNotFoundException(string clanName)
            : base($"Clan {clanName} was not found.")
        {
        }
    }
}
