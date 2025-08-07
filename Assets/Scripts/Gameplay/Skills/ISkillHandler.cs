public interface ISkillHandler
{
    string SkillName { get; }
    string SkillDescription { get; }
    float CooldownTime { get; }
    void Cast();
}