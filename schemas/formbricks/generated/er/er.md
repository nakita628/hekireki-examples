```mermaid
erDiagram
    Workspace ||--o{ Creative : "(id) - (workspaceId)"
    User |o--o{ Creative : "(id) - (createdBy)"
    Survey |o--o{ Creative : "(id) - (surveyId)"
    Creative ||--o{ CreativeDailyStat : "(id) - (creativeId)"
    Creative ||--o{ CreativeDailyPageStat : "(id) - (creativeId)"
    Creative ||--o{ CreativeTrigger : "(id) - (creativeId)"
    ActionClass ||--o{ CreativeTrigger : "(id) - (actionClassId)"
    Language ||--o{ CreativeLanguage : "(id) - (languageId)"
    Creative ||--o{ CreativeLanguage : "(id) - (creativeId)"
    Workspace ||--o{ Webhook : "(id) - (workspaceId)"
    ContactAttributeKey ||--o{ ContactAttribute : "(id) - (attributeKeyId)"
    Contact ||--o{ ContactAttribute : "(id) - (contactId)"
    Workspace ||--o{ ContactAttributeKey : "(id) - (workspaceId)"
    Workspace ||--o{ Contact : "(id) - (workspaceId)"
    Survey ||--o{ Response : "(id) - (surveyId)"
    Contact |o--o{ Response : "(id) - (contactId)"
    Display |o--o| Response : "(id) - (displayId)"
    Workspace ||--o{ Tag : "(id) - (workspaceId)"
    Response ||--o{ TagsOnResponses : "(id) - (responseId)"
    Tag ||--o{ TagsOnResponses : "(id) - (tagId)"
    Survey ||--o{ Display : "(id) - (surveyId)"
    Contact |o--o{ Display : "(id) - (contactId)"
    Survey ||--o{ SurveyTrigger : "(id) - (surveyId)"
    ActionClass ||--o{ SurveyTrigger : "(id) - (actionClassId)"
    ContactAttributeKey ||--o{ SurveyAttributeFilter : "(id) - (attributeKeyId)"
    Survey ||--o{ SurveyAttributeFilter : "(id) - (surveyId)"
    Workspace ||--o{ Survey : "(id) - (workspaceId)"
    User |o--o{ Survey : "(id) - (createdBy)"
    User |o--o{ Survey : "(id) - (ownerId)"
    Segment |o--o{ Survey : "(id) - (segmentId)"
    Survey ||--o{ SurveyQuota : "(id) - (surveyId)"
    Response ||--o{ ResponseQuotaLink : "(id) - (responseId)"
    SurveyQuota ||--o{ ResponseQuotaLink : "(id) - (quotaId)"
    Survey ||--o{ SurveyFollowUp : "(id) - (surveyId)"
    Workspace ||--o{ ActionClass : "(id) - (workspaceId)"
    Workspace ||--o{ Integration : "(id) - (workspaceId)"
    Organization ||--o{ Workspace : "(id) - (organizationId)"
    Organization ||--o| OrganizationBilling : "(id) - (organizationId)"
    Organization ||--o{ Membership : "(id) - (organizationId)"
    User ||--o{ Membership : "(id) - (userId)"
    Organization ||--o{ Invite : "(id) - (organizationId)"
    User ||--o{ Invite : "(id) - (creatorId)"
    User |o--o{ Invite : "(id) - (acceptorId)"
    Organization ||--o{ ApiKey : "(id) - (organizationId)"
    ApiKey ||--o{ ApiKeyWorkspace : "(id) - (apiKeyId)"
    Workspace ||--o{ ApiKeyWorkspace : "(id) - (workspaceId)"
    User |o--o{ Account : "(id) - (userId)"
    User ||--o{ Session : "(id) - (userId)"
    User ||--o| TwoFactor : "(id) - (userId)"
    User |o--o{ oauthClient : "(id) - (userId)"
    oauthClient ||--o{ oauthAccessToken : "(clientId) - (clientId)"
    Session |o--o{ oauthAccessToken : "(id) - (sessionId)"
    User |o--o{ oauthAccessToken : "(id) - (userId)"
    oauthRefreshToken |o--o{ oauthAccessToken : "(id) - (refreshId)"
    oauthClient ||--o{ oauthRefreshToken : "(clientId) - (clientId)"
    Session |o--o{ oauthRefreshToken : "(id) - (sessionId)"
    User ||--o{ oauthRefreshToken : "(id) - (userId)"
    oauthClient ||--o{ oauthConsent : "(clientId) - (clientId)"
    User |o--o{ oauthConsent : "(id) - (userId)"
    oauthClient ||--o{ oauthClientResource : "(clientId) - (clientId)"
    oauthResource ||--o{ oauthClientResource : "(identifier) - (resourceId)"
    User ||--o| PasswordResetToken : "(id) - (userId)"
    Workspace ||--o{ Segment : "(id) - (workspaceId)"
    Workspace ||--o{ Language : "(id) - (workspaceId)"
    Language ||--o{ SurveyLanguage : "(id) - (languageId)"
    Survey ||--o{ SurveyLanguage : "(id) - (surveyId)"
    Survey |o--o{ EmbeddedData : "(id) - (surveyId)"
    Workspace ||--o{ EmbeddedData : "(id) - (workspaceId)"
    Survey ||--o{ SurveyEmbeddedData : "(id) - (surveyId)"
    EmbeddedData ||--o{ SurveyEmbeddedData : "(id) - (embeddedDataId)"
    Organization ||--o{ Team : "(id) - (organizationId)"
    Team ||--o{ TeamUser : "(id) - (teamId)"
    User ||--o{ TeamUser : "(id) - (userId)"
    Workspace ||--o{ WorkspaceTeam : "(id) - (workspaceId)"
    Team ||--o{ WorkspaceTeam : "(id) - (teamId)"
    Workspace ||--o{ Chart : "(id) - (workspaceId)"
    User |o--o{ Chart : "(id) - (createdBy)"
    FeedbackDirectory ||--o{ Chart : "(id) - (feedbackDirectoryId)"
    Workspace ||--o{ Dashboard : "(id) - (workspaceId)"
    User |o--o{ Dashboard : "(id) - (createdBy)"
    Dashboard ||--o{ DashboardWidget : "(id) - (dashboardId)"
    Chart ||--o{ DashboardWidget : "(id) - (chartId)"
    Survey ||--o{ AttachmentExport : "(id) - (surveyId)"
    Workspace ||--o{ AttachmentExport : "(id) - (workspaceId)"
    User |o--o{ AttachmentExport : "(id) - (requestedById)"
    Workspace ||--o{ FeedbackSource : "(id) - (workspaceId)"
    FeedbackDirectory ||--o{ FeedbackSource : "(id) - (feedbackDirectoryId)"
    User |o--o{ FeedbackSource : "(id) - (createdBy)"
    FeedbackDirectoryWorkspace ||--o{ FeedbackSource : "(feedbackDirectoryId) - (feedbackDirectoryId)"
    FeedbackSource ||--o{ FeedbackSourceFormbricksMapping : "(id) - (feedbackSourceId)"
    Survey ||--o{ FeedbackSourceFormbricksMapping : "(id) - (surveyId)"
    FeedbackSource ||--o{ FeedbackSourceFieldMapping : "(id) - (feedbackSourceId)"
    Organization ||--o{ FeedbackDirectory : "(id) - (organizationId)"
    FeedbackDirectory ||--o{ FeedbackDirectoryWorkspace : "(id) - (feedbackDirectoryId)"
    Workspace ||--o{ FeedbackDirectoryWorkspace : "(id) - (workspaceId)"
    Organization ||--o{ RetentionPolicy : "(id) - (organizationId)"
    User |o--o{ RetentionPolicy : "(id) - (updatedById)"
    Organization ||--o{ RetentionExemption : "(id) - (organizationId)"
    Survey |o--o{ RetentionExemption : "(id) - (surveyId)"
    User |o--o{ RetentionExemption : "(id) - (createdById)"
    User |o--o{ RetentionExemption : "(id) - (revokedById)"
    Organization ||--o{ RetentionRun : "(id) - (organizationId)"
    RetentionRun ||--o{ RetentionRunItem : "(id) - (runId)"
    Organization ||--o{ RetentionNotice : "(id) - (organizationId)"
    Survey |o--o{ RetentionNotice : "(id) - (surveyId)"
    User |o--o{ RetentionNotice : "(id) - (userId)"
    Survey ||--o| RetentionSurveyRestore : "(id) - (surveyId)"
    Workspace ||--o{ Workflow : "(id) - (workspaceId)"
    User |o--o{ Workflow : "(id) - (createdBy)"
    Workflow ||--o{ WorkflowVersion : "(id) - (workflowId)"
    User |o--o{ WorkflowVersion : "(id) - (publishedBy)"
    Workflow ||--o{ WorkflowRun : "(id) - (workflowId)"
    Workspace ||--o{ WorkflowRun : "(id) - (workspaceId)"
    WorkflowVersion |o--o{ WorkflowRun : "(id) - (workflowVersionId)"
    Response |o--o{ WorkflowRun : "(id) - (responseId)"
    WorkflowRun ||--o{ WorkflowRunLog : "(id) - (runId)"
    Creative {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        creativetype type
        creativestatus status
        datetime publishedAt
        datetime archivedAt
        string workspaceId FK
        string createdBy FK
        json content
        json styling
        string surveyId FK
        string linkUrl
        creativeshowon showOn
        json showConditions
        float displayPercentage
        datetime startAt
        datetime endAt
        boolean autoSelectLanguage
    }
    CreativeDailyStat {
        string creativeId PK, FK
        datetime date PK "UTC day."
        creativedevicetype deviceType PK
        int impressions
        int clicks
    }
    CreativeDailyPageStat {
        string creativeId PK, FK
        datetime date PK "UTC day."
        string page PK "Host + path, without query string or hash, or `(other)`."
        int impressions
        int clicks
    }
    CreativeTrigger {
        string id PK
        datetime createdAt
        datetime updatedAt
        string creativeId FK, UK
        string actionClassId FK, UK
    }
    CreativeLanguage {
        string languageId PK, FK
        string creativeId PK, FK
        boolean default
        boolean enabled
    }
    OutboundEmailAudit {
        string id PK
        datetime createdAt
        datetime updatedAt
        datetime expiresAt
        datetime contentExpiresAt
        string environment
        string emailType
        string trigger
        string actorKind
        string actorUserId
        string configuredByUserId
        string actorUserHash
        string configuredByUserHash
        string organizationId
        string surveyId
        string responseId
        string followUpId
        string workflowId
        string workflowRunId
        string inviteId
        string recipientMac
        string sesMessageHash
        string snapshotKey
        string snapshotSha256
        string status
        datetime statusRecordedAt
        boolean auditGap
        datetime erasureRequestedAt
        datetime erasedAt
        datetime contentDeleteRetryAt
        int contentDeleteFailures
    }
    OutboundEmailAuditErasureIntent {
        string id PK
        string userId
        string userHash
        string recipientMac
        datetime createdAt
        datetime activatedAt
        datetime checkedAt
        datetime expiresAt
    }
    Webhook {
        string id PK
        string name
        datetime createdAt
        datetime updatedAt
        string url
        webhooksource source
        string workspaceId FK
        pipelinetriggers[] triggers
        string[] surveyIds
        string secret
    }
    ContactAttribute {
        string id PK
        datetime createdAt
        datetime updatedAt
        string attributeKeyId FK, UK
        string contactId FK, UK
        string value
        float valueNumber
        datetime valueDate
    }
    ContactAttributeKey {
        string id PK
        datetime createdAt
        datetime updatedAt
        boolean isUnique
        string key UK
        string name
        string description
        contactattributetype type
        contactattributedatatype dataType
        string workspaceId FK, UK
    }
    Contact {
        string id PK
        datetime createdAt
        datetime updatedAt
        string workspaceId FK
    }
    Response {
        string id PK
        datetime createdAt
        datetime updatedAt
        boolean finished
        string surveyId FK, UK
        string contactId FK
        string endingId
        json data "[ResponseData]"
        json variables "[ResponseVariables]"
        json ttc "[ResponseTtc]"
        json meta "[ResponseMeta]"
        json ingestFlags "Why a value in `data` will not read back as its declared Embedded Data type — a failed coercion or a truncated oversize value (ENG-1845). Server-computed on every ingest and never accepted from the client. Null means no ingest boundary has written it, which is not the same claim as an empty list: without this column a wrong-typed value is indistinguishable from an absent one, and surfacing that in the UI (v2) would have nothing to read. [ResponseIngestFlags]"
        responseoutcome outcome "A reviewer's verdict on the response; null means none. `lowQuality` hides it from the Analysis export by default and from Feedback Records, and nothing else: it is never deleted and every count, list, webhook and integration still includes it (ENG-6780)."
        json contactAttributes "[ResponseContactAttributes]"
        string singleUseId UK
        string language
        string displayId FK, UK
    }
    Tag {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string workspaceId FK, UK
    }
    TagsOnResponses {
        string responseId PK, FK
        string tagId PK, FK
    }
    Display {
        string id PK
        datetime createdAt
        datetime updatedAt
        string surveyId FK
        string contactId FK
        string creativeId
    }
    SurveyTrigger {
        string id PK
        datetime createdAt
        datetime updatedAt
        string surveyId FK, UK
        string actionClassId FK, UK
    }
    SurveyAttributeFilter {
        string id PK
        datetime createdAt
        datetime updatedAt
        string attributeKeyId FK, UK
        string surveyId FK, UK
        surveyattributefiltercondition condition
        string value
    }
    Survey {
        json hiddenFields "[SurveyHiddenFields]"
        json variables "[SurveyVariables]"
        boolean ownershipInitialized
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string name
        string redirectUrl
        surveytype type
        string workspaceId FK, UK
        string createdBy FK
        surveyvisibility visibility "ENG-3282: authorization facts, projected into SpiceDB by the survey outbox trigger. `ownerId` is the durable ownership source; `createdBy` stays pure attribution and is never rewritten."
        string ownerId FK
        int visibilityVersion "Incremented by every accepted visibility change; fences projection work. An INSERT starts at 0 with `visibilityProjectedVersion` -1 (the `survey_visibility_pending` trigger): the initial projection, decided from PostgreSQL facts until the projector acknowledges it."
        int visibilityProjectedVersion "The `visibilityVersion` SpiceDB last acknowledged; -1 while none ever was."
        boolean visibilityPending "#quot;visibilityVersion#quot; <> #quot;visibilityProjectedVersion#quot;, kept by the `survey_visibility_pending` trigger (a BEFORE trigger overrides whatever an INSERT or UPDATE supplies). Read-only: never put it in create/update data."
        datetime visibilityChangedAt
        string visibilityChangedById
        surveystatus status
        json welcomeCard "[SurveyWelcomeCard]"
        json questions "[SurveyQuestions]"
        json[] blocks "[SurveyBlocks]"
        json[] endings "[SurveyEnding]"
        displayoptions displayOption
        int recontactDays
        int displayLimit
        json inlineTriggers "[SurveyInlineTriggers]"
        int autoClose
        boolean keepEndingCardOpen
        int autoComplete
        int delay
        datetime publishOn
        datetime closeOn
        datetime archivedAt
        json surveyClosedMessage "[SurveyClosedMessage]"
        string segmentId FK
        json workspaceOverwrites "[SurveyWorkspaceOverwrites]"
        json styling "[SurveyStyling]"
        json singleUse "[SurveySingleUse]"
        boolean isVerifyEmailEnabled
        boolean isBackButtonHidden
        boolean isAutoProgressingEnabled
        boolean isCaptureIpEnabled
        boolean isAnonymizeResponsesEnabled
        string pin
        decimal displayPercentage
        boolean showLanguageSwitch
        boolean autoSelectLanguage "Open the survey in the respondent's browser language when it matches an enabled survey language. Null on surveys that predate the setting, which reads as off."
        json recaptcha "[SurveyRecaptcha]"
        json metadata "[SurveyLinkMetadata]"
        string slug UK
        string customHeadScripts
        surveyscriptmode customHeadScriptsMode
        json customCss "[CustomCssStored] Survey custom CSS (ENG-2949): source plus trusted compiled output, written only by the shared custom CSS save service. null = no survey CSS. Kept outside `styling` (M2.07)."
        json showConditions "[SurveyShowConditions] App surveys: conditions checked in the browser when a trigger fires (ENG-3873). null = no conditions, which is what every existing survey keeps."
    }
    SurveyQuota {
        string id PK
        datetime createdAt
        datetime updatedAt
        string surveyId FK, UK
        string name UK
        int limit
        json logic "[SurveyQuotaLogic]"
        surveyquotaaction action
        string endingCardId
        boolean countPartialSubmissions
    }
    ResponseQuotaLink {
        string responseId PK, FK
        string quotaId PK, FK
        responsequotalinkstatus status
    }
    SurveyFollowUp {
        string id PK
        datetime createdAt
        datetime updatedAt
        string surveyId FK
        string name
        json trigger "[SurveyFollowUpTrigger]"
        json action "[SurveyFollowUpAction]"
    }
    ActionClass {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string description
        actiontype type
        string key UK
        json noCodeConfig "[ActionClassNoCodeConfig]"
        string workspaceId FK, UK
    }
    Integration {
        string id PK
        integrationtype type UK
        json config "[IntegrationConfig]"
        string workspaceId FK, UK
    }
    DataMigration {
        string id PK
        datetime startedAt
        datetime finishedAt
        string name UK
        datamigrationstatus status
    }
    Workspace {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string legacyEnvironmentId UK
        string organizationId FK, UK
        json styling "[Styling]"
        json config "[WorkspaceConfig]"
        int recontactDays
        boolean linkSurveyBranding
        boolean inAppSurveyBranding
        widgetplacement placement
        boolean clickOutsideClose
        surveyoverlay overlay
        string overlayColor "Custom overlay colour (hex, no alpha) and opacity (10-100%). null = the preset for `overlay`."
        int overlayOpacity
        json logo "[Logo]"
        boolean appSetupCompleted
        string customHeadScripts
        json customCss "[CustomCssStored] Workspace custom CSS (ENG-2949), written only by the shared save service."
        json customCssPrevious "[CustomCssStored] The one recoverable previous workspace CSS revision, source included. Restored by submitting its source again through the current processor and authorization checks."
    }
    Organization {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        json whitelabel "[OrganizationWhitelabel]"
        boolean isAISmartToolsEnabled
        string displayTimeZone
    }
    OrganizationBilling {
        string organizationId PK, FK
        string stripeCustomerId UK
        json limits "[OrganizationBillingPlanLimits]"
        datetime usageCycleAnchor
        json stripe "[OrganizationStripeBilling]"
        datetime createdAt
        datetime updatedAt
    }
    Membership {
        string organizationId PK, FK
        string userId PK, FK
        boolean accepted
        organizationrole role
    }
    Invite {
        string id PK
        string email
        string name
        string organizationId FK
        string creatorId FK
        string acceptorId FK
        datetime createdAt
        datetime expiresAt
        organizationrole role
        string[] teamIds
    }
    ApiKey {
        string id PK
        datetime createdAt
        string createdBy
        datetime lastUsedAt
        string label
        string hashedKey
        string lookupHash UK
        string organizationId FK
        json organizationAccess "[OrganizationAccess]"
    }
    ApiKeyWorkspace {
        string id PK
        datetime createdAt
        datetime updatedAt
        string apiKeyId FK, UK
        string workspaceId FK, UK
        apikeypermission permission
    }
    Account {
        string id PK
        datetime createdAt
        datetime updatedAt
        string userId FK
        string type
        string provider UK
        string providerAccountId UK
        string access_token
        string refresh_token
        int expires_at
        int ext_expires_in
        string token_type
        string scope
        string id_token
        string session_state
        string password
        datetime accessTokenExpiresAt
        datetime refreshTokenExpiresAt
        string issuer UK "Better Auth 1.7.0 (ENG-2343): the account's identity namespace. 1.7.0 keyed accounts on (issuer, accountId) and filtered every lookup on it — findAccountByKey, findAccountOwnerByKey, findCredentialAccount and updatePassword — so on 1.7.0 a null here locks the user out. 1.7.3 dropped the field and 1.7.7 keys accounts on (provider, providerAccountId) again (ENG-3880). We keep writing it (see `account.create.before` in better-auth-hooks.ts) so a rollback to 1.7.0 still finds every row; drop it once that rollback is off the table.  NULLABLE on purpose. A NOT NULL column would fail every INSERT from a 1.6 pod still serving during a rolling deploy. The migration backfills every existing row; see the migration SQL for the value derivation."
    }
    Session {
        string id PK
        datetime createdAt
        datetime updatedAt
        string sessionToken UK
        string userId FK
        datetime expires
        string ipAddress
        string userAgent
    }
    VerificationToken {
        string identifier UK
        string token UK
        datetime expires
    }
    TwoFactor {
        string id PK
        string secret
        string backupCodes
        string userId FK, UK
        boolean verified
        int failedVerificationCount "Better Auth 1.6.23+ two-factor brute-force lockout: consecutive failed verification attempts."
        datetime lockedUntil "Better Auth 1.6.23+ two-factor brute-force lockout: set once failedVerificationCount trips the limit."
    }
    jwks {
        string id PK
        string publicKey
        string privateKey
        datetime createdAt
        datetime expiresAt
        string alg "Better Auth 1.7 (ENG-2343): the JWS algorithm this key signs with, so a keyring can hold more than one alg and `getLatestKeyByAlg` can pick the right key per resource.  NOT optional upstream in practice, despite being declared `required: false`: `createJwk` always puts `alg` in the row it writes (defaulting to EdDSA), so without this column every key mint is rejected by Prisma as an unknown argument — which breaks JWT signing, and with it the whole MCP OAuth flow, on any deployment that has yet to mint a key. Nullable because rows written before 1.7 have no value; upstream reads treat `alg: null` as the configured default alg, so existing keys keep working untouched."
        string crv "Better Auth 1.7 (ENG-2343): the curve for EC/OKP keys. Written whenever it can be derived from the generated key, which it can for the default EdDSA/Ed25519 config we run, so it is on the same always-written path as `alg` above."
    }
    oauthClient {
        string id PK
        string clientId UK
        string clientSecret
        boolean disabled
        boolean skipConsent
        boolean enableEndSession
        string subjectType
        string[] scopes
        string userId FK
        datetime createdAt
        datetime updatedAt
        string name
        string uri
        string icon
        string[] contacts
        string tos
        string policy
        string softwareId
        string softwareVersion
        string softwareStatement
        string[] redirectUris
        string[] postLogoutRedirectUris
        string tokenEndpointAuthMethod
        string[] grantTypes
        string[] responseTypes
        boolean public "Unwritten by Better Auth 1.7 (it dropped both from its schema), kept deliberately: they are nullable, so keeping them makes the 1.7 migration purely additive and a rollback to 1.6 needs no down-migration. Drop them in a separate cleanup after a bake period."
        string type
        boolean requirePKCE
        string referenceId
        json metadata
        string clientDiscoveryId "Better Auth 1.7 (ENG-2343)."
        string[] clientCredentialsScopes
        string backchannelLogoutUri
        boolean backchannelLogoutSessionRequired
        string applicationType
        string jwks
        string jwksUri
        boolean dpopBoundAccessTokens
    }
    oauthAccessToken {
        string id PK
        string token UK
        string clientId FK
        string sessionId FK
        string userId FK
        string referenceId
        string refreshId FK
        datetime expiresAt
        datetime createdAt
        string[] scopes
        string authorizationCodeId "Better Auth 1.7 (ENG-2343). `resources` is the RFC 8707 resource set the grant approved — the binding this whole upgrade exists to get (GHSA-p2fr-6hmx-4528)."
        string[] resources
        string[] requestedUserInfoClaims
        datetime revoked
        json confirmation
    }
    oauthRefreshToken {
        string id PK
        string token UK
        string clientId FK
        string sessionId FK
        string userId FK
        string referenceId
        datetime expiresAt
        datetime createdAt
        datetime revoked
        datetime authTime
        string[] scopes
        string authorizationCodeId "Better Auth 1.7 (ENG-2343). `resources` carries the full original grant so a refresh can narrow the resource set but never widen it."
        string[] resources
        string[] requestedUserInfoClaims
        datetime rotatedAt
        string rotationReplayResponse
        datetime rotationReplayExpiresAt
        json confirmation
    }
    oauthConsent {
        string id PK
        string clientId FK
        string userId FK
        string referenceId
        string[] scopes
        datetime createdAt
        datetime updatedAt
        string[] resources "Better Auth 1.7 (ENG-2343). /authorize compares the requested resources against these; without them every pre-existing connection is forced back through the consent screen."
        string[] requestedUserInfoClaims
    }
    oauthResource {
        string id PK
        string identifier UK
        string name
        int accessTokenTtl
        int refreshTokenTtl
        string signingAlgorithm
        string signingKeyId
        string[] allowedScopes
        json customClaims
        boolean dpopBoundAccessTokensRequired
        boolean disabled
        datetime createdAt
        datetime updatedAt
        int policyVersion
        json metadata
    }
    oauthClientResource {
        string id PK
        string clientId FK, UK
        string resourceId FK, UK
        json metadata
        datetime createdAt
    }
    oauthClientAssertion {
        string id PK
        datetime expiresAt
    }
    PasswordResetToken {
        string id PK
        datetime createdAt
        datetime updatedAt
        string tokenHash UK
        datetime expiresAt
        string userId FK, UK
    }
    User {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        string email UK
        boolean emailVerified
        string twoFactorSecret
        boolean twoFactorEnabled
        string backupCodes
        string password
        identityprovider identityProvider
        string identityProviderAccountId
        string groupId
        json notificationSettings "[UserNotificationSettings]"
        string locale "[Locale]"
        datetime lastLoginAt
        boolean isActive
        datetime reactivatedAt "When an admin last reactivated this user. Restarts the members retention clock (ENG-3713) without touching `lastLoginAt`, which the member list, usage counts and APIs report as a real sign-in."
        datetime lastActiveAt "When the user last completed a sign-in (`lastActivePlugin`, after any second factor) or renewed a session (Better Auth `databaseHooks.session.update`), on the database clock and never moved backwards. The members retention clock reads it: sessions are deleted on sign-out and expiry, so their own timestamps can't serve as a record of activity."
        bootstrapadminmarker isBootstrapAdmin UK
    }
    Segment {
        string id PK
        datetime createdAt
        datetime updatedAt
        string title UK
        string description
        boolean isPrivate
        json filters "[SegmentFilter]"
        string workspaceId FK, UK
    }
    Language {
        string id PK
        datetime createdAt
        datetime updatedAt
        string code UK
        string alias
        string workspaceId FK, UK
    }
    SurveyLanguage {
        string languageId PK, FK
        string surveyId PK, FK
        boolean default
        boolean enabled
    }
    EmbeddedData {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string key UK
        string name
        string description
        embeddeddatasource source
        embeddeddatatype dataType
        json defaultValue "[EmbeddedDataDefaultValue]"
        boolean locked
        string surveyId FK
        string workspaceId FK, UK
    }
    SurveyEmbeddedData {
        string id PK
        string workspaceId FK
        string surveyId FK, UK
        string embeddedDataId FK, UK
        string storageKey UK
        int order
    }
    Team {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string organizationId FK, UK
    }
    TeamUser {
        datetime createdAt
        datetime updatedAt
        string teamId PK, FK
        string userId PK, FK
        teamuserrole role
    }
    WorkspaceTeam {
        datetime createdAt
        datetime updatedAt
        string workspaceId PK, FK
        string teamId PK, FK
        workspaceteampermission permission
    }
    Chart {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        charttype type
        string workspaceId FK, UK
        json query "[ChartQuery] - Cube.js query configuration"
        json config "[ChartConfig] - Visualization configuration (colors, labels, formatting)"
        string createdBy FK
        string feedbackDirectoryId FK
    }
    Dashboard {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        string workspaceId FK, UK
        string createdBy FK
    }
    DashboardWidget {
        string id PK
        datetime createdAt
        datetime updatedAt
        string dashboardId FK, UK
        string chartId FK, UK
        json layout "[WidgetLayout] - Grid layout: { x, y, w, h }"
        int order
    }
    DeletionCleanup {
        string id PK
        datetime createdAt
        datetime updatedAt
        deletioncleanupkind kind
        string organizationId "Scoping and logs. Not foreign keys: the rows they name are usually gone already."
        string workspaceId
        string surveyId
        string[] tenantIds "Hub kinds: the organisation's feedback directories, read inside the delete's transaction."
        string[] responseIds "`hubResponses`: the deleted responses whose records go. `hubResponseOutcome`: exactly one live response."
        string[] fileKeys "`storageFiles`: the object keys or URLs to delete."
        int attempts
        datetime nextAttemptAt "Not drained before this, so a late pipeline write can't slip in behind a survey's cleanup."
        string lastError "The last failure: a short code and message, never record or file content."
    }
    AttachmentExport {
        string id PK
        datetime createdAt
        datetime updatedAt
        string surveyId FK
        string workspaceId FK
        string requestedById FK
        attachmentexportstatus status
        json filterCriteria "The response filter at request time, a `TResponseFilterCriteria`; null exports every response."
        string timeZone "IANA zone the archive's folder names are stamped in: the org's display zone at request time."
        int dispatchCount "Bumped on every enqueue and part of the job id, so a re-dispatch never dedupes against a job an older pod dropped as unknown during a rolling upgrade."
        int fileCount "The pre-flight's counts, shown while the archive builds."
        int responseCount
        int includedFileCount "What the archive actually holds, once built."
        bigint totalBytes
        boolean truncated
        string storageKey
        string errorCode "A short code (`stale`, `storage_upload_failed`, …), never a message or file name."
        datetime startedAt
        datetime finishedAt
        datetime expiresAt
    }
    FeedbackSource {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string name UK
        feedbacksourcetype type
        feedbacksourcestatus status
        feedbacksourceimportmode importMode "Defaults to completedOnly so a new source agrees with the live ingestion path, which only fires on responseFinished. Existing rows take the default; nothing is re-imported."
        feedbacksourceelementscope elementScope
        string workspaceId FK, UK
        string feedbackDirectoryId FK
        datetime lastSyncAt
        string createdBy FK
    }
    FeedbackSourceFormbricksMapping {
        string id PK
        datetime createdAt
        string feedbackSourceId FK, UK
        string workspaceId FK, UK
        string surveyId FK, UK
        string elementId UK
        hubfieldtype hubFieldType
        string customFieldLabel
    }
    FeedbackSourceFieldMapping {
        string id PK
        datetime createdAt
        string feedbackSourceId FK, UK
        string workspaceId FK, UK
        string sourceFieldId UK
        string targetFieldId UK
        string staticValue
    }
    FeedbackDirectory {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name UK
        boolean isArchived
        string organizationId FK, UK
    }
    FeedbackDirectoryWorkspace {
        datetime createdAt
        datetime updatedAt
        string feedbackDirectoryId PK, FK
        string workspaceId PK, FK
    }
    AuthzedProjectionScopeState {
        string scope PK
        datetime readyAt
        string readyBy
        datetime updatedAt
    }
    AuthzedProjectionOutbox {
        string id PK
        string targetType
        string primaryId
        string secondaryId
        boolean isRevocation
        int attempts "Total delivery attempts. Drives the retry backoff and reports how hard delivery was tried."
        int permanentFailures "Non-retryable failures attributable to this event alone. The only input to dead-lettering, so that a SpiceDB outage of any duration cannot dead-letter events that were never the problem."
        datetime availableAt
        datetime leasedAt
        datetime leaseExpiresAt
        string leaseOwner
        datetime processedAt
        datetime deadLetteredAt
        datetime lastAttemptAt
        string lastErrorCode
        datetime createdAt
        datetime updatedAt
    }
    OnlineUpgradeState {
        string release PK
        string targetImage
        string phase
        string[] completedPhases
        string surveyCursor
        bigint surveyCount
        datetime baselineCompletedAt
        datetime forwardOnlyAt
        datetime queuePausedAt
        datetime queuePauseDeadlineAt
        datetime queueResumedAt
        datetime activatedAt
        datetime updatedAt
    }
    RetentionPolicy {
        string id PK
        datetime createdAt
        datetime updatedAt
        string organizationId FK, UK
        retentionentity entity UK
        boolean enabled
        datetime enabledAt "When the current configuration took effect: the policy was switched on or unpaused, or changed in a way that could bring an action earlier (a shorter period or warning, different conditions). Lengthening does not move it. The warning always runs in full from here, so no change can delete data on the night it is made (ENG-3614). Also the clock for members with no recorded sign-in."
        int warnDays "How long before the action the notice goes out. The action never runs before the notice has been out for this long, so the warning always runs in full."
        int periodDays "The period, counted from the target's clock, after which the policy acts: responses are deleted, surveys archived (the archive purge deletes them 30 days later), members deactivated."
        retentionsurveycondition[] conditions "Surveys policy only."
        datetime deferredAt "The last night the sweep's time ran out before it reached this policy's organisation, licensed. The organisation was due and the sweep was running, so it counts as reached for the gap that restarts the warning (`RETENTION_SWEEP_GAP_MS`): a backlog of organisations never restarts warnings."
        string updatedById FK
    }
    RetentionExemption {
        string id PK
        datetime createdAt
        string organizationId FK
        retentionentity entity
        string surveyId FK
        datetime until
        retentionexemptionsource source
        string reason "Required for `manual`, absent for `restore` (`RetentionExemption_reason_check`)."
        string createdById FK
        datetime revokedAt
        string revokedById FK
    }
    RetentionRun {
        string id PK
        string organizationId FK
        retentionentity entity
        retentionrunkind kind
        datetime startedAt
        datetime finishedAt
        int notifiedCount
        int archivedCount "Archived surveys, or deactivated members."
        int deletedCount "Deleted responses, or, on a purge run, deleted surveys."
        int skippedCount
        boolean hasChanges "False when the run changed nothing. History hides those by default."
        string scanCursor "Where the run's scan of candidates stopped for want of time, so the next run resumes there instead of rescanning the same first targets every night. Null when the scan reached the end."
    }
    RetentionRunItem {
        string id PK
        string runId FK
        retentiontargettype targetType
        string targetId
        string targetName
        retentionrunitemaction action
        int count "1, except for grouped response deletions, where it is the number of responses deleted."
        string recipient "Who was emailed, for `notified` rows."
        retentionskipreason skipReason
    }
    RetentionNotice {
        string id PK
        string organizationId FK, UK
        retentionentity entity UK
        string surveyId FK, UK
        string userId FK, UK
        datetime sentAt "When the notice was claimed for sending. A claim that never got delivered can be taken over."
        datetime deliveredAt "When the notice was handed to the mail transport, or recorded without one (no SMTP). The warning only counts from a delivered notice, so a crash between claim and send can't let the action run without it."
        boolean emailSent "Whether an email actually went out. False when SMTP isn't configured."
        string claimToken "Set by each claim; delivery is recorded only for the claim that sent the email, so a slow sender can't mark a newer claim delivered."
        datetime clockAt "The target's clock the notice was computed for (surveys and members). A notice counts only while the target's clock is still this one, so activity between the sweep's read and its claim voids it. Null for the responses reminder, which is sent once per survey and follows no one clock, and for a survey notice voided for good: a person deleted or moved one of its responses, or the responses policy's deletion left it computed for another clock (see `NOTICE_FOLLOWS_CLOCK`)."
    }
    RetentionSurveyRestore {
        string surveyId PK, FK
        datetime restoredAt
    }
    Workflow {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string name
        string description
        workflowstatus status
        string workspaceId FK, UK
        string createdBy FK
        json definition "[WorkflowDefinition]"
    }
    WorkflowVersion {
        string id PK
        string workflowId FK, UK
        string workspaceId FK
        int version UK
        json definition "[WorkflowExecutableDefinition]"
        datetime publishedAt
        string publishedBy FK
    }
    WorkflowRun {
        string id PK
        datetime createdAt
        datetime updatedAt
        string workflowId FK, UK
        string workspaceId FK
        string workflowVersionId FK
        string responseId FK
        workflowrunstatus status
        string triggerType
        string surveyId
        boolean isDryRun
        string idempotencyKey UK
        int attempt
        datetime nextAttemptAt
        datetime lastErrorAt
        json triggerPayload "[WorkflowTriggerRunPayload]"
        json data "[WorkflowRunData]"
        string error
        datetime dispatchedAt
        datetime startedAt
        datetime finishedAt
    }
    WorkflowRunLog {
        string id PK
        string runId FK, UK
        int sequence
        string stepId UK
        string stepType
        workflowrunlogstatus status
        json input "[WorkflowRunLogInput]"
        json output "[WorkflowRunLogOutput]"
        string error
        datetime startedAt
        datetime finishedAt
    }
```