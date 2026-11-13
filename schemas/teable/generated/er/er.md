```mermaid
erDiagram
    space ||--o| space_data_db_binding : "(id) - (spaceId)"
    data_db_connection |o--o{ space_data_db_binding : "(id) - (dataDbConnectionId)"
    space ||--o{ space_data_db_migration_job : "(id) - (spaceId)"
    data_db_connection |o--o{ space_data_db_migration_job : "(id) - (sourceConnectionId)"
    data_db_connection |o--o{ space_data_db_migration_job : "(id) - (targetConnectionId)"
    base ||--o{ base_data_db_move_job : "(id) - (baseId)"
    space ||--o{ base_data_db_move_job : "(id) - (sourceSpaceId)"
    space ||--o{ base_data_db_move_job : "(id) - (targetSpaceId)"
    space ||--o{ base : "(id) - (spaceId)"
    base ||--o{ table_meta : "(id) - (baseId)"
    table_meta ||--o{ field : "(id) - (tableId)"
    computed_update_outbox ||--o{ computed_update_outbox_seed : "(id) - (taskId)"
    computed_update_dead_letter ||--o{ computed_update_dead_letter_seed : "(id) - (taskId)"
    table_meta ||--o{ view : "(id) - (tableId)"
    users ||--o{ account : "(id) - (userId)"
    oauth_app ||--o{ oauth_app_authorized : "(clientId) - (clientId)"
    oauth_app ||--o{ oauth_app_secret : "(clientId) - (clientId)"
    oauth_app_secret |o--o{ oauth_app_token : "(id) - (appSecretId)"
    oauth_app ||--o{ oauth_app_token : "(clientId) - (clientId)"
    plugin ||--o{ plugin_install : "(id) - (pluginId)"
    table_meta ||--o{ plugin_panel : "(id) - (tableId)"
    table_meta ||--o{ plugin_context_menu : "(id) - (tableId)"
    users ||--o{ user_base_order : "(id) - (userId)"
    base ||--o{ user_base_order : "(id) - (baseId)"
    task ||--o{ task_run : "(id) - (taskId)"
    base_node |o--o{ base_node : "(id) - (parentId)"
    space {
        string id PK
        string name
        string avatar
        int credit
        datetime deletedTime
        datetime createdTime
        string createdBy
        string lastModifiedBy
        datetime lastModifiedTime
        boolean isTemplate
    }
    data_db_connection {
        string id PK
        datadbprovider provider
        string encryptedUrl
        string urlFingerprint UK
        string displayHost
        string displayDatabase
        string internalSchema
        datadbconnectionstatus status
        string schemaVersion
        json capabilities
        datetime lastValidatedAt
        string lastError
        datadbconnectionhealthstate healthState
        string healthReason
        datetime healthChangedAt
        datetime lastHealthCheckAt
        int consecutiveHealthFailures
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
    }
    space_data_db_binding {
        string id PK
        string spaceId FK, UK
        string dataDbConnectionId FK
        spacedatadbbindingmode mode
        spacedatadbbindingstate state
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
        datetime lastHistoryFlushedAt
        datetime lastRemovalFlushedAt
    }
    space_data_db_migration_job {
        string id PK
        string spaceId FK
        string sourceConnectionId FK
        string targetConnectionId FK
        string targetMode
        boolean switchOnCompletion
        spacedatadbmigrationjobstate state
        string targetUrlFingerprint
        string targetInternalSchema
        json inventory
        json copyStats
        json validationStats
        string lastError
        datetime startedAt
        datetime completedAt
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
    }
    base_data_db_move_job {
        string id PK
        string baseId FK
        string sourceSpaceId FK
        string targetSpaceId FK
        string sourceConnectionId
        string targetConnectionId
        basedatadbmovejobstate state
        json inventory
        json copyStats
        json validationStats
        string lastError
        datetime startedAt
        datetime completedAt
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
    }
    pin_resource {
        string id PK
        string type
        string resourceId UK
        datetime createdTime
        string createdBy UK
        float order
    }
    base {
        string id PK
        string spaceId FK
        string name
        float order
        string icon
        string schemaPass
        boolean v2Enabled
        provisionstate provisionState
        datetime deletedTime
        datetime createdTime
        string createdBy
        string lastModifiedBy
        datetime lastModifiedTime
    }
    table_meta {
        string id PK
        string baseId FK
        string name
        string description
        string icon
        string dbTableName
        string dbViewName
        json searchIndex
        json searchIndexPolicy
        json queryIndexPolicy
        provisionstate provisionState
        int version
        float order
        datetime createdTime
        datetime lastModifiedTime
        datetime deletedTime
        string createdBy
        string lastModifiedBy
    }
    table_query_observation_shard {
        string spaceId
        string baseId
        string tableId PK
        string queryKind PK
        string shapeHash PK
        datetime windowStart PK
        string writerId PK
        int windowSizeSeconds
        int requestCount
        int slowCount
        int timeoutCount
        int dbErrorCount
        float totalDurationMs
        float maxDurationMs
        float totalDbDurationMs
        float maxDbDurationMs
        json shape
        json sqlDiagnostics
        datetime createdTime
        datetime lastModifiedTime
    }
    schema_operation {
        string id PK
        string type
        string status
        string phase
        string resourceType
        string resourceId
        string baseId
        string tableId
        string idempotencyKey UK
        json payload
        json result
        int attempts
        int maxAttempts
        datetime nextRunAt
        datetime lockedAt
        string lockedBy
        string lastError
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
    }
    field {
        string id PK
        string name
        string description
        string options
        string meta
        string aiConfig
        string type
        string cellValueType
        boolean isMultipleCellValue
        string dbFieldType
        string dbFieldName
        provisionstate provisionState
        boolean notNull
        boolean unique
        boolean isPrimary
        boolean isComputed
        boolean isLookup
        boolean isConditionalLookup
        boolean isPending
        boolean hasError
        string lookupLinkedFieldId
        string lookupOptions
        string tableId FK
        float order
        int version
        datetime createdTime
        datetime lastModifiedTime
        datetime deletedTime
        string createdBy
        string lastModifiedBy
    }
    computed_update_outbox {
        string id PK
        string baseId
        string workerLane
        string seedTableId
        json seedRecordIds
        string changeType
        json steps
        json edges
        string status
        int attempts
        int maxAttempts
        datetime nextRunAt
        datetime lockedAt
        string lockedBy
        string lastError
        int estimatedComplexity
        string planHash
        json dirtyStats
        string runId
        string[] originRunIds
        int runTotalSteps
        int runCompletedStepsBefore
        string[] affectedTableIds
        string[] affectedFieldIds
        int syncMaxLevel
        datetime sourceChangedAt
        int stageDepth
        string predecessorTaskId
        datetime createdAt
        datetime updatedAt
    }
    computed_update_outbox_seed {
        string id PK
        string taskId FK, UK
        string tableId UK
        string recordId UK
    }
    computed_update_change_frontier {
        string scopeId PK
        string kind PK
        string tableId PK
        string recordId PK
        string fieldId PK
    }
    computed_update_stage_ledger {
        string scopeId PK
        string kind PK
        string tableId PK
        string recordId PK
        bigint seq
    }
    computed_update_dead_letter {
        string id PK
        string baseId
        string workerLane
        string seedTableId
        json seedRecordIds
        string changeType
        json steps
        json edges
        string status
        int attempts
        int maxAttempts
        datetime nextRunAt
        datetime lockedAt
        string lockedBy
        string lastError
        int estimatedComplexity
        string planHash
        json dirtyStats
        string runId
        string[] originRunIds
        int runTotalSteps
        int runCompletedStepsBefore
        string[] affectedTableIds
        string[] affectedFieldIds
        int syncMaxLevel
        datetime sourceChangedAt
        int stageDepth
        string predecessorTaskId
        json traceData
        datetime failedAt
        datetime createdAt
        datetime updatedAt
    }
    computed_update_dead_letter_seed {
        string taskId PK, FK
        string tableId PK
        string recordId PK
    }
    computed_update_run_history {
        string taskId PK
        string baseId
        string seedTableId
        string changeType
        string runId
        string[] originRunIds
        json steps
        json edges
        string[] affectedTableIds
        string[] affectedFieldIds
        string[] sourceFieldIds "Source (changed) field ids; populated for seed tasks, where affected_field_ids carries the trigger fields rather than targets."
        int seedRecordCount
        int stageDepth
        string predecessorTaskId
        int runTotalSteps
        int runCompletedStepsBefore
        int syncMaxLevel
        int estimatedComplexity
        int attempts
        string outcome
        datetime sourceChangedAt
        datetime enqueuedAt
        datetime startedAt
        datetime completedAt
        int durationMs
    }
    computed_update_pause_scope {
        string id PK
        string scopeType UK
        string scopeId UK
        datetime pausedAt
        string pausedBy
        datetime resumeAt
        string reason
        string writePolicy
        datetime updatedAt
        string updatedBy
    }
    computed_field_activity {
        string fieldId PK
        string tableId
        string baseId
        string status
        int activeTaskCount
        int processingTaskCount
        bigint generation
        bigint estimatedComplexity
        bigint estimatedDirtyRecords
        boolean hasAllTargetRecords
        datetime queuedAt
        datetime startedAt
        datetime lastCompletedAt
        int lastDurationMs
        json lastError
        json extensions
        datetime updatedAt
    }
    computed_table_activity {
        string tableId PK
        string baseId
        string status
        int calculatingFieldCount
        int queuedFieldCount
        bigint estimatedComplexity
        json recentCompletions
        bigint generation
        datetime updatedAt
    }
    computed_task_field_ref {
        string taskId PK
        string fieldId PK
        string tableId
        string baseId
        boolean wasProcessing
        datetime createdAt
    }
    view {
        string id PK
        string name
        string description
        string tableId FK
        string type
        string sort
        string filter
        string group
        string options
        float order
        int version
        string columnMeta
        boolean isLocked
        boolean enableShare
        string shareId UK
        string shareMeta
        datetime createdTime
        datetime lastModifiedTime
        datetime deletedTime
        string createdBy
        string lastModifiedBy
    }
    ops {
        string id PK
        string collection UK
        string docId UK
        string docType
        int version UK
        string operation
        datetime createdTime
        string createdBy
    }
    reference {
        string id PK
        string fromFieldId UK
        string toFieldId UK
        datetime createdTime
    }
    users {
        string id PK
        string name
        string password
        string salt
        string phone UK
        string email UK
        string avatar
        boolean isSystem
        boolean isAdmin
        boolean isTrialUsed
        string lang
        string notifyMeta
        datetime lastSignTime
        datetime deactivatedTime
        datetime createdTime
        datetime deletedTime
        datetime lastModifiedTime
        datetime permanentDeletedTime
        string refMeta
    }
    account {
        string id PK
        string userId FK
        string type
        string provider UK
        string providerId UK
        string connectionId UK
        datetime createdTime
    }
    attachments {
        string id PK
        string token UK
        string hash
        bigint size
        string mimetype
        string path
        int width
        int height
        datetime deletedTime
        datetime createdTime
        string createdBy
        string lastModifiedBy
        string thumbnailPath
    }
    attachments_table {
        string id PK
        string attachmentId
        string name
        string token
        string tableId
        string recordId
        string fieldId
        datetime createdTime
        string createdBy
        string lastModifiedBy
        datetime lastModifiedTime
    }
    collaborator {
        string id PK
        string roleName
        string resourceType UK
        string resourceId UK
        string principalId UK
        string principalType UK
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
        string lastModifiedBy
    }
    invitation {
        string id PK
        string baseId
        string spaceId
        string type
        string role
        string invitationCode
        datetime expiredTime
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
        string lastModifiedBy
        datetime deletedTime
    }
    invitation_record {
        string id PK
        string invitationId
        string baseId
        string spaceId
        string type
        string inviter
        string accepter
        datetime createdTime
    }
    notification {
        string id PK
        string fromUserId
        string toUserId
        string type
        string message
        string messageI18n
        string severity
        string urlPath
        boolean isRead
        datetime createdTime
        string createdBy
    }
    access_token {
        string id PK
        string name
        string description
        string userId
        string scopes
        string spaceIds
        string baseIds
        string sign
        string clientId
        boolean hasFullAccess
        datetime expiredTime
        datetime lastUsedTime
        datetime createdTime
        datetime lastModifiedTime
    }
    setting {
        string name UK
        string content
        datetime createdTime
        datetime lastModifiedTime
        string createdBy
        string lastModifiedBy
    }
    oauth_app {
        string id PK
        string name
        string logo
        string homepage
        string description
        string clientId UK
        string redirectUris
        string scopes
        boolean allowDeviceFlow
        boolean dynamicRegistration
        datetime createdTime
        datetime lastModifiedTime
        string createdBy
    }
    oauth_app_authorized {
        string id PK
        string clientId FK, UK
        string userId UK
        datetime authorizedTime
    }
    oauth_app_secret {
        string id PK
        string clientId FK
        string secret UK
        string maskedSecret
        datetime createdTime
        string createdBy
        datetime lastUsedTime
    }
    oauth_app_token {
        string id PK
        string clientId FK
        string appSecretId FK
        string refreshTokenSign UK
        datetime expiredTime
        datetime createdTime
        string createdBy
    }
    record_history {
        string id PK
        string tableId
        string recordId
        string fieldId
        string before
        string after
        datetime createdTime
        string createdBy
    }
    trash {
        string id PK
        string resourceType UK
        string resourceId UK
        string parentId
        datetime deletedTime
        string deletedBy
    }
    table_trash {
        string id PK
        string tableId
        string resourceType
        string snapshot
        datetime createdTime
        string createdBy
    }
    record_trash {
        string id PK
        string tableId
        string recordId
        string snapshot
        datetime createdTime
        string createdBy
        string reason
        datetime recordCreatedTime
        string recordCreatedBy
        datetime recordLastModifiedTime
        string recordLastModifiedBy
        string operationId
    }
    record_removal_tombstone {
        string id PK
        string tableId
        string recordId
        string type
        datetime createdTime
    }
    plugin {
        string id PK
        string name
        string description
        string detailDesc
        string logo
        string helpUrl
        string status
        string positions
        string url
        string secret UK
        string maskedSecret
        string i18n
        string config
        string pluginUser
        datetime createdTime
        datetime lastModifiedTime
        string createdBy
        string lastModifiedBy
    }
    plugin_install {
        string id PK
        string pluginId FK
        string baseId
        string name
        string positionId
        string position
        string storage
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
    }
    dashboard {
        string id PK
        string name
        string baseId
        string layout
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
        string lastModifiedBy
        datetime deletedTime
    }
    comment {
        string id PK
        string tableId
        string recordId
        string quoteId
        string content
        string reaction
        datetime deletedTime
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
    }
    comment_subscription {
        string id PK
        string tableId UK
        string recordId UK
        string createdBy
        datetime createdTime
    }
    integration {
        string id PK
        string resourceId UK
        string config
        string type
        boolean enable
        datetime createdTime
        datetime lastModifiedTime
    }
    plugin_panel {
        string id PK
        string name
        string tableId FK
        string layout
        string createdBy
        datetime createdTime
        datetime lastModifiedTime
        string lastModifiedBy
    }
    plugin_context_menu {
        string id PK
        string tableId FK
        string pluginInstallId UK
        float order
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
    }
    user_last_visit {
        string id PK
        string userId UK
        string resourceType UK
        string resourceId UK
        string parentResourceId
        datetime lastVisitTime
    }
    user_base_order {
        string id PK
        string userId FK, UK
        string baseId FK, UK
        float order
        datetime createdTime
        datetime lastModifiedTime
    }
    template {
        string id PK
        string baseId UK
        string cover
        string name
        string description
        string markdownDescription
        string[] categoryId
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
        boolean isSystem
        boolean isPublished
        boolean featured
        string snapshot
        float order
        int usageCount
        json publishInfo
        int visitCount
        string kind
        int version
        json manifest
        json review
        json history
        string parentTemplateId
        string slug UK
    }
    template_category {
        string id PK
        string name UK
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
        float order
        string parentId
        string kind
        string key UK
    }
    task {
        string id PK
        string type
        string status
        string snapshot
        datetime createdTime
        datetime lastModifiedTime
        string createdBy
        string lastModifiedBy
    }
    task_run {
        string id PK
        string taskId FK
        string baseId
        string status
        string snapshot
        string[] dependsOnRunIds
        int spent
        string log
        string errorMsg
        datetime startedTime
        datetime createdTime
        datetime lastModifiedTime
    }
    task_reference {
        string id PK
        string fromFieldId UK
        string toFieldId UK
        datetime createdTime
    }
    waitlist {
        string email UK
        boolean invite
        datetime inviteTime
        datetime createdTime
    }
    base_node {
        string id PK
        string parentId FK
        string baseId UK
        string resourceType UK
        string resourceId UK
        float order
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
    }
    base_node_folder {
        string id PK
        string baseId
        string name
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
        string lastModifiedBy
        datetime deletedTime
    }
    base_share {
        string id PK
        string baseId UK
        string shareId UK
        string password
        string nodeId UK
        boolean allowSave
        boolean allowCopy
        boolean allowEdit
        boolean enabled
        datetime createdTime
        string createdBy
        datetime lastModifiedTime
    }
    short_link {
        string id PK
        string code UK
        string type UK
        string resourceId UK
        datetime createdTime
        string createdBy
        datetime deletedTime
    }
```