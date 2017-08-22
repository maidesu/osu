﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using Newtonsoft.Json;

namespace osu.Game.Beatmaps
{
    /// <summary>
    /// Beatmap set info retrieved for previewing locally without having the set downloaded.
    /// </summary>
    public class BeatmapSetOnlineInfo
    {
        /// <summary>
        /// The different sizes of cover art for this beatmap set.
        /// </summary>
        [JsonProperty(@"covers")]
        public BeatmapSetOnlineCovers Covers { get; set; }

        /// <summary>
        /// A small sample clip of this beatmap set's song.
        /// </summary>
        [JsonProperty(@"previewUrl")]
        public string Preview { get; set; }

        /// <summary>
        /// The amount of plays this beatmap set has.
        /// </summary>
        [JsonProperty(@"play_count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// The amount of people who have favourited this beatmap set.
        /// </summary>
        [JsonProperty(@"favourite_count")]
        public int FavouriteCount { get; set; }
    }

    public class BeatmapSetOnlineCovers
    {
        public string CoverLowRes { get; set; }

        [JsonProperty(@"cover@2x")]
        public string Cover { get; set; }

        public string CardLowRes { get; set; }

        [JsonProperty(@"card@2x")]
        public string Card { get; set; }

        public string ListLowRes { get; set; }

        [JsonProperty(@"list@2x")]
        public string List { get; set; }
    }
}
