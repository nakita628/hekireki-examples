```mermaid
erDiagram
    Document ||--o{ DocumentAnnotation : "(id) - (documentId)"
    Team ||--o{ DocumentAnnotation : "(id) - (teamId)"
    User ||--o{ DocumentAnnotation : "(id) - (createdById)"
    DocumentAnnotation ||--o{ AnnotationImage : "(id) - (annotationId)"
    Team ||--o{ Brand : "(id) - (teamId)"
    Dataroom ||--o{ Conversation : "(id) - (dataroomId)"
    DataroomDocument |o--o{ Conversation : "(id) - (dataroomDocumentId)"
    Link |o--o{ Conversation : "(id) - (linkId)"
    ViewerGroup |o--o{ Conversation : "(id) - (viewerGroupId)"
    DataroomQuestion |o--o{ Conversation : "(id) - (questionId)"
    Viewer |o--o{ Conversation : "(id) - (recipientViewerId)"
    View |o--o{ Conversation : "(id) - (initialViewId)"
    Team ||--o{ Conversation : "(id) - (teamId)"
    Dataroom ||--o{ DataroomQuestion : "(id) - (dataroomId)"
    Team ||--o{ DataroomQuestion : "(id) - (teamId)"
    User |o--o{ DataroomQuestion : "(id) - (createdByUserId)"
    DataroomDocument |o--o{ DataroomQuestion : "(id) - (dataroomDocumentId)"
    DataroomQuestion ||--o{ DataroomQuestionAssignment : "(id) - (questionId)"
    Link |o--o{ DataroomQuestionAssignment : "(id) - (linkId)"
    ViewerGroup |o--o{ DataroomQuestionAssignment : "(id) - (groupId)"
    Viewer |o--o{ DataroomQuestionAssignment : "(id) - (viewerId)"
    Conversation ||--o{ ConversationParticipant : "(id) - (conversationId)"
    Viewer |o--o{ ConversationParticipant : "(id) - (viewerId)"
    User |o--o{ ConversationParticipant : "(id) - (userId)"
    Conversation ||--o{ Message : "(id) - (conversationId)"
    User |o--o{ Message : "(id) - (userId)"
    Viewer |o--o{ Message : "(id) - (viewerId)"
    View |o--o{ Message : "(id) - (viewId)"
    Conversation ||--o{ ConversationView : "(id) - (conversationId)"
    View ||--o{ ConversationView : "(id) - (viewId)"
    Dataroom ||--o{ DataroomFaqItem : "(id) - (dataroomId)"
    Link |o--o{ DataroomFaqItem : "(id) - (linkId)"
    DataroomDocument |o--o{ DataroomFaqItem : "(id) - (dataroomDocumentId)"
    Conversation |o--o{ DataroomFaqItem : "(id) - (sourceConversationId)"
    Message |o--o{ DataroomFaqItem : "(id) - (questionMessageId)"
    Message |o--o{ DataroomFaqItem : "(id) - (answerMessageId)"
    Team ||--o{ DataroomFaqItem : "(id) - (teamId)"
    User ||--o{ DataroomFaqItem : "(id) - (publishedByUserId)"
    Team ||--o{ Dataroom : "(id) - (teamId)"
    Brand |o--o{ Dataroom : "(id) - (brandId)"
    User |o--o{ Dataroom : "(id) - (frozenBy)"
    Dataroom ||--o{ TaskList : "(id) - (dataroomId)"
    Team ||--o{ TaskList : "(id) - (teamId)"
    TaskList ||--o{ Task : "(id) - (taskListId)"
    Dataroom ||--o{ Task : "(id) - (dataroomId)"
    Team ||--o{ Task : "(id) - (teamId)"
    DataroomFolder |o--o{ Task : "(id) - (uploadFolderId)"
    User |o--o{ Task : "(id) - (createdByUserId)"
    Task ||--o{ TaskAssignment : "(id) - (taskId)"
    Viewer |o--o{ TaskAssignment : "(id) - (viewerId)"
    ViewerGroup |o--o{ TaskAssignment : "(id) - (groupId)"
    Link |o--o{ TaskAssignment : "(id) - (linkId)"
    Task ||--o{ TaskActivity : "(id) - (taskId)"
    User |o--o{ TaskActivity : "(id) - (userId)"
    Viewer |o--o{ TaskActivity : "(id) - (viewerId)"
    View |o--o{ TaskActivity : "(id) - (viewId)"
    Dataroom ||--o{ DataroomDocument : "(id) - (dataroomId)"
    Document ||--o{ DataroomDocument : "(id) - (documentId)"
    DataroomFolder |o--o{ DataroomDocument : "(id) - (folderId)"
    DataroomFolder |o--o{ DataroomFolder : "(id) - (parentId)"
    Dataroom ||--o{ DataroomFolder : "(id) - (dataroomId)"
    Dataroom ||--o| DataroomBrand : "(id) - (dataroomId)"
    Dataroom ||--o{ ViewerGroup : "(id) - (dataroomId)"
    Team ||--o{ ViewerGroup : "(id) - (teamId)"
    Viewer ||--o{ ViewerGroupMembership : "(id) - (viewerId)"
    ViewerGroup ||--o{ ViewerGroupMembership : "(id) - (groupId)"
    ViewerGroup ||--o{ ViewerGroupAccessControls : "(id) - (groupId)"
    Dataroom ||--o{ PermissionGroup : "(id) - (dataroomId)"
    Team ||--o{ PermissionGroup : "(id) - (teamId)"
    PermissionGroup ||--o{ PermissionGroupAccessControls : "(id) - (groupId)"
    User |o--o{ Document : "(id) - (ownerId)"
    Team ||--o{ Document : "(id) - (teamId)"
    Folder |o--o{ Document : "(id) - (folderId)"
    Document ||--o{ DocumentVersion : "(id) - (documentId)"
    DocumentVersion ||--o{ DocumentPage : "(id) - (versionId)"
    Folder |o--o{ Folder : "(id) - (parentId)"
    Team ||--o{ Folder : "(id) - (teamId)"
    Document ||--o{ DocumentUpload : "(id) - (documentId)"
    Team ||--o{ DocumentUpload : "(id) - (teamId)"
    Viewer |o--o{ DocumentUpload : "(id) - (viewerId)"
    View |o--o{ DocumentUpload : "(id) - (viewId)"
    Link ||--o{ DocumentUpload : "(id) - (linkId)"
    Dataroom |o--o{ DocumentUpload : "(id) - (dataroomId)"
    DataroomDocument |o--o{ DocumentUpload : "(id) - (dataroomDocumentId)"
    Task |o--o{ DocumentUpload : "(id) - (taskId)"
    Integration ||--o{ InstalledIntegration : "(id) - (integrationId)"
    User |o--o{ InstalledIntegration : "(id) - (userId)"
    Team ||--o{ InstalledIntegration : "(id) - (teamId)"
    Document |o--o{ Link : "(id) - (documentId)"
    Dataroom |o--o{ Link : "(id) - (dataroomId)"
    Domain |o--o{ Link : "(id) - (domainId)"
    Agreement |o--o{ Link : "(id) - (agreementId)"
    ViewerGroup |o--o{ Link : "(id) - (groupId)"
    PermissionGroup |o--o{ Link : "(id) - (permissionGroupId)"
    Team |o--o{ Link : "(id) - (teamId)"
    Brand |o--o{ Link : "(id) - (brandId)"
    User |o--o{ Link : "(id) - (ownerId)"
    Team ||--o{ LinkPreset : "(id) - (teamId)"
    Team ||--o{ VisitorGroup : "(id) - (teamId)"
    Link ||--o{ LinkVisitorGroup : "(id) - (linkId)"
    VisitorGroup ||--o{ LinkVisitorGroup : "(id) - (visitorGroupId)"
    Link ||--o{ CustomField : "(id) - (linkId)"
    View ||--o| CustomFieldResponse : "(id) - (viewId)"
    User ||--o{ NotificationPreference : "(id) - (userId)"
    Team ||--o{ NotificationPreference : "(id) - (teamId)"
    Document ||--o{ DocumentRedactionJob : "(id) - (documentId)"
    DocumentVersion ||--o{ DocumentRedactionJob : "(id) - (documentVersionId)"
    Team ||--o{ DocumentRedactionJob : "(id) - (teamId)"
    User ||--o{ DocumentRedactionJob : "(id) - (createdById)"
    DocumentRedactionJob ||--o{ DocumentRedaction : "(id) - (jobId)"
    User ||--o{ ReferralInvite : "(id) - (userId)"
    Team |o--o{ ReferralInvite : "(id) - (teamId)"
    User ||--o{ Account : "(id) - (userId)"
    User ||--o{ Session : "(id) - (userId)"
    User |o--o{ Domain : "(id) - (userId)"
    Team ||--o{ Domain : "(id) - (teamId)"
    Link ||--o{ View : "(id) - (linkId)"
    Document |o--o{ View : "(id) - (documentId)"
    Dataroom |o--o{ View : "(id) - (dataroomId)"
    Viewer |o--o{ View : "(id) - (viewerId)"
    ViewerGroup |o--o{ View : "(id) - (groupId)"
    Team |o--o{ View : "(id) - (teamId)"
    Dataroom |o--o{ Viewer : "(id) - (dataroomId)"
    Team ||--o{ Viewer : "(id) - (teamId)"
    View ||--o{ Reaction : "(id) - (viewId)"
    Team ||--o{ Invitation : "(id) - (teamId)"
    Team ||--o{ SentEmail : "(id) - (teamId)"
    Team ||--o{ Chat : "(id) - (teamId)"
    Document |o--o{ Chat : "(id) - (documentId)"
    Dataroom |o--o{ Chat : "(id) - (dataroomId)"
    Link |o--o{ Chat : "(id) - (linkId)"
    View |o--o{ Chat : "(id) - (viewId)"
    User |o--o{ Chat : "(id) - (userId)"
    Viewer |o--o{ Chat : "(id) - (viewerId)"
    Chat ||--o{ ChatMessage : "(id) - (chatId)"
    Link ||--o| Feedback : "(id) - (linkId)"
    Feedback ||--o{ FeedbackResponse : "(id) - (feedbackId)"
    View ||--o| FeedbackResponse : "(id) - (viewId)"
    Team ||--o{ Agreement : "(id) - (teamId)"
    Agreement ||--o{ AgreementResponse : "(id) - (agreementId)"
    View |o--o| AgreementResponse : "(id) - (viewId)"
    Team ||--o{ IncomingWebhook : "(id) - (teamId)"
    User ||--o{ RestrictedToken : "(id) - (userId)"
    Team ||--o{ RestrictedToken : "(id) - (teamId)"
    OAuthClient |o--o{ RestrictedToken : "(id) - (clientId)"
    Team ||--o{ Webhook : "(id) - (teamId)"
    Team ||--o{ Tag : "(id) - (teamId)"
    Tag ||--o{ TagItem : "(id) - (tagId)"
    Link |o--o{ TagItem : "(id) - (linkId)"
    Document |o--o{ TagItem : "(id) - (documentId)"
    Dataroom |o--o{ TagItem : "(id) - (dataroomId)"
    Viewer ||--o{ ViewerInvitation : "(id) - (viewerId)"
    Link ||--o{ ViewerInvitation : "(id) - (linkId)"
    ViewerGroup |o--o{ ViewerInvitation : "(id) - (groupId)"
    Brand |o--o{ Team : "(id) - (defaultBrandId)"
    User ||--o{ UserDataroom : "(id) - (userId)"
    Team ||--o{ UserDataroom : "(id) - (teamId)"
    Dataroom ||--o{ UserDataroom : "(id) - (dataroomId)"
    User ||--o{ UserTeam : "(id) - (userId)"
    Team ||--o{ UserTeam : "(id) - (teamId)"
    Link ||--o| Workflow : "(id) - (entryLinkId)"
    Team ||--o{ Workflow : "(id) - (teamId)"
    Workflow ||--o{ WorkflowStep : "(id) - (workflowId)"
    Workflow ||--o{ WorkflowExecution : "(id) - (workflowId)"
    WorkflowExecution ||--o{ WorkflowStepLog : "(id) - (executionId)"
    WorkflowStep ||--o{ WorkflowStepLog : "(id) - (workflowStepId)"
    DocumentAnnotation {
        string id PK
        string title
        json content
        int[] pages
        string documentId FK
        string teamId FK
        string createdById FK
        boolean isVisible
        datetime createdAt
        datetime updatedAt
    }
    AnnotationImage {
        string id PK
        string filename
        string url
        int size
        string mimeType
        string annotationId FK
        datetime createdAt
    }
    Brand {
        string id PK
        string logo
        boolean hideLogo
        string banner
        string brandColor
        string accentColor
        string accentButtonColor
        boolean applyAccentColorToDataroomView
        string welcomeMessage
        string ctaLabel
        string ctaUrl
        string privacyPolicyUrl
        string cardLayout
        boolean showFolderTree
        string viewerLayoutPreset
        string viewerHeaderStyle
        boolean hideFolderIconsInMain
        boolean customLinkPreviewEnabled
        string linkPreviewTitle
        string linkPreviewDescription
        string linkPreviewImage
        string linkPreviewFavicon
        string defaultLanguage
        string name UK
        string teamId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    Conversation {
        string id PK
        string title
        boolean isEnabled
        conversationvisibility visibilityMode
        string dataroomId FK
        string dataroomDocumentId FK
        int documentVersionNumber
        int documentPageNumber
        string linkId FK
        string viewerGroupId FK
        string questionId FK, UK
        string origin
        string category
        string priority
        string recipientViewerId FK, UK
        string initialViewId FK
        string teamId FK
        datetime lastMessageAt
        datetime createdAt
        datetime updatedAt
    }
    DataroomQuestion {
        string id PK
        string content
        string category
        string priority
        string status
        int orderIndex
        string importBatchId
        string dataroomId FK
        string teamId FK
        string createdByUserId FK
        string dataroomDocumentId FK
        int documentPageNumber
        datetime createdAt
        datetime updatedAt
    }
    DataroomQuestionAssignment {
        string id PK
        string questionId FK, UK
        string linkId FK, UK
        string groupId FK, UK
        string viewerId FK, UK
        string email UK
        datetime createdAt
    }
    ConversationParticipant {
        string id PK
        string conversationId FK, UK
        participantrole role
        string viewerId FK, UK
        string userId FK, UK
        boolean receiveNotifications
        datetime createdAt
    }
    Message {
        string id PK
        string content
        string conversationId FK
        string userId FK
        string viewerId FK
        string viewId FK
        boolean isRead
        boolean isInternalNote
        datetime createdAt
        datetime updatedAt
    }
    ConversationView {
        string id PK
        string conversationId FK, UK
        string viewId FK, UK
        datetime createdAt
    }
    DataroomFaqItem {
        string id PK
        string title
        string editedQuestion
        string originalQuestion
        string answer
        string description
        string dataroomId FK
        string linkId FK
        string dataroomDocumentId FK
        string sourceConversationId FK
        string questionMessageId FK
        string answerMessageId FK
        string teamId FK
        string publishedByUserId FK
        faqvisibility visibilityMode
        faqstatus status
        boolean isAnonymized
        int viewCount
        string[] tags
        int documentPageNumber
        int documentVersionNumber
        datetime createdAt
        datetime updatedAt
    }
    Dataroom {
        string id PK
        string pId UK
        string name
        string internalName
        string description
        string teamId FK
        string brandId FK
        boolean conversationsEnabled
        boolean agentsEnabled
        string vectorStoreId
        boolean requestListEnabled
        boolean enableChangeNotifications
        boolean enableVisitorUploadChangeNotifications
        defaultpermissionstrategy defaultPermissionStrategy
        boolean defaultShowBanner
        defaultpermissionstrategy defaultGroupPermissionStrategy
        rootitemaccess defaultRootItemAccess
        rootitemaccess defaultGroupRootItemAccess
        boolean allowBulkDownload
        boolean showLastUpdated
        boolean introductionEnabled
        json introductionContent
        boolean isFrozen
        datetime frozenAt
        string frozenBy FK
        string freezeArchiveUrl
        string freezeArchiveHash
        datetime createdAt
        datetime updatedAt
    }
    TaskList {
        string id PK
        string name
        int orderIndex
        string dataroomId FK
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    Task {
        string id PK
        string title
        string description
        string type
        string status
        string category
        datetime dueDate
        int orderIndex
        string taskListId FK
        string dataroomId FK
        string teamId FK
        string uploadFolderId FK
        string createdByUserId FK
        datetime lastActivityNotifiedAt
        datetime createdAt
        datetime updatedAt
    }
    TaskAssignment {
        string id PK
        string taskId FK, UK
        string viewerId FK, UK
        string groupId FK, UK
        string linkId FK, UK
        string email UK
        datetime notifiedAt
        datetime lastReminderAt
        datetime createdAt
        datetime updatedAt
    }
    TaskActivity {
        string id PK
        string taskId FK
        string type
        string comment
        string fromStatus
        string toStatus
        string userId FK
        string viewerId FK
        string viewId FK
        datetime createdAt
    }
    DataroomDocument {
        string id PK
        string dataroomId FK, UK
        string documentId FK, UK
        string folderId FK
        int orderIndex
        string hierarchicalIndex
        string vectorStoreFileId
        datetime createdAt
        datetime updatedAt
    }
    DataroomFolder {
        string id PK
        string name
        string path UK
        string parentId FK
        string icon
        string color
        string dataroomId FK, UK
        int orderIndex
        string hierarchicalIndex
        datetime createdAt
        datetime updatedAt
    }
    DataroomBrand {
        string id PK
        string logo
        boolean hideLogo
        string banner
        string brandColor
        string accentColor
        string accentButtonColor
        boolean applyAccentColorToDataroomView
        string welcomeMessage
        string cardLayout
        boolean showFolderTree
        string viewerLayoutPreset
        string viewerHeaderStyle
        boolean hideFolderIconsInMain
        string ctaLabel
        string ctaUrl
        boolean customLinkPreviewEnabled
        string linkPreviewTitle
        string linkPreviewDescription
        string linkPreviewImage
        string linkPreviewFavicon
        string defaultLanguage
        string dataroomId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    ViewerGroup {
        string id PK
        string name
        string[] domains
        boolean allowAll
        string dataroomId FK
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    ViewerGroupMembership {
        string id PK
        string viewerId FK, UK
        string groupId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    ViewerGroupAccessControls {
        string id PK
        string groupId FK, UK
        string itemId UK
        itemtype itemType
        boolean canView
        boolean canDownload
        datetime createdAt
        datetime updatedAt
    }
    PermissionGroup {
        string id PK
        string name
        string description
        string dataroomId FK
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    PermissionGroupAccessControls {
        string id PK
        string groupId FK, UK
        string itemId UK
        itemtype itemType
        boolean canView
        boolean canDownload
        boolean canDownloadOriginal
        datetime createdAt
        datetime updatedAt
    }
    Document {
        string id PK
        string name
        string description
        string file
        string originalFile
        string type
        string contentType
        documentstoragetype storageType
        int numPages
        string teamId FK
        string ownerId FK
        boolean assistantEnabled
        boolean advancedExcelEnabled
        boolean agentsEnabled
        boolean downloadOnly
        boolean hiddenInAllDocuments
        datetime createdAt
        datetime updatedAt
        string folderId FK
        boolean isExternalUpload
    }
    DocumentVersion {
        string id PK
        int versionNumber UK
        string documentId FK, UK
        string file
        string originalFile
        string type
        string contentType
        bigint fileSize
        documentstoragetype storageType
        int numPages
        boolean isPrimary
        boolean isVertical
        string fileId
        string vectorStoreFileId
        boolean hasPages
        int length
        datetime createdAt
        datetime updatedAt
    }
    DocumentPage {
        string id PK
        string versionId FK, UK
        int pageNumber UK
        string[] embeddedLinks
        json pageLinks
        json metadata
        string file
        documentstoragetype storageType
        datetime createdAt
        datetime updatedAt
    }
    Folder {
        string id PK
        string name
        string path UK
        string parentId FK
        boolean hiddenInAllDocuments
        string icon
        string color
        string teamId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    DocumentUpload {
        string id PK
        string documentId FK
        string teamId FK
        string viewerId FK
        string viewId FK
        string linkId FK
        string dataroomId FK
        string dataroomDocumentId FK
        string taskId FK
        string originalFilename
        bigint fileSize
        int numPages
        string mimeType
        datetime uploadedAt
        datetime createdAt
        datetime updatedAt
    }
    PendingDocumentUpload {
        string id PK
        string uploadId UK
        string teamId
        string tokenId
        string key
        string contentType
        documentstoragetype storageType
        datetime expiresAt
        datetime consumedAt
        datetime createdAt
    }
    Integration {
        string id PK
        string name
        string slug UK
        string description
        string readme
        string developer
        string website
        string logo
        json screenshots
        boolean verified
        string installUrl
        string category
        boolean comingSoon
        datetime createdAt
        datetime updatedAt
    }
    InstalledIntegration {
        string id PK
        json credentials
        json configuration
        boolean enabled
        string integrationId FK, UK
        string userId FK
        string teamId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    jackson_index {
        int id PK
        string key
        string storeKey
    }
    jackson_store {
        string key PK
        string value
        string iv
        string tag
        string namespace
        datetime createdAt
        datetime modifiedAt
    }
    jackson_ttl {
        string key PK
        bigint expiresAt
    }
    Link {
        string id PK
        string documentId FK
        string dataroomId FK
        linktype linkType
        string url UK
        string name
        string slug UK
        datetime expiresAt
        string password
        string[] allowList
        string[] denyList
        boolean emailProtected
        boolean emailAuthenticated
        boolean allowDownload
        boolean isArchived
        datetime deletedAt
        string domainId FK
        string domainSlug UK
        datetime createdAt
        datetime updatedAt
        boolean enableNotification
        boolean enableFeedback
        boolean enableQuestion
        boolean enableScreenshotProtection
        boolean enableConfidentialView
        boolean enableAgreement
        string agreementId FK
        boolean showBanner
        boolean enableWatermark
        json watermarkConfig
        linkaudiencetype audienceType
        string groupId FK
        string permissionGroupId FK
        string metaTitle
        string metaDescription
        string metaImage
        string metaFavicon
        boolean enableCustomMetatag
        string welcomeMessage
        boolean enableConversation
        boolean enableAIAgents
        boolean enableUpload
        boolean isFileRequestOnly
        string uploadFolderId "Deprecated: replaced by `uploadFolderIds`. Retained in the DB only so old rows can be inspected; no application code reads or writes this column."
        string[] uploadFolderIds
        boolean enableIndexFile
        string teamId FK
        string brandId FK
        string ownerId FK
    }
    LinkPreset {
        string id PK
        string name
        string teamId FK
        string pId UK
        boolean enableCustomMetaTag
        string metaTitle
        string metaDescription
        string metaImage
        string metaFavicon
        boolean enableNotification
        boolean emailProtected
        boolean emailAuthenticated
        boolean allowDownload
        boolean enableAllowList
        string[] allowList
        boolean enableDenyList
        string[] denyList
        int expiresIn
        boolean enableScreenshotProtection
        boolean enableConfidentialView
        datetime expiresAt
        boolean enablePassword
        string password
        boolean enableWatermark
        json watermarkConfig
        boolean enableAgreement
        string agreementId
        boolean enableCustomFields
        json customFields
        boolean showBanner
        string welcomeMessage
        boolean isDefault
        datetime createdAt
        datetime updatedAt
    }
    VisitorGroup {
        string id PK
        string name
        string[] emails
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    LinkVisitorGroup {
        string id PK
        string linkId FK, UK
        string visitorGroupId FK, UK
        datetime createdAt
    }
    CustomField {
        string id PK
        datetime createdAt
        datetime updatedAt
        customfieldtype type
        string identifier
        string label
        string placeholder
        boolean required
        boolean disabled
        string linkId FK
        int orderIndex
    }
    CustomFieldResponse {
        string id PK
        json data
        string viewId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    NotificationPreference {
        string id PK
        string userId FK, UK
        string teamId FK, UK
        string type UK
        string frequency
        string scope
        datetime createdAt
        datetime updatedAt
    }
    OAuthClient {
        string id PK
        string clientId UK
        string clientName
        string clientUri
        string logoUri
        string[] redirectUris
        string[] grantTypes
        string scopes
        string tokenAuthMethod
        string hashedSecret
        string registrationType
        string metadataUrl
        datetime createdAt
        datetime updatedAt
    }
    OAuthClientApproval {
        string id PK
        string userId UK
        string clientId UK
        datetime approvedAt
        datetime revokedAt
        string clientMetadataHash
    }
    OAuthGrantTeam {
        string id PK
        string grantId UK
        string teamId
        string userId
        datetime createdAt
    }
    OAuthRecord {
        string id PK
        string kind
        json payload
        string grantId
        string userCode
        string uid
        datetime expiresAt
        datetime consumedAt
        datetime createdAt
    }
    DocumentRedactionJob {
        string id PK
        string documentId FK
        string documentVersionId FK
        string teamId FK
        string createdById FK
        string status
        string[] customTerms
        string[] reasons
        string resultVersionId
        string error
        string detectRunId
        string applyRunId
        datetime createdAt
        datetime updatedAt
    }
    DocumentRedaction {
        string id PK
        string jobId FK
        int pageNumber
        float x
        float y
        float width
        float height
        string textPreview
        string category
        string confidence
        string reason
        string source
        string status
        datetime createdAt
        datetime updatedAt
    }
    ReferralInvite {
        string id PK
        string userId FK
        string teamId FK
        string scopeId UK "* * Whose cap and discount this invite counts against — the fund's team, or the * sender alone. One non-null key rather than a pair of nullable ones: on a * nullable teamId every personal invite is distinct to Postgres, so the two * keys guarded different scopes and a member could not invite a company both * for their fund and for themselves."
        string email UK
        string referralLink
        datetime sentAt
    }
    Account {
        string id PK
        string userId FK
        string type
        string provider UK
        string providerAccountId UK
        string refresh_token
        string access_token
        int expires_at
        string token_type
        string scope
        string id_token
        string session_state
    }
    Session {
        string id PK
        string sessionToken UK
        string userId FK
        datetime expires
    }
    User {
        string id PK
        string name
        string email UK
        datetime emailVerified
        string image
        datetime createdAt
        string contactId
        string plan
        string stripeId UK
        string subscriptionId UK
        datetime startsAt
        datetime endsAt
        referraltrack referralTrack
        datetime referralTrackSelectedAt
        json referralProfile
    }
    VerificationToken {
        string identifier UK
        string token UK
        datetime expires
    }
    Domain {
        string id PK
        string slug UK
        string userId FK
        string teamId FK
        boolean verified
        boolean isDefault
        string redirectUrl
        datetime lastChecked
        datetime createdAt
        datetime updatedAt
    }
    View {
        string id PK
        string linkId FK
        string documentId FK
        string dataroomId FK
        string dataroomViewId
        string viewerEmail
        string viewerName
        boolean verified
        datetime viewedAt
        datetime downloadedAt
        downloadtype downloadType
        json downloadMetadata
        viewtype viewType
        string viewerId FK
        string groupId FK
        boolean isArchived
        string teamId FK
    }
    Viewer {
        string id PK
        string email UK
        boolean verified
        datetime invitedAt
        json notificationPreferences
        string dataroomId FK
        string teamId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    Reaction {
        string id PK
        string viewId FK
        int pageNumber
        string type
        datetime createdAt
    }
    Invitation {
        string email UK
        datetime expires
        string teamId FK, UK
        datetime createdAt
        string token UK
        role role
        string[] dataroomIds
    }
    SentEmail {
        string id PK
        emailtype type
        string recipient
        boolean marketing
        datetime createdAt
        string teamId FK
        string domainSlug
    }
    Chat {
        string id PK
        string title
        string teamId FK
        string documentId FK
        string dataroomId FK
        string linkId FK
        string viewId FK
        string userId FK
        string viewerId FK
        string vectorStoreId
        datetime createdAt
        datetime updatedAt
        datetime lastMessageAt
    }
    ChatMessage {
        string id PK
        string chatId FK
        string role
        string content
        json metadata
        datetime createdAt
    }
    Feedback {
        string id PK
        string linkId FK, UK
        json data
        datetime createdAt
        datetime updatedAt
    }
    FeedbackResponse {
        string id PK
        string feedbackId FK
        json data
        string viewId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    Agreement {
        string id PK
        string name
        string content
        string contentType
        string signingProvider
        string signingExternalId UK
        string signingEnvelopeId
        string signingTemplateId
        boolean requireName
        string teamId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        string deletedBy
    }
    AgreementResponse {
        string id PK
        string agreementId FK
        string viewId FK, UK
        string linkId
        string signerEmail
        string signerName
        string signingStatus
        string signingExternalId UK
        string signingEnvelopeId
        int signingDocumentId
        datetime signedAt
        datetime completedAt
        string signedFileKey
        string signedFileName
        documentstoragetype signedFileStorageType
        datetime createdAt
        datetime updatedAt
    }
    IncomingWebhook {
        string id PK
        string externalId UK
        string name
        string secret
        string source
        string actions
        int consecutiveFailures
        datetime lastFailedAt
        datetime disabledAt
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    RestrictedToken {
        string id PK
        string name
        string hashedKey UK
        string partialKey
        string scopes
        string mode
        string source
        string subjectType
        string clientId FK
        datetime expires
        datetime lastUsed
        int rateLimit
        string userId FK
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    Webhook {
        string id PK
        string pId UK
        string name
        string url
        string secret
        json triggers
        string teamId FK
        datetime createdAt
        datetime updatedAt
    }
    YearInReview {
        string id PK
        string teamId
        string status
        int attempts
        datetime lastAttempted
        string error
        json stats
        datetime createdAt
        datetime updatedAt
    }
    Tag {
        string id PK
        string name UK
        string color
        string description
        string teamId FK, UK
        string createdBy
        datetime createdAt
        datetime updatedAt
    }
    TagItem {
        string id PK
        string tagId FK
        tagtype itemType
        string linkId FK
        string documentId FK
        string dataroomId FK
        string taggedBy
        datetime createdAt
        datetime updatedAt
    }
    ViewerInvitation {
        string id PK
        string viewerId FK
        string linkId FK
        string groupId FK
        string invitedBy
        string customMessage
        datetime sentAt
        invitationstatus status
        datetime createdAt
    }
    Team {
        string id PK
        string name
        string slug UK
        string defaultBrandId FK
        string plan
        string stripeId UK
        string subscriptionId UK
        datetime startsAt
        datetime endsAt
        datetime pausedAt
        datetime pauseStartsAt
        datetime pauseEndsAt
        datetime cancelledAt
        datetime trialStartedAt
        datetime trialEndsAt
        string trialReminderRunId
        string trialExpiredRunId
        json limits
        boolean enableExcelAdvancedMode
        boolean replicateDataroomFolders
        string timezone
        boolean ssoEnabled
        string ssoEmailDomain UK
        datetime ssoEnforcedAt
        json surveyData
        boolean agentsEnabled
        string vectorStoreId
        datetime createdAt
        datetime updatedAt
        string[] ignoredDomains
        string[] globalBlockList
        referraltrack referralTrack
        datetime referralTrackSelectedAt
        json referralProfile
    }
    UserDataroom {
        string userId PK, FK
        string teamId FK
        string dataroomId PK, FK
        datetime createdAt
    }
    UserTeam {
        role role
        string status
        string userId PK, FK
        string teamId PK, FK
        datetime blockedAt
        json notificationPreferences
    }
    Workflow {
        string id PK
        string name
        string description
        string entryLinkId FK, UK
        string teamId FK
        boolean isActive
        datetime createdAt
        datetime updatedAt
    }
    WorkflowStep {
        string id PK
        string workflowId FK, UK
        string name
        int stepOrder UK
        workflowsteptype stepType
        json conditions
        json actions
        datetime createdAt
        datetime updatedAt
    }
    WorkflowExecution {
        string id PK
        string workflowId FK
        string visitorEmail
        string visitorIp
        executionstatus status
        datetime startedAt
        datetime completedAt
        json result
        json metadata
    }
    WorkflowStepLog {
        string id PK
        string executionId FK
        string workflowStepId FK
        boolean conditionsMatched
        json conditionResults
        json actionsExecuted
        datetime executedAt
        int duration
        string error
    }
```