using Solar.Service.Models;
using Vidyano.Service.Repository;

namespace Solar.Service
{
    public class FusionSolarCredentialActions : DefaultPersistentObjectActions<SolarContext, FusionSolarCredential>
    {
        private readonly FusionSolarApi api;

        /// <inheritdoc />
        public FusionSolarCredentialActions(SolarContext context, FusionSolarApi api)
            : base(context)
        {
            this.api = api;
        }

        public override void OnLoad(PersistentObject obj, PersistentObject? parent)
        {
            base.OnLoad(obj, parent);

            obj[nameof(FusionSolarCredential.Password)].SetOriginalValue("*");
        }

        public override void OnSave(PersistentObject obj)
        {
            if (!CheckRules(obj))
                return;

            var userNameAttr = obj[nameof(FusionSolarCredential.UserName)];
            var passwordAttr = obj[nameof(FusionSolarCredential.Password)];
            if (userNameAttr.IsValueChanged || passwordAttr.IsValueChanged)
            {
                var userName = (string)userNameAttr!;
                var password = (string)passwordAttr!;

                var token = api.Login(userName, password).GetAwaiter().GetResult();
                // TODO: Store token immediately?
            }

            base.OnSave(obj);
        }
    }
}