using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

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

    public class RankingCsvViewModel
    {
        public int record_id { get; set; }
        public int rank { get; set; }
        public int rank2 { get; set; }
        public long card_id { get; set; }
        public string player_name { get; set; }
        public int pcol1 { get; set; }
        public string pcol1_name { get; set; }
        public int score_ui1 { get; set; }
        public int score_ui5 { get; set; }
        public int last_play_tenpo_id { get; set; }
        public string tenpo_name { get; set; }
        public int pref_id { get; set; }
        public string pref { get; set; }
        public int area_id { get; set; }
        public string area { get; set; }
        public DateTime start_date { get; set; }
    }

    public sealed class RankingCsvViewModelMap : CsvClassMap<RankingCsvViewModel>
    {
        public RankingCsvViewModelMap()
        {
            Map(m => m.record_id).Index(0).Default(0);
            Map(m => m.rank).Index(1).Default(0);
            Map(m => m.rank2).Index(2).Default(0);
            Map(m => m.card_id).Index(3).Default(0);
            Map(m => m.player_name).Index(4).Default("");
            Map(m => m.pcol1).Index(5).Default(0);
            Map(m => m.pcol1_name).Index(6).Default("");
            Map(m => m.score_ui1).Index(7).Default(0);
            Map(m => m.score_ui5).Index(8).Default(0);
            Map(m => m.last_play_tenpo_id).Index(9).Default(-1);
            Map(m => m.tenpo_name).Index(10).Default("");
            Map(m => m.pref_id).Index(11).Default(-1);
            Map(m => m.pref).Index(12).Default("");
            Map(m => m.area_id).Index(13).Default(-1);
            Map(m => m.area).Index(14).Default("");
            Map(m => m.start_date).Index(15);
        }
    }

    public class RankingEntity : TableEntity
    {
        public int record_id { get; set; }
        public int rank { get; set; }
        public int rank2 { get; set; }
        public long card_id { get; set; }
        public string player_name { get; set; }
        public int pcol1 { get; set; }
        public string pcol1_name { get; set; }
        public int score_ui1 { get; set; }
        public int score_ui5 { get; set; }
        public int last_play_tenpo_id { get; set; }
        public string tenpo_name { get; set; }
        public int pref_id { get; set; }
        public string pref { get; set; }
        public int area_id { get; set; }
        public string area { get; set; }
        public DateTime start_date { get; set; }
    }

    public static class RankingConverter
    {
        public static RankingEntity ToEntity(this RankingCsvViewModel model)
        {
            return new RankingEntity
            {
                record_id = model.record_id,
                rank = model.rank,
                rank2 = model.rank2,
                card_id = model.card_id,
                player_name = model.player_name,
                pcol1 = model.pcol1,
                pcol1_name = model.pcol1_name,
                score_ui1 = model.score_ui1,
                score_ui5 = model.score_ui5,
                last_play_tenpo_id = model.last_play_tenpo_id,
                tenpo_name = model.tenpo_name,
                pref_id = model.pref_id,
                pref = model.pref,
                area_id = model.area_id,
                area = model.area,
                start_date = model.start_date,
            };
        }
    }
}
