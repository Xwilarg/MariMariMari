namespace TouhouPride.Player
{
	public class FollowerController : PlayerController
	{
        protected override void Start()
        {
            base.Start();
            GetComponent<Follower>().SetInfo(true);
        }
    }
}