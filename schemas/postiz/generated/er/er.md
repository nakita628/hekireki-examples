```mermaid
erDiagram
    Organization ||--o{ Tags : "(id) - (orgId)"
    Post ||--o{ TagsPosts : "(id) - (postId)"
    Tags ||--o{ TagsPosts : "(id) - (tagId)"
    Media |o--o{ User : "(id) - (pictureId)"
    Organization ||--o{ UsedCodes : "(id) - (orgId)"
    Organization ||--o{ UserOrganization : "(id) - (organizationId)"
    User ||--o{ UserOrganization : "(id) - (userId)"
    Organization ||--o{ GitHub : "(id) - (organizationId)"
    User ||--o{ ItemUser : "(id) - (userId)"
    Organization ||--o{ Media : "(id) - (organizationId)"
    Media |o--o{ SocialMediaAgency : "(id) - (logoId)"
    User ||--o| SocialMediaAgency : "(id) - (userId)"
    SocialMediaAgency ||--o{ SocialMediaAgencyNiche : "(id) - (agencyId)"
    Organization ||--o{ Credits : "(id) - (organizationId)"
    Organization ||--o{ Clipping : "(id) - (organizationId)"
    Clipping ||--o{ ClippingClip : "(id) - (clippingId)"
    Organization ||--o| Subscription : "(id) - (organizationId)"
    Organization ||--o{ Customer : "(id) - (orgId)"
    Customer |o--o{ Integration : "(id) - (customerId)"
    Organization ||--o{ Integration : "(id) - (organizationId)"
    Organization ||--o{ Signatures : "(id) - (organizationId)"
    Organization ||--o{ Comments : "(id) - (organizationId)"
    Post ||--o{ Comments : "(id) - (postId)"
    User |o--o{ Comments : "(id) - (userId)"
    Comments |o--o{ Comments : "(id) - (parentId)"
    Integration ||--o{ Post : "(id) - (integrationId)"
    Messages |o--o{ Post : "(id) - (lastMessageId)"
    Organization ||--o{ Post : "(id) - (organizationId)"
    Post |o--o{ Post : "(id) - (parentPostId)"
    Orders |o--o{ Post : "(id) - (submittedForOrderId)"
    Organization |o--o{ Post : "(id) - (submittedForOrganizationId)"
    Organization ||--o{ Notifications : "(id) - (organizationId)"
    User ||--o{ MessagesGroup : "(id) - (buyerId)"
    Organization ||--o{ MessagesGroup : "(id) - (buyerOrganizationId)"
    User ||--o{ MessagesGroup : "(id) - (sellerId)"
    Orders ||--o{ PayoutProblems : "(id) - (orderId)"
    Post |o--o{ PayoutProblems : "(id) - (postId)"
    User ||--o{ PayoutProblems : "(id) - (userId)"
    User ||--o{ Orders : "(id) - (buyerId)"
    MessagesGroup ||--o{ Orders : "(id) - (messageGroupId)"
    User ||--o{ Orders : "(id) - (sellerId)"
    Integration ||--o{ OrderItems : "(id) - (integrationId)"
    Orders ||--o{ OrderItems : "(id) - (orderId)"
    MessagesGroup ||--o{ Messages : "(id) - (groupId)"
    Integration ||--o{ Plugs : "(id) - (integrationId)"
    Organization ||--o{ Plugs : "(id) - (organizationId)"
    Integration ||--o{ ExisingPlugData : "(id) - (integrationId)"
    Integration ||--o{ IntegrationsWebhooks : "(id) - (integrationId)"
    Webhooks ||--o{ IntegrationsWebhooks : "(id) - (webhookId)"
    Organization ||--o{ Webhooks : "(id) - (organizationId)"
    Organization ||--o{ AutoPost : "(id) - (organizationId)"
    Organization ||--o{ Sets : "(id) - (organizationId)"
    Organization ||--o{ ThirdParty : "(id) - (organizationId)"
    Organization ||--o{ Errors : "(id) - (organizationId)"
    Post ||--o{ Errors : "(id) - (postId)"
    Organization |o--o{ OAuthApp : "(id) - (organizationId)"
    Media |o--o{ OAuthApp : "(id) - (pictureId)"
    OAuthApp ||--o{ OAuthAuthorization : "(id) - (oauthAppId)"
    User ||--o{ OAuthAuthorization : "(id) - (userId)"
    Organization ||--o{ OAuthAuthorization : "(id) - (organizationId)"
    OAuthApp ||--o{ OAuthSelfHostedAuthorization : "(id) - (oauthAppId)"
    Organization {
        string id PK
        string name
        string description
        string apiKey
        string paymentId
        datetime streakSince
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        boolean allowTrial
        boolean isTrailing
        shortlinkpreference shortlink
    }
    Tags {
        string id PK
        string name
        string color
        string orgId FK
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
    }
    TagsPosts {
        string postId PK, FK, UK
        string tagId PK, FK, UK
        datetime createdAt
        datetime updatedAt
    }
    User {
        string id PK
        string email UK
        string password
        provider providerName UK
        string name
        string lastName
        boolean isSuperAdmin
        string bio
        int audience
        string pictureId FK
        string providerId
        int timezone
        string timezoneName
        datetime createdAt
        datetime updatedAt
        datetime lastReadNotifications
        string inviteId
        boolean activated
        string account
        boolean connectedAccount
        datetime lastOnline
        string ip
        string agent
        datetime deletedAt
        boolean sendSuccessEmails
        boolean sendFailureEmails
        boolean sendStreakEmails
    }
    UsedCodes {
        string id PK
        string code
        string orgId FK
        datetime createdAt
        datetime updatedAt
    }
    UserOrganization {
        string id PK
        string userId FK, UK
        string organizationId FK, UK
        boolean disabled
        role role
        datetime createdAt
        datetime updatedAt
    }
    GitHub {
        string id PK
        string login
        string name
        string token
        string jobId
        string organizationId FK
        datetime createdAt
        datetime updatedAt
    }
    Trending {
        string id PK
        string trendingList
        string language UK
        string hash
        datetime date
        datetime createdAt
        datetime updatedAt
    }
    TrendingLog {
        string id PK
        string language
        datetime date
    }
    ItemUser {
        string id PK
        string userId FK, UK
        string key UK
    }
    Star {
        string id PK
        int stars
        int totalStars
        int forks
        int totalForks
        string login UK
        datetime date UK
        datetime createdAt
        datetime updatedAt
    }
    Media {
        string id PK
        string name
        string originalName
        string path
        string organizationId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        int fileSize
        string type
        string thumbnail
        string alt
        int thumbnailTimestamp
        string status
        string processingError
    }
    SocialMediaAgency {
        string id PK
        string userId FK, UK
        string name
        string logoId FK
        string website
        string slug
        string facebook
        string instagram
        string twitter
        string linkedIn
        string youtube
        string tiktok
        string otherSocialMedia
        string shortDescription
        string description
        boolean approved
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    SocialMediaAgencyNiche {
        string agencyId PK, FK
        string niche PK
    }
    Credits {
        string id PK
        string organizationId FK
        int credits
        datetime createdAt
        datetime updatedAt
        string type
    }
    Clipping {
        string id PK
        string organizationId FK
        string url
        string status
        string error
        string title
        string thumbnail
        int duration
        int maxClips
        string fit
        string integrations
        string creditsId
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
    }
    ClippingClip {
        string id PK
        string clippingId FK
        string title
        string content
        float start
        float end
        int trimStart
        string status
        string error
        string mediaId
        string path
        string thumbnail
        datetime draftedAt
        datetime createdAt
        datetime updatedAt
    }
    Subscription {
        string id PK
        string organizationId FK, UK
        subscriptiontier subscriptionTier
        string provider
        string identifier
        datetime cancelAt
        period period
        int totalChannels
        boolean isLifetime
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    Customer {
        string id PK
        string name UK
        string orgId FK, UK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt UK
    }
    Integration {
        string id PK
        string internalId UK
        string organizationId FK, UK
        string name
        string customName
        string picture
        string providerIdentifier
        string type
        string token
        boolean disabled
        datetime tokenExpiration
        string refreshToken
        string profile
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
        boolean inBetweenSteps
        boolean refreshNeeded
        string postingTimes
        string customInstanceDetails
        string customerId FK
        string rootInternalId
        string additionalSettings
    }
    Signatures {
        string id PK
        string organizationId FK
        string content
        boolean autoAdd
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    Comments {
        string id PK
        string content
        string organizationId FK
        string postId FK
        string userId FK
        string displayName
        string parentId FK
        int anchorStart
        int anchorEnd
        string anchorQuote
        datetime resolvedAt
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    Post {
        string id PK
        state state
        datetime publishDate
        string organizationId FK
        string integrationId FK
        string content
        int delay
        string group
        string batchId
        string title
        string description
        string parentPostId FK
        string releaseId
        string releaseURL
        string settings
        string image
        string submittedForOrderId FK
        string submittedForOrganizationId FK
        approved_submit_for_order approvedSubmitForOrder
        creationmethod creationMethod
        string lastMessageId FK
        int intervalInDays
        string error
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    Notifications {
        string id PK
        string organizationId FK
        string content
        string link
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    MessagesGroup {
        string id PK
        string buyerOrganizationId FK
        string buyerId FK, UK
        string sellerId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    PayoutProblems {
        string id PK
        string status
        string orderId FK
        string userId FK
        string postId FK
        int amount
        datetime createdAt
        datetime updatedAt
    }
    Orders {
        string id PK
        string buyerId FK
        string sellerId FK
        orderstatus status
        string messageGroupId FK
        string captureId
        datetime createdAt
        datetime updatedAt
    }
    OrderItems {
        string id PK
        string orderId FK
        string integrationId FK
        int quantity
        int price
    }
    Messages {
        string id PK
        from from
        string content
        string groupId FK
        string special
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    Plugs {
        string id PK
        string organizationId FK
        string plugFunction UK
        string data
        string integrationId FK, UK
        boolean activated
    }
    ExisingPlugData {
        string id PK
        string integrationId FK, UK
        string methodName UK
        string value UK
    }
    PopularPosts {
        string id PK
        string category
        string topic
        string content
        string hook
        datetime createdAt
        datetime updatedAt
    }
    IntegrationsWebhooks {
        string integrationId PK, FK, UK
        string webhookId PK, FK, UK
    }
    Webhooks {
        string id PK
        string name
        string organizationId FK
        string url
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
    }
    AutoPost {
        string id PK
        string organizationId FK
        string title
        string content
        boolean onSlot
        boolean syncLast
        string url
        string lastUrl
        boolean active
        boolean addPicture
        boolean generateContent
        string integrations
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
    }
    Sets {
        string id PK
        string organizationId FK
        string name
        string content
        datetime createdAt
        datetime updatedAt
    }
    ThirdParty {
        string id PK
        string organizationId FK, UK
        string identifier
        string name
        string internalId UK
        string apiKey
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    Errors {
        string id PK
        string message
        string platform
        string organizationId FK
        datetime createdAt
        datetime updatedAt
        string postId FK
        string body
    }
    Mentions {
        string name PK
        string username PK
        string platform PK
        datetime createdAt
        datetime updatedAt
        string image PK
    }
    mastra_messages {
        string id PK
        string thread_id
        string content
        string role
        string type
        datetime createdAt
        string resourceId
        datetime createdAtZ
    }
    mastra_resources {
        string id PK
        string workingMemory
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_scorers {
        string id PK
        string scorerId
        string traceId
        string runId
        json scorer
        json preprocessStepResult
        json extractStepResult
        json analyzeStepResult
        float score
        string reason
        json metadata
        string preprocessPrompt
        string extractPrompt
        string generateScorePrompt
        string generateReasonPrompt
        string analyzePrompt
        string reasonPrompt
        json input
        json output
        json additionalContext
        json runtimeContext
        string entityType
        json entity
        string entityId
        string source
        string resourceId
        string threadId
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
        string spanId
        json requestContext
        string organizationId
        string projectId
        string batchId
        string datasetId
        string datasetItemId
    }
    mastra_threads {
        string id PK
        string resourceId
        string title
        string metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_traces {
        string id PK
        string parentSpanId
        string name
        string traceId
        string scope
        int kind
        json attributes
        json status
        json events
        json links
        string other
        bigint startTime
        bigint endTime
        datetime createdAt
        datetime createdAtZ
    }
    mastra_workflow_snapshot {
        string workflow_name UK
        string run_id UK
        string resourceId
        string snapshot
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_agent_versions {
        string id PK
        string agentId
        int versionNumber
        string name
        string description
        string instructions
        json model
        json tools
        json defaultOptions
        json workflows
        json agents
        json integrationTools
        json inputProcessors
        json outputProcessors
        json memory
        json scorers
        json mcpClients
        json requestContextSchema
        json workspace
        json skills
        string skillsFormat
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
        json durable
        json browser
        json toolProviders
    }
    mastra_agents {
        string id PK
        string status
        string activeVersionId
        string authorId
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
        string visibility
        int favoriteCount
    }
    mastra_background_tasks {
        string id PK
        string tool_call_id
        string tool_name
        string agent_id
        string run_id
        string thread_id
        string resource_id
        string status
        json args
        json result
        json error
        json suspend_payload
        int retry_count
        int max_retries
        int timeout_ms
        datetime createdAt
        datetime startedAt
        datetime suspendedAt
        datetime completedAt
        datetime createdAtZ
        datetime startedAtZ
        datetime suspendedAtZ
        datetime completedAtZ
    }
    mastra_channel_config {
        string platform PK
        json data
        datetime updatedAt
        datetime updatedAtZ
    }
    mastra_channel_installations {
        string id PK
        string platform
        string agentId
        string status
        string webhookId UK
        json data
        string configHash
        string error
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_dataset_items {
        string id PK
        string datasetId
        int datasetVersion PK
        int validTo
        boolean isDeleted
        json input
        json groundTruth
        json requestContext
        json metadata
        json source
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
        json expectedTrajectory
        string organizationId
        string projectId
        json toolMocks
        string unmockedToolPolicy
        json scorerIds
        string externalId
    }
    mastra_dataset_versions {
        string id PK
        string datasetId UK
        int version UK
        datetime createdAt
        datetime createdAtZ
    }
    mastra_datasets {
        string id PK
        string name
        string description
        json metadata
        json inputSchema
        json groundTruthSchema
        json requestContextSchema
        json tags
        string targetType
        json targetIds
        json scorerIds
        int version
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
        string organizationId
        string projectId
        string candidateKey
        string candidateId
    }
    mastra_experiment_results {
        string id PK
        string experimentId UK
        string itemId UK
        int itemDatasetVersion
        json input
        json output
        json groundTruth
        json error
        datetime startedAt
        datetime completedAt
        int retryCount
        string traceId
        string status
        json tags
        datetime createdAt
        datetime startedAtZ
        datetime completedAtZ
        datetime createdAtZ
        string comment
        json toolMockReport
        json metadata
        string organizationId
        string projectId
        int attempt UK
    }
    mastra_experiments {
        string id PK
        string name
        string description
        json metadata
        string datasetId
        int datasetVersion
        string targetType
        string targetId
        string status
        int totalItems
        int succeededCount
        int failedCount
        int skippedCount
        datetime startedAt
        datetime completedAt
        string agentVersion
        datetime createdAt
        datetime updatedAt
        datetime startedAtZ
        datetime completedAtZ
        datetime createdAtZ
        datetime updatedAtZ
        string organizationId
        string projectId
        json provenance
        json runnerAttestation
        string experimentSetId
        string comparisonId
        string variantId
        int trialIndex
        json scorerIds
    }
    mastra_favorites {
        string userId PK
        string entityType PK
        string entityId PK
        datetime createdAt
        datetime createdAtZ
    }
    mastra_knowledge_activity {
        string id PK
        string action
        string recordType
        string recordId
        json scope
        string scopeKey
        string sourceThreadId
        datetime createdAt
        datetime createdAtZ
    }
    mastra_knowledge_cursors {
        string sourceThreadId PK
        string agent PK
        string lastKnowledgeId
        datetime updatedAt
        datetime updatedAtZ
    }
    mastra_knowledge_mentions {
        string sourceType PK
        string sourceId PK
        string recordId PK
    }
    mastra_knowledge_nodes {
        string id PK
        string type UK
        string name
        string canonicalName UK
        string kind
        string content
        string description
        json scope
        string scopeKey UK
        int version
        string mergedInto
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_knowledge_records {
        string id PK
        string node
        string text
        json scope
        string scopeKey
        string sourceThreadId
        datetime capturedAt
        datetime when
        string maxScope
        json metadata
        datetime deletedAt
        string deletedBy
        datetime capturedAtZ
        datetime whenZ
        datetime deletedAtZ
    }
    mastra_knowledge_semantic_outbox {
        string id PK
        string idempotencyKey UK
        string documentId
        string documentType
        string operation
        json scope
        string scopeKey
        string status
        int attempts
        datetime availableAt
        datetime claimedAt
        string claimedBy
        datetime createdAt
        datetime completedAt
        datetime availableAtZ
        datetime claimedAtZ
        datetime createdAtZ
        datetime completedAtZ
    }
    mastra_mcp_client_versions {
        string id PK
        string mcpClientId UK
        int versionNumber UK
        string name
        string description
        json servers
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
    }
    mastra_mcp_clients {
        string id PK
        string status
        string activeVersionId
        string authorId
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_mcp_server_versions {
        string id PK
        string mcpServerId UK
        int versionNumber UK
        string name
        string version
        string description
        string instructions
        json repository
        string releaseDate
        boolean isLatest
        string packageCanonical
        json tools
        json agents
        json workflows
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
    }
    mastra_mcp_servers {
        string id PK
        string status
        string activeVersionId
        string authorId
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_observational_memory {
        string id PK
        string lookupKey
        string scope
        string resourceId
        string threadId
        string activeObservations
        string activeObservationsPendingUpdate
        string originType
        string config
        int generationCount
        datetime lastObservedAt
        datetime lastReflectionAt
        int pendingMessageTokens
        int totalTokensObserved
        int observationTokenCount
        boolean isObserving
        boolean isReflecting
        json observedMessageIds
        string observedTimezone
        string bufferedObservations
        int bufferedObservationTokens
        json bufferedMessageIds
        string bufferedReflection
        int bufferedReflectionTokens
        int bufferedReflectionInputTokens
        int reflectedObservationLineCount
        json bufferedObservationChunks
        boolean isBufferingObservation
        boolean isBufferingReflection
        int lastBufferedAtTokens
        datetime lastBufferedAtTime
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime lastObservedAtZ
        datetime lastReflectionAtZ
        datetime lastBufferedAtTimeZ
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_prompt_block_versions {
        string id PK
        string blockId UK
        int versionNumber UK
        string name
        string description
        string content
        json rules
        json requestContextSchema
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
    }
    mastra_prompt_blocks {
        string id PK
        string status
        string activeVersionId
        string authorId
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_schedule_triggers {
        string id PK
        string schedule_id
        string run_id
        bigint scheduled_fire_at
        bigint actual_fire_at
        string outcome
        string error
        string trigger_kind
        string parent_trigger_id
        json metadata
    }
    mastra_schedules {
        string id PK
        json target
        string cron
        string timezone
        string status
        bigint next_fire_at
        bigint last_fire_at
        string last_run_id
        bigint created_at
        bigint updated_at
        json metadata
        string owner_type
        string owner_id
    }
    mastra_scorer_definition_versions {
        string id PK
        string scorerDefinitionId UK
        int versionNumber UK
        string name
        string description
        string type
        json model
        string instructions
        json scoreRange
        json presetConfig
        json defaultSampling
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
    }
    mastra_scorer_definitions {
        string id PK
        string status
        string activeVersionId
        string authorId
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
        string organizationId
        string projectId
    }
    mastra_skill_blobs {
        string hash PK
        string content
        int size
        string mimeType
        datetime createdAt
        datetime createdAtZ
    }
    mastra_skill_versions {
        string id PK
        string skillId UK
        int versionNumber UK
        string name
        string description
        string instructions
        string license
        json compatibility
        json source
        json references
        json scripts
        json assets
        json metadata
        json tree
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
        json files
    }
    mastra_skills {
        string id PK
        string status
        string activeVersionId
        string authorId
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
        string visibility
        int favoriteCount
    }
    mastra_thread_state {
        string threadId PK
        string type PK
        json value
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_tool_provider_connections {
        string authorId PK
        string providerId PK
        string connectionId PK
        string toolkit
        string label
        string scope
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_workflow_definitions {
        string id PK
        string description
        json metadata
        json inputSchema
        json outputSchema
        json stateSchema
        json requestContextSchema
        json graph
        json schedule
        string status
        string source
        string authorId
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    mastra_workspace_versions {
        string id PK
        string workspaceId UK
        int versionNumber UK
        string name
        string description
        json filesystem
        json sandbox
        json mounts
        json search
        json skills
        json tools
        boolean autoSync
        int operationTimeout
        json changedFields
        string changeMessage
        datetime createdAt
        datetime createdAtZ
    }
    mastra_workspaces {
        string id PK
        string status
        string activeVersionId
        string authorId
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime createdAtZ
        datetime updatedAtZ
    }
    OAuthApp {
        string id PK
        string organizationId FK, UK
        string name
        string description
        string pictureId FK
        string redirectUrl
        string redirectUris
        string clientId UK
        string clientSecret
        boolean dynamic
        string tokenEndpointAuthMethod
        datetime createdAt
        datetime updatedAt
        datetime deletedAt UK
    }
    OAuthAuthorization {
        string id PK
        string oauthAppId FK, UK
        string userId FK, UK
        string organizationId FK, UK
        string accessToken
        string authorizationCode
        datetime codeExpiresAt
        string codeChallenge
        string codeChallengeMethod
        string redirectUri
        datetime revokedAt
        datetime createdAt
        datetime updatedAt
    }
    OAuthSelfHostedAuthorization {
        string id PK
        string oauthAppId FK
        string mcpUrl
        string apiKey
        string email
        string accessToken
        string authorizationCode
        datetime codeExpiresAt
        string codeChallenge
        string codeChallengeMethod
        string redirectUri
        datetime revokedAt
        datetime createdAt
        datetime updatedAt
    }
    Announcement {
        string id PK
        string title
        string description
        announcementcolor color
        datetime createdAt
    }
```