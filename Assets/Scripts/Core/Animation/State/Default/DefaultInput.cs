namespace Core.Animation.State.Default
{
    public readonly struct DefaultInput : IAnimationInput
    {
        public bool isAttack { get; }
        public bool isDead { get; }
        public float speed { get; }

        public DefaultInput(bool isAttack, bool isDead, float speed)
        {
            this.isAttack = isAttack;
            this.isDead = isDead;
            this.speed = speed;
        }
    }
}