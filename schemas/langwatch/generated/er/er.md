```mermaid
erDiagram
    User ||--o{ Account : "(id) - (userId)"
    User ||--o{ AccountCredential : "(id) - (userId)"
    User ||--o{ Session : "(id) - (userId)"
    User ||--o{ TwoFactor : "(id) - (userId)"
    User ||--o{ Passkey : "(id) - (userId)"
    SsoConnection ||--o| SsoConnectionReproofCursor : "(id) - (connectionId)"
    SsoVerifiedDomain ||--o{ SsoVerifiedDomainHolder : "(domain) - (domain)"
    CustomRole |o--o{ TeamUser : "(id) - (assignedRoleId)"
    User ||--o{ TeamUser : "(id) - (userId)"
    Team ||--o{ TeamUser : "(id) - (teamId)"
    User ||--o{ OrganizationUser : "(id) - (userId)"
    Organization ||--o{ OrganizationUser : "(id) - (organizationId)"
    Organization ||--o{ Team : "(id) - (organizationId)"
    User |o--o{ Team : "(id) - (ownerUserId)"
    Organization |o--o{ IssuedLicense : "(id) - (organizationId)"
    Organization ||--o| ConnectedBillingAccount : "(id) - (organizationId)"
    Organization ||--o{ ScimRequestLog : "(id) - (organizationId)"
    Organization ||--o{ ScimToken : "(id) - (organizationId)"
    User ||--o{ ScimExternalId : "(id) - (userId)"
    User ||--o{ ScimUserResource : "(id) - (userId)"
    Team ||--o{ Project : "(id) - (teamId)"
    User |o--o{ Project : "(id) - (ownerUserId)"
    Organization ||--o{ OrganizationInvite : "(id) - (organizationId)"
    User |o--o{ OrganizationInvite : "(id) - (requestedBy)"
    Project ||--o{ Monitor : "(id) - (projectId)"
    Experiment |o--o| Monitor : "(id) - (experimentId)"
    Evaluator |o--o{ Monitor : "(id) - (evaluatorId)"
    Project ||--o{ Cost : "(id) - (projectId)"
    Project ||--o{ Topic : "(id) - (projectId)"
    Topic |o--o{ Topic : "(id) - (parentId)"
    Project ||--o{ Dataset : "(id) - (projectId)"
    Dataset ||--o{ DatasetRecord : "(id) - (datasetId)"
    Project ||--o{ DatasetRecord : "(id) - (projectId)"
    Project ||--o{ Dashboard : "(id) - (projectId)"
    Project ||--o{ SavedView : "(id) - (projectId)"
    User |o--o{ SavedView : "(id) - (userId)"
    Project ||--o{ CustomGraph : "(id) - (projectId)"
    Dashboard |o--o{ CustomGraph : "(id) - (dashboardId)"
    Experiment ||--o{ BatchEvaluation : "(id) - (experimentId)"
    Project ||--o{ BatchEvaluation : "(id) - (projectId)"
    Dataset ||--o{ BatchEvaluation : "(id) - (datasetId)"
    Project ||--o{ Trigger : "(id) - (projectId)"
    CustomGraph |o--o| Trigger : "(id) - (customGraphId)"
    Organization ||--o{ WebhookEndpoint : "(id) - (organizationId)"
    WebhookEndpoint |o--o{ WebhookEndpointDelivery : "(id) - (endpointId)"
    Project |o--o{ WebhookEndpointDelivery : "(id) - (projectId)"
    Trigger |o--o{ WebhookEndpointDelivery : "(id) - (triggerId)"
    Project ||--o{ Experiment : "(id) - (projectId)"
    Workflow |o--o{ Experiment : "(id) - (workflowId)"
    Project ||--o{ Annotation : "(id) - (projectId)"
    User |o--o{ Annotation : "(id) - (userId)"
    ModelProvider ||--o{ ModelProviderScope : "(id) - (modelProviderId)"
    ModelDefaultConfig ||--o{ ModelDefaultConfigScope : "(id) - (configId)"
    Organization ||--o{ GithubInstallation : "(id) - (organizationId)"
    Organization ||--o{ GithubPullRequest : "(id) - (organizationId)"
    Organization ||--o{ GithubBranchPullRequestCheck : "(id) - (organizationId)"
    Project ||--o{ ProjectSecret : "(id) - (projectId)"
    User ||--o{ ProjectSecret : "(id) - (createdById)"
    User ||--o{ ProjectSecret : "(id) - (updatedById)"
    CustomGraph |o--o{ TriggerSent : "(id) - (customGraphId)"
    Project ||--o{ TriggerSent : "(id) - (projectId)"
    Trigger ||--o{ TriggerSent : "(id) - (triggerId)"
    Trigger ||--o| TriggerLatestEvaluation : "(id) - (triggerId)"
    Project ||--o{ EmailSuppression : "(id) - (projectId)"
    Project ||--o{ AnnotationScore : "(id) - (projectId)"
    Project ||--o{ AnnotationQueue : "(id) - (projectId)"
    User ||--o{ AnnotationQueueMembers : "(id) - (userId)"
    AnnotationQueue ||--o{ AnnotationQueueMembers : "(id) - (annotationQueueId)"
    AnnotationScore ||--o{ AnnotationQueueScores : "(id) - (annotationScoreId)"
    AnnotationQueue ||--o{ AnnotationQueueScores : "(id) - (annotationQueueId)"
    AnnotationQueue |o--o{ AnnotationQueueItem : "(id) - (annotationQueueId)"
    User |o--o{ AnnotationQueueItem : "(id) - (userId)"
    User |o--o{ AnnotationQueueItem : "(id) - (createdByUserId)"
    Project ||--o{ AnnotationQueueItem : "(id) - (projectId)"
    Project ||--o{ ShareLink : "(id) - (projectId)"
    User |o--o{ ShareLink : "(id) - (userId)"
    Project ||--o{ PinnedTrace : "(id) - (projectId)"
    User |o--o{ PinnedTrace : "(id) - (userId)"
    Project ||--o{ TraceEditOverlay : "(id) - (projectId)"
    User |o--o{ TraceEditOverlay : "(id) - (createdById)"
    User |o--o{ TraceEditOverlay : "(id) - (updatedById)"
    Project ||--o{ Workflow : "(id) - (projectId)"
    WorkflowVersion |o--o{ Workflow : "(id) - (latestVersionId)"
    WorkflowVersion |o--o{ Workflow : "(id) - (currentVersionId)"
    User |o--o{ Workflow : "(id) - (publishedById)"
    Workflow |o--o{ Workflow : "(id) - (copiedFromWorkflowId)"
    User ||--o{ WorkflowVersion : "(id) - (authorId)"
    Project ||--o{ WorkflowVersion : "(id) - (projectId)"
    Workflow ||--o{ WorkflowVersion : "(id) - (workflowId)"
    WorkflowVersion |o--o{ WorkflowVersion : "(id) - (parentId)"
    Project ||--o{ LlmPromptConfig : "(id) - (projectId)"
    LlmPromptConfig |o--o{ LlmPromptConfig : "(id) - (copiedFromPromptId)"
    User |o--o{ LlmPromptConfigVersion : "(id) - (authorId)"
    LlmPromptConfig ||--o{ LlmPromptConfigVersion : "(id) - (configId)"
    LlmPromptConfig ||--o{ PromptTagAssignment : "(id) - (configId)"
    LlmPromptConfigVersion ||--o{ PromptTagAssignment : "(id) - (versionId)"
    PromptTag ||--o{ PromptTagAssignment : "(id) - (tagId)"
    User |o--o{ PromptTagAssignment : "(id) - (createdById)"
    User |o--o{ PromptTagAssignment : "(id) - (updatedById)"
    Organization ||--o{ PromptTag : "(id) - (organizationId)"
    User |o--o{ PromptTag : "(id) - (createdById)"
    User |o--o{ PromptTag : "(id) - (updatedById)"
    Project ||--o{ Analytics : "(id) - (projectId)"
    Organization ||--o{ CustomRole : "(id) - (organizationId)"
    Organization |o--o{ Notification : "(id) - (organizationId)"
    Project |o--o{ Notification : "(id) - (projectId)"
    Project ||--o{ Agent : "(id) - (projectId)"
    Workflow |o--o{ Agent : "(id) - (workflowId)"
    Agent |o--o{ Agent : "(id) - (copiedFromAgentId)"
    Project ||--o{ Evaluator : "(id) - (projectId)"
    Workflow |o--o{ Evaluator : "(id) - (workflowId)"
    Evaluator |o--o{ Evaluator : "(id) - (copiedFromEvaluatorId)"
    Project ||--o{ Scenario : "(id) - (projectId)"
    SimulationSuite |o--o{ Scenario : "(id) - (testSuiteId)"
    User |o--o{ Scenario : "(id) - (lastUpdatedById)"
    Scenario ||--o{ ScenarioVersion : "(id) - (scenarioId)"
    Project ||--o{ SimulationSuite : "(id) - (projectId)"
    Organization ||--o{ Subscription : "(id) - (organizationId)"
    Subscription ||--o{ Invoice : "(id) - (subscriptionId)"
    Invoice ||--o{ InvoiceItem : "(id) - (invoiceId)"
    Organization ||--o{ Group : "(id) - (organizationId)"
    User ||--o{ GroupMembership : "(id) - (userId)"
    Group ||--o{ GroupMembership : "(id) - (groupId)"
    Organization ||--o{ RoleBinding : "(id) - (organizationId)"
    User |o--o{ RoleBinding : "(id) - (userId)"
    Group |o--o{ RoleBinding : "(id) - (groupId)"
    ApiKey |o--o{ RoleBinding : "(id) - (apiKeyId)"
    CustomRole |o--o{ RoleBinding : "(id) - (customRoleId)"
    User |o--o{ ApiKey : "(id) - (userId)"
    Organization ||--o{ ApiKey : "(id) - (organizationId)"
    User |o--o{ VirtualKey : "(id) - (principalUserId)"
    RoutingPolicy |o--o{ VirtualKey : "(id) - (routingPolicyId)"
    VirtualKey ||--o{ VirtualKeyScope : "(id) - (virtualKeyId)"
    Organization ||--o{ RoutingPolicy : "(id) - (organizationId)"
    User |o--o{ RoutingPolicy : "(id) - (createdById)"
    User |o--o{ RoutingPolicy : "(id) - (updatedById)"
    RoutingPolicy ||--o{ RoutingPolicyScope : "(id) - (routingPolicyId)"
    Project ||--o{ GatewayGuardrail : "(id) - (projectId)"
    Evaluator ||--o{ GatewayGuardrail : "(id) - (evaluatorId)"
    User |o--o{ GatewayGuardrail : "(id) - (createdById)"
    User |o--o{ GatewayGuardrail : "(id) - (updatedById)"
    Organization ||--o{ IngestionSource : "(id) - (organizationId)"
    Team |o--o{ IngestionSource : "(id) - (teamId)"
    User |o--o{ IngestionSource : "(id) - (createdById)"
    Organization ||--o{ AnomalyRule : "(id) - (organizationId)"
    User |o--o{ AnomalyRule : "(id) - (createdById)"
    Organization ||--o{ AnomalyAlert : "(id) - (organizationId)"
    AnomalyRule ||--o{ AnomalyAlert : "(id) - (ruleId)"
    Organization ||--o{ AiToolEntry : "(id) - (organizationId)"
    AiToolEntry ||--o{ AiToolEntryTeam : "(id) - (entryId)"
    Team ||--o{ AiToolEntryTeam : "(id) - (teamId)"
    AiToolEntry ||--o{ AiToolEntryDepartment : "(id) - (entryId)"
    Organization |o--o{ IngestionTemplate : "(id) - (organizationId)"
    User ||--o{ GatewayBudget : "(id) - (createdById)"
    GatewayBudget ||--o{ GatewayBudgetBucketBoundary : "(id) - (budgetId)"
    GatewayBudget ||--o{ GatewayBudgetLedger : "(id) - (budgetId)"
    VirtualKey ||--o{ GatewayBudgetLedger : "(id) - (virtualKeyId)"
    Organization ||--o{ GatewayChangeEvent : "(id) - (organizationId)"
    Project |o--o{ GatewayChangeEvent : "(id) - (projectId)"
    ProcessManagerOutbox ||--o{ ProcessManagerOutboxAttempt : "(id) - (outboxId)"
    Account {
        string id PK
        string userId FK
        string type
        string provider UK
        string issuer "WHO asserted `providerAccountId`. Better Auth keys accounts by `(provider, providerAccountId)`; auth's account create hook writes this (modules/auth/specs/account-issuer.feature). Nullable only so the column could be added to a populated table; the migration backfilled every row."
        string providerAccountId UK
        string refresh_token
        string access_token
        datetime expires_at
        int ext_expires_in
        string token_type
        string scope
        string id_token
        string session_state
        string password
        datetime createdAt
        datetime updatedAt
    }
    AccountCredential {
        string id PK
        string userId FK
        string provider
        string password
        string accessToken
        string refreshToken
        string idToken
        datetime accessTokenExpiresAt
        datetime refreshTokenExpiresAt
        string scope
        datetime createdAt
        datetime updatedAt
    }
    Session {
        string id PK
        string sessionToken UK
        string userId FK
        datetime expires
        string ipAddress
        string userAgent
        string identifierId "Which of the user's sign-in methods minted this session (D06). NULL on every session that predates the column and on any mint we could not attribute - an ordinary value, not an error. Nothing reads it to decide whether the session is valid, so no session was ended to add it."
        string[] amr "What the sign-in that minted this session PROVED, as RFC 8176 method references (D06). Empty on every session minted before the column, and on any ordinary password sign-in. An organization that requires a second factor reads this when a member reaches its data; it is never read at mint and never ends a session."
        boolean startedBySignUp "Minted while the account had never signed in: the session its sign-up started, which skips the secure-account offer. False on every later one and on every session that predates the column (specs/identity/passkeys.feature)."
        json impersonating "The legacy impersonation payload. NOTHING READS OR WRITES IT any more - the {actor, subject} columns below replaced it, and the migration beside them ended every session that still carried a value.  It stays for ONE release, and that is the whole reason it is still here. `prisma migrate deploy` runs at container start, so the first pod of a new release drops a column while the previous release's pods are still serving - and those pods select this column on EVERY authenticated request and fail closed when the row does not come back. Dropping it in the same release that stops reading it signs everybody out for the length of the rollout. Expand now, contract next release."
        string actorUserId "The impersonation claims (D06), aligned with the authz Principal {actor, subject}. The actor is the operator who really made the request and the subject is the person whose access they are borrowing; both are recorded on every authorization decision, so the audit trail can answer who really did it. These replace the legacy `impersonating` JSON blob, which is dropped in the release AFTER this one.  NULL on every ordinary session, which is all of them: an impersonation is a handful of rows at a time and nothing else reads these columns."
        string subjectUserId
        string impersonationReason "Why the operator started it. Required to start one (specs/features/backoffice-user-impersonation-reason.feature) and kept beside both people rather than only in the audit log, so a request under impersonation carries its own justification."
        datetime impersonationExpiresAt "When the borrowed access lapses. A session outlives its impersonation window: past this the claims are ignored and the operator is themselves again, with nothing ended."
        datetime lastSeenAt "When this session was last used, for the idle timeout an organization may set (GAC-10, specs/identity/org-session-lifetime.feature).  A COLUMN OF OUR OWN rather than `updatedAt`, because better-auth rolls a live session's expiry once per `updateAge` - a day - so `updatedAt` is activity to the nearest day and cannot tell an idle hour from an idle minute. Written only for sessions whose owner belongs to an organization that has set a window, and then at most once per quarter-window, so a deployment where nobody has turned this on writes it never.  NULL on every session minted before the column and on every session under no window at all. A null READS AS `updatedAt`, which better-auth keeps rolled to within a day of the last use. That fallback is conservative in the direction that matters - `updatedAt` is never earlier than the real last use, so it can only ever judge a session MORE active than it was, never less - and it is what lets a window turned on today end the sessions that have been idle since February instead of waiting a day for a stamp they have never had."
        datetime createdAt
        datetime updatedAt
    }
    UserErasure {
        string id PK
        datetime accountCreatedAt
        datetime erasedAt
    }
    User {
        string id PK
        string name
        string email UK
        boolean emailVerified
        boolean signupConfirmationPending
        string passkeySignupClaimHash
        string image
        boolean pendingSsoSetup
        string userHashKey
        boolean twoFactorEnabled "Owned by the two-factor plugin (D06), which is why it is a plain boolean rather than a state: the plugin flips it when a setup is confirmed and when it is turned off, and challenges every sign-in while it is true. The lifecycle - and the history - lives on MfaEnrollment."
        datetime createdAt
        datetime updatedAt
        datetime lastLoginAt
        datetime deactivatedAt
        string lastHomePath
        datetime tracesExplorerTourDismissedAt
        string langyCodeAccessPreference "How Langy should reach this person's code when a task needs a change (ADR-129). #quot;github#quot; means they chose the pull request path and asked to be remembered; NULL means ask. A shared local folder is never stored here, because a folder has to be shared again each time. Spec: specs/langy/langy-code-access.feature"
        datetime passkeyNudgeDismissedAt "When this person last said #quot;not now#quot; to being offered a passkey (ADR-120). On the account rather than in browser storage for two reasons: a per-browser record forgets on a new device, which is exactly where the offer should be MORE eager, and an interval nobody can enforce is not an interval. NULL means never asked."
        string[] joinOfferDismissedDomains "The domains this person has said #quot;no thanks#quot; to being offered their colleagues on (D12). Per domain rather than one flag: somebody who changes jobs and verifies a second address should be offered their new employer's organization, not silenced forever by a decision they made about the old one. Empty means never dismissed anything."
        json notificationPreferences "This person's answer about browser notifications, one entry per topic: { #quot;langy#quot;: #quot;enabled#quot; | #quot;declined#quot; }. A topic that is absent was never answered. The browser's own permission is not stored here. Spec: specs/langy/langy-notifications.feature"
        datetime passwordResetRequiredAt "When an operator marked this person's password as no longer trusted. A password sign-in opens no session while it is set; any write of a new password hash clears it. Spec: modules/auth/specs/forced-password-reset.feature"
    }
    VerificationToken {
        string id PK
        string identifier UK
        string token UK "Not unique alone: better-auth stores plain values here (a user id, a counter). See WEB-19201."
        datetime expires
        datetime createdAt
        datetime updatedAt
    }
    Identifier {
        string id PK
        string userId
        string provider
        string value
        string domain
        string identifierHash
        string accountId
        string providerId
        string issuer
        string providerAccountId
        string state
        string connectionId
        datetime verifiedAt
        datetime attachedAt
        datetime detachedAt
        datetime lastUsedAt
        datetime createdAt
        datetime updatedAt
    }
    IdentitySignInSwitch {
        string userId PK
        boolean onIdentifiers
        boolean pinned
        datetime switchedAt
        string lastAppliedEventId
        datetime updatedAt
    }
    IdentifierReservation {
        string normalizedValue PK
        string userId
        string identifierId
        string commandId
        datetime createdAt
    }
    IdentityProjectionCursor {
        string userId PK
        string lastEventId
        datetime acceptedAt
        datetime occurredAt
        string projectionVersion
        datetime createdAt
        datetime updatedAt
    }
    TwoFactor {
        string id PK
        string userId FK
        string secret
        string backupCodes
        boolean verified
        int failedVerificationCount
        datetime lockedUntil
        datetime createdAt
        datetime updatedAt
    }
    SignInAttemptLock {
        string id PK
        string identifierHash UK "Keyed hash of the normalised address. Never the address itself."
        int failedCount "Consecutive failures since the last success. Reset by a success, not by time - #quot;five consecutive#quot;, not five ever."
        datetime lockedUntil "When the current lock lifts. Null when not locked."
        int consecutiveLockouts "Locks taken since the last success. At five this stops lifting by itself and becomes an incident - see `heldForReview`."
        boolean heldForReview "Held past its lock because the count of locks reached the ceiling the control names. Cleared by an administrator, or by the owner proving mailbox control through a password reset - the second is what keeps a held account from being a denial of service anyone can inflict by typing a colleague's address wrong often enough."
        string userId "Whose account this resolved to at the time, when it resolved to one. Null for an address with no account - those rows are the reason this is nullable, and the reason this table is not hung off User."
        datetime createdAt
        datetime updatedAt
    }
    Passkey {
        string id PK
        string name
        string publicKey
        string userId FK
        string credentialID
        int counter
        string deviceType
        boolean backedUp
        string transports
        string aaguid
        datetime createdAt
        datetime updatedAt
    }
    MfaEnrollment {
        string userId PK "The user. Also the aggregate id and the tenant - one identity history per person (ADR-029 §4)."
        string enrollmentId "The current enrollment, or NULL for somebody who never started one."
        string method "totp. Never sms."
        string state "NONE | PENDING | ENABLED | EXPIRED | DISABLED"
        datetime enrolledAt
        datetime confirmedAt
        datetime expiredAt
        datetime disabledAt
        string disabledVia "password+totp | org-admin - who ended it, and on whose authority."
        int backupCodeCount
        int[] consumedBackupCodeIndexes "Positions already spent, ascending. Never codes."
        int failedCount
        datetime occurredAt
        string lastEventId
        datetime acceptedAt
        string projectionVersion
        datetime createdAt
        datetime updatedAt
    }
    SsoConnection {
        string id PK
        string organizationId
        string type "oidc | saml"
        string state "DRAFT | CLAIMED | APPROVED | REJECTED | DISCARDED | VERIFICATION_PENDING | VERIFIED | ACTIVE | SUSPENDED | TEARDOWN_PENDING | TORN_DOWN"
        string[] claimedDomains
        json domainClaims "[{ domain, state, claimedAtMs, claimedByActorId, decidedAtMs, decidedByActorId, authority, waitedMs, note }] - every claim this connection has made and how long each waited to be decided. The tier-3 approval queue is a read of this column, and the epic's Open Q2 wants that latency measured from the day the queue exists."
        string[] approvedDomains
        string[] verifiedDomains "The only domains that ever route, and only while state is ACTIVE."
        string[] lapsedDomains "The subset of `verifiedDomains` whose published record stayed missing through its grace window (ADR-123). They still ROUTE - everyone already here signs in unchanged - and they stop vouching for anybody NEW: no provisioning on first sign-in, no walking in by domain. A column rather than a read of the JSON below, because both questions are asked on a sign-in path and have to be one indexed predicate."
        json domainVerifications "[{ domain, method, actorId, verifiedAtMs, proofState, firstAbsentAtMs, graceEndsAtMs }] - what proved each verified domain, who, and whether that evidence is still there. Kept on the row rather than only in the log so that an operator-attested domain can never be read as one the customer proved, and a wavering one never as one nothing is doubting."
        json pendingVerification "{ domain, method, tokenHash } while a ceremony is in flight."
        json idpMetadata
        string arrivalPolicy "admit | request | refuse - what this connection does with somebody who signs in through it and is not a member yet (ADR-117 3). Stated at registration and changed by the setup journey, so it is never absent and no reader has to decide what absence means. This was two fields for a while - a boolean `allowsJit` beside it, kept in step by the fold - and they disagreed about the middle answer."
        boolean allowsJit "DEAD, AND STAYING FOR ONE RELEASE. Nothing reads or writes this; the routing repository derives the same answer from `arrivalPolicy`. It is declared so the schema still describes the table, and left in place so the PREVIOUS release's pods — which select it by name on every connection read — keep working through the rollout. Dropping it in the release that stopped reading it would stop single sign-on routing and the connection projection for the length of the deploy. Dropped by `20260827120000_drop_sso_connection_allows_jit` in the next one."
        datetime arrivalPolicyDecidedAt "When somebody CHOSE the policy above, and null while the registration default stands. A different fact from the policy rather than a second copy of it: a connection always has a behaviour, and separately somebody has or has not decided on it. Going live waits for the deciding - #quot;turn everybody away#quot; is a decision too."
        string source "self-serve | legacy-grandfathered"
        string testLoginAccountId
        string replacesConnectionId "The grandfathered connection this direct connection replaces. Null for ordinary connections and for the grandfathered predecessor itself.  Partial uniqueness for nonterminal replacements is installed by the flattened migration; Prisma cannot express that predicate here."
        ssoconnectionmigrationphase migrationPhase "SETUP | GRACE_LEGACY | GRACE_DIRECT | FINALIZING | FINALIZED. Null means this connection is not a legacy-to-direct replacement."
        datetime graceStartedAt
        datetime routeChangedAt "When an administrator last selected the normal route. This is audit evidence only: routing is derived from `migrationPhase`, never recency."
        datetime finalizationRequestedAt
        datetime finalizedAt
        json rejection "{ domain, note } - the last ops rejection, read back on a re-claim."
        string createdBy
        datetime tearDownAfter "While TEARDOWN_PENDING: when the process manager's wake completes it."
        datetime occurredAt
        string lastEventId
        datetime acceptedAt
        string projectionVersion
        datetime createdAt "Event-derived, not database-managed: `@default(now())` / `@updatedAt` would make a replayed row differ from the row it rebuilds, which is exactly the parity this projection has to keep."
        datetime updatedAt
    }
    SsoConnectionRegistrationSlot {
        string organizationId PK
        string kind PK "legacy | direct"
        string connectionId UK
        string replacesConnectionId
        string commandId
        datetime createdAt
        datetime updatedAt
    }
    SsoConnectionReproofCursor {
        string connectionId PK, FK
        datetime lastReproofAt
    }
    SsoCredential {
        string id PK
        string organizationId
        string connectionId "The connection whose registration minted this. Not a relation, matching `SsoConnection` itself: a projection rebuilt from events must not depend on relational state the log does not carry."
        string kind "oidc-client-id | oidc-client-secret | saml-idp-config"
        string ciphertext
        datetime createdAt
        datetime updatedAt
    }
    SsoVerifiedDomain {
        string domain PK, UK
        string organizationId UK
    }
    SsoVerifiedDomainHolder {
        string domain PK, FK
        string connectionId PK
        string organizationId FK
    }
    SsoAuthenticationActivity {
        string id PK
        string organizationId
        string connectionId
        string userId
        datetime authenticatedAt
        string providerAccountId "The provider-side account the assertion named; null before #7633's arrival door recorded it."
    }
    SsoProvider {
        string id PK
        string issuer
        string oidcConfig
        string samlConfig
        string userId
        string providerId UK
        string organizationId
        string domain
    }
    SsoBreakGlassBinding {
        string id PK
        string organizationId
        string userId "Who holds the way in."
        string grantedByUserId "Who granted it. A way back in is never self-served."
        datetime grantedAt
        datetime expiresAt
        datetime supersededAt "Set when a renewal replaced this row; the row itself is never edited."
        string renewedFromId "The binding this one renewed, so the previous end date stays readable."
        int[] warnedDays "Which of the fourteen, seven and one day warnings have been sent, so a sweep that runs twice in a day never says the same thing twice."
        datetime createdAt
    }
    SsoActivationRecoveryReservation {
        string commandId PK
        string organizationId UK
        string connectionId UK
        datetime createdAt
    }
    JoinRequest {
        string id PK
        string userId
        string organizationId
        string domain "The normalized domain the match was made on."
        string state "PENDING | APPROVED | REJECTED | EXPIRED | WITHDRAWN"
        string matchedVia "How the domain was matched: `verified-identifier-domain` or `sso-connection-domain`."
        string origin "Where the request was made: `web` or `cli` (ADR-171 v6). A request from the terminal lands a Developer seat whatever the joiner seat says."
        datetime expiresAt "While PENDING: when the day-14 wake lapses it. Null once resolved."
        datetime resolvedAt
        string resolvedByType "user | policy | invite - who ended it. `policy` is domain auto-join's own principal, deliberately not the one SSO auto-join uses."
        string resolvedById "The admin's user id, `domain-auto`, or the invitation id."
        string withdrawalCause "user | invite-accepted, on a withdrawal."
        string connectionId "The SSO connection a single sign-on arrival was admitted through; null otherwise."
        datetime occurredAt
        string lastEventId
        datetime acceptedAt
        string projectionVersion
        datetime createdAt "Event-derived, not database-managed: `@default(now())` / `@updatedAt` would make a replayed row differ from the row it rebuilds, which is exactly the parity this projection has to keep."
        datetime updatedAt
    }
    TeamUser {
        string userId PK, FK
        string teamId PK, FK
        teamuserrole role
        string assignedRoleId FK
        datetime createdAt
        datetime updatedAt
    }
    OrganizationUser {
        string userId PK, FK
        string organizationId PK, FK
        organizationuserrole role
        datetime createdAt
        datetime updatedAt
        string departmentId "Department assignment for spend attribution (the people lens). Single-valued, org-scoped, accounting-only (never gates access). Plain column ref (relationMode=#quot;prisma#quot;); the service validates the id belongs to this org. See departments.feature."
        datetime disabledAt "Set when an admin disables this membership to stay within the licensed seat count. The person loses access to this organization only, keeps their role, department and everything they did, and frees their seat. Reversible: re-enabling restores access if a seat is available. Seat counting excludes disabled memberships. See seat-reconciliation.feature."
        string pendingSsoGrantId "Stable grant intent for an unfinished automatic SSO admission. Cleared after the authoritative grant is observed; never inferred from missing access."
        string membershipStamp "A new generation for each membership lifetime. Stamped USER grant facts must match the current active membership before they can be projected; removing and rejoining therefore cannot revive a delayed old attach."
        datetime fullSeatSince "When this membership started holding the full seat it holds now; null means createdAt. Anchors the 30-day seat hold (specs/licensing/seat-rotation.feature)."
        datetime seatPendingSince "Set when admission wrote this membership disabled because no seat was free: the person is waiting for one. Cleared when the membership is enabled."
    }
    SeatHold {
        string id PK
        string organizationId UK
        string userId UK
        string email "Display copy for the Users list, kept because a removed person has no membership row."
        string name
        datetime seatTakenAt "When the person took the seat this hold keeps."
        datetime heldUntil "seatTakenAt plus 30 days; expired holds are ignored by every count and deleted hourly."
        string reason "REMOVED | DISABLED | DEMOTED"
        datetime createdAt
    }
    SeatRequest {
        string id PK
        string organizationId UK
        string email UK "Lowercased address of the person asking."
        string userId "Null when only the address is known (a sign-in the identity provider refused)."
        string name
        string status "OPEN | APPROVED | DECLINED"
        string source "pending | lite | idp_refused: how the person was left when the request opened."
        boolean idpRefused "The identity provider also refused this person's sign-in."
        string idpKind "okta | entra | google | onelogin | jumpcloud | ping | auth0 | oidc | saml"
        datetime salesNotifiedAt "When sales were told about this request; reset when it reopens."
        datetime openedAt
        datetime resolvedAt
        string resolvedByUserId
        datetime seatPendingSince "Set when an approval claims the request, cleared once its seat is given; an approval still pending long after is one whose seat never landed, and is reopened."
        datetime createdAt
        datetime updatedAt
    }
    OrganizationSeatLease {
        string organizationId PK
        string holder "Random per hold, so a release deletes only its own lease."
        datetime expiresAt
        datetime createdAt
    }
    OrganizationAccessNotice {
        string id PK
        string organizationId UK
        string kind UK "seat_request | idp_refused"
        string email UK
        string name
        string idp
        string dayKey UK "UTC day the notice was recorded on, YYYY-MM-DD."
        boolean idpRefused "Whether the seat request it announces was also refused by the identity provider."
        datetime createdAt
        datetime mailedAt "Set once the notice was handed to the mailer; null if a send never started."
    }
    Team {
        string id PK
        string name
        string slug UK
        string organizationId FK
        datetime createdAt
        datetime updatedAt
        datetime archivedAt
        boolean isPersonal
        string ownerUserId FK
        string departmentId "Department assignment inherited as the default for members and projects that have none of their own. Accounting-only."
    }
    Organization {
        string id PK
        string name
        string kind "`customer` for every organization a person founds or an operator provisions; `system` for the install's one hidden organization the Ops checkup canaries run in, never listed to a person. Spec: modules/ops/specs/checkup-canary-project.feature."
        string phoneNumber
        string slug UK
        datetime createdAt
        datetime updatedAt
        int usageSpendingMaxLimit
        int datasetAttachmentMaxMb "The largest file a dataset image or file cell accepts for this organization, in mebibytes. Null means the default. Set by an operator from the back office only; every other dataset size limit scales with it."
        string privateClickHouseLabel "The label of this organization's `CLICKHOUSE_URL__<label>__<id>` route, recorded by the organization module the first time it is seen. Once set, a missing route refuses the organization instead of serving it from the shared ClickHouse. Null: never private. Spec: specs/private-dataplane/clickhouse-routing.feature."
        string privateS3Label "The same for `DATAPLANE_S3__<label>__<id>`. Spec: specs/private-dataplane/s3-routing.feature."
        datetime privateDataplaneSeenAt "When either label was first recorded."
        int maxSessionDurationDays "Phase 8 - admin-configurable max lifetime for CLI device-flow sessions, in days. 0 = unbounded (default, mirrors GitHub CLI / gh-style flows). Companies that need finite session lifetime (eg. SOC 2 / ISO 27001 customers with mandated session expiration) can set a positive value. The /api/auth/cli/refresh endpoint rejects refresh attempts where now - issued_at exceeds this value, regardless of the underlying refresh-token TTL. Spec: specs/ai-governance/sessions/session-ttl.feature."
        boolean mfaRequired "Every member of this organization can prove a second factor (D06). A MEMBERSHIP CONDITION, not a session policy: it is asked when a member reaches this organization's data, it holds the ones who cannot prove one at an enrollment gate for THIS organization alone, and it ends no session, ever. Turning it on signs nobody out. Spec: specs/identity/mfa-and-session-shape.feature."
        int lockoutAfterFailedAttempts "How many consecutive failed sign-ins lock an account (GAC-09). 0 = never lock, which is the default and what every organization that has not asked for this keeps. Counts EVERY credential together - a password, a one-time code, a backup code, a passkey - because five per factor hands an attacker one budget per factor. Spec: specs/identity/org-account-lockout.feature."
        int lockoutMinutes "How long a lock lasts, in minutes. Ignored while the threshold is 0."
        int sessionIdleTimeoutMinutes "How long a BROWSER session may sit idle before it ends, in minutes (GAC-10). 0 = no idle timeout. Activity rolls the window, which is the #quot;refreshed every hour#quot; half of the control.  A day for organizations created from now on; organizations that predate the column keep 0 and are unchanged, because the migration adds the column at zero and moves the default afterwards.  NOT `maxSessionDurationDays`, which bounds CLI and device sessions and is enforced when the CLI refreshes. Two surfaces, two lifetimes, two decisions an administrator makes separately. Spec: specs/identity/org-session-lifetime.feature."
        int sessionMaxLifetimeMinutes "The ceiling on a browser session from the moment it was minted, in minutes, which no amount of activity extends. 0 = no ceiling, the default. Offered beside the idle timeout because a session kept alive forever by a polling page is not really bounded."
        json signupData
        boolean signedDPA
        string elasticsearchNodeUrl
        string elasticsearchApiKey
        boolean useCustomElasticsearch
        string s3Endpoint
        string s3AccessKeyId
        string s3SecretAccessKey
        string s3Bucket
        boolean useCustomS3
        datetime sentPlanLimitAlert
        string ssoDomain UK
        string ssoProvider
        string domainJoin "How colleagues on a matching domain get in (D12, ADR-117): `off` (invisible to them entirely) | `request` (they ask, an admin approves) | `auto` (a verified colleague walks straight in).  `request` is the default for a self-serve organization; `auto` is never a default and never inferred - an administrator turns it on, names the domain in `joinDomains` while doing it, and the licence gate holds that value while letting `request` through."
        organizationuserrole joinerRole "The organisation role handed to a person admitted without an invitation: a self-service domain join (`domainJoin`) or an SSO-admitted login. `MEMBER` (default, a Full seat) or `DEVELOPER` (ADR-171). Set on the organisation settings page only; the join flow never asks, and an invitation always names its own role."
        string[] joinDomains "The domains an administrator NAMED when turning automatic joining on. Empty means automatic joining admits nobody whatever `domainJoin` says - which is what keeps a setting left on from admitting a domain nobody ever agreed to."
        boolean presenceEnabled
        boolean traceSharingEnabled "Org-level trace-sharing kill switch. Off = trace share links cannot be created in ANY project of this org, and existing links stop resolving — the org toggle is the global switch, the per-project `Project.traceSharingEnabled` scopes the kill to a single project (mirrors `presenceEnabled`). Effective sharing = org AND project. ADR-039."
        string supportContact "Org-configurable contact surfaced to users in CLI #quot;contact your admin#quot; messages and the in-app budget-exceeded banner. Free text: accepts an email, a URL (internal ticketing system), or any other instruction. When null, callers fall back to the first ADMIN member's email (`resolveSupportContact` helper)."
        organizationintent primaryIntent "Signup intent, editable later as the org #quot;Primary use#quot; setting. AGENT_GOVERNANCE lands #quot;/#quot; on /me, LLM_OPS on the project home. NULL means intent unset (every pre-fork org and any org created outside onboarding) — the home resolver keeps legacy behavior. ADR-038."
        string promoCode
        string stripeCustomerId UK
        currency currency
        pricingmodel pricingModel
        string license
        datetime licenseExpiresAt
        datetime licenseLastValidatedAt
        boolean selfHostedCustomer "A customer that runs LangWatch self-hosted. The hosted usage, budgets and invoices of its connected installs attach to this organization (ADR-139)."
        string[] connectServicesDisabled "LangWatch-hosted services this organization has switched OFF, named the way its license names them (#quot;instant_evals#quot;). A service the license is entitled to is on unless it is listed here, so a customer who bought hosted judging gets it without finding a settings page first, and an administrator who switched one off keeps it off across license changes (ADR-139, section 2). A license that names no service reaches nothing, whatever this column holds."
        datetime instantEvalsEnabledAt "When a member of this organization switched Instant Evals on from the search bar, which is the organization's consent to have judged text sent to the judge's provider. Null until then: a self-serve organization is never opted in on its behalf, and an enterprise one is switched on by an operator through the release flag after asking, never here."
        string instantEvalsEnabledByUserId "The member who switched them on, so the audit trail names a person and not just a moment."
        datetime connectLastSyncAt "When the last license sync succeeded."
        string connectLastSyncError "The code of the failure the last sync ended on, cleared on the next success. Settings, Connect renders the copy for it."
    }
    IssuedLicense {
        string id PK
        string licenseId UK "The `licenseId` inside the signed license (`lic-<uuid>`)."
        string tokenHash UK "SHA-256 of the `lwl_` token. Looked up on every credential resolution."
        string organizationId FK "The customer organization on LangWatch Cloud. Null for a license that is recorded but not linked to a customer yet, such as a self-serve purchase: it resolves to nothing until an operator links it."
        string organizationName "As signed into the license."
        string email
        string planType
        int maxMembers
        int maxMembersLite "The lite seats sold: null when not stated or when unlimited. Never a stand-in number."
        boolean liteSeatsUnlimited "True when the license sells unlimited lite seats; maxMembersLite is then null."
        int maxMembersDeveloper "The Developer seats sold: null when not stated, which sells none."
        datetime issuedAt
        datetime expiresAt
        issuedlicensesource source
        string issuedById "The operator who issued or registered it. Null for the purchase flow and the script."
        datetime revokedAt
        string revokedById
        string revokedReason
        datetime supersededAt "Set when the install first presents the license that replaced this one."
        string replacesId UK "The id of the IssuedLicense this one replaces, when it was reissued. A plain column ref (relationMode=#quot;prisma#quot; cannot declare a self-relation on Postgres); the registry service validates it. Unique, so a license is replaced by one license at most."
        string pendingDeliveryLicense "A reissued license waiting to be delivered over sync, encrypted. Erased when the install first presents it. The only license text the registry holds."
        string[] services "Hosted services this license may call, such as `instant_evals`."
        int seatRateCents "The annual rate of one seat, in cents of `seatCurrency`. Needed to prorate added seats."
        currency seatCurrency
        int commitUsdCents "The prepaid usage commit for the term, in USD cents."
        boolean overageEnabled "Whether usage may continue past the commit, up to `overageMaxUsdCents`."
        int overageMaxUsdCents
        string instanceId "The install this license is bound to. Set on first use, cleared by an operator."
        datetime instanceBoundAt
        datetime lastSyncAt "The last license sync this install reported: when it arrived, the LangWatch version that sent it, and the seats in use at that moment. Null until the install syncs for the first time."
        string lastSyncVersion
        int reportedMembers
        int reportedMembersLite
        string virtualKeyId UK "The managed gateway key (purpose CONNECT) that hosted-service traffic of this license runs under. One per license, so spend rows name the install. Created when the license first resolves. A plain column, not a relation."
        int seatsRaisedFrom "The seats of the license this one replaced, set only when a seat change raised a linked license. Billing reads it through licensing to invoice the added seats, so the fact commits with the license or not at all."
        datetime createdAt
        datetime updatedAt
    }
    OrganizationLicense {
        string organizationId PK
        string licenseKey
        datetime expiresAt
        datetime validatedAt
        datetime updatedAt
    }
    ActivationCode {
        string id PK
        string codeHash UK "SHA-256 of the normalised code. Looked up on every redemption."
        string codeHint "The last four characters, so an operator tells two codes apart in a list without the list holding either one."
        string organizationId "The customer this code belongs to, and the terms it mints."
        string organizationName
        string email
        string planType
        int maxMembers
        int maxMembersLite "The lite seats the minted license carries: null when unlimited. Never a stand-in number."
        boolean liteSeatsUnlimited "True when the minted license carries unlimited lite seats; maxMembersLite is then null."
        int maxMembersDeveloper "The Developer seats the minted license carries: null when not stated, which sells none."
        int licenseTermDays "How long the license this code mints runs for."
        string[] services "Hosted services the minted license may call."
        datetime expiresAt "When the code stops working, which is not the term of what it mints."
        boolean reusable "A code a customer may redeem on more than one install, for a rollout."
        datetime redeemedAt "Set by the redemption that won, which is what makes a single-use code single use: the write itself carries the condition."
        string redeemedByInstanceId
        string issuedLicenseId "The license the winning redemption minted."
        int redemptionCount "How many installs have redeemed it, which only moves on a reusable code."
        datetime revokedAt
        string revokedById
        string createdById
        datetime createdAt
        datetime updatedAt
    }
    ConnectedBillingAccount {
        string id PK
        string organizationId FK, UK
        string stripeCustomerId UK "The customer at the payment provider. Invoiced, never charged."
        string usageSubscriptionId "The one metered subscription, and the item hosted usage is reported on. Null between the customer being created and the subscription existing, which is where a half-finished onboarding resumes from."
        string usageSubscriptionItemId
        datetime termStartsAt
        datetime termEndsAt
        int commitUsdCents "Prepaid usage for the term, in USD cents. The credit grants add up to it."
        currency seatCurrency "The seat contract, which may be in another currency than hosted usage."
        int seatRateCents
        int seats
        string bankTransferType "`us_bank_transfer` or `eu_bank_transfer` when the customer pays into a virtual bank account at the provider. Null when it wires to LangWatch's own account and finance marks the invoice paid."
        string bankTransferCountry
        string billingEmail
        json pendingRenewal "A renewal whose credit is waiting for the last usage invoice of the old term to be finalized: the new term's end and commit, and who asked."
        string createdById
        datetime createdAt
        datetime updatedAt
    }
    ConnectedCreditGrant {
        string id PK
        string accountId "The `ConnectedBillingAccount` row. A plain column ref, like `IssuedLicense.replacesId`: relationMode=#quot;prisma#quot; carries no foreign keys."
        string stripeCreditGrantId UK "The grant at the payment provider. Unique, so a retry records one row."
        int amountUsdCents
        connectedcreditgrantkind kind
        datetime termEndsAt "The term this credit was granted for, and when the provider lets it lapse: after the last invoice of that term has been finalized, never on the day the term ends."
        datetime expiresAt
        datetime createdAt
    }
    ConnectedInvoice {
        string id PK
        string accountId "The `ConnectedBillingAccount` row, as a plain column ref."
        string stripeInvoiceId UK
        connectedinvoicekind kind
        currency currency
        int amountCents
        string status "The provider's own status: `draft`, `open`, `paid`, `void` or `uncollectible`."
        datetime paidOutOfBandAt
        datetime termStartsAt
        datetime createdAt
        datetime updatedAt
    }
    ScimRequestLog {
        string id PK
        string organizationId FK
        string connectionId "Null when the refusal resolved an organization and stopped before the connection — a lapsed plan is answered off the token alone."
        string method
        string resource "The SCIM resource asked for (#quot;Users#quot;, #quot;Users/:id#quot;, #quot;Groups#quot;), never the raw path: a path carries ids and query strings, and this is read by people rather than matched by machines."
        int status
        string reason "A stable slug, only on a refusal, so a reader branches on this and never on the sentence beside it."
        string detail "Our own short sentence, customer-safe."
        datetime occurredAt
    }
    ScimToken {
        string id PK
        string organizationId FK
        string connectionId "D08: the SSO connection this token was issued for, and its ENTIRE write authority — a token minted for one connection can never touch another connection's people. No relation, matching `SsoConnection` itself: that projection carries no foreign keys, so neither may a row pointing at it.  Nullable only for the tokens that predate the column. They were minted when #quot;which organization#quot; was the whole answer, so they keep the organization-wide authority they were sold with; the backfill points each at its organization's first connection where there is one. Minting a NEW token without a connection is refused (`scim_connection_required`)."
        string hashedToken UK "UNIQUE, and that is a tenancy boundary rather than tidiness.  The lookup that turns a bearer token into an organization is keyed on this column alone — it has to be, since the token is the only thing the caller presents. While every token was 32 random bytes a collision was impossible and the missing constraint cost nothing. An administrator may now supply a value they already hold, so two organizations CAN choose the same string, and without this the lookup would answer with whichever row the planner reached first: one customer's directory provisioning and deleting another customer's people."
        string hashScheme "How `hashedToken` was derived, so the two schemes can coexist while tokens minted under the old one are still in use.  `sha256` is a bare digest, which is correct for 32 random bytes and wrong for a value a person chose — a database dump of those is a wordlist away from live credentials. `hmac-sha256` is keyed on the deployment's own secret, so a dump on its own is inert. Every new row is the latter."
        string description
        datetime createdAt
        datetime lastUsedAt
    }
    ScimExternalId {
        string organizationId
        string id PK
        string connectionId UK
        string externalId UK
        string userId FK
        datetime createdAt
        datetime updatedAt
    }
    ScimUserResource {
        string organizationId PK
        string userId PK, FK
        string userName
        string name
        boolean active
        datetime deletedAt
        datetime createdAt
        datetime updatedAt
    }
    ScimDirectoryUser {
        string organizationId
        string connectionId PK
        string userId PK
    }
    ScimSyncState {
        string id PK "The aggregate: the connection's id, which IS its sync's id."
        string connectionId
        string organizationId
        string state "TOKEN_ISSUED | SYNCING | ERROR | REVOKED"
        datetime lastPushedAt
        json lastFailure "The failure standing right now: { op, errorCode, attempts, retiredAtMs, userId, occurredAtMs }. A reason CODE and ids — never a token, never a secret, never an internal hostname."
        json deadLetters "Every failure retired without ever being applied, newest last. The dead letters an administrator has to see; retiring never drops one."
        string revokedCause "revoke | teardown, once the sync has ended."
        datetime occurredAt
        string lastEventId
        datetime acceptedAt
        string projectionVersion
        datetime createdAt "Event-derived, not database-managed: `@default(now())` / `@updatedAt` would make a replayed row differ from the row it rebuilds, which is exactly the parity this projection has to keep."
        datetime updatedAt
    }
    ScimSsoConnectionView {
        string id PK "The aggregate: identity's connection id."
        string organizationId
        json folded "The connection as the reducer folded it (SsoConnectionState + LastEventOccurredAt)."
        string[] appliedEventIds "The event ids of the last delivery folded in, so a redelivery folds once."
        string projectionVersion "A row of another version reads as undecodable and is re-folded from the log."
        datetime createdAt "Event-derived, so a replayed row equals the row it rebuilds."
        datetime updatedAt
    }
    OrganizationSeatPeak {
        string organizationId PK "The organization, which is also the aggregate id."
        float openedAt "When the opening snapshot was taken, epoch milliseconds."
        int fullSeats
        int liteSeats
        int developerSeats
        float latestChangeAt "The latest change time applied to any holder; invitations expire up to it."
        int mismatches "Changes whose `from` disagreed with the seat the fold held."
    }
    OrganizationSeatPeakHolder {
        string organizationId PK
        string holderKind PK "`member`, `invitation` or `hold` (a seat held after somebody left)."
        string holderId PK "The member's or the hold's userId, or the invitation's id."
        string seat "`full`, `lite`, `developer` or `none`."
        float lastChangedAt
        string lastChangeId "Absent for a seat the opening snapshot recorded."
        float expiresAt "An invitation's expiry; null for a member or an invitation that never expires."
        boolean expiredByFold "The fold gave the invitation's seat back at `expiresAt`."
        string suppressedBy "A hold whose seat a live full invitation to its address took: that invitation's id."
        boolean restoredByFold "The fold gave a suppressed hold its seat back when that invitation expired."
    }
    OrganizationSeatPeakMonth {
        string organizationId PK
        string month PK "`YYYY-MM`."
        int fullPeak
        float fullPeakAt
        int litePeak
        float litePeakAt
        int fullAtEnd
        int liteAtEnd
    }
    Project {
        string id PK
        string name
        string slug UK
        string apiKey UK
        datetime legacyApiKeyLastUsedAt "When the legacy apiKey last authenticated a request, written at most once a minute per project and process. Spec: modules/api-key/specs/request-credential-type.feature"
        string lwqlKey UK "Random per-project secret whose SHA-256 digest is this project's LangWatchQL SQL tenant capability in ClickHouse. Deliberately NOT the API key: the credential a caller authenticates with and the capability the database resolves a tenant from stay decoupled, so rotating one never invalidates the other, and a leak of the ClickHouse key map's input discloses nothing a caller could authenticate with. Generated by the database on insert."
        string teamId FK
        string language
        string framework
        string kind "Project kind: - #quot;application#quot;: user-facing workspace where developers send their own app traces. The default for everything created via the onboarding/quickstart flow. - #quot;internal_governance#quot;: hidden per-org routing/tenancy artifact for IngestionSource ingest data. Auto-created lazily on the first IngestionSource mint via ensureHiddenGovernanceProject(). MUST be filtered out of every user-visible Project surface (project picker, /api/v1/projects, billing exports, RBAC pickers, etc.) - the regression-test invariant in specs/ai-gateway/governance/ui-contract.feature codifies this. - #quot;aggregate#quot;: a project that owns no traces and reads its member projects through shared project-reader grants (ADR-177). Hidden from every #quot;send traces here#quot; picker and from ingestion; visible in the project switcher to organisation admins only. Stored as a free-form string (matching IngestionSource.sourceType pattern) so future routing kinds can ship without schema migrations."
        json aggregateRule "The scope rule an aggregate project materialises into grants. Null on every other kind; decided in ADR-177. Shapes: { kind: #quot;all-personal#quot; } | { kind: #quot;personal-by-department#quot;, departmentId } | { kind: #quot;explicit#quot;, projectIds: string[] }."
        boolean firstMessage
        boolean integrated
        datetime createdAt
        datetime updatedAt
        string userLinkTemplate
        boolean traceSharingEnabled
        boolean presenceEnabled
        string s3Endpoint
        string s3AccessKeyId
        string s3SecretAccessKey
        string s3Bucket
        datetime archivedAt
        boolean isPersonal
        string ownerUserId FK
        json personalFeatures "Per-feature unlock flags for personal projects only. Shape: `{ evaluations: bool, datasets: bool, annotations: bool, automations: bool }`. Empty `{}` (default) reads as all-false at the service layer - existing rows pre-migration require no backfill. The bundle is a UI/nav predicate, NOT an auth gate (the underlying tRPC routers stay open even when the bundle is off; admin tooling + offline migration scripts can still operate on the data). Team / org-scope projects are not feature-gated - this column is meaningful only when `isPersonal=true`."
        string departmentId "Department assignment for spend attribution (the project / agent lens, used when a trace has no principal user). Accounting-only."
        json langyEgressAllowlist "Per-project Langy egress allow-list (ADR-076). A nullable JSON array of host patterns (`[#quot;registry.npmjs.org#quot;, #quot;*.internal.acme.com#quot;]`) that the project's Langy workers may reach outbound. The *presence* of the list is the enforcement mode: null/absent ⇒ monitor-only (watch, never block); non-empty ⇒ the worker's egress adapter restricts outbound to floor ∪ this list. Enforced in the agent pod's egress adapter — NOT the gateway — so it lives on the project, not the VirtualKey. Resolved by LangyCredentialService.getEgressAllowlist and threaded through the per-request credentials envelope. See specs/langy/langy-egress-enforcement.feature."
        datetime lastCodingAgentSessionAt "When a coding-agent session was last folded for this project, and when a pull request was last mapped for a branch one of those sessions ran on. Both are instrumentation signals, not counters: the project sidebar shows its Sessions and Pull requests destinations only while the matching column is inside the recency window, so a project that never sends coding-agent telemetry never grows the two links, and one that stops sending loses them again. Written through the throttled touch methods on ProjectService, which skip the write while the stored value is still recent, so a busy project costs at most one update an hour per column. No index: every read is by project id, which is the primary key. See specs/coding-agent/project-menu-links.feature."
        datetime lastCodingAgentPullRequestAt
    }
    Department {
        string id PK
        string organizationId
        string name
        datetime createdAt
        datetime updatedAt
        datetime archivedAt
    }
    DepartmentMembershipHistory {
        string id PK
        string organizationId
        string userId
        string departmentId
        datetime validFrom
        datetime validTo "Null means the link is open. Reassignment and clearing close it."
        datetime createdAt
    }
    PlatformToolPolicy {
        string id PK
        string organizationId UK
        string toolSlug UK
        boolean allowVk
        boolean allowOtelDirect
        datetime createdAt
        datetime updatedAt
    }
    OrganizationInvite {
        string id PK
        string email
        string inviteCode UK
        datetime expiration
        invite_status status
        string organizationId FK
        string teamIds
        json teamAssignments
        organizationuserrole role
        string requestedBy FK
        string subscriptionId
        string acceptedByUserId
        string acceptedViaIdentifierId
        datetime createdAt
        datetime updatedAt
    }
    Monitor {
        string id PK
        string projectId FK, UK
        string experimentId FK, UK
        string evaluatorId FK
        string checkType
        string name
        string slug UK
        evaluationexecutionmode executionMode
        boolean enabled
        json preconditions
        json parameters
        json mappings
        float sample
        string level
        int threadIdleTimeout
        datetime createdAt
        datetime updatedAt
        datetime autoPausedAt
        string autoPausedReason
        string evaluatorLabel
        string createdById
        string updatedById
    }
    Cost {
        string id PK
        string projectId FK
        costtype costType
        string costName
        costreferencetype referenceType
        string referenceId
        float amount
        string currency
        datetime createdAt
        datetime updatedAt
        json extraInfo
    }
    Topic {
        string id PK
        string projectId FK
        string name
        string parentId FK
        string embeddings_model
        json centroid
        float p95Distance
        boolean automaticallyGenerated
        string lastEventId "The `topics_recorded` event that recorded this topic (its id in the event log). Null means the row predates event ownership and has not been seeded onto the stream yet; the projection sets it on every row it writes."
        datetime createdAt
        datetime updatedAt
    }
    Dataset {
        string id PK
        string projectId FK, UK
        string name
        string slug UK
        json columnTypes
        datetime createdAt
        datetime updatedAt
        datetime archivedAt
        json mapping
        boolean useS3
        int s3RecordCount
        string contentLayout
        string status
        string statusError
        string stagingKey
        string sourceStoredObjectId
        string uploadFilename
        int rowCount
        bigint sizeBytes
        int chunkCount
        json chunkOffsets
    }
    DatasetRecord {
        string id PK
        string datasetId FK
        string projectId FK
        json entry
        datetime createdAt
        datetime updatedAt
    }
    DatasetVersion {
        string id PK
        string projectId
        string datasetId UK
        int version UK
        string name "A name given by hand; a named version is never thinned by retention."
        string trigger "#quot;autosave#quot;, #quot;manual#quot;, #quot;experiment_run#quot;, #quot;restore#quot; or #quot;initial#quot;."
        string contentHash "SHA-256 over the columns and the rows in order; equal content saves no new version."
        json columnTypes
        int rowCount
        bigint sizeBytes
        int chunkCount
        int restoredFromVersion
        string authorId
        string authorKind "#quot;user#quot;, #quot;api_key#quot; or #quot;system#quot;: who saved the version when authorId is null."
        datetime pinnedAt "Set when an experiment pins this version; a pinned version is never thinned."
        datetime createdAt
    }
    DatasetVersionRun {
        string id PK
        string projectId UK
        string datasetId UK
        string versionId
        string runId UK
        string experimentId
        datetime createdAt
    }
    Dashboard {
        string id PK
        string projectId FK "The project that owns the board; only there can it be edited."
        string name
        int order "Legacy board sort position. The Dashboards v2 app orders boards by a member's favourites and never writes this; kept because the legacy Analytics reports section and the REST reorder endpoint still write it, and a rolling deploy's older image still reads it."
        string description "Free text under the board's name; null until a member writes one. Retiring: Dashboards v2 boards live in DashboardsV2, so no new write sets it."
        string createdById "The member who created it; null for boards that predate the column. Retiring: Dashboards v2 boards live in DashboardsV2, so no new write sets it."
        dashboardscope scope "Who sees it. Only the author changes it; a board with no author stays PROJECT. Retiring: Dashboards v2 boards live in DashboardsV2, so no new write sets it."
        string organizationId "The owning project's organization, stamped when the board is first set to ORGANIZATION; null before. No relation, per the database rule. Retiring: Dashboards v2 boards live in DashboardsV2, so no new write sets it."
        datetime createdAt
        datetime updatedAt
    }
    DashboardFavourite {
        string id PK
        string userId UK
        string dashboardId UK "A DashboardsV2 board's id (copied boards kept their Dashboard id); null when the star is a template."
        string templateId UK "The starred From LangWatch template id; null when the star is a board."
        string projectId UK
        int position "The member's drag order among their own stars in the project."
        datetime createdAt
    }
    DashboardsV2 {
        string id PK
        string projectId "The project that owns the board; only there can it be edited."
        string name
        string description "Free text under the board's name; null until a member writes one."
        string createdById "The member who created it; null for boards that predate authorship."
        dashboardscope scope "Who sees it. Only the author changes it; a board with no author stays PROJECT."
        string organizationId "The owning project's organization, stamped when the board is first set to ORGANIZATION; null before."
        json excludedOrigins "Deprecated: unused since each widget query names its own sources (dashboards-v2 AC200, 2026-10-28); dropped in a later migration, once no serving release reads it."
        datetime createdAt
        datetime updatedAt
    }
    DashboardWidget {
        string id PK
        string projectId
        string dashboardId "The DashboardsV2 board it sits on; null while unplaced."
        string name
        json definition "The widget's srcdoc definition."
        int gridColumn
        int gridRow
        int colSpan
        int rowSpan
        datetime createdAt
        datetime updatedAt
    }
    InsightProjection {
        string id PK "The insight id, which is also its aggregate id."
        string projectId
        string title
        string body
        string tone "bad | watch | good"
        string topic
        int validDays
        string lwql
        string sourceConversationId
        string sourceMessageId
        string filedByUserId
        float filedAt
        float renewedAt "Set by a later run that found the insight still true."
        float createdAt
        float updatedAt
        float occurredAt
        float acceptedAt
        string lastEventId
        string projectionVersion
        string boardId "The board and widget it came from. A pointer only, never a relation: the row reads the same once either is deleted, so the names are kept as filed."
        string boardName
        string widgetId
        string widgetName
        string filedVia "chat | run"
        float replayStart "The fixed window `lwql` replays over (epoch milliseconds, half-open) and its step."
        float replayEnd
        int replayGranularitySeconds
        json replayContext "The values in force when it was filed: `{ period, parameters }`."
        string ownerUserId "Whose insight it is: the one person who reads it. NULL on a row folded before the column existed, which is owned by whoever filed it (`filedByUserId`)."
        string findingKey "The stable key a daily run matches a repeated finding by, so it renews the insight and files no twin. NULL for an insight saved from a chat and on a row folded before the column existed."
    }
    InsightReaderProjection {
        string id PK
        string projectId UK
        string insightId UK
        string userId UK
        float seenAt
        float archivedAt
        float keptAt
        float createdAt
        float updatedAt
        float occurredAt
        float acceptedAt
        string lastEventId
        string projectionVersion
    }
    InsightDailyScheduleProjection {
        string id PK "The schedule id, which is also its aggregate id: one per project, person and board."
        string projectId UK
        string userId UK
        string boardKind UK "dashboard | template. A pointer only, never a relation: a run checks the board is there."
        string boardId UK
        string boardName
        string state "on | off | undecided."
        string offReason "board_deleted | skipped_days | member_removed | project_archived. Why the system turned it off. NULL while it is on, and when the person turned it off."
        int hour
        string timezone
        int maxInsights
        string lastRunId "The last run that recorded an outcome. NULL until one did."
        float lastRunAt
        string lastRunOutcome "filed | nothing | failed | skipped"
        string lastRunReason
        int lastRunFiled
        int lastRunRenewed "How many insights the run found still true and renewed."
        string lastRunConversationId
        int consecutiveSkips "The scheduled days in a row whose run ended skipped, and the slot of the newest counted."
        float skipStreakSlot
        float createdAt
        float updatedAt
        float occurredAt
        float acceptedAt
        string lastEventId
        string projectionVersion
    }
    SavedView {
        string id PK
        string projectId FK
        string userId FK
        string name
        json filters
        string query
        json period
        int order
        string kind "Discriminator for the two storage shapes that share this table: - #quot;v1-traces-filter#quot; (default) - legacy /messages page filter map saved views - #quot;v2-traces-lens#quot;             - new traces v2 lens configs (columns, addons, grouping, sort, filterText) encoded inside `filters` JSON Each client filters by its own kind on read so neither sees the other's rows."
        datetime createdAt
        datetime updatedAt
    }
    CustomGraph {
        string id PK
        string projectId FK
        string name
        json graph "The chart's definition, in the shape its `kind` names."
        json filters
        string kind "Discriminator for the chart shapes that share this table: - #quot;builder#quot; (default) - the chart builder's own `graph` payload - #quot;workbench_sql#quot;     - a saved LangWatchQL workbench chart, whose `graph` holds the versioned { sql, parameters, vegaLiteSpec } definition (see server/analytics/saved-workbench-charts) - #quot;dashboard_srcdoc#quot;  - frozen legacy copies of Dashboards v2 widgets, which now live in DashboardWidget; nothing new writes this kind Each reader filters by its own kind, so neither sees the other's rows and a builder chart can never be interpreted as a workbench definition."
        datetime createdAt
        datetime updatedAt
        string dashboardId FK
        int gridColumn
        int gridRow
        int colSpan
        int rowSpan
    }
    BatchEvaluation {
        string id PK, UK
        string experimentId FK
        string projectId FK
        json data
        string status
        float score
        string label
        boolean passed
        string details
        float cost
        string datasetSlug
        string datasetId FK
        string evaluation
        datetime createdAt
        datetime updatedAt
    }
    Trigger {
        string id PK
        string name
        string projectId FK
        triggeraction action
        triggerkind triggerKind
        json actionParams
        json filters
        string filterQuery
        float lastRunAt
        datetime createdAt
        datetime updatedAt
        boolean active
        string pausedReason
        datetime pausedAt
        string message
        boolean deleted
        alerttype alertType
        string slackTemplateType
        string slackTemplate
        string emailSubjectTemplate
        string emailBodyTemplate
        string notificationCadence
        int traceDebounceMs
        string customGraphId FK, UK
    }
    SlackIntegration {
        string id PK
        string name
        slackintegrationkind kind
        slackintegrationscopetype scopeType UK
        string scopeId UK
        string organizationId UK
        string botTokenEncrypted
        string webhookUrlEncrypted
        string secretFingerprint UK
        string secretHint
        string slackTeamId
        string slackTeamName
        string createdById
        string updatedById
        datetime createdAt
        datetime updatedAt
    }
    slack_connection_claim {
        string connectionId PK
        string claimantId PK
        string claimantLabel
        string organizationId
        string projectId
        datetime createdAt
        datetime updatedAt
    }
    WebhookEndpoint {
        string id PK
        string organizationId FK, UK
        webhookdestinationkind destinationKind "Where this endpoint delivers. Everything above the last hop is shared: same batching, same retry ladder, same delivery log, same signature over the same bytes."
        string url "The receiver URL. Present on `http` endpoints and null on the others; the per-kind shape is pinned by a CHECK constraint Prisma cannot model."
        string sqsQueueUrl "The Amazon SQS queue this endpoint delivers to, on `sqs` endpoints. The region and the owning account are read off it rather than configured separately, so they can never disagree with the queue."
        string sqsRoleArn "The role we assume to write to the queue, and the ExternalId that proves the request came from us rather than from anyone who learned the role's name. The recommended credential mode."
        string sqsExternalId
        string sqsAccessKeyId "A static key pair for the queue, encrypted at rest exactly like the signing secret. Both halves are present or both are absent: a half-filled pair is what the AWS SDK reads as #quot;we have credentials#quot; and stops looking with, so the CHECK constraint refuses it."
        string sqsSecretAccessKeyEncrypted
        string secretEncrypted
        string signatureScheme "How deliveries are formatted and signed. Null: the batch envelope signed `t=,v1=`. `legacy_sha256`: one raw message per POST signed `sha256=`, kept only for endpoints migrated from governance anomaly destinations."
        string idempotencyKey UK "Set only by governance's anomaly destination migration: a create that repeats it in the same organization answers the endpoint it made. An archived endpoint gives its key up, so a retry after cleanup creates anew."
        string previousSecretEncrypted "The secret this endpoint signed with before the last roll. Deliveries carry a second `v1=` computed from it until it expires, so a receiver swaps secrets on its own schedule instead of dropping deliveries in the gap. Null once the window closes or if the secret was never rolled."
        datetime previousSecretExpiresAt "When the previous secret stops being signed with and accepted."
        string[] enabledEvents
        webhookendpointstatus status
        string disabledReason "Why the endpoint is disabled: #quot;auto_failures_72h#quot; or #quot;manual#quot;."
        datetime disabledAt
        datetime failingSince "Start of the current consecutive-failure streak; cleared on any 2xx. 72 hours of unbroken failures from this instant auto-disables."
        int maxBatchSize "Delivery controls, each server-bounded (see webhookDeliveryControls constants): envelopes per POST, coalescing window before a partial batch ships, and concurrent POSTs to this receiver."
        int maxBatchDelayMs
        int maxInFlight
        boolean allowSelfSignedCertificate "Self-hosted only: deliveries accept the receiver's self-signed TLS certificate. Off by default; the hosted product ignores it."
        datetime lastSuccessAt
        datetime lastFailureAt
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
    }
    WebhookEndpointDelivery {
        string id PK
        webhookdeliverychannel channel
        string organizationId "Platform tenancy. Null on automations rows."
        string endpointId FK
        string projectId FK "Automations tenancy. Null on platform rows."
        string triggerId FK
        string dispatchId
        int attempt "1-based attempt and the batch's envelope count. Both null on automations rows, which record neither: one trigger fire is one event, and the channel's sender does not thread its attempt number into the log."
        int eventCount
        int responseStatus
        int latencyMs
        string error
        json response
        webhookdeliveryoutcome outcome
        datetime firedAt
        datetime createdAt
    }
    Experiment {
        string id PK
        string name
        experimenttype type
        string slug UK
        string projectId FK, UK
        string workflowId FK
        datetime createdAt
        datetime updatedAt
        datetime archivedAt
        json workbenchState
        int workbenchVersion
    }
    ExperimentVersion {
        string id PK
        string experimentId UK
        string projectId
        int version UK "The number the version is addressed and displayed by. Numbered rows hold a gap-free sequence. The rolling autosave row rides on the counter instead, which is what keeps it clear of that sequence."
        int counterVersion "`Experiment.workbenchVersion` at the moment this row was last written. The rolling row is rewritten in place, so this is the only column that says how recent its content is, and the version list sorts and pages by it. It also joins a row to the live state: the row whose `counterVersion` equals the experiment's counter holds what the workbench shows now."
        boolean autoSaved
        string commitMessage
        string authorId
        string authorLabel "Who wrote this version: #quot;user#quot;, #quot;langy#quot; or #quot;api#quot;."
        string runId "The run that wrote this version, when a run wrote it.  A run writes its cells into the workbench state, which advances the counter. Without this, the page that started that run reads the bump as somebody else's write, refuses to save, and asks the reader to reload over their own unsaved edits. Named here, the page adopts the version and carries on."
        json state
        string schemaVersion "Schema version of `state`, so a later shape change can be read forward."
        datetime createdAt
        datetime updatedAt
    }
    Annotation {
        string id PK
        string projectId FK
        string traceId
        string comment
        boolean isThumbsUp
        string userId FK
        datetime createdAt
        datetime updatedAt
        string email
        json scoreOptions
        string expectedOutput
        string anchorKind
        string anchorId
        string anchorPath
    }
    ModelProvider {
        string id PK
        string name
        string provider
        string routingHandle UK
        boolean enabled
        json customKeys
        json extraHeaders
        json customModels
        json customEmbeddingsModels
        json deploymentMapping
        int rateLimitRpm
        int rateLimitTpm
        int rateLimitRpd
        gatewayproviderrotationpolicy rotationPolicy
        json providerConfig
        int fallbackPriorityGlobal
        json langySkipPermissionsModels
        gatewayproviderhealthstatus healthStatus
        datetime circuitOpenedAt
        datetime lastHealthCheckAt
        datetime disabledAt
        datetime createdAt
        datetime updatedAt
        string organizationId UK
    }
    ModelProviderScope {
        string id PK
        string modelProviderId FK, UK
        modelproviderscopetype scopeType UK
        string scopeId UK
        datetime createdAt
    }
    ModelDefaultConfig {
        string id PK
        json config
        string authorId
        datetime createdAt
        datetime updatedAt
        string organizationId
    }
    ModelDefaultConfigScope {
        string id PK
        string configId FK, UK
        modeldefaultscopetype scopeType UK
        string scopeId UK
        datetime createdAt
    }
    GithubInstallation {
        string id PK
        string installationId UK "GitHub installation id (numeric; stored as string). Globally unique."
        string organizationId FK
        string accountLogin "The GitHub account (organization or user) the App is installed on."
        string accountType "#quot;Organization#quot; or #quot;User#quot;."
        string accountId "GitHub numeric account id (stable across renames)."
        string repositorySelection "#quot;all#quot; or #quot;selected#quot; — whether the App can see every repo on the account or only a chosen subset."
        json repositories "Cached repository list [{ id, fullName }] when repositorySelection = #quot;selected#quot;. Null when #quot;all#quot; (the full set is resolved live at mint time)."
        datetime suspendedAt "Set when GitHub suspends the installation; cleared on unsuspend. A suspended installation cannot mint tokens."
        datetime createdAt
        datetime updatedAt
    }
    GithubPullRequest {
        string id PK
        string organizationId FK, UK
        string repositoryHost UK "The GitHub host the repository lives on, e.g. #quot;github.com#quot;."
        string repositoryFullName UK "#quot;owner/repo#quot;, stored LOWERCASED so a lookup matches whatever casing the session reported."
        string headBranch "The branch the pull request was opened from."
        int prNumber UK
        string htmlUrl
        string title
        string state "GitHub's own state string: #quot;open#quot; or #quot;closed#quot;."
        boolean isDraft
        string authorLogin
        datetime prCreatedAt
        datetime prClosedAt
        datetime prMergedAt
        datetime prUpdatedAt "GitHub's own `updated_at` for the snapshot this row holds, and the freshness guard on every write to it: a snapshot older than the stored one is skipped. GitHub may deliver webhooks out of order, and without the guard a late `opened` lands after `closed` and reopens a merged pull request. NULL means the row predates the column, so any write is accepted."
        datetime mappedAt "When this row was first mapped to its branch."
        datetime lastCheckedAt "When the snapshot columns were last refreshed from GitHub."
        datetime createdAt
        datetime updatedAt
    }
    GithubBranchPullRequestCheck {
        string id PK
        string organizationId FK, UK
        string repositoryHost UK
        string repositoryFullName UK "#quot;owner/repo#quot;, stored LOWERCASED, matching GithubPullRequest."
        string headBranch UK
        datetime lastCheckedAt
        int prCount
        datetime notFoundAt "Set while the branch resolves to no pull request; cleared once one is found."
        datetime recheckAfter "The earliest instant GitHub may be asked about this branch again."
        int attempts "Consecutive lookups that found nothing, backing off the recheck."
        datetime lastRequestedAt "When a reader last asked about this branch, whether or not GitHub was called."
        datetime createdAt
        datetime updatedAt
    }
    LangyConversationProjection {
        string id PK
        string projectId UK
        string ConversationId UK
        string UserId "Owner; first-writer-wins from the first message's userId."
        string Title
        langyprojectiontitlesource TitleSource "derived | auto | user — governs auto-title precedence (user is sticky)."
        string Origin "Who started the conversation: `interactive` for the person in the panel, `run` for a module acting for them with nobody at the keyboard. A row folded before the column existed reads as interactive."
        string Status
        boolean IsShared
        float SharedAt
        string SharedById
        int MessageCount
        float LastActivityAt
        string CurrentTurnId "The turn in flight, or null when idle (busy guard reads this)."
        string LastError
        string LastModel "The model the latest accepted turn ran on; reopening seeds the picker from it."
        string PendingHandoffToken "ADR-048 shutdown-handoff resume token (opaque, worker-authored)."
        string PendingHandoffTurnId
        string RunToken "Per-conversation HMAC key for worker stream frames; server-side only."
        float ArchivedAt
        float CreatedAt
        float UpdatedAt
        float OccurredAt "Business time of the latest applied event."
        float AcceptedAt "Event-log acceptance time. Together with lastEventId this is the cursor."
        string LastEventId
        string ProjectionVersion
    }
    LangyTurnRequest {
        string id PK
        string projectId UK
        string userId UK
        string requestId UK
        string conversationId
        string turnId
        string status
        string leaseOwner
        datetime leaseExpiresAt
        datetime createdAt
        datetime updatedAt
    }
    LangyActiveTurn {
        string id PK
        string projectId UK
        string conversationId UK
        string turnId
        string requestId
        string userId
        string status
        string leaseOwner
        datetime leaseExpiresAt
        datetime createdAt
        datetime updatedAt
    }
    LangyConversationTurnProjection {
        string id PK
        string projectId UK
        string ConversationId UK
        string TurnId UK
        string Status "LangyConversationTurnStatus. Deliberately TEXT, not a Postgres enum: this is a PROJECTION — one writer (the fold), rebuildable from the event log, and nothing queries or indexes on it. An enum bought no selectivity here, only typo protection, and charged a migration for every new status that Postgres then could not remove. The one definition lives in `LANGY_CONVERSATION_TURN_STATUS`; the repository validates against it."
        json QuestionParts "LangyMessagePart[] — the user question that opened the turn."
        json AnswerParts "LangyMessagePart[] — the agent's whole final answer."
        json ToolCalls "LangyTurnToolCall[] — tool-call lifecycle audit in initiation order."
        json Plan "LangyPlanItemData[] | null — null means the turn never kept a plan."
        string Error
        float StartedAt
        float EndedAt
        float CreatedAt
        float UpdatedAt
        float OccurredAt
        float AcceptedAt
        string LastEventId
        string ProjectionVersion
    }
    LangyMessageProjection {
        string id PK
        string projectId UK
        string ConversationId UK
        string MessageId UK
        string Role
        json Parts
        string SourceEventId UK
        float OccurredAt
        float AcceptedAt
        float CreatedAt
        float UpdatedAt
    }
    ProjectSecret {
        string id PK
        string projectId FK, UK
        string name UK
        string encryptedValue
        string createdById FK
        string updatedById FK
        datetime createdAt
        datetime updatedAt
    }
    TriggerSent {
        string id PK
        string triggerId FK, UK
        string traceId UK
        string customGraphId FK
        string projectId FK
        datetime resolvedAt
        string openIncidentKey UK
        datetime createdAt
        datetime updatedAt
    }
    TriggerLatestEvaluation {
        string triggerId PK, FK
        string projectId
        datetime evaluatedAt
        string verdict
        float observedValue
        float threshold
        string operator
        int timePeriodMinutes
        string skipCode
        datetime updatedAt
    }
    EmailSuppression {
        string id PK
        string projectId FK, UK
        string email UK
        string triggerId UK
        string reason
        datetime createdAt
    }
    AnnotationScore {
        string id PK
        string name
        string projectId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        string description
        boolean active
        annotationscoredatatype dataType
        json options
        json defaultValue
        boolean global
    }
    AnnotationQueue {
        string id PK
        string name
        string slug UK
        string projectId FK, UK
        string description
        datetime createdAt
        datetime updatedAt
    }
    AnnotationQueueMembers {
        string annotationQueueId PK, FK
        string userId PK, FK
    }
    AnnotationQueueScores {
        string annotationQueueId PK, FK
        string annotationScoreId PK, FK
    }
    AnnotationQueueItem {
        string id PK
        string annotationQueueId FK, UK
        string userId FK, UK
        datetime createdAt
        datetime updatedAt
        string createdByUserId FK
        string traceId UK
        string projectId FK, UK
        datetime doneAt
        datetime markedForDatasetAt "Set while the annotator wants this item's trace added to a dataset once the queue walk is finished. Persisted rather than kept in the browser so the selection survives a refresh and stays visible to teammates."
    }
    ShareLink {
        string id PK
        string token UK
        shareresourcetype resourceType
        string resourceId
        string threadId
        string projectId FK
        string userId FK
        sharevisibility visibility
        datetime expiresAt
        int maxViews
        int viewCount
        datetime createdAt
        datetime updatedAt
    }
    PinnedTrace {
        string id PK
        string projectId FK, UK
        string traceId UK
        string userId FK
        pinsource source
        string reason
        datetime createdAt
    }
    TraceEditOverlay {
        string id PK
        string projectId FK, UK
        string traceId UK
        json patch
        string createdById FK
        string updatedById FK
        datetime createdAt
        datetime updatedAt
    }
    TraceIngestSourceBilling {
        string organizationId PK
        string sourceType PK
        boolean billed
        datetime recordedAt
        datetime updatedAt
    }
    RetentionPolicy {
        string id PK
        string organizationId
        retentionpolicyscopetype scopeType UK
        string scopeId UK
        string category UK
        int retentionDays
        datetime createdAt
        datetime updatedAt
    }
    DataPrivacyPolicy {
        string id PK
        string organizationId
        dataprivacyscopetype scopeType UK
        string scopeId UK
        boolean personalOnly UK
        json config
        datetime createdAt
        datetime updatedAt
    }
    InstantEvalJudgeProject {
        string projectId PK
        string organizationId
        datetime createdAt
    }
    InstantEvalJudgeUsageBilling {
        string organizationId PK
        boolean usageBilled
        datetime occurredAt
        boolean fromCatchUp
    }
    InstantEvalJudgeSpend {
        string organizationId PK
        string requestId PK
        bigint spendNanoUsd
        datetime occurredAt
    }
    CustomLLMModelCost {
        string id PK
        string organizationId
        customllmmodelcostscopetype scopeType
        string scopeId
        string projectId
        string model
        string regex
        float inputCostPerToken
        float outputCostPerToken
        float cacheReadCostPerToken
        float cacheCreationCostPerToken
        float cacheCreation1hCostPerToken
        datetime createdAt
        datetime updatedAt
    }
    Workflow {
        string id PK
        string projectId FK
        string name
        string icon
        string description
        datetime createdAt
        datetime updatedAt
        string latestVersionId FK
        string currentVersionId FK
        string publishedId
        string publishedById FK
        datetime archivedAt
        boolean isEvaluator
        boolean isComponent
        string copiedFromWorkflowId FK
    }
    WorkflowVersion {
        string id PK
        string version UK
        string commitMessage
        string authorId FK
        string projectId FK
        string workflowId FK, UK
        string parentId FK
        boolean autoSaved
        datetime createdAt
        datetime updatedAt
        json dsl
    }
    AuditLog {
        string id PK
        datetime createdAt
        string userId
        string projectId
        string organizationId
        string action
        json args
        string error
        string ipAddress
        string userAgent
        json metadata
        string actorUserId "WHO REALLY DID IT, when that is not `userId`.  Under an impersonation the session's `user.id` is the SUBJECT — the customer whose access an operator is borrowing — so a row naming only `userId` filed the operator's act against the customer. An operator granting a permanent way into somebody's organization appeared, in the only durable record of it, to be that somebody.  Null on an ordinary request, where the two would be the same person and saying so twice adds nothing. ADR-118 / D06: every decision names both."
        string targetKind
        string targetId
        json before
        json after
        string idempotencyKey UK "A producer's outbox key: a redelivered audit intent finds its row here (Alex, audit R2)."
    }
    LlmPromptConfig {
        string id PK
        string handle UK
        string name
        string projectId FK
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
        string organizationId
        promptscope scope
        string copiedFromPromptId FK
    }
    LlmPromptConfigVersion {
        string id PK
        int version UK
        string commitMessage
        string authorId FK
        string configId FK, UK
        json configData
        string schemaVersion
        json runtimeParameters
        datetime createdAt
        string projectId
    }
    PromptTagAssignment {
        string id PK
        string configId FK, UK
        string versionId FK
        string tagId FK, UK
        string projectId
        datetime createdAt
        datetime updatedAt
        string createdById FK
        string updatedById FK
    }
    PromptTag {
        string id PK
        string organizationId FK, UK
        string name UK
        datetime createdAt
        datetime updatedAt
        string createdById FK
        string updatedById FK
    }
    Analytics {
        string id PK
        string projectId FK
        string key
        json value
        float numericValue
        string stringValue
        boolean boolValue
        datetime createdAt
    }
    CustomRole {
        string id PK
        string organizationId FK, UK
        string name UK
        string description
        json permissions
        string kind
        datetime createdAt
        datetime updatedAt
    }
    Notification {
        string id PK
        string organizationId FK
        string projectId FK
        json metadata
        datetime createdAt
        datetime updatedAt
        datetime sentAt
    }
    Agent {
        string id PK
        string projectId FK, UK
        string name
        string type
        json config
        string workflowId FK
        string copiedFromAgentId FK
        string environment
        string ownerUserId
        string hostLabel
        string identityKey UK
        datetime lastSeenAt
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById
        string updatedById
    }
    AgentVersion {
        string id PK
        string projectId
        string agentId UK
        int version UK
        string name
        string type
        json config
        string workflowId
        string commitMessage
        string authorId
        string authorKind "#quot;user#quot;, #quot;api_key#quot; or #quot;system#quot;: who wrote the version when authorId is null."
        datetime createdAt
    }
    AgentVersionLabel {
        string id PK
        string projectId
        string agentId UK
        string label UK
        string versionId
        string createdById
        string updatedById
        datetime createdAt
        datetime updatedAt
    }
    Evaluator {
        string id PK
        string projectId FK, UK
        string name
        string slug UK
        string type
        json config
        string workflowId FK
        string copiedFromEvaluatorId FK
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById
        string updatedById
    }
    EvaluatorVersion {
        string id PK
        string projectId
        string evaluatorId UK
        int version UK
        string name
        string type
        json config
        string workflowId
        string commitMessage
        string authorId
        string authorKind "#quot;user#quot;, #quot;api_key#quot; or #quot;system#quot;: who wrote the version when authorId is null."
        datetime createdAt
    }
    EvaluatorVersionLabel {
        string id PK
        string projectId
        string evaluatorId UK
        string label UK
        string versionId
        string createdById
        string updatedById
        datetime createdAt
        datetime updatedAt
    }
    Scenario {
        string id PK
        string projectId FK
        string name
        string situation
        string[] criteria
        string[] labels
        json parameters
        string simulatorModel
        string judgeModel
        int maxTurns
        int minTurns
        json fields
        json callerVoice
        string testSuiteId FK
        int version
        string lastUpdatedById FK
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
    }
    ScenarioVersion {
        string id PK
        string scenarioId FK, UK
        string projectId "The project of the parent case, carried here so every read filters by tenant without a join. The service is what keeps the pair honest: no composite foreign key can, under relationMode=#quot;prisma#quot;."
        int version UK
        string authorId
        string authorLabel "Who wrote this version: #quot;user#quot;, #quot;api#quot;, #quot;cli#quot; or #quot;langy#quot;."
        string changeDescription
        json snapshot "Envelope { schemaVersion, fields, changedFields } holding the editable field set as saved and the names of the fields this save changed."
        int schemaVersion "Schema version of `snapshot`, so a later shape change can be read forward."
        datetime createdAt
    }
    SimulationSuite {
        string id PK
        string projectId FK, UK
        string name
        string slug UK
        string description
        string[] scenarioIds
        json targets
        int repeatCount
        string[] labels
        string simulatorModel
        string judgeModel
        string kind
        json scope
        json fields
        json evaluators
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
    }
    Subscription {
        string id PK
        string organizationId FK
        plantypes plan
        string stripeSubscriptionId UK
        subscriptionstatus status
        datetime startDate
        datetime endDate
        datetime lastPaymentFailedDate
        datetime createdAt
        datetime updatedAt
        int maxMembers
        int maxProjects
        int maxMessagesPerMonth
        int maxWorkflows
        int maxRetentionDays
        int maxMembersLite
        int maxMembersDeveloper "Developer seats the organization pays for; null falls to the plan's own cap."
        int maxTeams
        int maxPrompts
        int maxEvaluators
        int maxScenarios
        int maxAgents
        int maxExperiments
        int maxOnlineEvaluations
        int maxDatasets
        int maxDashboards
        int maxCustomGraphs
        int maxAutomations
        int seatsKept "Seats paid for this period that stay usable after a seat decrease, until seatsKeptUntil."
        datetime seatsKeptUntil
        int seatLineQuantity "The seat quantity the last finished seat change set the seat line to; provider events never write it."
        datetime seatLineQuantityAt
        int[] seatLineWriteQuantities "A Stripe seat line write recorded before it is sent and cleared once it is known to have finished under the lease; one left behind is settled by the seat line repair. Every seat quantity written while this record was pending."
        datetime seatLineWriteSince "The first write still pending; a later holder's write keeps it."
        datetime seatLineWriteAt "The latest write sent; the repair waits a grace window after it."
        int seatLineWriteAttempts
        datetime seatLineWriteFlaggedAt
        int seatRepairQuantity "The seat quantity Stripe should bill if a seat change stops after lifting the seat line; null once settled."
        int seatRepairLiftedQuantity "The quantity the change lifted the line to; the repair moves the line back only while Stripe still bills it."
        datetime seatRepairSince
        int seatRepairAttempts
        datetime seatRepairFlaggedAt "Set once the seat line repair has failed often enough that Ops should look at it."
        string seatLineLeaseToken "The lease a seat change or seat line repair holds while it reads and moves the seat line."
        datetime seatLineLeaseUntil
    }
    Invoice {
        string id PK
        string subscriptionId FK
        float totalAmount
        string currency
        datetime dueDate
        boolean paid
        datetime createdAt
        datetime updatedAt
    }
    InvoiceItem {
        string id PK
        string invoiceId FK
        string description
        float amount
        datetime createdAt
        datetime updatedAt
    }
    Group {
        string id PK
        string organizationId FK, UK
        string name
        string slug UK
        string externalId UK
        string scimSource
        string scimConnectionId UK "D08: which SSO connection provisioned this group. Null for manually created groups and for the SCIM groups that predate connection scoping."
        datetime createdAt
        datetime updatedAt
    }
    GroupMembership {
        string userId PK, FK
        string groupId PK, FK
        datetime createdAt
    }
    AuthzUserStanding {
        string userId PK
        datetime deactivatedAt
        datetime erasedAt
        datetime standingChangedAt
    }
    AuthzEngineSwitch {
        string organizationId PK
        boolean onEngine
        boolean pinned
        datetime switchedAt
        string lastAppliedEventId
        datetime updatedAt
    }
    AuthzOrganizationPolicy {
        string organizationId PK
        boolean braidsOrganizationAdminsOnly
        string updatedByUserId
        datetime updatedAt
    }
    RoleBinding {
        string id PK
        string organizationId FK
        string userId FK
        string groupId FK
        string apiKeyId FK
        teamuserrole role
        string customRoleId FK
        rolebindingscopetype scopeType
        string scopeId
        datetime createdAt
        datetime updatedAt
    }
    Grant {
        string id PK
        string organizationId
        grantprincipaltype principalType
        string principalId
        string roleKey
        string legacyRole
        string source
        grantscopetype scopeType
        string scopeId
        string token UK
        string permission
        string resourceKind
        string projectId
        string createdByUserId
        datetime expiresAt
        int maxViews
        json condition
        string replacesGrantId
        datetime occurredAt
        datetime createdAt
        datetime updatedAt
        datetime revokedAt
        string revokedReason
    }
    GrantUsage {
        string grantId PK
        string organizationId
        string projectId
        int viewCount
        datetime lastViewedAt
        datetime createdAt
        datetime updatedAt
    }
    Role {
        string id PK
        string organizationId
        string name
        string description
        json permissions
        string kind
        datetime occurredAt
        datetime createdAt
        datetime updatedAt
        datetime deletedAt
    }
    ApiKey {
        string id PK
        string name
        string description
        string lookupId UK
        string hashedSecret
        string permissionMode
        string userId FK
        string createdByUserId
        string organizationId FK
        datetime expiresAt
        datetime lastUsedAt
        datetime revokedAt
        string revocationCause "Why the key was revoked, set with `revokedAt`: #quot;user#quot; when a person revoked it (the API-keys page, the REST API, the devices tab), #quot;rotation#quot; when a mint replaced it, #quot;session#quot; when the CLI login key it was parented to was revoked, #quot;expired#quot; when that session ran out, #quot;offboarded#quot; when the person's membership of the organization ended. #quot;cap#quot; survives on rows the retired per-tool cap revoked. The CLI reads it to decide whether a dead ingest key may be re-minted: a key a person revoked stays dead. NULL for live keys and for keys revoked before the cause was recorded."
        string parentApiKeyId "The CLI login key of the device session that minted this ingestion key. Set by the CLI's personal mint, NULL for every other key. Revoking the parent (logout, the devices tab, a re-login from the same device, session expiry) revokes the children with it. Column ref only, no FK and no index: lookups filter by the indexed `userId` first and match the parent in memory over a person's few live ingest keys."
        string ingestSourceType "Non-null marks this key as an #quot;ingestion key#quot;: a project-scoped, ingest-only ApiKey the unified `langwatch <tool>` CLI (and template installs) mint for OTLP Path B. The slug (claude_code / codex / gemini / opencode / claude_cowork) is stamped as `langwatch.source` provenance on every span the key authorizes. NULL for normal API / service keys."
        string ingestionTemplateId "Set only for ingestion keys derived from an admin IngestionTemplate (e.g. claude_cowork). Drives receiver OTTL application + the `langwatch.template.id` provenance stamp. NULL for unified-CLI tools (which have no template) and for normal keys. Column ref only (no FK, per the app-layer-integrity convention); resolved by the ingest-key service."
        string createdByDeviceLabel "Human label of the CLI device session that minted this ingestion key (e.g. #quot;Rogerio's MacBook Pro#quot;), snapshotted at mint time from the device flow's `client_info`. Denormalized so the org-wide API-keys settings page can show which device a key came from without resolving another user's Redis-stored sessions. NULL for non-ingestion keys and for keys minted by a CLI old enough to predate device metadata."
        boolean isSystemManaged "True for a key LangWatch minted for itself (a run, the gateway's trace export, a Langy session). Hides it from listings, refuses customer edits and revokes, and lets an ownerless run key act as the system. Never set by a customer create; rows before the column stay false."
        string createdByBrowserSessionId "The browser session that minted a docs session key (ADR-185), so signing out of that session revokes it. NULL for every other key. Unindexed: sign-out filters by the indexed `userId` first."
        datetime createdAt
        datetime updatedAt
    }
    SigningKey {
        string id PK
        string organizationId
        string projectId
        string name
        string publicId UK
        string secretCiphertext
        string createdByUserId
        datetime createdAt
        datetime rotatedAt
        datetime verifiesUntil
        datetime revokedAt
        datetime lastUsedAt
    }
    ProjectActiveDay {
        string id PK
        string projectId UK
        string day UK
        datetime createdAt
    }
    NurturingProject {
        string projectId PK
        string organizationId
    }
    NurturingUncopiedProject {
        string projectId PK
        datetime firstSeenAt
    }
    NurturingOrganizationMember {
        string organizationId PK
        string userId PK
        datetime joinedAt
        datetime leftAt
        datetime returnedAt
    }
    NurturingOrganizationCreated {
        string organizationId PK
        datetime createdAt
    }
    NurturingOrganization {
        string organizationId PK
        string adminUserId
        boolean seeded
        int evaluationCount
        int simulationRunCount
        datetime codingFirstSessionAt
        string[] codingAgentTypes
        int codingActiveDays
        string codingLastActiveDay
        string[] codingTerminalTypes
        string[] codingRepositoryKeys
        int[] codingFirstDays
        datetime lastTraceAt
        datetime updatedAt
    }
    NurturingProjectUsageView {
        string projectId PK
        string owner PK "The owner pipeline whose peer fold wrote the row (prompt, evaluator, scenario, simulation), or `evaluation`: the project's evaluation days, written by a subscriber, at most once a day."
        json folded "The project's usage as the fold folded it (NurturingProjectUsageState)."
        string[] appliedEventIds "The event ids of the last delivery folded in, so a redelivery folds once."
        string projectionVersion "A row of another version reads as undecodable and is re-folded from the log."
        datetime updatedAt "Event-derived, so a replayed row equals the row it rebuilds."
    }
    NurturingProjectTraceDay {
        string projectId PK
        string day PK "UTC `YYYY-MM-DD` of the root spans' append time."
        int traceCount
        int errorCount "Traces trace recorded failed (lw.trace.trace_failed)."
        datetime lastTraceAt "Append time of the newest root span."
        datetime lastFailedTraceAt "Append time of the newest trace_failed."
    }
    NurturingPersonalProject {
        string projectId PK
        string organizationId
        string ownerUserId
    }
    NurturingTracedProject {
        string projectId PK
        string organizationId
    }
    BillingMeterCheckpoint {
        string id PK
        string organizationId UK
        string billingMonth UK
        string meter UK
        int lastReportedTotal
        int pendingReportedTotal
        int consecutiveFailures
        string lastCountedEventId
        datetime updatedAt
    }
    VirtualKey {
        string id PK
        string organizationId UK
        string name
        string description
        virtualkeystatus status
        virtualkeypurpose purpose
        string externalId UK
        json metadata
        datetime disabledAt
        string disabledReason
        datetime expiresAt
        string hashedSecret UK
        string displayPrefix
        string principalUserId FK
        string traceProjectId
        json config
        bigint revision
        string previousHashedSecret
        datetime previousSecretValidUntil
        datetime revokedAt
        string revokedById
        datetime createdAt
        datetime updatedAt
        string createdById
        datetime lastUsedAt
        string routingPolicyId FK
        virtualkeyroutingmode routingMode
        string[] connectServices
        string licenseTokenHash UK
        string licenseInstanceId
        datetime licenseExpiresAt
        string licenseId
    }
    VirtualKeyScope {
        string id PK
        string virtualKeyId FK, UK
        virtualkeyscopetype scopeType UK
        string scopeId UK
        datetime createdAt
    }
    RoutingPolicy {
        string id PK
        string organizationId FK
        string name
        string description
        json modelProviderIds
        json modelAliases
        string defaultModel
        json policyRules
        boolean isDefault
        datetime createdAt
        datetime updatedAt
        string createdById FK
        string updatedById FK
    }
    RoutingPolicyScope {
        string id PK
        string routingPolicyId FK, UK
        routingpolicyscopetype scopeType UK
        string scopeId UK
        datetime createdAt
    }
    GatewayGuardrail {
        string id PK
        string projectId FK
        string name
        string description
        string evaluatorId FK
        gatewayguardraildirection direction
        gatewayguardrailfailuremode failureMode
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById FK
        string updatedById FK
    }
    IngestionSource {
        string id PK
        string organizationId FK, UK
        string teamId FK "Optional team-scope. null = org-wide."
        string sourceType "'otel_generic' | 'claude_cowork' | 'workato' | 'copilot_studio' | 'openai_compliance' | 'claude_compliance' | 's3_custom' (extensible). String not enum so adapters can be added without migrations - the supported set lives in code (activity-monitor adapter registry)."
        string name UK "Human-friendly label for the admin UI. Unique per-org so two #quot;Cowork#quot; sources can coexist with different names."
        string description
        string ingestSecretHash "HMAC bearer token the upstream operator pastes into the platform (or LangWatch sends in pull mode). Stored encrypted at rest via the same pepper-derived KEK as VirtualKey secrets. Auto-generated on create, rotatable via the admin UI with a 24h grace window."
        json parserConfig "Per-source-type configuration. Shape is platform-specific: - otel_generic: { allowedSourceTypeLabel?: string } - claude_cowork: { workspaceId: string } - workato: { sharedSecretLastFour: string } - copilot_studio: { tenantId, clientId, clientSecretHash, pollEverySec } - openai_compliance: { bucket, prefix, roleArn, pollEverySec } - claude_compliance: { workspaceApiKeyHash, pollEverySec } - s3_custom: { bucket, prefix, roleArn, parserDsl, pollEverySec }"
        json pollerCursor "For pull-based sources, persisted between polls so the next poll resumes where the last left off (timestamp / cursor / file marker)."
        int errorCount "Phase 10 - number of consecutive errors observed by the puller worker. Reset to 0 on a successful run. Drives admin-UI banners (#quot;Microsoft authentication failed - re-authorize#quot;) + back-off scheduling when the source has been broken for a while. Spec: specs/ai-governance/puller-framework/puller-adapter-contract.feature"
        string pullSchedule "Phase 10 - cron schedule for pull-mode sources. Validated by the adapter's pullConfig schema; mirrored here for fast worker dispatch. Null for push-mode sources."
        string status "Status flag. 'active' (default), 'disabled' (admin paused), or 'awaiting_first_event' (created but never received)."
        string traceProjectId "ADR-088 v7: optional trace DESTINATION for conversation-bearing pulls (Databricks Genie). Null = don't route. Same shape as VirtualKey.traceProjectId: a destination is NOT a scope row and grants no access to the source; it only decides where routed conversation traces land. Must be a project of this org (validated at write time). Freely editable; already-routed history stays where it landed. NOT the deferred cost-attribution projectId (ADR-088 Decision 4)."
        string providerAccountId "The account the provider itself named when this connection was saved - an OpenAI or Anthropic organization id, read once per save and compared so a second connection cannot read one account's spend twice. NOT derived from the administrator key: nothing worked out from a customer credential is stored here or anywhere else, because the account id is not a secret and cannot be turned back into a key. Nullable because every connection that predates the guard has none, and because Azure and Power Platform name what they read in parserConfig instead. #quot;Unknown account#quot; has to be a storable state. Spec: specs/ai-gateway/governance/ingestion-sources.feature"
        datetime lastEventAt "Last event timestamp observed (any direction). Surfaces in the admin UI as #quot;last event 2 minutes ago#quot;."
        datetime lastSuccessAt "ADR-128 - when a pull run last reached the provider without erroring, mirrored from the ingestionPullRunStatus projection. Deliberately NOT lastEventAt: an empty period is a working puller, and a run that delivered data and then failed is not. Drives the health badge and the #quot;no data since#quot; cut-off, below which a day's spend is unknown rather than zero. Spec: specs/governance/ingestion-source-health.feature"
        datetime lastReadThroughAt "ADR-128 - how far the last run actually read, and whether it reached the end. Mirrored from the ingestionPullRunStatus projection, in the same way and for the same reason as lastSuccessAt above: every reader of this surface selects from this row, and two of them render a LIST of sources, so reaching a per-run projection would be one query per row on a screen built to show many at once.  These two are the only evidence separating a source that truncated once and then finished from one that is stuck. A stuck source's run clock advances on every attempt, so the clock cannot be that evidence and is deliberately not stored here - only the point the run REACHED.  Completeness is #quot;complete#quot; or #quot;truncated#quot;. Null on both is unknown, which is what every row written before this existed reads as, and unknown must never render as either answer. Spec: specs/governance/ingestion-source-health.feature"
        string lastRunCompleteness
        datetime unpricedUsageSince "ADR-088 - the window this source read while pulled cost recording was off for the organization, so its spend was dropped rather than priced. Both null = nothing dropped. The pull cursor advances whether or not the money path is live (audit-only is a supported mode), so without this the loss is unrecoverable AND invisible: those days read as no spend rather than as unknown. Widened by any run that drops a priced item, cleared only when a later run re-reads back across the whole window. Spec: specs/governance/pulled-usage-cost-gap.feature"
        datetime unpricedUsageThrough
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById FK
    }
    AnomalyRule {
        string id PK
        string organizationId FK, UK
        string scope "'organization' | 'team' | 'project' | 'source_type' | 'source'. String not enum so new scopes ship without migrations."
        string scopeId "Org id / team id / project id / source-type string / source id - narrows what the rule applies to."
        string name UK
        string description
        string severity "'critical' | 'warning' | 'info'. Drives default destination routing (e.g. critical → PagerDuty, warning → Slack only)."
        string ruleType "Extensible rule taxonomy: 'spend_spike' | 'rate_limit' | 'tool_mismatch' | 'after_hours' | 'unusual_model' | 'pii_leak' | 'custom'. New types ship without migrations - evaluator picks a per-type implementation from a code registry."
        json thresholdConfig "Per-rule-type threshold shape. Examples: spend_spike: { windowSec, ratioVsBaseline, minBaselineUsd } rate_limit: { windowSec, requestsThreshold } after_hours: { startHour, endHour, timezone, requestsThreshold }"
        json destinationConfig "Alert routing per destination. Shape: { slack?: { channelId }, siem?: { endpointUrl, authHeader }, webhook?: { url, authHeader }, pagerduty?: { routingKey }, email?: { to: string[] } } Empty map = use org defaults (when defaults exist; today no-op)."
        string status "'active' (default - evaluator picks it up) | 'disabled' (admin paused; rule rows kept for audit). 'archived' uses archivedAt timestamp instead so we can filter `archivedAt IS NULL` cheaply."
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById FK
    }
    AnomalyAlert {
        string id PK
        string organizationId FK
        string ruleId FK, UK
        string severity "Denormalised from rule for fast WHERE filtering on the dashboard query (no join needed for the common path)."
        string ruleName
        string ruleType
        datetime triggerWindowStart UK "Window the trigger refers to (UTC)."
        datetime triggerWindowEnd
        decimal triggerSpendUsd "Numeric trigger context. Per-rule-type detail in `detail` JSONB."
        int triggerEventCount
        json detail "Per-rule-type extra context: {topActors: [...], baseline: ..., observed: ..., timezone: ..., etc}."
        string state "'open' | 'acknowledged' | 'resolved'."
        json destinationStatus "Per-destination dispatch outcome: { webhook: { lastAttemptIso, lastError?, attemptCount, statusCode? }, slack: ..., pagerduty: ..., siem: ..., email: ... }"
        datetime detectedAt
        datetime acknowledgedAt
        datetime resolvedAt
    }
    AiToolEntry {
        string id PK
        string organizationId FK
        string scope "#quot;organization#quot; | #quot;team#quot;. String not enum so new scopes ship without migrations."
        string scopeId "organizationId or teamId, narrowing what scope this entry applies to. Always equals organizationId when scope=organization."
        string type "#quot;coding_assistant#quot; | #quot;model_provider#quot; | #quot;external_tool#quot;. Discriminator drives the per-tile UX shape and validates the `config` JSON column at the app layer."
        string displayName
        string slug "Stable slug for team-overrides-org resolution (a team-scoped entry with the same slug as an org-scoped one hides the org default for users in that team). Server-owned: generated from displayName + nanoid(6) on create, immutable on update."
        string iconKey "@deprecated Use `iconAsset` instead. Kept for back-compat with pre-refactor rows; new writes should populate iconAsset."
        string iconAsset "Icon source - single column, prefix-discriminated: #quot;preset:claude_code#quot; / #quot;preset:codex#quot; / ...   (built-in) #quot;data:image/svg+xml;base64,...#quot;               (admin-uploaded) null                                          → fall back to type-default in UI"
        int order "Admin-curated sort within the user-facing portal grid."
        boolean enabled "When false the entry is hidden from the user-facing list but remains in the admin list (so admins can re-enable without losing per-entry config). Disabled is #quot;soft-hide#quot;, archived is #quot;soft-delete#quot; (preserves audit trail + scope binding history)."
        json config "Per-type config JSON. Validated via zod discriminated union at the service layer (see AiToolEntryService)."
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById
        string updatedById
    }
    AiToolEntryTeam {
        string id PK
        string entryId FK, UK
        string teamId FK, UK
    }
    AiToolEntryDepartment {
        string id PK
        string entryId FK, UK
        string departmentId UK
    }
    IngestionTemplate {
        string id PK
        string organizationId FK, UK
        string slug UK "Stable slug - unique within (organizationId, slug) scope. Generated from displayName + nanoid(6) on create for org-authored; canonical values for platform-published (`claude_code` / `cursor` / `claude_cowork` / `raw_otlp_advanced`)."
        string sourceType "Mirrors the slug for v1 platform-published rows. Kept as a separate column so a future v2 may publish two templates that share an upstream sourceType (e.g. claude_code-personal vs claude_code-team) without a migration. String not enum - extensibility."
        string displayName
        string description
        string iconAsset "Icon source - single column, prefix-discriminated, mirrors AiToolEntry.iconAsset: `preset:<kind>`                 (built-in slot) `data:image/svg+xml;base64,…`   (admin-uploaded for org-authored) null                            → fall back to slug-default in UI"
        string credentialSchema "Discriminator for what (if any) extra credential metadata the install drawer captures from the user: null              → otlp_token-only (drawer auto-issues the ingestion key, no input form) `static_api_key`  → drawer captures the user's upstream tool API key `agent_id`        → drawer captures an agent identifier (e.g. Cursor agentId) String not enum so v2 shapes (oauth_redirect / s3_bucket) ship without a migration."
        string ottlRules "Admin/platform-authored OTTL statements. Applied at receive time AFTER the receiver re-stamps the binding's authoritative principal + provenance keys (see `protectedTemplateAttributeKeys` 19-key closed list). Templates write canonical OTTL outputs (gen_ai.*, cost.usd, response.model) - they CANNOT write attribution or provenance keys; the receiver guard restores those post-OTTL."
        boolean platformPublished "True for the v1 platform-published default templates (organizationId IS NULL). Future org-authored rows write false. Drives the admin UI #quot;READ-ONLY view + Request a custom template#quot; surface - admins consume + opt-out only in v1."
        boolean enabled "Admin/platform enable. False = hidden from /me catalog. Existing ingestion keys continue to land traces; new installs are blocked."
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById
        string updatedById
    }
    GatewayConnectUpstream {
        string organizationId PK
        string baseUrl
        string encryptedToken
        string instanceId
        datetime updatedAt
    }
    GatewayTraceExportKey {
        string projectId PK
        string apiKeyId
        string encryptedToken
        datetime createdAt
    }
    GatewayBudget {
        string id PK
        string organizationId UK
        gatewaybudgetscopetype scopeType
        string scopeId
        string providerKey
        string name
        string description
        gatewaybudgetwindow window
        decimal limitUsd
        gatewaybudgetbreachaction onBreach
        string timezone
        string externalId UK
        json metadata
        decimal spentUsd
        datetime currentPeriodStartedAt
        datetime resetsAt
        datetime lastResetAt
        datetime cycleAnchorAt
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById FK
        string managedByVirtualKeyId
    }
    GatewayBudgetBucketBoundary {
        string id PK
        string organizationId
        string budgetId FK, UK
        string bucketScopeId UK
        datetime periodStartedAt
        datetime createdAt
        datetime updatedAt
    }
    GatewayBudgetLedger {
        string id PK
        string budgetId FK, UK
        string virtualKeyId FK
        string modelProviderId
        string providerKey
        string gatewayRequestId UK
        decimal amountUsd
        int tokensInput
        int tokensOutput
        int tokensCacheRead
        int tokensCacheWrite
        string model
        int durationMs
        gatewaybudgetledgerstatus status
        datetime occurredAt
    }
    GatewayChangeEvent {
        bigint revision PK
        string organizationId FK
        gatewaychangeeventkind kind
        string virtualKeyId
        string budgetId
        string modelProviderId
        string projectId FK
        json payload
        datetime createdAt
    }
    GatewayCacheRule {
        string id PK
        string organizationId
        string name
        string description
        int priority
        boolean enabled
        json matchers
        json action
        gatewaycacherulemode modeEnum
        datetime archivedAt
        datetime createdAt
        datetime updatedAt
        string createdById
    }
    FeatureFlag {
        string key PK
        boolean enabled
        json rules
        string lastEditedBy
        datetime createdAt
        datetime updatedAt
    }
    FeatureFlagExperimentSetting {
        string flagKey PK
        featureflagexperimentsubjecttype subjectType PK
        string subjectId PK
        boolean enabled
        string changedByUserId
        datetime createdAt
        datetime updatedAt
    }
    ProcessManagerInstance {
        string id PK
        string processName UK
        string projectId UK
        string processKey UK
        string tenantId
        string userId
        json state
        int revision
        datetime nextWakeAt
        datetime updatedAt
    }
    ProcessManagerInbox {
        string id PK
        string processName UK
        string projectId UK
        string processKey
        string tenantId
        string sourceEventId "The raw source event id, kept UNINDEXED for diagnostics. A caller composes this and its length is that domain's business, so it must never carry a btree constraint — see `sourceEventKey`."
        string sourceEventKey UK "The fixed-width digest of `sourceEventId` the uniqueness is enforced on, derived by the store (`stores/inboxKey.ts`). Postgres cannot index a btree row past ~2704 bytes; keying on a digest makes the constraint the same width no matter what any pipeline concatenates into an idempotency key.  Nullable ONLY for the rolling deploy that introduces it — pods running the previous build write rows without it, and a NOT NULL would fail their commits outright. The store always supplies it.  The trade, stated plainly: a NULL is not constrained by the unique index (btree treats NULLs as distinct), so while an old writer is serving, the one case that loses its guard is the same source event reaching two different process keys at once — the store's own read-check plus its per-processKey advisory lock cover everything else. A follow-up migration adds NOT NULL once the fleet has cycled and closes it."
        datetime consumedAt
    }
    ProcessManagerOutbox {
        string id PK
        string processName UK
        string projectId UK
        string processKey
        string tenantId
        string userId
        string messageKey UK
        string intentType
        json payload
        json traceCarrier
        string sourceEventId
        processmanageroutboxstatus status
        int attempts
        datetime nextAttemptAt
        datetime leasedUntil
        string leaseToken UK
        datetime dispatchedAt
        datetime createdAt
        datetime updatedAt
    }
    ProcessManagerOutboxAttempt {
        string id PK
        string outboxId FK
        string projectId
        int attempt "1-based attempt number, as the dispatcher counts it."
        datetime occurredAt
        processmanageroutboxattemptoutcome outcome
        string errorType
        string errorMessage "The safe failure diagnostic the dispatcher already computes for its span — never a raw provider body."
        int retryAfterMs
    }
    TopicClusteringRunProjection {
        string id PK "KSUID (`topicrun_…`), generated by the repository on first upsert."
        string projectId UK
        float LastRequestedAt
        string LastRequestTrigger
        float LastRunAt
        string LastRunOutcome "completed | skipped | failed"
        string LastRunMode
        string LastRunSkippedReason
        string LastRunError
        string LastRunErrorCode
        boolean LastRunErrorUserActionable "True when the customer can resolve the failure (credentials, quota)."
        int LastRunTracesProcessed
        int LastRunTopicsCount
        int LastRunSubtopicsCount
        int LastRunPages
        string InProgressRunId
        int InProgressTraces
        int InProgressPages
        float InProgressStartedAt "Business time the in-progress run opened; bounds how long the read model may report it as running (mirrors the process's stale-run guard)."
        float CreatedAt
        float UpdatedAt
        float OccurredAt "Business time of the latest applied event."
        float AcceptedAt "Event-log acceptance time. Together with lastEventId this is the cursor."
        string LastEventId
        string ProjectionVersion
    }
    TopicModelProjection {
        string id PK "KSUID (`topicmodel_…`), generated by the repository on first upsert."
        string projectId UK
        float CreatedAt
        float UpdatedAt
        float OccurredAt "Business time of the latest applied event."
        float AcceptedAt "Event-log acceptance time. Together with lastEventId this is the cursor."
        string LastEventId
        string ProjectionVersion
    }
    TopicClusteringRunHistoryProjection {
        string id PK "KSUID (`topicrunhist_…`), generated by the repository on first upsert."
        string projectId UK
        json Runs
        float CreatedAt
        float UpdatedAt
        float OccurredAt "Business time of the latest applied event."
        float AcceptedAt "Event-log acceptance time. Together with lastEventId this is the cursor."
        string LastEventId
        string ProjectionVersion
    }
    IngestionPullRunProjection {
        string id PK
        string sourceId UK
        string projectId
        boolean Enabled
        string Cron
        string Cursor
        float LastRunAt
        string LastRunOutcome
        int LastRunEventCount
        string LastRunError
        string LastRunErrorCode
        int ConsecutiveErrors
        float LastSuccessAt "When a run last completed without erroring. Backfilled by replay, which is why the projection version moved with this column."
        float LastRunScheduledFor "Identity of the run whose outcome this row reflects. Outcomes from a superseded run arrive with a smaller value and are ignored, so a late completion cannot regress Cursor or the status fields."
        float LastReadThroughAt "ADR-128 - how far the last run read, in epoch milliseconds, and whether it reached the end. The fold writes both; the IngestionSource row mirrors them for the screens. Null read-through means no run has reported one. Null completeness reads as unknown, never as either answer, which is what every row written before this existed holds.  Nullable on purpose, so replay is not required to make this migration safe: a row that has never seen a run carrying these stays null and every reader already handles unknown. Spec: specs/governance/ingestion-source-health.feature"
        string LastRunCompleteness
        float LastAgentsListingAt "The last time somebody asked this source to enumerate its agents, and how that ask ended. A listing is a different scope on the same connection as the pull above: a credential that cannot enumerate a directory still pulls cost perfectly, so no health, coverage or completeness reader may treat a refusal here as the source failing to pull.  Count is set ONLY when the outcome is listed, and stays null on a refusal. Zero is a real answer meaning the provider returned an empty list, so a zero default would manufacture that answer for a source that refused. Reason is set only on a refusal and is a plain string, never an enum and never a foreign key, because the log outlives the vocabulary. Status is nullable even when refused, because a refusal we generated ourselves has no HTTP status, and it is never rendered to a customer.  The two `...ListingAt` columns hold the instant the ASK was accepted, not the instant its answer was recorded, and the fold orders outcomes by them so a superseded listing cannot overwrite the one that replaced it. The name predates that job and is kept because renaming a column is a migration; the fold's own field docs are the statement of record."
        string LastAgentsListingOutcome
        int LastAgentsListingCount
        string LastAgentsListingReason
        int LastAgentsListingStatus
        float LastPeopleListingAt "The same for people, in its own set of columns rather than sharing the agents set with a kind discriminator. A source can be refused for people and fine for agents, and one shared set holds only the most recent listing of either kind, so syncing people would erase an agents refusal and the agents page would go back to not knowing after briefly knowing.  LastPeopleDirectoryCount is everyone the provider's directory named. LastPeopleWithheldCount is how many of those this deployment does not hold because erasure suppression removed them: a SUBSET of the directory count, never an addition to it. Adding the two counts the same people twice. Subtracting is the valid arithmetic, and the surviving total is that subtraction rather than a third column that could disagree.  The withheld count is safe to show as a CURRENT figure and never as a series: it moving from zero to one at a known moment says an erasure happened then, which on a small tenant identifies the person as surely as a name would. So no trend line, no history drawer, no per-run export, and never beside a per-person list."
        string LastPeopleListingOutcome
        int LastPeopleDirectoryCount
        int LastPeopleWithheldCount
        string LastPeopleListingReason
        int LastPeopleListingStatus
        float CreatedAt
        float UpdatedAt
        float OccurredAt
        float AcceptedAt
        string LastEventId
        string ProjectionVersion
    }
    ScheduledJob {
        string id PK
        string projectId
        string targetType UK
        string targetId UK
        string cron
        string timezone
        datetime nextRunAt
        datetime lastSlot
        datetime currentSlot
        int attempts
        string lastError
        boolean active
        datetime createdAt
        datetime updatedAt
    }
    BugReport {
        string id PK
        datetime createdAt
        string source
        string kind
        string title
        string summary
        string sessionData
        boolean sessionTruncated
        string agent
        string contactEmail
        string cliVersion
        string linkedProjectId
        json metadata
    }
    IdempotencyReceipt {
        string id PK
        string scopeId UK
        string key UK
        string claimId
        string requestFingerprint
        int responseStatus
        string responseBody
        datetime createdAt
        datetime heartbeatAt
        datetime expiresAt
    }
    GatewayRealtimeSession {
        string id PK
        string projectId
        string organizationId
        string virtualKeyId
        string modelProviderId
        string vendor UK
        string agentId
        string model
        string requestedModel
        string vendorConversationId UK
        gatewayrealtimesessionstatus status
        datetime mintedAt
        datetime closedAt
        string closeReason
        string traceId
        json vendorCostRaw
        string kind
        string metering
        datetime credentialExpiresAt
        string transcriptionModel
        string endUserId
        datetime lastReportAt
        bigint reportedCostNanoUsd
        int reportCount
        datetime createdAt
        datetime updatedAt
    }
    GatewayRealtimeSessionReport {
        string id PK
        string sessionId UK
        string reportKey UK
        string projectId
        string model
        json usage
        bigint costNanoUsd
        datetime createdAt
    }
    SystemMigrationTenantState {
        string migrationName PK
        string tenantId PK
        string status
        json report
        datetime occurredAt
        datetime createdAt
        datetime updatedAt
    }
    SystemMigrationEnrollment {
        string organizationId PK
        string migrationName PK
        string enrolledByUserId
        datetime createdAt
    }
    StoredObject {
        string tenantId PK
        string id PK
        string status
        string purpose
        string ownerKind
        string ownerId
        string filename
        string sha256
        bigint sizeBytes
        string mediaType
        boolean mediaTypeVerified
        string storageProvider
        string storageDestinationId
        string storageProviderRelativeId
        int generation
        json audiences
        datetime expiresAt
        datetime availableAt
        datetime deletedAt
        string source
        string legacyFingerprint
        datetime createdAt
        datetime updatedAt
    }
    DiscoveredPerson {
        string id PK
        string organizationId UK
        string provider UK "#quot;anthropic#quot; | #quot;openai#quot; | #quot;databricks#quot; | ... - the provider that named them."
        string rawActorId UK "What the provider calls them, verbatim. The join key onto the money rows' RawActorId. Replaced by a deterministic pseudonym on erasure, which keeps the row addressable for spend attribution without keeping the identifier."
        string displayText "Name or email text as seen. Pseudonymized alongside rawActorId on erasure."
        string kind "person | service_account - determined deterministically per ADR-128 §10 (a bare UUID in Databricks `executed_by` is a service account, never an agent), never guessed from the shape of a name."
        string department "The department the provider's directory filed this person under, verbatim. An OBSERVATION, not the `Department` entity: it is free text a tenant owns on their side, it exists for people who hold no LangWatch account, and nothing rolls spend up by it. Spend attribution stays on `Department`, via the membership the match engine proves (directoryDepartmentSync.service). Null means no directory has named one - never #quot;no department#quot;, because a blank field in the tenant's directory and a person no directory lists are the same null here."
        datetime firstSeenAt
        datetime lastSeenAt
        datetime erasedAt "Set when this row's identity has been erased. The row survives so its spend stays attributable to *someone*; the identifier does not."
        datetime moneyRowsPendingAt "Set alongside `erasedAt` and cleared once the daily cost rows have been removed and their rebuild requested. Non-null means the ClickHouse half of the erasure did not finish — a crash between the delete and the replay leaves days short by the erased amount, and without this marker a re-run reads `erasedAt`, short-circuits, and reports a clean erasure that did no work. The re-run resumes at that step instead."
        string moneyRebuildSince "The earliest day to rebuild from, recorded BEFORE the rows are deleted. Once they are gone there is nothing left to ask which days they were on, so a resume with no record of this could only replay everything or nothing. Null means nothing was rebuildable — either no day carried the identifier, or every one of them is older than the event log keeps."
        datetime suspendedAt "ADR-128 §12 conflict rule: automatic linking has stopped for this identity because evidence pointed at two candidates, or contradicted an existing link. Stored HERE rather than on the suggestion rows, because a halt on auto-linking that a recompute or a restart clears is not a halt."
        string suspendedReason "What tripped the suspension, for the human who reviews it."
        datetime createdAt
        datetime updatedAt
    }
    DiscoveredAgent {
        string id PK
        string organizationId UK
        string provider UK
        string rawAgentId UK "Genie space / Copilot bot / OpenAI project id, verbatim."
        string displayText
        json metadata "Workspace, plan, and kin - provider-native descriptive fields only. Never credentials: this row is read by admin screens, not by any pull path."
        datetime firstSeenAt
        datetime lastSeenAt
        datetime createdAt
        datetime updatedAt
    }
    IdentityMatch {
        string id PK
        string organizationId
        string discoveredPersonId "The provider-side identity this link is about."
        string userId "The platform user. Nullable, and blanked by erasure (ADR-128 §9 step 2) - the row and its dates remain, so the history of the link is not rewritten by the erasure either."
        string evidenceKind "directory_id | verified_email | human_confirmed. Exact verified-email equality is the primary evidence; a directory id corroborates but never stands alone (ADR-128 §12)."
        datetime validFrom
        datetime validTo "Null means the link is open. Offboarding and corrections close it."
        datetime createdAt
    }
    GovernanceTenantHistory {
        string id PK
        string organizationId UK
        string tenantId UK "A hidden governance project id this organization has written rows under."
        datetime firstUsedAt
        datetime lastUsedAt
    }
    ErasedIdentifierSuppression {
        string id PK
        string organizationId UK
        string provider UK
        string identifierHash UK
        datetime erasedAt
    }
    IdentityMatchSuggestion {
        string id PK
        string organizationId UK
        string discoveredPersonId UK
        string userId UK "The platform user this might be. Not nullable: a suggestion with no candidate is not a suggestion."
        float score "Similarity of the two name texts, after the prefilter, in [0, 1]. A display hint and an ordering key - never a threshold anything auto-links on, which is the whole distinction this table exists to hold."
        datetime computedAt
    }
    ConnectedSeatChange {
        string id PK
        string licenseId UK "The reissued `IssuedLicense` row that carries the new seat count. A plain column ref, like `replacesId`: relationMode=#quot;prisma#quot; carries no foreign keys."
        string accountId "The `ConnectedBillingAccount` row the invoice is raised against. Null when the customer had no billing account (`not_onboarded`)."
        string organizationId "The customer organization the license belongs to."
        datetime changedAt "When the seats changed, which is the day the proration counts from."
        int addedSeats "Seats on the new license above the seats on the one it replaced."
        int unitAmountCents "What one added seat costs for the rest of the term, in cents of `currency`."
        int amountCents "`unitAmountCents` times `addedSeats`. Zero when nothing is owed."
        currency currency "Null when no billing account named one (`not_onboarded`)."
        string stripeInvoiceId "The one-off invoice the added seats were billed on."
        connectedseatchangestate state
        datetime createdAt
        datetime updatedAt
    }
    ConnectedStatement {
        string id PK
        string accountId UK "The `ConnectedBillingAccount` row. A plain column ref."
        datetime month UK "First day of the month the statement covers, in UTC."
        datetime sentAt
    }
    LicenseRenewalClaim {
        string stripeInvoiceId PK
        string stripeSubscriptionId
        string claimToken "Fences completion and release to the delivery that holds the claim."
        datetime claimedAt
        json issuedLicense "The signed licence between minting and mailing, so a retry mails it again instead of minting another; cleared once delivered."
        datetime deliveredAt
        string licenseId "The `licenseId` inside the delivered licence."
    }
    InstanceIdentity {
        string id PK "Fixed, so this table holds one row. A second insert loses the race on the primary key and re-reads the winner's id."
        string instanceId UK "A random UUID. Carries nothing about the customer."
        datetime createdAt
        datetime lastReportAt "When the usage report last reached LangWatch, and why the last attempt did not. Both are what the checkup page reads to say whether reporting works, which nothing recorded before: the sender logged success as soon as `fetch` resolved and never looked at the response."
        string lastReportError
        boolean optionalMetricsOptOut "The optional category of the usage report, switched off from the checkup page. With it off the report still says which release runs and how many projects the install carries, and says nothing about what anyone does with it (ADR-139, section 10)."
        boolean hostnameOptOut "Hostname, which names the customer's own network and so has a switch of its own rather than travelling with its category."
        boolean upgradeDiagnosticsOptOut "Sending a failed upgrade step's diagnostics to LangWatch on its own, switched off by the operator (upgrade-diagnostics-via-connect, decision 4)."
    }
    SelfHostedInstance {
        string id PK
        string instanceId UK "The UUID the install minted for itself. The join key for everything else."
        datetime firstSeenAt
        datetime lastSeenAt
        string version "What the install says about itself."
        string installMethod
        string chartVersion
        string hostname "The host it answers on, when the operator left hostname reporting on."
        string environment
        datetime installedAt "The day the install itself says it first came up, which predates the first report on an install that switched reporting on later."
        int reportSchemaVersion
        string organizationId "The customer, and the license that named them. Plain column refs rather than relations, like `IssuedLicense.replacesId`: relationMode=#quot;prisma#quot; carries no foreign keys, and the receiver resolves both from the license registry rather than from the report."
        string issuedLicenseId
        json userEmailDomains "Domains with counts, as `{#quot;acme.test#quot;: 14}`. Never an address. Null on an install that switched the optional category off."
        string[] userDomains "The same domains as the keys of `userEmailDomains`, flattened so an operator can search for one. A JSON object's keys are the domains themselves, and no query language addresses a key it does not already know the name of."
        json latestReport "What the last report carried, so the list and the drawer read one row: the onboarding ladder, the usage counts and the operational block, under the same keys the dictionary declares."
        boolean optionalMetricsReported "What the last report said the operator had switched off, read from which categories arrived rather than from a field the install could assert."
        boolean hostnameReported
        int reportCount "How many reports have landed, and how many fields the last one carried that this release has no name for. A count above zero says the senders are ahead of the receiver."
        int lastUnknownFields
        string[] raisedSignals "The lead signals already raised for this install. A signal on this list is never raised again, which is what keeps a large install from reporting that it is large every morning."
        datetime createdAt
        datetime updatedAt
    }
    SelfHostedInstanceDiagnostic {
        string id PK
        string instanceId "The `SelfHostedInstance.instanceId` the licence caller presented."
        string stepId
        string stepDescription
        string release
        int attempt
        string trigger "`operator` or `automatic`."
        int byteLength
        string objectKey
        datetime receivedAt
        datetime expiresAt "When the lifecycle rule removes the bundle; the row is listed until then."
        datetime slackAnnouncedAt "Set on the report that posted this step and release's one Slack message."
    }
    SelfHostedNotice {
        string id PK
        string severity "`info`, `warning` or `security`."
        string title
        string body
        string url "An https link to read more; null where the notice names none."
        string fixedIn "The release that fixes what the notice is about; an install on it or newer hides it."
        string status "`draft`, `published` or `withdrawn`. Only a published notice that has not expired is sent."
        datetime expiresAt
        datetime publishedAt
        string publishedBy "The staff member who published it, by user id."
        datetime createdAt
        datetime updatedAt
    }
    SelfHostedInstanceReport {
        string id PK
        string instanceId "The `SelfHostedInstance.instanceId` this report came from. A plain column ref for the same reason the instance row's refs are."
        datetime receivedAt
        string version
        int reportSchemaVersion
        int unknownFields "Fields this release had no name for. Counted, never stored by name."
        json payload "The validated report, with every unknown field already dropped."
    }
    WebPushSubscription {
        string id PK
        string userId "The person the pushes are for. A plain column: the row leaves with the person through notification's own subscriber on the user lifecycle."
        string endpoint UK "The push service URL the browser handed out. Unique per browser."
        string p256dh "The browser's P-256 public key and auth secret, base64url, as the browser reports them. Payloads are encrypted to these."
        string auth
        string userAgent
        datetime createdAt
        datetime lastSuccessAt "When the push service last accepted a push for this browser."
    }
    WebPushVapidKey {
        string id PK
        string publicKey "The uncompressed P-256 public key, base64url. The browser subscribes with it."
        string privateKeyEncrypted "The private key, base64url, encrypted with the process encryption key."
        datetime createdAt
    }
    SignInProviderMount {
        string id PK "Fixed, so this table holds one row: licensing's copy of sso's sign-in provider fact (ADR-179)."
        string provider "The configured provider's name; null when the deployment signs in by email."
        boolean mounted
        string lastAppliedEventId "The last sso event applied: the row's idempotency guard and its version."
        datetime updatedAt
    }
```