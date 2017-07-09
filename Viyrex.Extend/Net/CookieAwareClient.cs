namespace System.Net
{
    public class CookieAwareClient : WebClient
    {
        public CookieAwareClient() : this(null)
        {
        }

        public CookieAwareClient(CookieContainer cookieContainer) : base()
            => this.CookieContainer = cookieContainer ?? new CookieContainer();

        public readonly CookieContainer CookieContainer;

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest castRequest)
                castRequest.CookieContainer = this.CookieContainer;
            return request;
        }
    }
}
