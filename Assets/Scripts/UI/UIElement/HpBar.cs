using Unity.Properties;
using UnityEngine.UIElements;

namespace UI.UIElement
{
    [UxmlElement]
    public partial class HpBar : VisualElement
    {
        private readonly VisualElement _hpBarContainer;
        private readonly VisualElement _hpBarSlot;
        private readonly VisualElement _hpBarMask;
        private readonly VisualElement _hpBarFill;
        private readonly VisualElement _hpBarHeart;
        
        private readonly string _classHeartFull = "hp-heart-full";
        private readonly string _classHeartHalf = "hp-heart-half";
        private readonly string _classHeartEmpty = "hp-heart-empty";

        private int _health = 100;
        
        
        [CreateProperty,UxmlAttribute, UnityEngine.Range(0, 100)]
        public int Health
        {
            get => _health;
            set
            {
                if (value == _health)
                    return;
                _health = value;
                _hpBarMask.style.width = new StyleLength(new Length(_health, LengthUnit.Percent));
                if (value <= 0)
                {
                    _hpBarHeart.RemoveFromClassList(_classHeartFull);
                    _hpBarHeart.RemoveFromClassList(_classHeartHalf);
                    _hpBarHeart.AddToClassList(_classHeartEmpty);
                }
                else if (value < 50)
                {
                    _hpBarHeart.RemoveFromClassList(_classHeartFull);
                    _hpBarHeart.RemoveFromClassList(_classHeartEmpty);
                    _hpBarHeart.AddToClassList(_classHeartHalf);
                }
                else
                {
                    _hpBarHeart.RemoveFromClassList(_classHeartHalf);
                    _hpBarHeart.RemoveFromClassList(_classHeartEmpty);
                    _hpBarHeart.AddToClassList(_classHeartFull);
                }
            }
        }

        public HpBar()
        {
            _hpBarContainer = new VisualElement();
            _hpBarContainer.AddToClassList("hp-container");
            _hpBarContainer.name = "hp-bar-container";

            _hpBarSlot = new VisualElement();
            _hpBarSlot.AddToClassList("hp-slot");
            _hpBarSlot.name = "hp-bar-slot";

            _hpBarMask = new VisualElement();
            _hpBarMask.AddToClassList("hp-mask");
            _hpBarMask.name = "hp-bar-mask";

            _hpBarFill = new VisualElement();
            _hpBarFill.AddToClassList("hp-fill");
            _hpBarFill.name = "hp-bar-fill";

            _hpBarHeart = new VisualElement();
            _hpBarHeart.AddToClassList(_classHeartFull);
            _hpBarHeart.name = "hp-heart";

            _hpBarContainer.Add(_hpBarSlot);
            _hpBarContainer.Add(_hpBarHeart);

            _hpBarSlot.Add(_hpBarMask);

            _hpBarMask.Add(_hpBarFill);
            Add(_hpBarContainer);
        }
    }
}