namespace OAuth2PersonalNotes.Share
{
    public class Constants
    {
        public const string NotesApi = "https://localhost:44307/";
        public const string NotesMvc = "https://localhost:44306/";
        public const string NotesMvcstsCallback = "https://localhost:44306/stscallback";
        public const string NotesAngular = "https://localhost:44316/";

        public const string NotesClientSecret = "myrandomclientsecret";

        public const string NotesIssuerUri = "https://CuongDuong.Personalnotes/identity";
        public const string NotesStsOrigin = "https://localhost:44304";
        public const string NotesSts = NotesStsOrigin + "/identity";
        public const string NotesStsTokenEndpoint = NotesSts + "/connect/token";
        public const string NotesStsAuthorizationEndpoint = NotesSts + "/connect/authorize";
        public const string NotesStsUserInfoEndpoint = NotesSts + "/connect/userinfo";
        public const string NotesStsEndSessionEndpoint = NotesSts + "/connect/endsession";
        public const string NotesStsRevokeTokenEndpoint = NotesSts + "/connect/revocation";

        public const string NotesAdminRole = "PersonalNotesAdmin";
        public const string NotesUserRole = "PersonalNotesUser";
    }
}
