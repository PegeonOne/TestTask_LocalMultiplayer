using Game.App;

namespace Game.Core
{
    public abstract class BaseController : BaseBehavior
    {
        public virtual void Init(AppController app) { }
        public virtual void Init(AppConfig config) { }
    }
}
