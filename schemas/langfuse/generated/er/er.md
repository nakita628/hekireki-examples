```mermaid
erDiagram
    users ||--o{ Account : "(id) - (userId)"
    users ||--o{ Session : "(id) - (userId)"
    dashboards |o--o{ projects : "(id) - (homeDashboardId)"
    organizations ||--o{ projects : "(id) - (orgId)"
    projects |o--o{ api_keys : "(id) - (projectId)"
    organizations |o--o{ api_keys : "(id) - (orgId)"
    users |o--o{ api_keys : "(id) - (createdByUserId)"
    users |o--o{ api_keys : "(id) - (userId)"
    api_keys |o--o{ api_keys : "(id) - (createdByApiKeyId)"
    projects |o--o{ role_assignments : "(id) - (projectId)"
    organizations ||--o{ role_assignments : "(id) - (orgId)"
    users |o--o{ role_assignments : "(id) - (userId)"
    api_keys |o--o{ role_assignments : "(id) - (apiKeyId)"
    organizations ||--o| gateway_configs : "(id) - (organizationId)"
    projects |o--o{ gateway_configs : "(id) - (defaultIngestionProjectId)"
    organizations ||--o{ gateway_ai_connections : "(id) - (organizationId)"
    api_keys ||--o| gateway_api_key_associations : "(id) - (apiKeyId)"
    projects ||--o{ in_app_agent_conversations : "(id) - (projectId)"
    users |o--o{ in_app_agent_conversations : "(id) - (createdByUserId)"
    projects ||--o{ in_app_agent_events : "(id) - (projectId)"
    in_app_agent_conversations ||--o{ in_app_agent_events : "(id) - (conversationId)"
    in_app_agent_runs ||--o{ in_app_agent_events : "(id) - (runId)"
    projects ||--o{ in_app_agent_runs : "(id) - (projectId)"
    in_app_agent_conversations ||--o{ in_app_agent_runs : "(id) - (conversationId)"
    users |o--o{ in_app_agent_runs : "(id) - (triggeredByUserId)"
    projects ||--o{ in_app_agent_pending_tool_approvals : "(id) - (projectId)"
    in_app_agent_conversations ||--o{ in_app_agent_pending_tool_approvals : "(id) - (conversationId)"
    projects |o--o{ llm_api_keys : "(id) - (projectId)"
    organizations |o--o{ llm_api_keys : "(id) - (organizationId)"
    organizations ||--o{ organization_memberships : "(id) - (orgId)"
    users ||--o{ organization_memberships : "(id) - (userId)"
    organization_memberships ||--o{ project_memberships : "(id) - (orgMembershipId)"
    projects ||--o{ project_memberships : "(id) - (projectId)"
    users ||--o{ project_memberships : "(id) - (userId)"
    organizations ||--o{ membership_invitations : "(id) - (orgId)"
    projects |o--o{ membership_invitations : "(id) - (projectId)"
    users |o--o{ membership_invitations : "(id) - (invitedByUserId)"
    projects ||--o{ trace_sessions : "(id) - (projectId)"
    projects ||--o{ score_configs : "(id) - (projectId)"
    projects ||--o{ annotation_queues : "(id) - (projectId)"
    annotation_queues ||--o{ annotation_queue_items : "(id) - (queueId)"
    users |o--o{ annotation_queue_items : "(id) - (lockedByUserId)"
    users |o--o{ annotation_queue_items : "(id) - (annotatorUserId)"
    projects ||--o{ annotation_queue_items : "(id) - (projectId)"
    projects ||--o{ annotation_queue_assignments : "(id) - (projectId)"
    users ||--o{ annotation_queue_assignments : "(id) - (userId)"
    annotation_queues ||--o{ annotation_queue_assignments : "(id) - (queueId)"
    projects ||--o{ datasets : "(id) - (projectId)"
    datasets ||--o{ dataset_items : "(id) - (datasetId)"
    datasets ||--o{ dataset_runs : "(id) - (datasetId)"
    projects ||--o{ comments : "(id) - (projectId)"
    projects ||--o{ comment_reactions : "(id) - (projectId)"
    comments ||--o{ comment_reactions : "(id) - (commentId)"
    users ||--o{ comment_reactions : "(id) - (userId)"
    users ||--o{ notification_preferences : "(id) - (userId)"
    projects ||--o{ notification_preferences : "(id) - (projectId)"
    projects ||--o{ prompts : "(id) - (projectId)"
    projects ||--o{ prompt_dependencies : "(id) - (projectId)"
    prompts ||--o{ prompt_dependencies : "(id) - (parentId)"
    projects ||--o{ prompt_protected_labels : "(id) - (projectId)"
    projects ||--o{ skills : "(id) - (projectId)"
    projects ||--o{ skill_files : "(id) - (projectId)"
    skills ||--o{ skill_files : "(id) - (skillId)"
    skill_blobs ||--o{ skill_files : "(id) - (blobId)"
    projects ||--o{ skill_blobs : "(id) - (projectId)"
    projects |o--o{ models : "(id) - (projectId)"
    models ||--o{ prices : "(id) - (modelId)"
    projects |o--o{ prices : "(id) - (projectId)"
    pricing_tiers ||--o{ prices : "(id) - (pricingTierId)"
    models ||--o{ pricing_tiers : "(id) - (modelId)"
    projects |o--o{ eval_templates : "(id) - (projectId)"
    projects ||--o{ evaluators : "(id) - (projectId)"
    users |o--o{ evaluators : "(id) - (createdByUserId)"
    evaluators ||--o{ evaluator_versions : "(id) - (evaluatorId)"
    users |o--o{ evaluator_versions : "(id) - (createdByUserId)"
    projects ||--o{ job_configurations : "(id) - (projectId)"
    eval_templates |o--o{ job_configurations : "(id) - (evalTemplateId)"
    projects ||--o{ evaluation_rules : "(id) - (projectId)"
    users |o--o{ evaluation_rules : "(id) - (createdByUserId)"
    projects ||--o| tracing_readiness_settings : "(id) - (projectId)"
    users |o--o{ tracing_readiness_settings : "(id) - (createdBy)"
    users |o--o{ tracing_readiness_settings : "(id) - (updatedBy)"
    projects ||--o{ evaluation_rule_evaluator_assignments : "(id) - (projectId)"
    evaluation_rules ||--o{ evaluation_rule_evaluator_assignments : "(id) - (evaluationRuleId)"
    evaluators ||--o{ evaluation_rule_evaluator_assignments : "(id) - (evaluatorId)"
    projects ||--o{ job_executions : "(id) - (projectId)"
    projects ||--o{ default_llm_models : "(id) - (projectId)"
    llm_api_keys ||--o{ default_llm_models : "(id) - (llmApiKeyId)"
    users |o--o{ topic_configs : "(id) - (createdBy)"
    users |o--o{ topic_configs : "(id) - (updatedBy)"
    projects ||--o| topic_configs : "(id) - (projectId)"
    llm_api_keys |o--o{ topic_configs : "(id) - (summaryLlmApiKeyId)"
    llm_api_keys |o--o{ topic_configs : "(id) - (embeddingLlmApiKeyId)"
    llm_api_keys |o--o{ topic_configs : "(id) - (clusteringLlmApiKeyId)"
    organizations ||--o{ verified_domains : "(id) - (organizationId)"
    projects ||--o{ posthog_integrations : "(id) - (projectId)"
    projects ||--o{ mixpanel_integrations : "(id) - (projectId)"
    projects ||--o{ blob_storage_integrations : "(id) - (projectId)"
    projects ||--o| external_media_storage_integrations : "(id) - (projectId)"
    projects ||--o{ web_callout_endpoints : "(id) - (projectId)"
    projects ||--o{ batch_exports : "(id) - (projectId)"
    projects ||--o{ batch_actions : "(id) - (projectId)"
    projects ||--o{ media : "(id) - (projectId)"
    projects ||--o{ llm_schemas : "(id) - (projectId)"
    projects ||--o{ llm_tools : "(id) - (projectId)"
    users |o--o{ dashboards : "(id) - (createdBy)"
    users |o--o{ dashboards : "(id) - (updatedBy)"
    projects |o--o{ dashboards : "(id) - (projectId)"
    users |o--o{ dashboard_widgets : "(id) - (createdBy)"
    users |o--o{ dashboard_widgets : "(id) - (updatedBy)"
    projects |o--o{ dashboard_widgets : "(id) - (projectId)"
    projects ||--o{ table_view_presets : "(id) - (projectId)"
    users |o--o{ table_view_presets : "(id) - (createdBy)"
    users |o--o{ table_view_presets : "(id) - (updatedBy)"
    projects ||--o{ default_views : "(id) - (projectId)"
    users |o--o{ default_views : "(id) - (userId)"
    projects ||--o{ actions : "(id) - (projectId)"
    projects ||--o{ triggers : "(id) - (projectId)"
    triggers ||--o{ automations : "(id) - (triggerId)"
    actions ||--o{ automations : "(id) - (actionId)"
    projects ||--o{ automations : "(id) - (projectId)"
    automations ||--o{ automation_executions : "(id) - (automationId)"
    triggers ||--o{ automation_executions : "(id) - (triggerId)"
    actions ||--o{ automation_executions : "(id) - (actionId)"
    projects ||--o{ automation_executions : "(id) - (projectId)"
    users |o--o{ monitors : "(id) - (createdBy)"
    users |o--o{ monitors : "(id) - (updatedBy)"
    projects ||--o{ monitors : "(id) - (projectId)"
    projects ||--o| slack_integrations : "(id) - (projectId)"
    projects ||--o{ pending_deletions : "(id) - (projectId)"
    organizations |o--o{ surveys : "(id) - (orgId)"
    users |o--o{ surveys : "(id) - (userId)"
    organizations ||--o{ cloud_spend_alerts : "(id) - (orgId)"
    projects ||--o{ topic_clustering_runs : "(id) - (projectId)"
    evaluator_versions ||--o{ topic_clustering_runs : "(evaluatorId) - (facetId)"
    Account {
        string id PK
        string userId FK
        string type
        string provider UK
        string providerAccountId UK
        string refresh_token
        string access_token
        int expires_at
        int expires_in
        int ext_expires_in
        string token_type
        string scope
        string id_token
        string session_state
        int refresh_token_expires_in
        int created_at
    }
    Session {
        string id PK
        string sessionToken UK
        string userId FK
        datetime expires
    }
    users {
        string id PK
        string name
        string email UK
        datetime emailVerified
        string password
        datetime sessionsExpiredAt
        string image
        boolean admin
        boolean v4BetaEnabled
        json supportAccess
        datetime createdAt
        datetime updatedAt
        string[] featureFlags
    }
    verification_tokens {
        string identifier UK
        string token UK
        datetime expires
    }
    organizations {
        string id PK
        string name
        datetime createdAt
        datetime updatedAt
        json cloudConfig
        json metadata
        datetime cloudBillingCycleAnchor
        datetime cloudBillingCycleUpdatedAt
        int cloudCurrentCycleUsage
        string cloudFreeTierUsageThresholdState
        boolean aiFeaturesEnabled
        boolean aiTelemetryEnabled
        boolean accessAuditLogEnabled
        string[] featureFlagOrgDefaults
        string sfdcOrgId
    }
    projects {
        string id PK
        string orgId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        string name
        int retentionDays
        boolean hasTraces
        json metadata
        string homeDashboardId FK
    }
    api_keys {
        string id PK
        datetime createdAt
        string note
        string publicKey UK
        string hashedSecretKey UK
        string fastHashedSecretKey UK
        string displaySecretKey
        datetime lastUsedAt
        datetime expiresAt
        boolean isInAppAgentKey
        string projectId FK
        string orgId FK
        apikeyscope scope
        string createdByUserId FK
        string userId FK
        string createdByApiKeyId FK
    }
    role_assignments {
        string id PK
        string ownerId UK
        string projectId FK
        string orgId FK
        string principalId UK
        string userId FK
        string apiKeyId FK
        string roleId UK
        systemrole systemRole UK
        datetime createdAt
        datetime updatedAt
    }
    gateway_configs {
        string organizationId PK, FK
        string defaultIngestionProjectId FK
        gatewayingestionmode ingestionMode
        datetime createdAt
        datetime updatedAt
    }
    gateway_ai_connections {
        string id PK
        string organizationId FK, UK
        string name UK
        gatewayprovider provider
        string encryptedCredential
        string displaySecret
        string createdById
        gatewayconnectionstatus status
        json allowedModels
        json customModels
        json config
        string baseURL
        string extraHeaders
        string[] extraHeaderKeys
        datetime createdAt
        datetime updatedAt
    }
    gateway_api_key_associations {
        string apiKeyId PK, FK
        json metadata
    }
    in_app_agent_conversations {
        string id PK
        string projectId PK, FK
        string createdByUserId FK
        string title
        datetime renamedByUserAt
        inappagentconversationvisibilityscope visibilityScope
        string providerSessionId
        string[] alwaysAllowedTools
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
    }
    in_app_agent_events {
        string projectId PK, FK
        string conversationId PK, FK
        string runId FK
        int sequenceNumber PK
        string type
        json event
        datetime createdAt
    }
    in_app_agent_runs {
        string id PK
        string projectId PK, FK
        string conversationId FK
        string triggeredByUserId FK
        string model
        string mcpApiKeyId
        string errorCode
        string errorMessage
        datetime finishedAt
        string status
        json request
        datetime claimedAt
        datetime heartbeatAt
        datetime cancelRequestedAt
        datetime createdAt
        datetime updatedAt
    }
    in_app_agent_pending_tool_approvals {
        string projectId PK, FK
        string conversationId PK, FK
        string toolCallId PK
        string approvalFingerprint
        datetime expiresAt
        datetime createdAt
        datetime updatedAt
    }
    background_migrations {
        string id PK
        string name UK
        string script
        json args
        json state
        datetime finishedAt
        datetime failedAt
        string failedReason
        string workerId
        datetime lockedAt
    }
    llm_api_keys {
        string id PK
        datetime createdAt
        datetime updatedAt
        string provider UK
        string adapter
        string displaySecretKey
        string secretKey
        string baseURL
        string[] customModels
        boolean withDefaultModels
        string extraHeaders
        string[] extraHeaderKeys
        json config
        string projectId FK, UK
        string organizationId FK, UK
    }
    organization_memberships {
        string id PK
        string orgId FK, UK
        string userId FK, UK
        role role
        datetime createdAt
        datetime updatedAt
    }
    project_memberships {
        string orgMembershipId FK
        string projectId PK, FK
        string userId PK, FK
        role role
        datetime createdAt
        datetime updatedAt
    }
    membership_invitations {
        string id PK
        string email UK
        string orgId FK, UK
        role orgRole
        string projectId FK
        role projectRole
        string invitedByUserId FK
        datetime createdAt
        datetime updatedAt
    }
    trace_sessions {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId PK, FK
        boolean bookmarked
        boolean public
        string environment
    }
    score_configs {
        string id PK, UK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string name
        scoreconfigdatatype dataType
        boolean isArchived
        float minValue
        float maxValue
        json categories
        string description
    }
    annotation_queues {
        string id PK
        string name UK
        string description
        string[] scoreConfigIds
        string projectId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    annotation_queue_items {
        string id PK
        string queueId FK
        string objectId
        annotationqueueobjecttype objectType
        annotationqueuestatus status
        datetime lockedAt
        string lockedByUserId FK
        string annotatorUserId FK
        datetime completedAt
        string projectId FK
        datetime createdAt
        datetime updatedAt
    }
    annotation_queue_assignments {
        string id PK
        string projectId FK, UK
        string userId FK, UK
        string queueId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    cron_jobs {
        string name PK
        datetime lastRun
        datetime jobStartedAt
        string state
    }
    datasets {
        string id PK
        string projectId PK, FK, UK
        string name UK
        string description
        json metadata
        string remoteExperimentUrl
        json remoteExperimentPayload
        boolean remoteExperimentEnabled
        string remoteExperimentSecretKey
        string remoteExperimentDisplaySecretKey
        json remoteExperimentRequestHeaders
        json remoteExperimentDisplayHeaders
        json inputSchema
        json expectedOutputSchema
        datetime createdAt
        datetime updatedAt
    }
    dataset_items {
        string id PK
        string projectId PK, FK
        datasetstatus status
        json input
        json expectedOutput
        json metadata
        string sourceTraceId
        string sourceObservationId
        string datasetId FK
        datetime createdAt
        datetime updatedAt
        datetime validFrom PK
        datetime validTo
        boolean isDeleted
    }
    dataset_runs {
        string id PK
        string projectId PK, FK, UK
        string name UK
        string description
        json metadata
        string datasetId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    comments {
        string id PK
        string projectId FK
        commentobjecttype objectType
        string objectId
        datetime createdAt
        datetime updatedAt
        string content
        string authorUserId
        string dataField
        string[] path
        int[] rangeStart
        int[] rangeEnd
    }
    comment_reactions {
        string id PK
        string projectId FK
        string commentId FK, UK
        string userId FK, UK
        string emoji UK
        datetime createdAt
    }
    notification_preferences {
        string id PK
        string userId FK, UK
        string projectId FK, UK
        notificationchannel channel UK
        notificationtype type UK
        boolean enabled
        datetime createdAt
        datetime updatedAt
    }
    prompts {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string createdBy
        json prompt
        string name UK
        int version UK
        string type
        boolean isActive
        json config
        string[] tags
        string[] labels
        string commitMessage
    }
    prompt_dependencies {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string parentId FK
        string childName
        string childLabel
        int childVersion
    }
    prompt_protected_labels {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string label UK
    }
    skills {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string createdBy
        string name UK
        string description
        json frontmatter
        int version UK
        string[] tags
        string[] labels
        string commitMessage
    }
    skill_files {
        string id PK
        datetime createdAt
        string projectId FK, UK
        string skillId FK, UK
        string blobId FK
        string path UK
    }
    skill_blobs {
        string id PK
        datetime createdAt
        string projectId FK, UK
        string sha256Hash UK
        string contentType
        int contentLength
        string content
    }
    models {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string modelName UK
        string matchPattern
        datetime startDate UK
        decimal inputPrice
        decimal outputPrice
        decimal totalPrice
        string unit UK
        string tokenizerId
        json tokenizerConfig
    }
    prices {
        string id PK
        datetime createdAt
        datetime updatedAt
        string modelId FK, UK
        string projectId FK
        string pricingTierId FK, UK
        string usageType UK
        decimal price
    }
    pricing_tiers {
        string id PK
        datetime createdAt
        datetime updatedAt
        string modelId FK, UK
        string name UK
        boolean isDefault
        int priority UK
        json conditions
    }
    audit_logs {
        string id PK
        datetime createdAt
        datetime updatedAt
        auditlogrecordtype type
        string apiKeyId
        string userId
        string orgId
        string userOrgRole
        string projectId
        string userProjectRole
        string resourceType
        string resourceId
        string action
        string before
        string after
    }
    audit_log_staging {
        string id PK
        datetime createdAt
        json payload
        datetime failedAt
        string lastError
    }
    eval_templates {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string name UK
        int version UK
        string prompt
        evaltemplatetype type
        string partner
        string model
        string provider
        json modelParams
        string[] vars
        json outputDefinition
        string sourceCode
        evaltemplatesourcecodelanguage sourceCodeLanguage
    }
    evaluators {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string name
        evaltemplatetype type
        boolean isBuiltIn
        string description
        string createdByUserId FK
        datetime blockedAt
        evaluatorblockreason blockReason
        string blockMessage
    }
    evaluator_versions {
        string id PK
        datetime createdAt
        string evaluatorId FK, UK
        int version UK
        string createdByUserId FK
        string prompt
        json promptMessages
        string partner
        string model
        string provider
        json modelParams
        string[] vars
        json variableMapping
        json outputDefinition
        string sourceCode
        evaluatorsourcecodelanguage sourceCodeLanguage
        json questions
    }
    job_configurations {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        jobtype jobType
        jobconfigstate status
        datetime blockedAt
        evaluatorblockreason blockReason
        string blockMessage
        string evalTemplateId FK
        string scoreName
        json filter
        string targetObject
        json variableMapping
        decimal sampling
        int delay
        string[] timeScope
    }
    evaluation_rules {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string createdByUserId FK
        string name
        jobconfigstate status
        string targetObject
        json filter
        decimal sampling
        int delay
        int idleTime
        string[] timeScope
    }
    tracing_readiness_settings {
        string projectId PK, FK
        datetime createdAt
        datetime updatedAt
        string createdBy FK
        string updatedBy FK
        int traceIdleMs
    }
    evaluation_rule_evaluator_assignments {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string evaluationRuleId FK, UK
        string evaluatorId FK, UK
        json variableMapping
    }
    job_executions {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string jobConfigurationId
        string jobTemplateId
        jobexecutionstatus status
        datetime startTime
        datetime endTime
        string error
        string jobInputTraceId
        datetime jobInputTraceTimestamp
        string jobInputObservationId
        string jobInputDatasetItemId
        datetime jobInputDatasetItemValidFrom
        string jobOutputScoreId
        string executionTraceId
    }
    default_llm_models {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string llmApiKeyId FK
        string provider
        string adapter
        string model
        json modelParams
    }
    topic_configs {
        string id PK
        datetime createdAt
        datetime updatedAt
        string createdBy FK
        string updatedBy FK
        string projectId FK, UK
        boolean enabled
        datetime blockedAt
        evaluatorblockreason blockReason
        string blockMessage
        string summaryLlmApiKeyId FK
        string summaryModel
        string embeddingLlmApiKeyId FK
        string embeddingModel
        int embeddingDimensions
        string clusteringLlmApiKeyId FK
        string clusteringModel
    }
    sso_configs {
        string domain PK
        datetime createdAt
        datetime updatedAt
        string authProvider
        json authConfig
    }
    verified_domains {
        string id PK
        string organizationId FK, UK
        string domain UK
        string verificationToken UK
        datetime verifiedAt
        string createdByUserId
        datetime createdAt
        datetime updatedAt
    }
    posthog_integrations {
        string projectId PK, FK
        string encryptedPosthogApiKey
        string posthogHostName
        datetime lastSyncAt
        boolean enabled
        datetime createdAt
        analyticsintegrationexportsource exportSource
        boolean backfill
        string lastError
        datetime lastErrorAt
    }
    mixpanel_integrations {
        string projectId PK, FK
        string encryptedMixpanelProjectToken
        string mixpanelRegion
        datetime lastSyncAt
        boolean enabled
        datetime createdAt
        analyticsintegrationexportsource exportSource
        boolean backfill
    }
    blob_storage_integrations {
        string projectId PK, FK
        blobstorageintegrationtype type
        string bucketName
        string prefix
        storageintegrationauthtype authType
        string accessKeyId
        string secretAccessKey
        string roleArn
        string externalId
        string region
        string endpoint
        boolean forcePathStyle
        datetime nextSyncAt
        datetime lastSyncAt
        boolean enabled
        string exportFrequency
        blobstorageintegrationfiletype fileType
        blobstorageexportmode exportMode
        datetime exportStartDate
        analyticsintegrationexportsource exportSource
        string[] exportFieldGroups
        boolean compressed
        json exportTuning
        datetime runStartedAt
        boolean backfill
        string lastError
        datetime lastErrorAt
        datetime lastFailureNotificationSentAt
        datetime createdAt
        datetime updatedAt
    }
    external_media_storage_integrations {
        string projectId PK, FK
        externalmediastorageintegrationtype type
        string bucketName
        string prefix
        storageintegrationauthtype authType
        string accessKeyId
        string secretAccessKey
        string roleArn
        string externalId
        string region
        string endpoint
        boolean forcePathStyle
        datetime createdAt
        datetime updatedAt
    }
    web_callout_endpoints {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string name
        string url
        boolean enabled
        string toastMessage
        string requestHeaders
        string[] requestHeaderKeys
    }
    batch_exports {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string userId
        datetime finishedAt
        datetime expiresAt
        string name
        string status
        json query
        string format
        string url
        string log
    }
    batch_actions {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string userId
        string actionType
        string tableName
        string status
        datetime finishedAt
        json query
        json config
        int totalCount
        int processedCount
        int failedCount
        string log
    }
    media {
        string id UK
        string sha256Hash UK
        string projectId FK, UK
        datetime createdAt
        datetime updatedAt
        datetime uploadedAt
        int uploadHttpStatus
        string uploadHttpError
        string bucketPath
        string bucketName
        string contentType
        bigint contentLength
    }
    trace_media {
        string id PK
        string projectId UK
        datetime createdAt
        datetime updatedAt
        string mediaId UK
        string traceId UK
        string field UK
        mediaassociationorigin origin
    }
    observation_media {
        string id PK
        string projectId UK
        datetime createdAt
        datetime updatedAt
        string mediaId UK
        string traceId UK
        string observationId UK
        string field UK
        mediaassociationorigin origin
    }
    dataset_item_media {
        string id PK
        string projectId UK
        datetime createdAt
        datetime updatedAt
        string mediaId
        string datasetId
        string datasetItemId UK
        datetime datasetItemValidFrom UK
        string field UK
        string jsonPath UK
        string referenceString
    }
    billing_meter_backups {
        string stripeCustomerId UK
        string meterId UK
        datetime startTime UK
        datetime endTime UK
        int aggregatedValue
        string eventName
        string orgId
        datetime createdAt
        datetime updatedAt
    }
    llm_schemas {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string name UK
        string description
        json schema
    }
    llm_tools {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string name UK
        string description
        json parameters
    }
    dashboards {
        string id PK
        datetime createdAt
        datetime updatedAt
        string createdBy FK
        string updatedBy FK
        string projectId FK
        string name
        string description
        json definition
        json filters
    }
    dashboard_widgets {
        string id PK
        datetime createdAt
        datetime updatedAt
        string createdBy FK
        string updatedBy FK
        string projectId FK
        string name
        string description
        dashboardwidgetviews view
        json dimensions
        json metrics
        json filters
        dashboardwidgetcharttype chartType
        json chartConfig
        int minVersion
    }
    table_view_presets {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK, UK
        string name UK
        string tableName UK
        string createdBy FK
        string updatedBy FK
        json filters
        json columnOrder
        json columnVisibility
        string searchQuery
        json orderBy
    }
    default_views {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string userId FK
        string viewName
        string viewId
    }
    actions {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        actiontype type
        json config
    }
    triggers {
        string id PK
        datetime createdAt
        datetime updatedAt
        string projectId FK
        string eventSource
        string[] eventActions
        json filter
        jobconfigstate status
    }
    automations {
        string id PK
        string name
        string triggerId FK
        string actionId FK
        datetime createdAt
        string projectId FK
    }
    automation_executions {
        string id PK
        datetime createdAt
        datetime updatedAt
        string sourceId
        string automationId FK
        string triggerId FK
        string actionId FK
        string projectId FK
        actionexecutionstatus status
        json input
        json output
        datetime startedAt
        datetime finishedAt
        string error
    }
    monitors {
        string id PK
        datetime createdAt
        datetime updatedAt
        string createdBy FK
        string updatedBy FK
        string projectId FK
        monitorview view
        json filters
        json metric
        bigint windowMs
        bigint cadenceMs
        monitorthresholdoperator thresholdOperator
        decimal alertThreshold
        decimal warningThreshold
        monitorseverity severity
        datetime severityChangedAt
        json noData
        json renotify
        monitorstatus status
        bigint schedulerBatchId
        datetime nextRunAt
        datetime lastPublishedAt
        datetime lastClaimedAt
        datetime lastCompletedAt
        string name
        string[] tags
        string[] triggerIds
        datetime alertedAt
    }
    slack_integrations {
        string id PK
        string projectId FK, UK
        string teamId
        string teamName
        string botToken
        string botUserId
        string installerUserId
        string installerSlackUserId
        datetime createdAt
        datetime updatedAt
    }
    pending_deletions {
        string id PK
        string projectId FK
        string object
        string objectId
        boolean isDeleted
        datetime createdAt
        datetime updatedAt
    }
    surveys {
        string id PK
        datetime createdAt
        surveyname surveyName
        json response
        string userId FK
        string userEmail
        string orgId FK
    }
    cloud_spend_alerts {
        string id PK
        string orgId FK
        string title
        decimal threshold
        datetime triggeredAt
        datetime createdAt
        datetime updatedAt
    }
    topic_clustering_runs {
        string id PK
        string projectId FK
        string facetId FK
        int facetVersion FK
        string status
        json config
        string error
        datetime createdAt
        datetime startedAt
        datetime finishedAt
        string[] topicVersionIds
        string ruleId
        string batchActionId
    }
```