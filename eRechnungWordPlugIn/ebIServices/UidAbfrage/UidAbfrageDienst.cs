using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using ebIServices.at.gv.bmf.finanzonline;
using ebIServices.at.gv.bmf.finanzonline1;

namespace ebIServices.UidAbfrage
{
    public class UidAbfrageDienst : IUidAbfrageDienst
    {
        private string _stufe = "2";

        private string _teilNehmerId;
        private string _benutzerId;
        private string _name;
        public string Name { get { return _name; } }
        public string[] Adrz { get; private set; }
        private string _sessionId;
        public bool IsCorrect { get; private set; }
        private string _message = "";    
        public string Message { get { return _message; } }

        private SessionWSIService _sessionWsi;
        private uidAbfrageService _uidAbfrage;

        public bool Login(string pin, string teilNehmerId, string benutzerId)
        {
            _teilNehmerId = teilNehmerId;
            _benutzerId = benutzerId;
            if (string.IsNullOrEmpty(teilNehmerId) || string.IsNullOrEmpty(benutzerId) || string.IsNullOrEmpty(pin))
            {
                IsCorrect = false;
                return false;
            }
            _sessionWsi = new SessionWSIService();
            try
            {
                _sessionId = _sessionWsi.Login(teilNehmerId, benutzerId, pin);
            }
            catch (Exception e)
            {
                _message = e.Message;
                return false;
            }
            IsCorrect = true;
            return true;
        }

        public void Logout()
        {
            if (_sessionWsi != null)
            {
                _sessionWsi.Logout(_sessionId, _teilNehmerId, _benutzerId);
                
            }
        }

        public bool UidAbfrage(string uid2Verify, string billerUid)
        {
            _uidAbfrage = new uidAbfrageService();
            Adrz = new string[6];

            var erg = _uidAbfrage.uidAbfrage(_sessionId,
                                             _teilNehmerId,
                                             _benutzerId,
                                             billerUid,
                                             uid2Verify,
                                             _stufe,
                                             out _name,
                                             out Adrz[0],
                                             out Adrz[1],out Adrz[2],out Adrz[3],out Adrz[4],out Adrz[5]);

            _message = UidErrorCodes.ErrorText(erg);
            IsCorrect = (erg=="0");
            return IsCorrect;
        }
    }
}