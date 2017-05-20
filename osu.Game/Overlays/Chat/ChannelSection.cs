﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.Collections.Generic;
using System.Linq;
using OpenTK;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Online.Chat;

namespace osu.Game.Overlays.Chat
{
    public class ChannelSection : Container, IHasFilterableChildren
    {
        private readonly FillFlowContainer<ChannelListItem> items;
        private readonly OsuSpriteText header;

        public IEnumerable<IFilterable> FilterableChildren => items.Children.OfType<IFilterable>();
        public string[] FilterTerms => new[] { Header };
        public bool MatchingCurrentFilter
        {
        	set
            {
                FadeTo(value ? 1f : 0f, 100);
            }
        }

        public string Header
        {
            get { return header.Text; }
            set { header.Text = value; }
        }

        public IEnumerable<Channel> Channels
        {
            set { items.Children = value.Select(c => new ChannelListItem { Channel = c }); }
        }

        public ChannelSection()
        {
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                header = new OsuSpriteText
                {
                    TextSize = 15,
                    Font = @"Exo2.0-Bold",
                },
                items = new FillFlowContainer<ChannelListItem>
                {
                    RelativeSizeAxes = Axes.X,
                    AutoSizeAxes = Axes.Y,
                    Margin = new MarginPadding { Top = 25 },
                    Spacing = new Vector2(0f, 5f),
                },
            };
        }
    }
}
