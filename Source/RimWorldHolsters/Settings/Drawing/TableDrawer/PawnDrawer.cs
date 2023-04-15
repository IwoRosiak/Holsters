using Holsters.Settings;
using RimWorld;
using SettingsDrawer.Sections;
using UnityEngine;
using Verse;

namespace Holsters.Utility.ModSettings.Settings_Drawing.TableDrawer
{
    internal class PawnDrawer : Operation
    {
        private BodyType _body = BodyType.male;
        private Rot4 _rotation;

        public PawnDrawer(Rect area, Rot4 rotation, BodyType body) : base(area)
        {
            _rotation = rotation;
            _body = body;
        }

        public override void ExecuteOperation()
        {
            DrawBody(area);

            DrawHead(area);
        }

        private void DrawBody(Rect rect)
        {
            Rect bodyRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            var texture = ChooseBodyTexture();

            Widgets.DrawTextureRotated(rect.center, texture, 0);
        }

        private  void DrawHead(Rect rect)
        {
            Rect headRect = new Rect(rect.x + (0.2f * rect.width), rect.y + (0.3f * rect.height), 0.6f * rect.width, 0.6f * rect.width);

            var texture = ChooseHeadTexture();

            float offset = 0;

            if (_rotation == Rot4.East)
            {
                offset = -ChooseHeadOffset();
            }
            else if (_rotation == Rot4.West)
            {
                offset = ChooseHeadOffset();
            }

            Widgets.DrawTextureRotated(rect.center - new Vector2(offset * ModSettingsUtilities.PixelRatio, 34), texture, 0);
        }


        private float ChooseHeadOffset()
        {
            switch (_body)
            {
                default:
                case BodyType.hulk:
                    return BodyTypeDefOf.Hulk.headOffset.x;
                    break;

                case BodyType.fat:
                    return BodyTypeDefOf.Fat.headOffset.x;
                    break;

                case BodyType.male:
                    return BodyTypeDefOf.Male.headOffset.x;
                    break;

                case BodyType.thin:
                    return BodyTypeDefOf.Thin.headOffset.x;
                    break;

                case BodyType.female:
                    return BodyTypeDefOf.Female.headOffset.x;
                    break;
            }
        }

        private Texture ChooseBodyTexture()
        {
            return IR_Textures.Bodies[_body][_rotation];
        }

        private Texture ChooseHeadTexture()
        {
            switch (_body)
            {
                default:
                case BodyType.hulk:
                case BodyType.fat:
                case BodyType.male:
                    return IR_Textures.MaleHead[_rotation];
                    break;

                case BodyType.thin:
                case BodyType.female:
                    return IR_Textures.FemaleHead[_rotation];
                    break;
            }
        }
    }
}
