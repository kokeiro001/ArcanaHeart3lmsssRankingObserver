using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArcanaHeart3lmsssRankingObserver.Core
{
    public enum CharacterId
    {
        Minori,
        Heart,
        Saki,
        Maori,
        Weiss,
        Scharlachrot,
        Petra,
        Elsa,
        Clarice,
        Eko,
        Zenia,
        Lieselote,
        Dorothy,
        Kamui,
        Konoha,
        Nazuna,
        Akane,
        Meifang,
        Kira,
        Catherine,
        Angelia,
        Fiona,
        Yoriko,
        Lilica
    }

    static class CharacterRankingCsvUrl
    {
        // CharacterId.Name.ToString().ToLower()で同等の取れるけど、Dictionaryに格納する。
        static public IReadOnlyDictionary<CharacterId, Uri> Dictionary { get; } = new Dictionary<CharacterId, Uri>
        {
            { CharacterId.Minori, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_minori.csv") },
            { CharacterId.Heart, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_heart.csv") },
            { CharacterId.Saki, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_saki.csv") },
            { CharacterId.Maori, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_maori.csv") },
            { CharacterId.Weiss, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_weiss.csv") },
            { CharacterId.Scharlachrot, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_scharlachrot.csv") },
            { CharacterId.Petra, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_petra.csv") },
            { CharacterId.Elsa, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_elsa.csv") },
            { CharacterId.Clarice, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_clarice.csv") },
            { CharacterId.Eko, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_eko.csv") },
            { CharacterId.Zenia, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_zenia.csv") },
            { CharacterId.Lieselote, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_lieselote.csv") },
            { CharacterId.Dorothy, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_dorothy.csv") },
            { CharacterId.Kamui, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_kamui.csv") },
            { CharacterId.Konoha, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_konoha.csv") },
            { CharacterId.Nazuna, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_nazuna.csv") },
            { CharacterId.Akane, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_akane.csv") },
            { CharacterId.Meifang, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_meifang.csv") },
            { CharacterId.Kira, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_kira.csv") },
            { CharacterId.Catherine, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_catherine.csv") },
            { CharacterId.Angelia, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_angelia.csv") },
            { CharacterId.Fiona, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_fiona.csv") },
            { CharacterId.Yoriko, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_yoriko.csv") },
            { CharacterId.Lilica, new Uri(@"http://www.examu.co.jp/arcanaheart3lm_sss/ranking/ranking_lilica.csv") },
        };
    }

    public class ArcanaHeart3lmsssRankingClient
    {
        HttpClient httpClient = new HttpClient();

        public async Task<string> GetCsvAsync(CharacterId characterId)
        {
            var uri = CharacterRankingCsvUrl.Dictionary[characterId];
            return await httpClient.GetStringAsync(uri);
        }

        public async Task<Stream> DownloadCsvAsync(CharacterId characterId)
        {
            var uri = CharacterRankingCsvUrl.Dictionary[characterId];
            return await httpClient.GetStreamAsync(uri);
        }
    }
}
