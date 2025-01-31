// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Localisation;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Objects;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Osu.Skinning.Default;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Osu.Mods
{
    public class OsuModSliderIndicator : Mod, IApplicableToBeatmap, IApplicableToDrawableHitObject
    {
        public override string Name => "Slider Indicator";
        public override string Acronym => "SI";
        public override ModType Type => ModType.Fun;
        public override LocalisableString Description => "Never get slow-slidered again.";
        public override double ScoreMultiplier => 1;
        public override bool Ranked => true;

        [SettingSource("Base brightness", "The lightness of the slowest sliders", SettingControlType = typeof(MultiplierSettingsSlider))]
        public BindableNumber<double> BaseLightness { get; } = new BindableDouble(0.2)
        {
            MinValue = 0,
            MaxValue = 1,
            Precision = 0.01,
        };

        public override Type[] IncompatibleMods => [];

        private (double Min, double Max, double Difference) sliderVelocityRange;

        public (double Min, double Max, double Difference) SliderVelocityRange => sliderVelocityRange;

        public void ApplyToBeatmap(IBeatmap beatmap)
        {
            double[] sliderVelocities = beatmap.HitObjects.OfType<Slider>().Select(sv => sv.Velocity).Order().ToArray();

            sliderVelocityRange = (sliderVelocities.First(), sliderVelocities.Last(), sliderVelocities.Last() - sliderVelocities.First());
        }

        public void ApplyToDrawableHitObject(DrawableHitObject drawable)
        {
            if (drawable is DrawableSlider slider)
            {
                slider.HitObjectApplied += _ =>
                {
                    if (SliderVelocityRange.Difference <= 0)
                        return;

                    Color4 baseBorder = ((PlaySliderBody)slider.Body.Drawable).BorderColour;

                    Vector4 hsl = Color4.ToHsl(baseBorder);

                    float lightnessFactor = (float)((slider.HitObject.Velocity - SliderVelocityRange.Min) / sliderVelocityRange.Difference);

                    hsl.Z = (float)BaseLightness.Value + (1.0f - (float)BaseLightness.Value) * lightnessFactor;

                    ((PlaySliderBody)slider.Body.Drawable).BorderColour = Color4.FromHsl(hsl);
                };
            }
        }
    }
}
