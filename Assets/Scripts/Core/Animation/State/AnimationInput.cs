namespace Core.Animation.State
{
    public readonly struct AnimationInput
    {
        public bool isAttack { get; }
        public bool isDead { get; }
        public float speed { get; }

        public AnimationInput(bool isAttack, bool isDead, float speed)
        {
            this.isAttack = isAttack;
            this.isDead = isDead;
            this.speed = speed;
        }
    }
}