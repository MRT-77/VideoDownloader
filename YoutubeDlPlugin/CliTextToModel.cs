using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using VideoDownloader.PluginSchema.Models;

namespace VideoDownloader.YoutubeDlPlugin
{
    internal static class CliTextToModel
    {
        private static int IndexOfNot(this string str, char chr, int start = 0)
        {
            for (int i = start; i < str.Length; i++)
                if (str[i] != chr) return i;
            return -1;
        }

        private static Dictionary<string, string> GetItemsInfo(string qualities)
        {
            var res = new Dictionary<string, string>();

            string[] qa = qualities
                .Replace("    ", "   ")
                .Replace("    ", "   ")
                .Replace("    ", "   ")
                .Split('\n');

            int fi = -1;
            for (int i = qa.Length - 1; i >= 0; i--)
                if (qa[i].Replace(" ", "").Replace("\t", "")
                    .Contains("FormatCodeExtensionResolutionNote".ToLower()))
                {
                    fi = i + 1;
                    break;
                }

            if (fi < 0)
                return res;

            try
            {


                for (int i = fi; i < qa.Length; i++)
                {
                    int si = qa[i].IndexOf(" ", 0, StringComparison.Ordinal);
                    string k = qa[i].Substring(0, si);
                    string v = qa[i].Substring(si, qa[i].Length - si).Trim();
                    res.Add(k, v);
                }

                return res;
            }
            catch { /* ignore */ }

            return res;
        }

        private static string[] GetItemMoreDetail(string line)
        {
            var s = new List<string> { "Normal" };

            string data = line.Trim();
            int ext = data.IndexOf(" ", StringComparison.CurrentCulture);
            s.Add(data.Substring(0, ext));

            int res = data.IndexOfNot(' ', ext);
            bool ao = data.Substring(res).StartsWith("audio only");
            int resE = data.IndexOf(" ", ao ? res + 10 : res, StringComparison.CurrentCulture);
            if (resE == -1)
            {
                s.Add(data.Substring(res));
                s.Add("");
            }
            else
            {
                s.Add(data.Substring(res, resE - res));
                int note = data.IndexOfNot(' ', resE);
                s.Add(data.Substring(note));
            }

            if (s[2].Equals("audio only"))
                s[2] = "Audio";

            if (ao)
                s[0] = "Audio";
            else if (s.Last().Contains("video only"))
                s[0] = "Video Only";

            return s.ToArray();
        }

        public static DownloadListItem[] GetItems(string str)
        {
            return GetItemsInfo(str)
                .Select(item => (item.Key, Value: GetItemMoreDetail(item.Value)))
                .Select(item => new DownloadListItem(item.Key,
                    Parse<DownloadListItemType>(item.Value[0].Replace(" ", "")),
                    item.Value[1], item.Value[2], item.Value[3]))
                .ToArray();
        }

        private static T Parse<T>(string name) where T : Enum
        {
            if (Enum.GetNames(typeof(T)).All(a => a != name))
                return default!;

            return (T)Enum.Parse(typeof(T), name);
        }

        public static string GetFileName(this string fileName)
        {
            const string word = "[download] Destination: ";
            const string word2 = "[download] ";
            const string word2E1 = " has already been downloaded and merged";
            const string word2E2 = " has already been downloaded";
            const string word3 = "[ffmpeg] Merging formats into ";

            if (fileName.StartsWith(word))
                return Path.GetFileName(fileName.Substring(word.Length));

            if (fileName.StartsWith(word2) && fileName.EndsWith(word2E1))
                return Path.GetFileName(
                    fileName.Substring(word2.Length, fileName.Length - word2.Length - word2E1.Length));

            if (fileName.StartsWith(word2) && fileName.EndsWith(word2E2))
                return Path.GetFileName(
                    fileName.Substring(word2.Length, fileName.Length - word2.Length - word2E2.Length));

            if (fileName.StartsWith(word3))
                return Path.GetFileName(fileName.Substring(word3.Length).Replace("\"", ""));

            return "";
        }

        public static float? GetProgressInfo(this string progress)
        {
            const string wWord = "[download] Destination:";
            const string word = "[download]";

            if (!progress.StartsWith(word) || progress.StartsWith(wWord))
                return null;

            int ei = progress.IndexOf("%", StringComparison.CurrentCulture);
            if (ei == -1)
                return null;

            int si = progress.LastIndexOf(" ", ei, StringComparison.CurrentCulture);
            var strPercent = progress.Substring(si, ei - si);

            if (float.TryParse(strPercent, out var percent))
                return percent;

            return null;
        }

        public static TimeSpan? GetProgressTime(this string progress)
        {
            const string sWord = "frame=";
            const string eWord = "x";
            const string tWord = "time";

            if (progress.IsNoE())
                return null;

            string[] array = progress.Split('\n');
            foreach (var s in array)
            {
                string line = s.Trim();
                if (!(line.StartsWith(sWord) && line.EndsWith(eWord))) continue;
                line = line.Replace(' ', '=');
                string[] la = line.Split('=');
                for (int i = 0; i < la.Length - 1; i++)
                {
                    if (!la[i].Equals(tWord, StringComparison.CurrentCultureIgnoreCase)) continue;
                    try
                    {
                        if (TimeSpan.TryParse(la[i + 1], new DateTimeFormatInfo { LongTimePattern = "HH:mm:ss.FF" },
                                out var ts))
                            return ts;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            return null;
        }

        public static string? GetSavedPage(this string str)
        {
            const string sWord = "[generic] Saving request to ";

            if (str.IsNoE())
                return null;

            string[] array = str.Split('\n');
            foreach (var s in array)
            {
                string line = s.Trim();
                if (!line.StartsWith(sWord)) continue;
                return line.Substring(sWord.Length, line.Length - sWord.Length).Trim();
            }

            return null;
        }

        public static TimeSpan? GetDuration(this string duration)
        {
            const string sWord = "Duration: ";
            const string eWord = "kb/s";
            const string tWord = "Duration";

            if (duration.IsNoE())
                return null;

            string[] array = duration.Split('\n');
            foreach (var s in array)
            {
                string line = s.Trim();
                if (!(line.StartsWith(sWord) && line.EndsWith(eWord))) continue;
                line = line.Replace(": ", ",");
                string[] la = line.Split(',');
                for (int i = 0; i < la.Length - 1; i++)
                {
                    if (!la[i].Equals(tWord, StringComparison.CurrentCultureIgnoreCase)) continue;
                    try
                    {
                        if (TimeSpan.TryParse(la[i + 1], new DateTimeFormatInfo { LongTimePattern = "HH:mm:ss.FF" }, out var ts))
                            return ts;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            return null;
        }

        public static bool IsNoE(this string str)
        {
            if (string.IsNullOrEmpty(str)) return true;
            return string.IsNullOrEmpty(str.Trim());
        }
    }
}
