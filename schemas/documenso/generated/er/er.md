```mermaid
erDiagram
    AvatarImage |o--o{ User : "(id) - (avatarImageId)"
    Team |o--o| TeamProfile : "(id) - (teamId)"
    User ||--o{ UserSecurityAuditLog : "(id) - (userId)"
    User ||--o{ PasswordResetToken : "(id) - (userId)"
    User ||--o{ Passkey : "(id) - (userId)"
    User ||--o{ VerificationToken : "(id) - (userId)"
    User ||--o{ Webhook : "(id) - (userId)"
    Team ||--o{ Webhook : "(id) - (teamId)"
    Webhook ||--o{ WebhookCall : "(id) - (webhookId)"
    User |o--o{ ApiToken : "(id) - (userId)"
    Team ||--o{ ApiToken : "(id) - (teamId)"
    Organisation ||--o| Subscription : "(id) - (organisationId)"
    EmailTransport |o--o{ SubscriptionClaim : "(id) - (emailTransportId)"
    EmailTransport |o--o{ OrganisationClaim : "(id) - (emailTransportId)"
    Organisation ||--o{ OrganisationMonthlyStat : "(id) - (organisationId)"
    User ||--o{ Account : "(id) - (userId)"
    User |o--o{ Session : "(id) - (userId)"
    User ||--o{ Folder : "(id) - (userId)"
    Team ||--o{ Folder : "(id) - (teamId)"
    Folder |o--o{ Folder : "(id) - (parentId)"
    User ||--o{ Envelope : "(id) - (userId)"
    Team ||--o{ Envelope : "(id) - (teamId)"
    Folder |o--o{ Envelope : "(id) - (folderId)"
    DocumentMeta ||--o| Envelope : "(id) - (documentMetaId)"
    DocumentData ||--o| EnvelopeItem : "(id) - (documentDataId)"
    Envelope ||--o{ EnvelopeItem : "(id) - (envelopeId)"
    Envelope |o--o{ DocumentAuditLog : "(id) - (envelopeId)"
    Envelope ||--o{ EnvelopeAttachment : "(id) - (envelopeId)"
    Envelope ||--o{ Recipient : "(id) - (envelopeId)"
    EnvelopeItem ||--o{ Field : "(id) - (envelopeItemId)"
    Envelope ||--o{ Field : "(id) - (envelopeId)"
    Recipient ||--o{ Field : "(id) - (recipientId)"
    Recipient ||--o{ Signature : "(id) - (recipientId)"
    Field ||--o| Signature : "(id) - (fieldId)"
    Recipient ||--o| CscCredential : "(id) - (recipientId)"
    Recipient ||--o| CscSession : "(id) - (recipientId)"
    Envelope ||--o{ DocumentShareLink : "(id) - (envelopeId)"
    OrganisationClaim ||--o| Organisation : "(id) - (organisationClaimId)"
    AvatarImage |o--o{ Organisation : "(id) - (avatarImageId)"
    User ||--o{ Organisation : "(id) - (ownerUserId)"
    OrganisationGlobalSettings ||--o| Organisation : "(id) - (organisationGlobalSettingsId)"
    OrganisationAuthenticationPortal ||--o| Organisation : "(id) - (organisationAuthenticationPortalId)"
    User ||--o{ OrganisationMember : "(id) - (userId)"
    Organisation ||--o{ OrganisationMember : "(id) - (organisationId)"
    Organisation ||--o{ OrganisationMemberInvite : "(id) - (organisationId)"
    Organisation ||--o{ OrganisationGroup : "(id) - (organisationId)"
    OrganisationGroup ||--o{ OrganisationGroupMember : "(id) - (groupId)"
    OrganisationMember ||--o{ OrganisationGroupMember : "(id) - (organisationMemberId)"
    OrganisationGroup ||--o{ TeamGroup : "(id) - (organisationGroupId)"
    Team ||--o{ TeamGroup : "(id) - (teamId)"
    OrganisationEmail |o--o{ OrganisationGlobalSettings : "(id) - (emailId)"
    OrganisationEmail |o--o{ TeamGlobalSettings : "(id) - (emailId)"
    AvatarImage |o--o{ Team : "(id) - (avatarImageId)"
    Organisation ||--o{ Team : "(id) - (organisationId)"
    TeamGlobalSettings ||--o| Team : "(id) - (teamGlobalSettingsId)"
    Team ||--o| TeamEmail : "(id) - (teamId)"
    Team ||--o| TeamEmailVerification : "(id) - (teamId)"
    Envelope ||--o| TemplateDirectLink : "(id) - (envelopeId)"
    User |o--o{ SiteSettings : "(id) - (lastModifiedByUserId)"
    BackgroundJob ||--o{ BackgroundJobTask : "(id) - (jobId)"
    Organisation ||--o{ EmailDomain : "(id) - (organisationId)"
    EmailDomain ||--o{ OrganisationEmail : "(id) - (emailDomainId)"
    Organisation ||--o{ OrganisationEmail : "(id) - (organisationId)"
    Organisation ||--o{ EmbedSignature : "(id) - (organisationId)"
    User {
        int id PK
        string name
        string email UK
        datetime emailVerified
        string password
        string source
        string signature
        datetime createdAt
        datetime updatedAt
        datetime lastSignedIn
        role[] roles
        identityprovider identityProvider
        string avatarImageId FK
        boolean disabled
        string twoFactorSecret
        boolean twoFactorEnabled
        string twoFactorBackupCodes
    }
    TeamProfile {
        string id PK
        boolean enabled
        int teamId FK, UK
        string bio
    }
    UserSecurityAuditLog {
        int id PK
        int userId FK
        datetime createdAt
        usersecurityauditlogtype type
        string userAgent
        string ipAddress
    }
    PasswordResetToken {
        int id PK
        string token UK
        datetime createdAt
        datetime expiry
        int userId FK
    }
    Passkey {
        string id PK
        int userId FK
        string name
        datetime createdAt
        datetime updatedAt
        datetime lastUsedAt
        bytes credentialId "@zod.custom.use(z.instanceof(Uint8Array))"
        bytes credentialPublicKey "@zod.custom.use(z.instanceof(Uint8Array))"
        bigint counter
        string credentialDeviceType
        boolean credentialBackedUp
        string[] transports
    }
    AnonymousVerificationToken {
        string id PK
        string token UK
        datetime expiresAt
        datetime createdAt
    }
    VerificationToken {
        int id PK
        string secondaryId UK
        string identifier
        string token UK
        boolean completed
        datetime expires
        datetime createdAt
        json metadata
        int userId FK
    }
    Webhook {
        string id PK
        string webhookUrl
        webhooktriggerevents[] eventTriggers
        string secret
        boolean enabled
        datetime createdAt
        datetime updatedAt
        int userId FK
        int teamId FK
    }
    WebhookCall {
        string id PK
        webhookcallstatus status
        string url
        webhooktriggerevents event
        json requestBody
        int responseCode
        json responseHeaders
        json responseBody
        datetime createdAt
        string webhookId FK
    }
    ApiToken {
        int id PK
        string name
        string token UK
        apitokenalgorithm algorithm
        datetime expires
        datetime createdAt
        datetime lastUsedAt
        int userId FK
        int teamId FK
    }
    Subscription {
        int id PK
        subscriptionstatus status
        string planId UK
        string priceId
        datetime periodEnd
        datetime createdAt
        datetime updatedAt
        boolean cancelAtPeriodEnd
        string customerId
        string organisationId FK, UK
    }
    SubscriptionClaim {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        boolean locked
        int teamCount
        int memberCount
        int envelopeItemCount
        int recipientCount
        json flags "[ClaimFlags] @zod.custom.use(ZClaimFlagsSchema)"
        json documentRateLimits "[RateLimitArray] @zod.custom.use(ZRateLimitArraySchema)"
        int documentQuota
        json emailRateLimits "[RateLimitArray] @zod.custom.use(ZRateLimitArraySchema)"
        int emailQuota
        json apiRateLimits "[RateLimitArray] @zod.custom.use(ZRateLimitArraySchema)"
        int apiQuota
        string emailTransportId FK
    }
    OrganisationClaim {
        string id PK
        datetime createdAt
        datetime updatedAt
        string originalSubscriptionClaimId
        int teamCount
        int memberCount
        int envelopeItemCount
        int recipientCount
        json flags "[ClaimFlags] @zod.custom.use(ZClaimFlagsSchema)"
        json documentRateLimits "[RateLimitArray] @zod.custom.use(ZRateLimitArraySchema)"
        int documentQuota
        json emailRateLimits "[RateLimitArray] @zod.custom.use(ZRateLimitArraySchema)"
        int emailQuota
        json apiRateLimits "[RateLimitArray] @zod.custom.use(ZRateLimitArraySchema)"
        int apiQuota
        string emailTransportId FK
    }
    OrganisationMonthlyStat {
        string id PK
        datetime createdAt
        datetime updatedAt
        string organisationId FK, UK
        string period UK "UTC calendar month in `YYYY-MM` form, e.g. #quot;2026-05#quot;."
        int documentCount
        int emailCount
        int apiCount
        int emailReports
    }
    Account {
        string id PK
        datetime createdAt
        int userId FK
        string type
        string provider UK
        string providerAccountId UK
        string refresh_token
        string access_token
        int expires_at
        int created_at
        int ext_expires_in
        string token_type
        string scope
        string id_token
        string session_state
        string password
    }
    Session {
        string id PK
        string sessionToken UK
        int userId FK
        string ipAddress
        string userAgent
        datetime expiresAt
        datetime createdAt
        datetime updatedAt
        boolean isEmbed
    }
    AuthHandoff {
        string id PK
        string type
        string payload
        datetime createdAt
        datetime expiresAt
    }
    Folder {
        string id PK
        string name
        int userId FK
        int teamId FK
        boolean pinned
        string parentId FK
        datetime createdAt
        datetime updatedAt
        documentvisibility visibility
        foldertype type
    }
    Envelope {
        string id PK
        string secondaryId UK
        string externalId "@zod.string.describe(#quot;A custom external ID you can use to identify the document.#quot;)"
        envelopetype type
        datetime createdAt
        datetime updatedAt
        datetime completedAt
        datetime deletedAt
        string title
        documentstatus status
        documentsource source
        string qrToken "@zod.string.describe(#quot;The token for viewing the document using the QR code on the certificate.#quot;)"
        string signatureLevel "[SignatureLevel] @zod.custom.use(ZSignatureLevelSchema)"
        int internalVersion
        boolean useLegacyFieldInsertion
        json authOptions "[DocumentAuthOptions] @zod.custom.use(ZDocumentAuthOptionsSchema)"
        json formValues "[DocumentFormValues] @zod.custom.use(ZDocumentFormValuesSchema)"
        documentvisibility visibility
        templatetype templateType
        string publicTitle
        string publicDescription
        int templateId
        int userId FK "@zod.number.describe(#quot;The ID of the user that created this document.#quot;)"
        int teamId FK
        string folderId FK
        string documentMetaId FK, UK
    }
    EnvelopeItem {
        string id PK
        string title
        int order
        string documentDataId FK, UK
        string envelopeId FK
    }
    DocumentAuditLog {
        string id PK
        string envelopeId FK
        datetime createdAt
        string type
        json data
        string name
        string email
        int userId
        string userAgent
        string ipAddress
    }
    DocumentData {
        string id PK
        documentdatatype type
        string data
        string initialData
    }
    DocumentMeta {
        string id PK
        string subject
        string message
        string timezone
        string dateFormat
        string redirectUrl
        documentsigningorder signingOrder
        boolean allowDictateNextSigner
        boolean typedSignatureEnabled
        boolean uploadSignatureEnabled
        boolean drawSignatureEnabled
        string language
        documentdistributionmethod distributionMethod
        json emailSettings "[DocumentEmailSettings] @zod.custom.use(ZDocumentEmailSettingsSchema)"
        string emailReplyTo
        string emailId
        json envelopeExpirationPeriod "[EnvelopeExpirationPeriod] @zod.custom.use(ZEnvelopeExpirationPeriodSchema)"
        json reminderSettings "[EnvelopeReminderSettings] @zod.custom.use(ZEnvelopeReminderSettingsSchema)"
    }
    EnvelopeAttachment {
        string id PK
        string type "[EnvelopeAttachmentType] @zod.custom.use(ZEnvelopeAttachmentTypeSchema)"
        string label
        string data
        datetime createdAt
        datetime updatedAt
        string envelopeId FK
    }
    Recipient {
        int id PK
        string envelopeId FK
        string email
        string name
        string token
        datetime documentDeletedAt
        datetime expired
        datetime expiresAt
        datetime expirationNotifiedAt
        datetime sentAt
        datetime signedAt
        datetime lastReminderSentAt
        datetime nextReminderAt
        int reminderCount
        json authOptions "[RecipientAuthOptions] @zod.custom.use(ZRecipientAuthOptionsSchema)"
        int signingOrder "@zod.number.describe(#quot;The order in which the recipient should sign the document. Only works if the document is set to sequential signing.#quot;)"
        string rejectionReason
        recipientrole role
        readstatus readStatus
        signingstatus signingStatus
        sendstatus sendStatus
    }
    Field {
        int id PK
        string secondaryId UK
        string envelopeId FK
        string envelopeItemId FK
        int recipientId FK
        fieldtype type
        int page "@zod.number.describe(#quot;The page number of the field on the document. Starts from 1.#quot;)"
        decimal positionX
        decimal positionY
        decimal width
        decimal height
        string customText
        boolean inserted
        json fieldMeta "[FieldMeta] @zod.custom.use(ZFieldMetaNotOptionalSchema)"
    }
    Signature {
        int id PK
        datetime created
        int recipientId FK
        int fieldId FK, UK
        string signatureImageAsBase64
        string typedSignature
    }
    CscCredential {
        string id PK
        string providerId
        string credentialId
        bytes certCache "TSP-validated certificate chain (length-prefixed). Nullable until the service-scope OAuth callback successfully retrieves and validates it."
        string signatureAlgorithm "Algorithm metadata derived from the CSC `credentials/info` response. Held as flat scalars so each is independently queryable."
        string keyType
        string digestAlgorithm
        int keyLenBits
        string signAlgoParams
        bytes serviceTokenCiphertext "Service-scope access token, symmetric-encrypted at the application layer before persistence. Nullable until the service-scope OAuth callback runs."
        datetime serviceTokenExpiresAt
        int recipientId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    CscSession {
        string id PK
        string envelopeId "Denormalised envelope id for ergonomic lookups. No FK relation — cleanup flows through the Recipient cascade."
        datetime signingTime "Pinned at prep time; stable across overlay re-renders so the captured signedAttrs digest matches the bytes the TSP signs."
        json itemsJson "Contract between prep and sign: an array of `{ envelopeItemId, documentDataId, hashB64, ordinal }` per envelope item. `documentDataId` pins the immutable bytes that produced the captured digest so sign-time can re-derive the same hash against the same content. [CscSessionItems] @zod.custom.use(ZCscSessionItemsSchema)"
        bytes encryptedSad "Populated by the credential-scope OAuth callback. Both nullable at row creation; set together when the SAD is exchanged."
        datetime sadExpiresAt
        datetime createdAt
        int recipientId FK, UK
    }
    DocumentShareLink {
        int id PK
        string email UK
        string slug UK
        string envelopeId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    Organisation {
        string id PK
        datetime createdAt
        datetime updatedAt
        organisationtype type
        string name
        string url UK
        string avatarImageId FK
        string customerId UK
        string organisationClaimId FK, UK
        int ownerUserId FK
        string organisationGlobalSettingsId FK, UK
        string organisationAuthenticationPortalId FK, UK
    }
    OrganisationMember {
        string id PK
        datetime createdAt
        datetime updatedAt
        int userId FK, UK
        string organisationId FK, UK
    }
    OrganisationMemberInvite {
        string id PK
        datetime createdAt
        string email
        string token UK
        organisationmemberinvitestatus status
        string organisationId FK
        organisationmemberrole organisationRole
    }
    OrganisationGroup {
        string id PK
        string name
        organisationgrouptype type
        organisationmemberrole organisationRole
        string organisationId FK
    }
    OrganisationGroupMember {
        string id PK
        string groupId FK, UK
        string organisationMemberId FK, UK
    }
    TeamGroup {
        string id PK
        string organisationGroupId FK, UK
        teammemberrole teamRole
        int teamId FK, UK
    }
    OrganisationGlobalSettings {
        string id PK
        documentvisibility documentVisibility
        string documentLanguage
        boolean includeSenderDetails
        boolean includeSigningCertificate
        boolean includeAuditLog
        string documentTimezone
        string documentDateFormat
        boolean delegateDocumentOwnership
        boolean typedSignatureEnabled
        boolean uploadSignatureEnabled
        boolean drawSignatureEnabled
        json defaultRecipients "[DefaultRecipient[]] @zod.custom.use(ZDefaultRecipientsSchema)"
        string emailId FK
        string emailReplyTo
        json emailDocumentSettings "[DocumentEmailSettings] @zod.custom.use(ZDocumentEmailSettingsSchema)"
        boolean brandingEnabled
        string brandingLogo
        string brandingUrl
        string brandingCompanyDetails
        json brandingColors "[TCssVarsSchema] @zod.custom.use(ZCssVarsSchema)"
        string brandingCss
        json envelopeExpirationPeriod "[EnvelopeExpirationPeriod] @zod.custom.use(ZEnvelopeExpirationPeriodSchema)"
        json reminderSettings "[EnvelopeReminderSettings] @zod.custom.use(ZEnvelopeReminderSettingsSchema)"
        boolean aiFeaturesEnabled
    }
    TeamGlobalSettings {
        string id PK
        documentvisibility documentVisibility
        string documentLanguage
        string documentTimezone
        string documentDateFormat
        boolean delegateDocumentOwnership
        boolean includeSenderDetails
        boolean includeSigningCertificate
        boolean includeAuditLog
        boolean typedSignatureEnabled
        boolean uploadSignatureEnabled
        boolean drawSignatureEnabled
        json defaultRecipients "[DefaultRecipient[]] @zod.custom.use(ZDefaultRecipientsSchema)"
        string emailId FK
        string emailReplyTo
        json emailDocumentSettings "[DocumentEmailSettingsNullable] @zod.custom.use(ZDocumentEmailSettingsSchema)"
        boolean brandingEnabled
        string brandingLogo
        string brandingUrl
        string brandingCompanyDetails
        json brandingColors "[TCssVarsSchema] @zod.custom.use(ZCssVarsSchema)"
        string brandingCss
        json envelopeExpirationPeriod "[EnvelopeExpirationPeriod] @zod.custom.use(ZEnvelopeExpirationPeriodSchema)"
        json reminderSettings "[EnvelopeReminderSettings] @zod.custom.use(ZEnvelopeReminderSettingsSchema)"
        boolean aiFeaturesEnabled
    }
    Team {
        int id PK
        string name
        string url UK
        datetime createdAt
        string avatarImageId FK
        string organisationId FK
        string teamGlobalSettingsId FK, UK
    }
    TeamEmail {
        int teamId PK, FK
        datetime createdAt
        string name
        string email UK
    }
    TeamEmailVerification {
        int teamId PK, FK
        string name
        string email
        string token UK
        boolean completed
        datetime expiresAt
        datetime createdAt
    }
    TemplateDirectLink {
        string id PK
        string envelopeId FK, UK
        string token UK
        datetime createdAt
        boolean enabled
        int directTemplateRecipientId
    }
    SiteSettings {
        string id PK
        boolean enabled
        json data
        int lastModifiedByUserId FK
        datetime lastModifiedAt
    }
    BackgroundJob {
        string id PK
        backgroundjobstatus status
        json payload
        int retried
        int maxRetries
        string jobId
        string name
        string version
        datetime submittedAt
        datetime updatedAt
        datetime completedAt
        datetime lastRetriedAt
    }
    BackgroundJobTask {
        string id PK
        string name
        backgroundjobtaskstatus status
        json result
        int retried
        int maxRetries
        datetime createdAt
        datetime updatedAt
        datetime completedAt
        string jobId FK
    }
    AvatarImage {
        string id PK
        string bytes
    }
    EmailDomain {
        string id PK
        datetime createdAt
        datetime updatedAt
        emaildomainstatus status
        string selector UK
        string domain UK
        string publicKey
        string privateKey
        datetime lastVerifiedAt
        string organisationId FK
    }
    OrganisationEmail {
        string id PK
        datetime createdAt
        datetime updatedAt
        string email UK
        string emailName
        string emailDomainId FK
        string organisationId FK
    }
    EmailTransport {
        string id PK
        datetime createdAt
        datetime updatedAt
        string name
        emailtransporttype type
        string fromName
        string fromAddress
        string config
    }
    OrganisationAuthenticationPortal {
        string id PK
        boolean enabled
        string clientId
        string clientSecret
        string wellKnownUrl
        organisationmemberrole defaultOrganisationRole
        boolean autoProvisionUsers
        string[] allowedDomains
        boolean allowPersonalOrganisations
    }
    Counter {
        string id PK
        int value
    }
    EmbedSignature {
        string key PK
        datetime createdAt
        datetime updatedAt
        string organisationId FK
        int recipientId "Last writer. Not a relation so the row survives document deletion and remains useful for abuse tracing."
        string signature
    }
    RateLimit {
        string key PK
        string action PK
        datetime bucket PK
        int count
        datetime createdAt
    }
```