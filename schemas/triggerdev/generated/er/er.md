```mermaid
erDiagram
    SecretReference |o--o{ User : "(id) - (mfaSecretReferenceId)"
    InvitationCode |o--o{ User : "(id) - (invitationCodeId)"
    User ||--o{ MfaBackupCode : "(id) - (userId)"
    PersonalAccessToken |o--o{ AuthorizationCode : "(id) - (personalAccessTokenId)"
    User ||--o{ PersonalAccessToken : "(id) - (userId)"
    Organization ||--o{ OrganizationAccessToken : "(id) - (organizationId)"
    Organization ||--o{ OrgMember : "(id) - (organizationId)"
    User ||--o{ OrgMember : "(id) - (userId)"
    Organization ||--o{ OrgMemberInvite : "(id) - (organizationId)"
    User ||--o{ OrgMemberInvite : "(id) - (inviterId)"
    RuntimeEnvironment |o--o{ RuntimeEnvironment : "(id) - (parentEnvironmentId)"
    Organization ||--o{ RuntimeEnvironment : "(id) - (organizationId)"
    Project ||--o{ RuntimeEnvironment : "(id) - (projectId)"
    OrgMember |o--o{ RuntimeEnvironment : "(id) - (orgMemberId)"
    RuntimeEnvironmentSession |o--o{ RuntimeEnvironment : "(id) - (currentSessionId)"
    RuntimeEnvironment ||--o{ api_keys : "(id) - (runtimeEnvironmentId)"
    User |o--o{ api_keys : "(id) - (createdByUserId)"
    RuntimeEnvironment ||--o{ RevokedApiKey : "(id) - (runtimeEnvironmentId)"
    Organization ||--o{ Project : "(id) - (organizationId)"
    WorkerInstanceGroup |o--o{ Project : "(id) - (defaultWorkerGroupId)"
    Project ||--o{ BackgroundWorker : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ BackgroundWorker : "(id) - (runtimeEnvironmentId)"
    WorkerInstanceGroup |o--o{ BackgroundWorker : "(id) - (workerGroupId)"
    Project ||--o{ BackgroundWorkerFile : "(id) - (projectId)"
    Organization ||--o{ prompts : "(id) - (organizationId)"
    Project ||--o{ prompts : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ prompts : "(id) - (runtimeEnvironmentId)"
    prompts ||--o{ prompt_versions : "(id) - (promptId)"
    BackgroundWorker |o--o{ prompt_versions : "(id) - (workerId)"
    BackgroundWorker ||--o{ BackgroundWorkerTask : "(id) - (workerId)"
    Project ||--o{ BackgroundWorkerTask : "(id) - (projectId)"
    BackgroundWorkerFile |o--o{ BackgroundWorkerTask : "(id) - (fileId)"
    RuntimeEnvironment ||--o{ BackgroundWorkerTask : "(id) - (runtimeEnvironmentId)"
    TaskQueue |o--o{ BackgroundWorkerTask : "(id) - (queueId)"
    TaskRun |o--o{ PlaygroundConversation : "(id) - (runId)"
    Project ||--o{ PlaygroundConversation : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ PlaygroundConversation : "(id) - (runtimeEnvironmentId)"
    Session ||--o{ SessionRun : "(id) - (sessionId)"
    RuntimeEnvironment ||--o{ TaskRun : "(id) - (runtimeEnvironmentId)"
    Project ||--o{ TaskRun : "(id) - (projectId)"
    BackgroundWorkerTask |o--o{ TaskRun : "(id) - (lockedById)"
    BackgroundWorker |o--o{ TaskRun : "(id) - (lockedToVersionId)"
    TaskRun |o--o{ TaskRun : "(id) - (rootTaskRunId)"
    TaskRun |o--o{ TaskRun : "(id) - (parentTaskRunId)"
    TaskRunAttempt |o--o{ TaskRun : "(id) - (parentTaskRunAttemptId)"
    BatchTaskRun |o--o{ TaskRun : "(id) - (batchId)"
    TaskRun ||--o{ TaskRunExecutionSnapshot : "(id) - (runId)"
    TaskRunCheckpoint |o--o{ TaskRunExecutionSnapshot : "(id) - (checkpointId)"
    Project ||--o{ TaskRunCheckpoint : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ TaskRunCheckpoint : "(id) - (runtimeEnvironmentId)"
    TaskRun |o--o| Waitpoint : "(id) - (completedByTaskRunId)"
    BatchTaskRun |o--o{ Waitpoint : "(id) - (completedByBatchId)"
    Project ||--o{ Waitpoint : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ Waitpoint : "(id) - (environmentId)"
    TaskRun ||--o{ TaskRunWaitpoint : "(id) - (taskRunId)"
    Waitpoint ||--o{ TaskRunWaitpoint : "(id) - (waitpointId)"
    Project ||--o{ TaskRunWaitpoint : "(id) - (projectId)"
    BatchTaskRun |o--o{ TaskRunWaitpoint : "(id) - (batchId)"
    RuntimeEnvironment ||--o{ WaitpointTag : "(id) - (environmentId)"
    Project ||--o{ WaitpointTag : "(id) - (projectId)"
    WorkerInstanceGroup ||--o{ WorkerInstance : "(id) - (workerGroupId)"
    Organization |o--o{ WorkerInstance : "(id) - (organizationId)"
    Project |o--o{ WorkerInstance : "(id) - (projectId)"
    RuntimeEnvironment |o--o{ WorkerInstance : "(id) - (environmentId)"
    WorkerDeployment |o--o{ WorkerInstance : "(id) - (deploymentId)"
    WorkerGroupToken ||--o| WorkerInstanceGroup : "(id) - (tokenId)"
    Organization |o--o{ WorkerInstanceGroup : "(id) - (organizationId)"
    Project |o--o{ WorkerInstanceGroup : "(id) - (projectId)"
    Project ||--o{ TaskRunTag : "(id) - (projectId)"
    TaskRun ||--o| TaskRunDependency : "(id) - (taskRunId)"
    CheckpointRestoreEvent |o--o| TaskRunDependency : "(id) - (checkpointEventId)"
    TaskRunAttempt |o--o{ TaskRunDependency : "(id) - (dependentAttemptId)"
    BatchTaskRun |o--o{ TaskRunDependency : "(id) - (dependentBatchRunId)"
    RuntimeEnvironment ||--o{ TaskRunNumberCounter : "(id) - (environmentId)"
    TaskRun ||--o{ TaskRunAttempt : "(id) - (taskRunId)"
    BackgroundWorker ||--o{ TaskRunAttempt : "(id) - (backgroundWorkerId)"
    BackgroundWorkerTask ||--o{ TaskRunAttempt : "(id) - (backgroundWorkerTaskId)"
    RuntimeEnvironment ||--o{ TaskRunAttempt : "(id) - (runtimeEnvironmentId)"
    TaskQueue ||--o{ TaskRunAttempt : "(id) - (queueId)"
    Project ||--o{ TaskQueue : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ TaskQueue : "(id) - (runtimeEnvironmentId)"
    CheckpointRestoreEvent |o--o| BatchTaskRun : "(id) - (checkpointEventId)"
    TaskRunAttempt |o--o{ BatchTaskRun : "(id) - (dependentTaskAttemptId)"
    BatchTaskRun ||--o{ BatchTaskRunItem : "(id) - (batchTaskRunId)"
    TaskRun ||--o{ BatchTaskRunItem : "(id) - (taskRunId)"
    TaskRunAttempt |o--o{ BatchTaskRunItem : "(id) - (taskRunAttemptId)"
    BatchTaskRun ||--o{ BatchTaskRunError : "(id) - (batchTaskRunId)"
    Project ||--o{ EnvironmentVariable : "(id) - (projectId)"
    SecretReference |o--o{ EnvironmentVariableValue : "(id) - (valueReferenceId)"
    EnvironmentVariable ||--o{ EnvironmentVariableValue : "(id) - (variableId)"
    RuntimeEnvironment ||--o{ EnvironmentVariableValue : "(id) - (environmentId)"
    TaskRun ||--o{ Checkpoint : "(id) - (runId)"
    TaskRunAttempt ||--o{ Checkpoint : "(id) - (attemptId)"
    Project ||--o{ Checkpoint : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ Checkpoint : "(id) - (runtimeEnvironmentId)"
    Checkpoint ||--o{ CheckpointRestoreEvent : "(id) - (checkpointId)"
    TaskRun ||--o{ CheckpointRestoreEvent : "(id) - (runId)"
    TaskRunAttempt ||--o{ CheckpointRestoreEvent : "(id) - (attemptId)"
    Project ||--o{ CheckpointRestoreEvent : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ CheckpointRestoreEvent : "(id) - (runtimeEnvironmentId)"
    Project ||--o{ WorkerDeployment : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ WorkerDeployment : "(id) - (environmentId)"
    BackgroundWorker |o--o| WorkerDeployment : "(id) - (workerId)"
    User |o--o{ WorkerDeployment : "(id) - (triggeredById)"
    WorkerDeployment ||--o{ WorkerDeploymentPromotion : "(id) - (deploymentId)"
    RuntimeEnvironment ||--o{ WorkerDeploymentPromotion : "(id) - (environmentId)"
    Project ||--o{ TaskSchedule : "(id) - (projectId)"
    TaskSchedule ||--o{ TaskScheduleInstance : "(id) - (taskScheduleId)"
    RuntimeEnvironment ||--o{ TaskScheduleInstance : "(id) - (environmentId)"
    Project ||--o{ TaskScheduleInstance : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ RuntimeEnvironmentSession : "(id) - (environmentId)"
    OrganizationIntegration |o--o{ ProjectAlertChannel : "(id) - (integrationId)"
    Project ||--o{ ProjectAlertChannel : "(id) - (projectId)"
    Project ||--o{ ProjectAlert : "(id) - (projectId)"
    RuntimeEnvironment ||--o{ ProjectAlert : "(id) - (environmentId)"
    ProjectAlertChannel ||--o{ ProjectAlert : "(id) - (channelId)"
    WorkerDeployment |o--o{ ProjectAlert : "(id) - (workerDeploymentId)"
    Project ||--o{ ProjectAlertStorage : "(id) - (projectId)"
    ProjectAlertChannel ||--o{ ProjectAlertStorage : "(id) - (alertChannelId)"
    SecretReference ||--o{ OrganizationIntegration : "(id) - (tokenReferenceId)"
    Organization ||--o{ OrganizationIntegration : "(id) - (organizationId)"
    OrganizationIntegration ||--o{ OrganizationProjectIntegration : "(id) - (organizationIntegrationId)"
    Project ||--o{ OrganizationProjectIntegration : "(id) - (projectId)"
    Project ||--o{ BulkActionGroup : "(id) - (projectId)"
    RuntimeEnvironment |o--o{ BulkActionGroup : "(id) - (environmentId)"
    User |o--o{ BulkActionGroup : "(id) - (userId)"
    TaskRun ||--o{ BulkActionItem : "(id) - (sourceRunId)"
    TaskRun |o--o{ BulkActionItem : "(id) - (destinationRunId)"
    Organization ||--o{ GithubAppInstallation : "(id) - (organizationId)"
    GithubAppInstallation ||--o{ GithubRepository : "(id) - (installationId)"
    Project ||--o| ConnectedGithubRepository : "(id) - (projectId)"
    GithubRepository ||--o{ ConnectedGithubRepository : "(id) - (repositoryId)"
    User ||--o{ ImpersonationAuditLog : "(id) - (adminId)"
    User ||--o{ ImpersonationAuditLog : "(id) - (targetId)"
    Organization ||--o{ SupportAccessRequest : "(id) - (organizationId)"
    User |o--o{ SupportAccessRequest : "(id) - (requestedById)"
    User |o--o{ SupportAccessRequest : "(id) - (approvedById)"
    Organization ||--o{ CustomerQuery : "(id) - (organizationId)"
    Project |o--o{ CustomerQuery : "(id) - (projectId)"
    RuntimeEnvironment |o--o{ CustomerQuery : "(id) - (environmentId)"
    User |o--o{ CustomerQuery : "(id) - (userId)"
    WorkerDeployment |o--o{ IntegrationDeployment : "(id) - (deploymentId)"
    Organization ||--o{ MetricsDashboard : "(id) - (organizationId)"
    Project |o--o{ MetricsDashboard : "(id) - (projectId)"
    User |o--o{ MetricsDashboard : "(id) - (ownerId)"
    Project |o--o{ llm_models : "(id) - (projectId)"
    llm_models ||--o{ llm_pricing_tiers : "(id) - (modelId)"
    llm_models ||--o{ llm_prices : "(id) - (modelId)"
    llm_pricing_tiers ||--o{ llm_prices : "(id) - (pricingTierId)"
    User |o--o{ PlatformNotification : "(id) - (userId)"
    Organization |o--o{ PlatformNotification : "(id) - (organizationId)"
    Project |o--o{ PlatformNotification : "(id) - (projectId)"
    PlatformNotification ||--o{ PlatformNotificationInteraction : "(id) - (notificationId)"
    User ||--o{ PlatformNotificationInteraction : "(id) - (userId)"
    Organization ||--o{ ErrorGroupState : "(id) - (organizationId)"
    Project ||--o{ ErrorGroupState : "(id) - (projectId)"
    RuntimeEnvironment |o--o{ ErrorGroupState : "(id) - (environmentId)"
    RuntimeEnvironment ||--o{ TaskIdentifier : "(id) - (runtimeEnvironmentId)"
    Project ||--o{ TaskIdentifier : "(id) - (projectId)"
    BackgroundWorker |o--o{ TaskIdentifier : "(id) - (currentWorkerId)"
    BackgroundWorker }o--o{ BackgroundWorkerFile : "(files) - (backgroundWorkers)"
    BackgroundWorker }o--o{ TaskQueue : "(queues) - (workers)"
    TaskRun }o--o{ Waitpoint : "(connectedWaitpoints) - (connectedRuns)"
    TaskRunExecutionSnapshot }o--o{ Waitpoint : "(completedWaitpoints) - (completedExecutionSnapshots)"
    User {
        string id PK
        string email UK
        string pendingEmail "Address awaiting confirmation; becomes `email` only after a link sent to it is opened"
        authenticationmethod authenticationMethod
        json authenticationProfile
        json authenticationExtraParams
        string authIdentifier UK
        string displayName
        string name
        string avatarUrl
        boolean admin
        json dashboardPreferences "Preferences for the dashboard"
        datetime createdAt
        datetime updatedAt
        boolean isOnCloudWaitlist "@deprecated"
        boolean featureCloud "@deprecated"
        boolean isOnHostedRepoWaitlist "@deprecated"
        boolean marketingEmails
        boolean confirmedBasicDetails
        string referralSource
        json onboardingData
        datetime mfaEnabledAt
        string mfaSecretReferenceId FK
        string mfaLastUsedCode "Hash of the last used code to prevent replay attacks"
        int sessionDuration "Maximum session lifetime in seconds for this user. Default = 1 year (Gregorian). May be further restricted by Organization.maxSessionDuration on any of the user's orgs."
        datetime nextSessionEnd "Absolute timestamp at which the user's current session must end. Written at login (and any time the effective duration is recomputed) and shortened when an admin lowers an org cap. Read on requireUser / getUser; null is treated as #quot;no enforced deadline#quot; (legacy sessions from before this shipped). Read-on-demand has zero per-request DB cost — the value piggybacks on the User fetch that requireUser already does."
        string invitationCodeId FK
    }
    MfaBackupCode {
        string id PK
        string code UK "Hash of the actual code"
        string userId FK, UK
        datetime usedAt
        datetime createdAt
        datetime updatedAt
    }
    InvitationCode {
        string id PK
        string code UK
        datetime createdAt
    }
    AuthorizationCode {
        string id PK
        string code UK
        string personalAccessTokenId FK
        string requesterIp "Client IP of the request that minted the code, compared with the approving browser's IP. Deleted with the code once it expires."
        string codeChallenge "S256 PKCE challenge from the client that minted the code. Null for codes minted by CLIs that predate it."
        int callbackPort "Loopback port the approving browser is redirected to with the approval code. Null means the user pastes it."
        string approvalCodeHash "sha256 of the approval code, which only the approving browser ever sees."
        datetime createdAt
        datetime updatedAt
    }
    PersonalAccessToken {
        string id PK
        string name "If generated by the CLI this will be #quot;cli#quot;, otherwise user-provided"
        json encryptedToken "This is the token encrypted using the ENCRYPTION_KEY"
        string obfuscatedToken "This is shown in the UI, with ********"
        string hashedToken UK "This is used to find the token in the database"
        string userId FK
        datetime revokedAt
        datetime lastAccessedAt
        datetime createdAt
        datetime updatedAt
    }
    OrganizationAccessToken {
        string id PK
        string name "User-provided name for the token"
        organizationaccesstokentype type "Used to differentiate between user-generated and system-generated tokens"
        string hashedToken UK "This is used to find the token in the database"
        string organizationId FK
        datetime expiresAt "Optional expiration date for the token"
        datetime revokedAt
        datetime lastAccessedAt
        datetime createdAt
        datetime updatedAt
    }
    Organization {
        string id PK
        string slug UK
        string title
        int maximumExecutionTimePerRunInMs
        int maximumConcurrencyLimit
        int maximumSchedulesLimit "This is deprecated and will be removed in the future"
        int maximumDevQueueSize
        int maximumDeployedQueueSize
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        string companySize
        json onboardingData
        json avatar
        boolean runsEnabled
        boolean isActivated
        boolean v2Enabled "@deprecated"
        boolean v2MarqsEnabled "@deprecated"
        boolean hasRequestedV3 "@deprecated"
        json apiRateLimiterConfig
        json realtimeRateLimiterConfig
        json batchRateLimitConfig
        json batchQueueConcurrencyConfig
        json webhookLimitsConfig "Webhook limits from the org's plan or the back office: { maxWaitersPerEnvironment, maxWaitersPerEndpoint, concurrency }. Null uses the env defaults."
        json featureFlags
        int maximumProjectCount
        int maxSessionDuration "Maximum session lifetime in seconds for members of this organization. When set, caps each member's User.sessionDuration. The most restrictive cap across all of a user's orgs (excluding nulls) wins."
        string streamBasinName "S2 basin that holds this org's realtime streams. Null until the per-org basin has been provisioned (OSS / s2-lite installs leave it null forever; reads fall back to the global basin env var). Set once at provisioning time; retention is reconfigured in-place when the org's plan changes."
        supportaccessmode supportAccessMode
        datetime supportAccessModeUpdatedAt
        string supportAccessModeUpdatedById
    }
    OrgMember {
        string id PK
        string organizationId FK, UK
        string userId FK, UK
        orgmemberrole role
        datetime createdAt
        datetime updatedAt
    }
    OrgMemberInvite {
        string id PK
        string token UK
        string email UK
        orgmemberrole role
        string rbacRoleId "Optional RBAC role to assign on invite acceptance. When set, the accept-invite flow calls the loaded RBAC plugin's setUserRole with this id after creating the OrgMember. Null = legacy behaviour, the runtime fallback derives the role from `role` above.  Plain text (not an FK) — the RBAC plugin's RbacRole table lives on a separate schema (Drizzle, not Prisma) so we can't model the FK here. Validation happens at write time (action) and read time (acceptInvite)."
        string organizationId FK, UK
        string inviterId FK
        datetime createdAt
        datetime updatedAt
    }
    RuntimeEnvironment {
        string id PK
        string slug UK
        string apiKey UK
        datetime rootApiKeyHiddenAt "Non-null prevents the root API key from being returned to users."
        string pkApiKey UK "@deprecated was for v2"
        runtimeenvironmenttype type
        boolean isBranchableEnvironment "If true, this environment has branches and is treated differently in the dashboard/API NB: this flag is NOT used for Development branches, instead (type, parentEnvironmentId) = (DEVELOPMENT, NULL) is used"
        string branchName
        string parentEnvironmentId FK
        int previewAutoArchiveAfterDays "Only configured on the PREVIEW root. Null disables automatic archiving."
        string[] previewAutoArchiveExcludedBranches
        datetime previewAutoArchiveNextCheckAt "Durable per-project cleanup progress. Null next-check disables scheduling."
        datetime previewAutoArchiveCursorCreatedAt
        string previewAutoArchiveCursorId
        json git
        datetime archivedAt "When set API calls will fail"
        string shortcode UK "A memorable code for the environment"
        int maximumConcurrencyLimit
        decimal concurrencyLimitBurstFactor
        boolean paused
        environmentpausesource pauseSource
        boolean autoEnableInternalSources
        string organizationId FK
        string projectId FK, UK
        string orgMemberId FK, UK
        datetime createdAt
        datetime updatedAt
        json builtInEnvironmentVariableOverrides "Allows us to customize the built-in environment variables for a specific environment, like TRIGGER_OTEL_SPAN_ATTRIBUTE_VALUE_LENGTH_LIMIT"
        string tunnelId
        string currentSessionId FK
    }
    api_keys {
        string id PK
        string name
        string keyHash UK
        string lastFour
        string runtimeEnvironmentId FK
        string createdByUserId FK
        string presetId
        string[] scopes
        datetime lastUsedAt
        datetime revokedAt
        datetime expiresAt
        datetime updatedAt
        datetime createdAt
    }
    RevokedApiKey {
        string id PK
        string apiKey
        string runtimeEnvironmentId FK
        datetime expiresAt
        datetime createdAt
    }
    Project {
        string id PK
        string slug UK
        string name
        string externalRef UK
        string organizationId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        datetime initializedAt "Set the first time the CLI `init` command completes against this project. Drives the dev onboarding progress."
        string defaultRuntime "Runtime used when a deployment config does not specify one. Null preserves the legacy Node 20 fallback."
        projectversion version
        runengineversion engine
        string builderProjectId
        string defaultWorkerGroupId FK
        string[] allowedWorkerQueues "The master queues they are allowed to use (impacts what they can set as default and trigger runs with)"
        json buildSettings
        json onboardingData
    }
    SecretReference {
        string id PK
        string key UK
        secretstoreprovider provider
        datetime createdAt
        datetime updatedAt
    }
    SecretStore {
        string key UK
        json value
        string version
        datetime createdAt
        datetime updatedAt
    }
    OidcSigningKey {
        string id PK
        string kid UK
        oidcsigningkeypurpose purpose
        oidcsigningkeystatus status "At most one STAGED and one ACTIVE key per purpose: enforced by a partial unique index in the migration."
        json publicJwk
        datetime readyAt
        datetime publicUntil
        string generatedByUserId
        datetime activatedAt
        string activatedByUserId
        datetime retiredAt
        string retiredByUserId
        datetime createdAt
        datetime updatedAt
    }
    DataMigration {
        string id PK
        string name UK
        datetime createdAt
        datetime updatedAt
        datetime completedAt
    }
    BackgroundWorker {
        string id PK
        string friendlyId UK
        runengineversion engine
        string contentHash
        string sdkVersion
        string cliVersion
        string projectId FK, UK
        string runtimeEnvironmentId FK, UK
        string version UK
        json metadata
        string runtime
        string runtimeVersion
        datetime createdAt
        datetime updatedAt
        string workerGroupId FK
        boolean supportsLazyAttempts
    }
    BackgroundWorkerFile {
        string id PK
        string friendlyId UK
        string filePath
        string contentHash UK
        bytes contents
        string projectId FK, UK
        datetime createdAt
    }
    prompts {
        string id PK
        string friendlyId UK
        string slug UK
        string description
        string type
        string organizationId FK
        string projectId FK, UK
        string runtimeEnvironmentId FK, UK
        string filePath
        string exportName
        json variableSchema
        string defaultModel
        json defaultConfig
        string[] tags
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
    }
    prompt_versions {
        string id PK
        int version UK
        string promptId FK, UK
        string textContent
        json chatContent
        string model
        json config
        string source
        string commitMessage
        string contentHash
        string[] labels
        string workerId FK
        string createdBy
        datetime createdAt
    }
    BackgroundWorkerTask {
        string id PK
        string slug UK
        string description
        string friendlyId UK
        string filePath
        string exportName
        string workerId FK, UK
        string projectId FK
        string fileId FK
        string runtimeEnvironmentId FK
        datetime createdAt
        datetime updatedAt
        json queueConfig
        json retryConfig
        json machineConfig
        json gates "Gates declared on the task: other queues its runs must also hold a concurrency slot in while executing. Shape: [{ queue: string, concurrencyKey?: string }]"
        string queueId FK
        int maxDurationInSeconds
        string ttl
        tasktriggersource triggerSource
        json config "Extra task configuration JSON. Shape depends on triggerSource. AGENT: { type: #quot;ai-sdk-chat#quot; }"
        json payloadSchema
    }
    WebhookEndpoint {
        string id PK
        string friendlyId UK
        string opaqueId UK "URL key. CSPRNG, NOT a friendlyId: crypto.randomBytes(16) base64url. Generated by the create path (deploy-sync / P2 API), NOT by @default."
        string organizationId
        string projectId
        string runtimeEnvironmentId UK
        runtimeenvironmenttype environmentType
        string endpointTenantId UK
        string endpointExternalRef UK
        string source
        string declaredId UK "The declared webhooks.endpoint.define id (string ref, no relation)."
        json routingTargets "Subscribers of this endpoint: StoredWebhookRoutingTarget[] ({ id, type: #quot;task#quot; | #quot;session#quot;, ..., filterAst })."
        json verifierArtifact
        json metadata
        string secretProvisioning
        string setupPrompt "The source's instructions for an AI agent registering this endpoint with the provider."
        string signingSecretKey "SecretReference.key string. Plain String, NO @relation -> no FK to SecretReference."
        webhookendpointstatus status
        datetime manuallyDeactivatedAt "When an operator disabled the endpoint via the dashboard/API. Null means the declarative sync owns the status: a redeploy that re-declares a previously-removed (auto-deactivated) webhook reactivates it. Non-null means the operator disabled it, so the sync leaves the status alone."
        datetime createdAt
        datetime updatedAt
    }
    WebhookDelivery {
        string id PK
        string friendlyId
        string webhookEndpointId
        string organizationId
        string projectId
        string runtimeEnvironmentId
        runtimeenvironmenttype environmentType
        string externalDeliveryId
        string idempotencyKey
        string runId
        webhookdeliverystatus status
        boolean isTest "Set from the x-trigger-test ingress header; marks console/test-send deliveries so the list can filter them."
        json parsedEvent
        json headers
        string rawBodyHash
        string errorMessage
        string filterReason
        json targets "Per-target results: WebhookDeliveryTargetResult[] ({ id, type, status, reason?, runId?, error? })."
        datetime createdAt PK
        datetime updatedAt
        datetime processedAt
        int retentionDays PK "Retention class in days: the LIST partition key, stamped at ingest from the org's plan and encoded in the id."
    }
    Alert {
        string id PK
        string organizationId
        string projectId
        string environmentId
        string capabilityKey
        json params
        json condition
        string query
        datetime expiresAt
        string lifecycle
        string attention
        string observedCondition
        datetime observedConditionChangedAt
        datetime pendingMetSince
        json evaluatorState
        string evaluationLeaseId
        datetime evaluationLeaseUntil
        datetime lastEvaluatedAt
        datetime nextEvaluationAt
        json notifiers
        string name
        string createdBy
        datetime createdAt
        datetime updatedAt
        datetime resolvedAt
        string resolvedBy
        datetime stoppedAt
        string stoppedBy
        datetime lastOccurrenceAt
        int occurrencesSinceRaise
        bigint occurrencesTotal
    }
    AlertEvent {
        string id PK
        string alertId
        string type
        datetime at PK
        json facts
        string actorId
        string sourceEventId
    }
    AlertOccurrenceSeen {
        string alertId PK
        string sourceEventId PK
        datetime rowTime
        datetime seenAt
        datetime expiresAt
    }
    AlertDelivery {
        string id PK
        string alertEventId UK
        datetime eventAt PK, UK
        string notifierId UK
        json notifier
        string status
        string claimId
        datetime claimedAt
        datetime deliveredAt
        string failureReason
        json providerMetadata
        int attempts
        datetime firstAttemptAt
        datetime createdAt
    }
    PlaygroundConversation {
        string id PK
        string chatId UK "The chat session ID used by the transport"
        string title "User-editable conversation title (auto-generated from first message)"
        string agentSlug "Which agent this conversation is with"
        string runId FK "The current active run backing this conversation (null if no run yet)"
        json clientData "The client data JSON used for this conversation"
        json messages "Accumulated UIMessages from completed turns (for resume without stream replay)"
        string lastEventId "Last SSE event ID — resume from this position to avoid replaying old turns"
        string projectId FK
        string runtimeEnvironmentId FK, UK
        string userId "The user who started this conversation"
        datetime createdAt
        datetime updatedAt
    }
    Session {
        string id PK
        string friendlyId UK
        string externalId UK "User-supplied identifier scoped to the environment. Used for idempotent upsert and for resolving sessions via the public API."
        string type "Plain string — intentionally not an enum."
        string projectId "Denormalized scoping columns — no FK relations."
        string runtimeEnvironmentId UK
        runtimeenvironmenttype environmentType
        string organizationId
        string taskIdentifier "Task this session triggers runs against. Required — Sessions are task-bound: creating a session also triggers its first run, and every subsequent re-trigger uses this same identifier."
        json triggerConfig "Trigger config used for every run this session schedules. Shape (validated at the route layer, opaque to the DB): { basePayload: object, machine?: string, queue?: string, tags?: string[], maxAttempts?: number, idleTimeoutInSeconds?: number } `basePayload` carries the customer's client-data; runtime fields (chatId, messages, trigger) are merged at trigger time."
        boolean isTest "Whether this session was created from the Test/playground UI rather than by a real chat.agent() trigger. Mirrors TaskRun.isTest."
        string[] tags
        json metadata
        string currentRunId "Live run pointer — non-FK so run deletion never cascades. Can lag reality; the `.in/append` handler re-checks the snapshot status before reusing it."
        int currentRunVersion "Monotonic counter used for optimistic locking on `currentRunId` swaps. Bumped atomically alongside any update that changes `currentRunId`."
        datetime closedAt "Terminal markers — written once, never flipped back."
        string closedReason
        datetime expiresAt
        datetime createdAt
        datetime updatedAt
        string streamBasinName "S2 basin where this session's stream pair lives. Stamped at create time from `Organization.streamBasinName` so reads can resolve the basin without joining org. Null when the org has no per-org basin (OSS, or pre-backfill); reads fall back to the global basin."
        string chatSnapshotStoragePath "Storage URI (with protocol prefix) for this session's chat.agent snapshot blob. Set on first snapshot write. Null = pre-column session, fall back to computed default path."
    }
    SessionRun {
        string id PK
        string sessionId FK
        string runId UK "TaskRun.id (no FK — runs may be archived independently of session history)"
        string reason "One of: #quot;initial#quot; | #quot;continuation#quot; | #quot;upgrade#quot; | #quot;manual#quot;. Plain string for forward-compat with future trigger reasons."
        datetime triggeredAt
    }
    TaskRun {
        string id PK
        int number
        string friendlyId UK
        runengineversion engine
        taskrunstatus status
        string statusReason
        string idempotencyKey UK
        datetime idempotencyKeyExpiresAt
        json idempotencyKeyOptions "Stores the user-provided key and scope: { key: string, scope: #quot;run#quot; | #quot;attempt#quot; | #quot;global#quot; }"
        json debounce "Debounce options: { key: string, delay: string, createdAt: Date }"
        string taskIdentifier UK
        boolean isTest
        string payload
        string payloadType
        json context
        json traceContext
        string traceId
        string spanId
        string runtimeEnvironmentId FK, UK
        runtimeenvironmenttype environmentType
        string projectId FK
        string organizationId
        string queue
        string lockedQueueId
        string workerQueue "The main queue that this run is part of"
        string region "User-facing geo region, stamped at trigger; workerQueue is where it actually ran."
        string secondaryMasterQueue "@deprecated"
        int attemptNumber "From engine v2+ this will be defined after a run has been dequeued (starting at 1)"
        datetime createdAt
        datetime updatedAt
        string[] runTags "Denormized column that holds the raw tags"
        string taskVersion "Denormalized version of the background worker task"
        string sdkVersion
        string cliVersion
        datetime startedAt "startedAt marks the point at which a run is dequeued from MarQS"
        datetime executedAt "executedAt is set when the first attempt is about to execute"
        datetime completedAt
        string machinePreset
        int usageDurationMs
        float costInCents
        float baseCostInCents
        datetime lockedAt
        string lockedById FK
        string lockedToVersionId FK
        int priorityMs "The #quot;priority#quot; of the run. This is just a negative offset in ms for the queue timestamp E.g. a value of 60_000 would put the run into the queue 60s ago."
        string concurrencyKey
        json gates "Gates for this run: other queues it must also hold a concurrency slot in while executing. Shape: [{ queue: string, concurrencyKey?: string }]"
        datetime delayUntil
        datetime queuedAt
        string ttl
        datetime expiredAt
        int maxAttempts
        json lockedRetryConfig
        string oneTimeUseToken UK "optional token that can be used to authenticate the task run"
        string taskEventStore "Where the logs are stored"
        datetime queueTimestamp
        string scheduleInstanceId
        string scheduleId
        string[] bulkActionGroupIds
        datetime logsDeletedAt
        string replayedFromTaskRunFriendlyId
        string rootTaskRunId FK
        string parentTaskRunId FK
        string parentTaskRunAttemptId FK
        string batchId FK
        boolean resumeParentOnCompletion "whether or not the task run was created because of a triggerAndWait for batchTriggerAndWait"
        int depth "The depth of this task run in the task run hierarchy"
        string parentSpanId "The span ID of the #quot;trigger#quot; span in the parent task run"
        json runChainState "Holds the state of the run chain for deadlock detection"
        string seedMetadata "seed run metadata"
        string seedMetadataType
        string metadata "Run metadata"
        string metadataType
        int metadataVersion
        json annotations "Structured annotations: triggerSource, triggerAction, rootTriggerSource, rootScheduleId"
        boolean isWarmStart "Whether the latest attempt was a warm start. Null until first attempt starts."
        string output "Run output"
        string outputType
        json error "Run error"
        string planType "Organization's billing plan type (cached for fallback when billing API fails)"
        int maxDurationInSeconds
        string realtimeStreamsVersion "The version of the realtime streams implementation used by the run"
        string[] realtimeStreams "Store the stream keys that are being used by the run"
        string streamBasinName "S2 basin where this run's realtime streams live. Stamped at create time from `Organization.streamBasinName` so reads can resolve the basin without joining org. Null when the org has no per-org basin (OSS, or pre-backfill); reads fall back to the global basin."
    }
    TaskRunTemplate {
        string id PK
        string taskSlug
        tasktriggersource triggerSource
        string label
        string payload
        string payloadType
        string metadata
        string metadataType
        string queue
        string concurrencyKey
        int ttlSeconds
        int maxAttempts
        int maxDurationSeconds
        string[] tags
        string machinePreset
        string projectId
        string organizationId
        datetime createdAt
        datetime updatedAt
    }
    TaskRunExecutionSnapshot {
        string id PK
        runengineversion engine "This should always be 2+ (V1 didn't use the run engine or snapshots)"
        taskrunexecutionstatus executionStatus "The execution status"
        string description "For debugging"
        boolean isValid "We store invalid snapshots as a record of the run state when we tried to move"
        string error
        string previousSnapshotId "The previous snapshot ID"
        string runId FK "Run"
        taskrunstatus runStatus
        string batchId
        int attemptNumber "This is the current run attempt number. Users can define how many attempts they want for a run."
        string environmentId "Environment"
        runtimeenvironmenttype environmentType
        string projectId
        string organizationId
        string[] completedWaitpointOrder "An array of waitpoint IDs in the correct order, used for batches"
        string checkpointId FK "Checkpoint"
        string workerId "Worker"
        string runnerId
        datetime createdAt
        datetime updatedAt
        datetime lastHeartbeatAt
        json metadata "Metadata used by various systems in the run engine"
    }
    TaskRunCheckpoint {
        string id PK
        string friendlyId UK
        taskruncheckpointtype type
        string location
        string imageRef
        string reason
        string metadata
        string projectId FK
        string runtimeEnvironmentId FK
        datetime createdAt
        datetime updatedAt
    }
    Waitpoint {
        string id PK
        string friendlyId UK
        waitpointtype type
        waitpointstatus status
        datetime completedAt
        string idempotencyKey UK "If it's an Event type waitpoint, this is the event. It can also be provided for the DATETIME type"
        boolean userProvidedIdempotencyKey "If this is true then we can show it in the dashboard/return it from the SDK"
        datetime idempotencyKeyExpiresAt "If there's a user provided idempotency key, this is the time it expires at"
        string inactiveIdempotencyKey "If an idempotencyKey is no longer active, we store it here and generate a new one for the idempotencyKey field. Clearing an idempotencyKey is useful for debounce or cancelling child runs. This is a workaround because Prisma doesn't support partial indexes."
        string completedByTaskRunId FK, UK "If it's a RUN type waitpoint, this is the associated run"
        datetime completedAfter "If it's a DATETIME type waitpoint, this is the date. If it's a MANUAL waitpoint, this can be set as the `timeout`."
        string completedByBatchId FK "If it's a BATCH type waitpoint, this is the associated batch"
        string output "When completed, an output can be stored here"
        string outputType
        boolean outputIsError
        string projectId FK
        string environmentId FK, UK
        datetime createdAt
        datetime updatedAt
        string[] tags "Denormized column that holds the raw tags Denormalized column that holds the raw tags"
    }
    TaskRunWaitpoint {
        string id PK
        string taskRunId FK, UK
        string waitpointId FK, UK
        string projectId FK
        string spanIdToComplete "This span id is completed when the waitpoint is completed. This is used with cached runs (idempotent)"
        string batchId FK
        int batchIndex UK
        datetime createdAt
        datetime updatedAt
    }
    WaitpointTag {
        string id PK
        string name UK
        string environmentId FK, UK
        string projectId FK
        datetime createdAt
    }
    FeatureFlag {
        string id PK
        string key UK
        json value
        datetime createdAt
        datetime updatedAt
    }
    WorkerInstance {
        string id PK
        string name "For example #quot;worker-1#quot;"
        string resourceIdentifier UK "If managed, it will default to the name, e.g. #quot;worker-1#quot; If unmanged, it will be prefixed with the deployment ID e.g. #quot;deploy-123-worker-1#quot;"
        json metadata
        string workerGroupId FK, UK
        string organizationId FK
        string projectId FK
        string environmentId FK
        string deploymentId FK
        datetime createdAt
        datetime updatedAt
        datetime lastDequeueAt
        datetime lastHeartbeatAt
    }
    WorkerInstanceGroup {
        string id PK
        workerinstancegrouptype type
        string name "For example #quot;us-east-1#quot;"
        string masterQueue UK "If managed, it will default to the name, e.g. #quot;us-east-1#quot; If unmanged, it will be prefixed with the project ID e.g. #quot;project_1-us-east-1#quot;"
        string description "#quot;N. Virginia, USA#quot;, #quot;Frankfurt, Germany#quot;, etc. Used for display purposes"
        boolean hidden
        string tokenId FK, UK
        string organizationId FK
        string projectId FK
        string cloudProvider
        string location "#quot;usa#quot;, #quot;europe#quot;, etc. Used like a pseudo enum for things like flags"
        string staticIPs
        workloadtype workloadType
        string region "Geo region; container + MICROVM groups for one geo share it. Set-once once it has runs."
        boolean enableFastPath "When true, runs enqueued to this worker queue may skip the intermediate queue and be pushed directly to the worker queue when concurrency is available."
        datetime createdAt
        datetime updatedAt
    }
    WorkerGroupToken {
        string id PK
        string tokenHash UK
        datetime createdAt
        datetime updatedAt
    }
    TaskRunTag {
        string id PK
        string name UK
        string friendlyId UK
        string projectId FK, UK
        datetime createdAt
    }
    TaskRunDependency {
        string id PK
        string taskRunId FK, UK
        string checkpointEventId FK, UK
        string dependentAttemptId FK
        string dependentBatchRunId FK
        datetime createdAt
        datetime updatedAt
        datetime resumedAt
    }
    TaskRunCounter {
        string taskIdentifier PK
        int lastNumber
    }
    TaskRunNumberCounter {
        string id PK
        string taskIdentifier UK
        string environmentId FK, UK
        int lastNumber
    }
    TaskRunAttempt {
        string id PK
        int number UK
        string friendlyId UK
        string taskRunId FK, UK
        string backgroundWorkerId FK
        string backgroundWorkerTaskId FK
        string runtimeEnvironmentId FK
        string queueId FK
        taskrunattemptstatus status
        datetime createdAt
        datetime updatedAt
        datetime startedAt
        datetime completedAt
        int usageDurationMs
        json error
        string output
        string outputType
    }
    TaskEvent {
        string id PK
        string message "This matches the span name for a trace event, or the log body for a log event"
        string traceId
        string spanId
        string parentId
        string tracestate
        boolean isError
        boolean isPartial
        boolean isCancelled
        boolean isDebug "deprecated: don't use this, moving this to properties, this now uses TaskEventKind.LOG"
        string serviceName
        string serviceNamespace
        taskeventlevel level
        taskeventkind kind
        taskeventstatus status
        json links
        json events
        bigint startTime "This is the time the event started in nanoseconds since the epoch"
        bigint duration "This is the duration of the event in nanoseconds"
        string attemptId
        int attemptNumber
        string environmentId
        runtimeenvironmenttype environmentType
        string organizationId
        string projectId
        string projectRef
        string runId
        boolean runIsTest
        string idempotencyKey
        string taskSlug
        string taskPath
        string taskExportName
        string workerId
        string workerVersion
        string queueId
        string queueName
        string batchId
        json properties "This represents all the span attributes available, like http.status_code, and special attributes like $style.icon, $output, $metadata.payload.userId, as it's used for searching and filtering"
        json metadata "This represents all span attributes in the $metadata namespace, like $metadata.payload"
        json style "This represents all span attributes in the $style namespace, like $style"
        json output "This represents all span attributes in the $output namespace, like $output"
        string outputType "This represents the mimetype of the output, such as application/json or application/super+json"
        json payload
        string payloadType
        datetime createdAt
        int usageDurationMs
        float usageCostInCents
        string machinePreset
        float machinePresetCpu
        float machinePresetMemory
        float machinePresetCentsPerMs
    }
    TaskQueue {
        string id PK
        string friendlyId UK
        string name UK
        taskqueuetype type
        taskqueueversion version
        string orderableName
        taskqueueconcurrencyversion concurrencyVersion "How the queue's concurrency was declared: V1 = legacy concurrencyLimit semantics (per key when keyed, whole queue otherwise), V2 = the explicit perKey/total shape."
        taskqueuerole role "QUEUE rows are real queues; LIMIT rows back named concurrency limits and are excluded from every queue-facing surface."
        string projectId FK
        string runtimeEnvironmentId FK, UK
        int concurrencyLimit "Represents the current concurrency limit for the queue"
        datetime concurrencyLimitOverriddenAt "When the concurrency limit was overridden"
        string concurrencyLimitOverriddenBy "Who overrode the concurrency limit (will be null if overridden via the API)"
        int concurrencyLimitBase "If concurrencyLimit is overridden, this is the overridden value"
        decimal concurrencyLimitOverridePercent "If the override was expressed as a percentage of the environment limit, this stores that percentage (the source of truth). The absolute concurrencyLimit is materialized from it. Decimal(5,2) allows fractional percentages like 12.50% (0.01–100.00)."
        int totalConcurrencyLimit "Caps total concurrent runs across ALL concurrencyKey values of this queue (concurrencyLimit applies per key value). Null = no total cap."
        datetime totalConcurrencyLimitOverriddenAt "When the total concurrency limit was overridden"
        string totalConcurrencyLimitOverriddenBy "Who overrode the total concurrency limit (null when overridden via the API)"
        int totalConcurrencyLimitBase "If totalConcurrencyLimit is overridden, the declared value it reverts to on reset"
        json rateLimit
        boolean paused
        datetime archivedAt "Hidden from the dashboard queue list. Display-only: never read on trigger/dequeue paths."
        datetime createdAt
        datetime updatedAt
    }
    BatchTaskRun {
        string id PK
        string friendlyId UK
        string idempotencyKey UK
        datetime idempotencyKeyExpiresAt
        batchtaskrunstatus status
        string runtimeEnvironmentId UK
        datetime createdAt
        datetime updatedAt
        string[] runIds "Friendly IDs"
        int runCount
        string payload
        string payloadType
        json options
        string batchVersion
        boolean sealed "sealed is set to true once no more items can be added to the batch"
        datetime sealedAt
        int expectedCount "this is the expected number of items in the batch"
        int completedCount "this is the completed number of items in the batch. once this reaches expectedCount, and the batch is sealed, the batch is considered completed"
        datetime completedAt
        datetime resumedAt
        int processingJobsCount "this is used to be able to #quot;seal#quot; this BatchTaskRun when all of the runs have been triggered asynchronously, and using the #quot;parallel#quot; processing strategy"
        int processingJobsExpectedCount
        string oneTimeUseToken UK "optional token that can be used to authenticate the task run"
        datetime processingStartedAt "When processing started (status changed to PROCESSING)"
        datetime processingCompletedAt "When processing completed (all items processed)"
        int successfulRunCount "Count of successfully created runs"
        int failedRunCount "Count of failed run creations"
        string taskIdentifier
        string checkpointEventId FK, UK
        string dependentTaskAttemptId FK
    }
    BatchTaskRunItem {
        string id PK
        batchtaskrunitemstatus status
        string batchTaskRunId FK, UK
        string taskRunId FK, UK
        string taskRunAttemptId FK
        datetime createdAt
        datetime updatedAt
        datetime completedAt
    }
    BatchTaskRunError {
        string id PK
        string batchTaskRunId FK, UK
        int index UK "Which item in the batch (0-based index)"
        string taskIdentifier "The task identifier that was being triggered"
        string payload "The payload that failed (JSON, may be truncated)"
        json options "The options that were used"
        string error "Error message"
        string errorCode "Error code if available"
        datetime createdAt
    }
    EnvironmentVariable {
        string id PK
        string friendlyId UK
        string key UK
        string projectId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    EnvironmentVariableValue {
        string id PK
        string valueReferenceId FK
        string variableId FK, UK
        string environmentId FK, UK
        boolean isSecret "If true, the value is secret and cannot be revealed"
        datetime createdAt
        datetime updatedAt
        int version
        json lastUpdatedBy
    }
    Checkpoint {
        string id PK
        string friendlyId UK
        checkpointtype type
        string location
        string imageRef
        string reason
        string metadata
        string runId FK
        string attemptId FK
        int attemptNumber
        string projectId FK
        string runtimeEnvironmentId FK
        datetime createdAt
        datetime updatedAt
    }
    CheckpointRestoreEvent {
        string id PK
        checkpointrestoreeventtype type
        string reason
        string metadata
        string checkpointId FK
        string runId FK
        string attemptId FK
        string projectId FK
        string runtimeEnvironmentId FK
        datetime createdAt
        datetime updatedAt
    }
    WorkerDeployment {
        string id PK
        string contentHash
        string friendlyId UK
        string shortCode UK
        string version UK
        string runtime
        string runtimeVersion
        string cliVersion "CLI version that initiated the deploy, stamped at initialization"
        string imageReference
        string imagePlatform
        string templateKey "Key of the microVM template prebuilt from this deployment's image, set when the builder succeeds"
        string indexObjectKey "Object key of the full index snapshot in the deployment index store. Set only after the upload succeeds."
        string indexSha256 "sha256 hex of the snapshot's uncompressed JSON"
        int indexFormatVersion
        json externalBuildData
        json buildServerMetadata
        json buildEnvVars "Encrypted build-time env vars for pre-bundled (fromBundle) deploys, as an EncryptedSecretValue envelope. Kept for the redeploy window, then cleared by a sweep."
        workerdeploymentstatus status
        workerdeploymenttype type
        string projectId FK, UK
        string environmentId FK, UK
        string workerId FK, UK
        string triggeredById FK
        string triggeredVia
        string commitSHA
        string externalId
        datetime startedAt
        datetime installedAt
        datetime builtAt
        datetime deployedAt
        datetime failedAt
        json errorData
        datetime canceledAt
        string canceledReason
        json git
        datetime createdAt
        datetime updatedAt
    }
    WorkerDeploymentPromotion {
        string id PK
        string label UK "This is the promotion label, e.g. #quot;current#quot;"
        string deploymentId FK
        string environmentId FK, UK
    }
    TaskSchedule {
        string id PK
        scheduletype type
        string friendlyId UK "users see this as `id`. They start with schedule_"
        string taskIdentifier "a reference to a task (not a foreign key because it's across versions)"
        string deduplicationKey UK "can be provided and we won't create another with the same key"
        boolean userProvidedDeduplicationKey
        string generatorExpression "the CRON pattern"
        string generatorDescription
        schedulegeneratortype generatorType
        string timezone "These are IANA format string, or the default #quot;UTC#quot;. E.g. #quot;America/New_York#quot;"
        int windowDurationSeconds
        int windowPercentage
        int defaultWindowDurationSeconds
        int minimumWindowDurationSeconds
        string externalId "Can be provided by the user then accessed inside a run"
        datetime lastRunTriggeredAt "@deprecated stop writing 2026-04-30; reads moved out of code (UI now derives from cron's previous slot). Drop in follow-up."
        string projectId FK, UK
        datetime createdAt
        datetime updatedAt
        boolean active
    }
    TaskScheduleInstance {
        string id PK
        string taskScheduleId FK, UK
        string environmentId FK, UK
        string projectId FK
        int schedulePhase
        datetime createdAt
        datetime updatedAt
        boolean active
        datetime lastScheduledTimestamp "@deprecated stop writing 2026-04-30; engine derives from cron + exactScheduleTime. Drop in follow-up."
        datetime nextScheduledTimestamp "@deprecated stop writing 2026-04-30; engine derives from cron + now(). Drop in follow-up."
    }
    RuntimeEnvironmentSession {
        string id PK
        string ipAddress
        string environmentId FK
        datetime createdAt
        datetime updatedAt
        datetime disconnectedAt
    }
    ProjectAlertChannel {
        string id PK
        string friendlyId UK
        string deduplicationKey UK "can be provided and we won't create another with the same key"
        boolean userProvidedDeduplicationKey
        string integrationId FK
        boolean enabled
        projectalertchanneltype type
        string name
        json properties
        projectalerttype[] alertTypes
        runtimeenvironmenttype[] environmentTypes
        json errorAlertConfig
        string projectId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    ProjectAlert {
        string id PK
        string friendlyId UK
        string projectId FK
        string environmentId FK
        string channelId FK
        projectalertstatus status
        projectalerttype type
        string taskRunAttemptId
        string taskRunId
        string workerDeploymentId FK
        datetime createdAt
        datetime updatedAt
    }
    ProjectAlertStorage {
        string id PK
        string projectId FK
        string alertChannelId FK
        projectalerttype alertType
        string storageId
        json storageData
        datetime createdAt
        datetime updatedAt
    }
    OrganizationIntegration {
        string id PK
        string friendlyId UK
        integrationservice service
        string externalOrganizationId "Identifier for external, integration's organization (e.g. Vercel's team)"
        json integrationData
        string tokenReferenceId FK
        string organizationId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    OrganizationProjectIntegration {
        string id PK
        string organizationIntegrationId FK
        string projectId FK
        string externalEntityId "Identifier for webhooks, for example Vercel's projectId"
        json integrationData "Save useful data like config or external entity name"
        string installedBy "UserId who installed the integration"
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    BulkActionGroup {
        string id PK
        string friendlyId UK
        string projectId FK
        string environmentId FK
        bulkactiontype type
        bulkactionstatus status "When the group is created it's pending. After we've processed all the items it's completed. This does not mean the associated runs are completed."
        string queryName "The query that will be executed to get the runs to process (if null, we are passing run ids directly)"
        json params "The params that will be passed to the query"
        string dedupeKey "Billing-limit dedupe key (grace hitAt or resolve job key). Null for dashboard bulk actions."
        json cursor "The cursor that will be passed to the query (null for the first page)"
        int successCount "The number of runs that have been processed successfully"
        int failureCount "The number of runs that have been failed"
        int totalCount "The total number of runs that will be processed"
        bulkactionnotificationtype completionNotification
        string userId FK
        string name "The name of the bulk action"
        datetime completedAt "The time the bulk action was completed"
        datetime createdAt
        datetime updatedAt
    }
    BulkActionItem {
        string id PK
        string friendlyId "@deprecated not used in new BulkActions"
        string groupId
        bulkactiontype type
        bulkactionitemstatus status "When the item is created it's pending. After we've processed the item it's completed. This does not mean the associated runs are completed."
        string sourceRunId FK
        string destinationRunId FK
        string error
        datetime createdAt
        datetime updatedAt
    }
    RealtimeStreamChunk {
        string id PK
        string key
        string value
        int sequence
        string runId
        datetime createdAt
    }
    TaskEventPartitioned {
        string id PK
        string message "This matches the span name for a trace event, or the log body for a log event"
        string traceId
        string spanId
        string parentId
        string tracestate
        boolean isError
        boolean isPartial
        boolean isCancelled
        string serviceName
        string serviceNamespace
        taskeventlevel level
        taskeventkind kind
        taskeventstatus status
        json links
        json events
        bigint startTime "This is the time the event started in nanoseconds since the epoch"
        bigint duration "This is the duration of the event in nanoseconds"
        string attemptId
        int attemptNumber
        string environmentId
        runtimeenvironmenttype environmentType
        string organizationId
        string projectId
        string projectRef
        string runId
        boolean runIsTest
        string idempotencyKey
        string taskSlug
        string taskPath
        string taskExportName
        string workerId
        string workerVersion
        string queueId
        string queueName
        string batchId
        json properties "This represents all the span attributes available, like http.status_code, and special attributes like $style.icon, $output, $metadata.payload.userId, as it's used for searching and filtering"
        json metadata "This represents all span attributes in the $metadata namespace, like $metadata.payload"
        json style "This represents all span attributes in the $style namespace, like $style"
        json output "This represents all span attributes in the $output namespace, like $output"
        string outputType "This represents the mimetype of the output, such as application/json or application/super+json"
        json payload
        string payloadType
        datetime createdAt PK
        int usageDurationMs
        float usageCostInCents
        string machinePreset
        float machinePresetCpu
        float machinePresetMemory
        float machinePresetCentsPerMs
    }
    GithubAppInstallation {
        string id PK
        bigint appInstallationId UK
        bigint targetId
        string targetType
        string accountHandle
        json permissions
        githubrepositoryselection repositorySelection
        string installedBy
        string installedByUserId
        string organizationId FK
        datetime deletedAt
        datetime suspendedAt
        datetime createdAt
        datetime updatedAt
    }
    GithubRepository {
        string id PK
        bigint githubId UK
        string name
        string fullName
        string htmlUrl
        boolean private
        string defaultBranch
        string installationId FK, UK
        datetime createdAt
        datetime updatedAt
    }
    ConnectedGithubRepository {
        string id PK
        string projectId FK, UK
        string repositoryId FK
        json branchTracking
        boolean previewDeploymentsEnabled
        json buildFilters
        datetime createdAt
        datetime updatedAt
    }
    ImpersonationAuditLog {
        string id PK
        impersonationauditlogaction action
        string adminId FK
        string targetId FK
        string ipAddress
        datetime createdAt
    }
    SupportAccessRequest {
        string id PK
        string organizationId FK
        string requestedById FK
        string reason
        supportaccessrequeststatus status
        string approvedById FK
        datetime approvedAt
        datetime expiresAt
        string cancelledById
        datetime cancelledAt
        datetime createdAt
        datetime updatedAt
    }
    CustomerQuery {
        string id PK
        string query "The TSQL query text that was executed"
        customerqueryscope scope "The scope of the query (determines which tenant IDs were used)"
        json stats "Query execution statistics from ClickHouse"
        string title "AI-generated title summarizing the query"
        customerquerysource source "Where the query originated from"
        string organizationId FK
        string projectId FK
        string environmentId FK
        string userId FK
        string filterPeriod "Time filter settings used when the query was executed Period like #quot;7d#quot;, #quot;24h#quot;, etc."
        datetime filterFrom "Custom start date (ISO 8601)"
        datetime filterTo "Custom end date (ISO 8601)"
        datetime createdAt
    }
    IntegrationDeployment {
        string id PK
        string integrationName "For example Vercel"
        string integrationDeploymentId "External ID"
        string commitSHA
        string deploymentId FK
        string status "External deployment status"
        datetime createdAt
        datetime updatedAt
    }
    MetricsDashboard {
        string id PK
        string friendlyId UK
        string title
        string description
        string organizationId FK
        string projectId FK
        string ownerId FK
        datetime createdAt
        datetime updatedAt
        string layout "JSON that defines the config, queries, layout and config of all widgets. There will be a version field for the format."
    }
    llm_models {
        string id PK
        string friendlyId UK
        string projectId FK, UK
        string modelName UK
        string matchPattern
        datetime startDate UK
        string source
        datetime createdAt
        datetime updatedAt
        string provider
        string description
        int contextWindow
        int maxOutputTokens
        string[] capabilities
        boolean isHidden
        string baseModelName
        string pricingUnit
    }
    llm_pricing_tiers {
        string id PK
        string modelId FK, UK
        string name UK
        boolean isDefault
        int priority UK
        json conditions
    }
    llm_prices {
        string id PK
        string modelId FK, UK
        string pricingTierId FK, UK
        string usageType UK
        decimal price
    }
    PlatformNotification {
        string id PK
        string friendlyId UK
        string title "Admin-facing title for identification in admin tools"
        json payload "Versioned JSON rendering content (see payload schema in spec)"
        platformnotificationsurface surface
        platformnotificationscope scope
        string userId FK
        string organizationId FK
        string projectId FK
        datetime startsAt "When notification becomes active"
        datetime endsAt "When notification expires"
        int cliMaxDaysAfterFirstSeen "CLI: auto-expire N days after user first saw it"
        int cliMaxShowCount "CLI: auto-expire after shown N times to user"
        int cliShowEvery "CLI: only show every N-th request (e.g. cliShowEvery=3 means show on 3rd, 6th, 9th…)"
        int priority "Ordering within same scope level (higher = more important)"
        boolean isDraft "Staged-but-unpublished. While true the notification is hidden from all user-facing reads regardless of startsAt/endsAt; publishing sets real dates and flips this to false."
        datetime archivedAt "Soft delete"
        datetime createdAt
        datetime updatedAt
    }
    PlatformNotificationInteraction {
        string id PK
        string notificationId FK, UK
        string userId FK, UK
        datetime firstSeenAt "Set by beacon/CLI GET on first impression"
        int showCount "Times shown (incremented per beacon/CLI view)"
        datetime webappDismissedAt "User dismissed in webapp"
        datetime webappClickedAt "User clicked a link in webapp"
        datetime cliDismissedAt "Auto-dismissed or expired in CLI"
        datetime createdAt
        datetime updatedAt
    }
    ErrorGroupState {
        string id PK
        string organizationId FK
        string projectId FK
        string environmentId FK, UK
        string taskIdentifier UK
        string errorFingerprint UK
        errorgroupstatus status
        datetime ignoredUntil "* * Error is ignored until this date"
        int ignoredUntilOccurrenceRate "* * Error is ignored until this occurrence rate"
        int ignoredUntilTotalOccurrences "* * Error is ignored until this total occurrences"
        bigint ignoredAtOccurrenceCount "Total occurrence count at the time the error was ignored (from ClickHouse). Used with ignoredUntilTotalOccurrences to compute occurrences since ignoring."
        datetime ignoredAt "* * Error was ignored at this date"
        string ignoredReason "* * Reason for ignoring the error"
        string ignoredByUserId "* * User who ignored the error"
        datetime resolvedAt "* * Error was resolved at this date"
        string resolvedInVersion "* * Error was resolved in this version"
        string resolvedBy "* * User who resolved the error"
        datetime createdAt
        datetime updatedAt
    }
    TaskIdentifier {
        string id PK
        string runtimeEnvironmentId FK, UK
        string projectId FK
        string slug UK
        tasktriggersource currentTriggerSource
        string currentWorkerId FK
        datetime firstSeenAt
        datetime lastSeenAt
        boolean isInLatestDeployment
    }
    OrganizationDataStore {
        string id PK
        string key UK "Human-readable unique key (e.g. #quot;hipaa-clickhouse-us-east#quot;)"
        string[] organizationIds "Organization IDs that use this data store"
        datastorekind kind
        json config "Versioned config JSON. Structure is discriminated by the top-level `version` field."
        datetime createdAt
        datetime updatedAt
    }
```