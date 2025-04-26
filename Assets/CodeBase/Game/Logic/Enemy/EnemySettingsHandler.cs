namespace Game.Logic.Enemy
{
    public class EnemySettingsHandler
    {
        private readonly EnemyMoveHandler.EnemySettings _moveSettings;
        private readonly EnemyDamageHandler.EnemySettings _damageSettings;
        private readonly EnemyWeaponHandler.Settings _weaponSettings;

        public EnemyMoveHandler.EnemySettings MoveSettings => _moveSettings;
        public EnemyDamageHandler.EnemySettings DamageSettings => _damageSettings;
        public EnemyWeaponHandler.Settings WeaponSettings => _weaponSettings;

        public EnemySettingsHandler(EnemyMoveHandler.EnemySettings moveSettings,
            EnemyDamageHandler.EnemySettings damageSettings,
            EnemyWeaponHandler.Settings weaponSettings)
        {
            _moveSettings = new(moveSettings);
            _damageSettings = new(damageSettings);
            _weaponSettings = new(weaponSettings);
        }
    }
}