```mermaid
erDiagram
    users ||--o{ subscriptions : "(id) - (userId)"
    spaces ||--o{ subscriptions : "(id) - (spaceId)"
    users ||--o{ payment_methods : "(id) - (userId)"
    spaces ||--o{ nonprofit_applications : "(id) - (spaceId)"
    users ||--o{ nonprofit_applications : "(id) - (userId)"
    users ||--o{ queued_emails : "(id) - (userId)"
    spaces ||--o{ event_types : "(id) - (spaceId)"
    users ||--o{ event_types : "(id) - (hostId)"
    users ||--o{ scheduled_events : "(id) - (userId)"
    spaces ||--o{ scheduled_events : "(id) - (spaceId)"
    event_types |o--o{ scheduled_events : "(id) - (eventTypeId)"
    sheet_slots |o--o| scheduled_events : "(id) - (sheetSlotId)"
    scheduled_events ||--o{ rescheduled_event_dates : "(id) - (scheduledEventId)"
    scheduled_events ||--o{ scheduled_event_invites : "(id) - (scheduledEventId)"
    users |o--o{ scheduled_event_invites : "(id) - (inviteeId)"
    users ||--o{ credentials : "(id) - (userId)"
    credentials ||--o{ calendar_connections : "(id) - (credentialId)"
    users ||--o{ calendar_connections : "(id) - (userId)"
    calendar_connections ||--o{ provider_calendars : "(id) - (calendarConnectionId)"
    credentials ||--o{ conferencing_connections : "(id) - (credentialId)"
    users ||--o{ conferencing_connections : "(id) - (userId)"
    licenses ||--o{ license_validations : "(id) - (licenseId)"
    users ||--o| user_notification_preferences : "(id) - (userId)"
    users |o--o{ polls : "(id) - (userId)"
    scheduled_events |o--o{ polls : "(id) - (scheduledEventId)"
    spaces |o--o{ polls : "(id) - (spaceId)"
    polls ||--o{ participants : "(id) - (pollId)"
    users |o--o{ participants : "(id) - (userId)"
    polls ||--o{ poll_invites : "(id) - (pollId)"
    participants |o--o| poll_invites : "(id) - (participantId)"
    polls ||--o{ poll_activities : "(id) - (pollId)"
    polls ||--o{ options : "(id) - (pollId)"
    participants ||--o{ votes : "(id) - (participantId)"
    options ||--o{ votes : "(id) - (optionId)"
    polls ||--o{ votes : "(id) - (pollId)"
    polls ||--o{ comments : "(id) - (pollId)"
    users |o--o{ comments : "(id) - (userId)"
    spaces ||--o{ sheets : "(id) - (spaceId)"
    users ||--o{ sheets : "(id) - (hostId)"
    sheets ||--o{ sheet_slots : "(id) - (sheetId)"
    event_types ||--o{ sheet_slots : "(id) - (eventTypeId)"
    users ||--o{ spaces : "(id) - (ownerId)"
    spaces ||--o{ space_members : "(id) - (spaceId)"
    users ||--o{ space_members : "(id) - (userId)"
    users ||--o{ space_member_invites : "(id) - (inviterId)"
    spaces ||--o{ space_member_invites : "(id) - (spaceId)"
    spaces ||--o{ space_api_keys : "(id) - (spaceId)"
    spaces ||--o{ space_webhooks : "(id) - (spaceId)"
    space_webhooks ||--o{ webhook_deliveries : "(id) - (webhookId)"
    users ||--o{ accounts : "(id) - (userId)"
    provider_calendars |o--o{ users : "(id) - (defaultDestinationCalendarId)"
    users ||--o{ sessions : "(id) - (userId)"
    users |o--o{ sessions : "(id) - (impersonatedBy)"
    subscriptions {
        string id PK
        string priceId
        int quantity
        string subscriptionItemId
        int amount
        float discountPercentOff
        int discountAmountOff
        subscriptionstatus status
        boolean active
        string currency
        subscriptioninterval interval
        datetime createdAt
        datetime periodStart
        datetime periodEnd
        boolean cancelAtPeriodEnd
        string userId FK
        string spaceId FK
    }
    payment_methods {
        string id PK
        string userId FK
        string type
        json data
        datetime createdAt
        datetime updatedAt
    }
    nonprofit_applications {
        string id PK
        string spaceId FK
        string userId FK
        string organizationName
        string website
        string emailDomain
        nonprofitapplicationstatus status
        string reason
        string model
        datetime createdAt
    }
    queued_emails {
        string id PK
        string kind "Which handler builds the email. A string rather than an enum so a new kind needs no migration; the claim only takes kinds the build knows."
        string subjectId "The row the email is about, interpreted by the kind. Not a foreign key: kinds point at different tables, and a subject deleted before the send makes its handler skip the email."
        string batchId "Groups the emails one action queued, so the action can send its own first batch at once and its progress can be read back."
        string userId FK "The account whose action queued the email. Banning it skips whatever is still pending."
        queuedemailstatus status
        datetime sendAfter "Not claimed before this time. Unset sends at the next run."
        int attempts
        datetime claimedAt
        datetime sentAt
        string lastError
        datetime createdAt
        datetime updatedAt
    }
    event_types {
        string id PK
        string spaceId FK
        string hostId FK
        string name
        int duration
        int capacity
        string description
        json location
        datetime createdAt
        datetime updatedAt
        boolean deleted
        datetime deletedAt
    }
    scheduled_events {
        string id PK
        string userId FK
        string spaceId FK
        string eventTypeId FK
        string sheetSlotId FK, UK
        string title
        string description
        json location
        json conferencing
        int capacity
        datetime createdAt
        datetime updatedAt
        scheduledeventstatus status
        string timeZone "Venue / anchor timezone for in-person or hybrid events. Null means no fixed venue (e.g. remote-only)."
        datetime start
        datetime end
        boolean allDay
        datetime deletedAt
        int sequence
        string uid UK
        boolean hideAttendees
    }
    rescheduled_event_dates {
        string id PK
        string scheduledEventId FK
        datetime start
        datetime end
        boolean allDay
        datetime createdAt
    }
    scheduled_event_invites {
        string id PK
        string uid UK
        string scheduledEventId FK, UK
        string inviteeName
        string inviteeEmail UK
        string inviteeId FK
        string inviteeTimeZone
        string inviteeLocale
        scheduledeventinvitestatus status
        datetime createdAt
        datetime updatedAt
    }
    instance_settings {
        int id PK
        boolean disableUserRegistration
        string instanceId UK
        string appName
        string primaryColor
        string primaryColorDark
        string logo
        string logoDark
        string logoIcon
        boolean hideAttribution
        json footerLinks
        datetime createdAt
        datetime updatedAt
    }
    credentials {
        string id PK
        string userId FK, UK
        string provider UK
        string providerAccountId UK
        credentialtype type
        string secret
        string[] scopes
        datetime expiresAt
        datetime createdAt
        datetime updatedAt
    }
    calendar_connections {
        string id PK
        string userId FK, UK
        string provider UK
        string integrationId
        string providerAccountId UK
        string email
        string displayName
        datetime createdAt
        datetime updatedAt
        string credentialId FK
    }
    provider_calendars {
        string id PK
        string calendarConnectionId FK, UK
        string providerCalendarId UK
        string name
        boolean isPrimary
        string timeZone
        boolean isSelected
        boolean isDeleted
        boolean isWritable
        json providerData
        datetime lastSyncedAt
        datetime createdAt
        datetime updatedAt
    }
    conferencing_connections {
        string id PK
        string userId FK, UK
        string provider UK
        string integrationId
        string providerAccountId UK
        string email
        string displayName
        datetime createdAt
        datetime updatedAt
        string credentialId FK
    }
    licenses {
        string id PK
        string licenseKey UK
        string idempotencyKey UK
        int version
        licensetype type
        int seats
        datetime issuedAt
        datetime expiresAt
        string licenseeEmail
        string licenseeName
        boolean whiteLabelAddon
        licensestatus status
    }
    license_validations {
        string id PK
        string licenseId FK
        string ipAddress
        string fingerprint
        datetime validatedAt
        string userAgent
    }
    instance_licenses {
        string id PK
        string licenseKey UK
        int version
        licensetype type
        int seats
        datetime issuedAt
        datetime expiresAt
        string licenseeEmail
        string licenseeName
        boolean whiteLabelAddon
        licensestatus status
    }
    user_notification_preferences {
        string id PK
        string userId FK, UK
        json prefs
        datetime createdAt
        datetime updatedAt
    }
    polls {
        string id PK
        datetime createdAt
        datetime updatedAt
        datetime deadline
        string title
        string description
        string location
        json conferencing "What the organizer asked for: a provider to mint a link with at finalize (#quot;zoom#quot; | #quot;meet#quot; | #quot;teams#quot; | #quot;webex#quot;), or a link they pasted themselves."
        string userId FK
        string timeZone
        pollstatus status
        pollclosedreason closedReason
        pollkind kind
        boolean deleted
        datetime deletedAt
        string eventId UK
        string scheduledEventId FK
        boolean hideParticipants
        boolean hideScores
        boolean disableComments
        boolean requireParticipantEmail
        boolean allowTentativeVotes "Whether participants may cast the tentative #quot;if need be#quot; vote. Positive flag so a future vote-type expansion adds columns rather than inverting a negative. Turning it off only blocks new tentative votes; existing ones are kept and stay visible."
        boolean muted
        string spaceId FK
    }
    participants {
        string id PK
        string name
        string email
        string note
        string token UK "Per response edit link: grants edit access to this response without a session. App minted at creation, CSPRNG alphanumeric ≥32 chars (nanoid customAlphabet — url safe with no linkifier edge cases, unlike base64url); never a DB default so a code path that forgets to mint fails loudly. A response created through an emailed invite takes the invite's token. Rows that predate minting were backfilled with gen_random_uuid() hex"
        string userId FK
        string pollId FK
        string locale
        string timeZone
        datetime createdAt
        datetime updatedAt
    }
    poll_invites {
        string id PK
        string pollId FK, UK
        string email UK
        string token UK "Per invitee link. App minted, CSPRNG alphanumeric ≥32 chars (nanoid customAlphabet) — no schema default, so forgetting to mint fails loudly. Rotated on reactivation so a revoked (possibly leaked) token never regains access"
        datetime openedAt
        datetime remindedAt
        datetime revokedAt "Revocation is always explicit: set when the host removes a pending invite, never as a side effect of close, schedule or reopen. Deleting a response instead hard deletes the participant, so the SetNull FK clears participantId and reverts the invite to pending with its token intact — the token the deleted response held is freed with it, so the next response through the same link can take it again. Cleared only by re-inviting (reactivation), which also rotates the token"
        string participantId FK, UK "Conversion join, merges invite history into the response timeline. Set only when the invitee responds via their token: a typed email is unverified, so a generic link response never claims an invite"
        datetime createdAt
        datetime updatedAt
    }
    poll_activities {
        string id PK
        string pollId FK
        string type "Validated by a zod union in the feature, not a DB enum: the activity vocabulary grows without migrations"
        string userId "Subject ids are soft references (no FKs): activity must outlive its subjects — including account deletion — so refs persist as identity and the payload carries the display snapshot; whether a ref still resolves is the renderer's question"
        string participantId
        string inviteId
        string optionId
        json payload
        datetime createdAt
    }
    options {
        string id PK
        datetime startTime
        int duration
        string pollId FK
        datetime createdAt
    }
    votes {
        string id PK
        string participantId FK
        string optionId FK
        string pollId FK
        votetype type
        datetime createdAt
        datetime updatedAt
    }
    comments {
        string id PK
        string content
        string pollId FK
        string authorName
        string userId FK
        datetime createdAt
        datetime updatedAt
    }
    registered_instances {
        string id PK
        string instanceId UK
        string version
        datetime firstSeenAt
        datetime lastSeenAt
    }
    sheets {
        string id PK
        string spaceId FK
        string hostId FK
        string title
        string description
        string urlId UK
        datetime createdAt
        datetime updatedAt
        boolean deleted
        datetime deletedAt
    }
    sheet_slots {
        string id PK
        string sheetId FK
        string eventTypeId FK
        datetime startTime
        datetime createdAt
        datetime updatedAt
    }
    spaces {
        string id PK
        string name
        string image
        string ownerId FK
        spacetier tier
        spacetype spaceType
        string industry
        string primaryColor
        boolean showBranding
        boolean hideAttribution
        boolean shared
        datetime nonprofitDiscountGrantedAt
        datetime createdAt
        datetime updatedAt
    }
    space_members {
        string id PK
        string spaceId FK, UK
        string userId FK, UK
        datetime createdAt
        datetime updatedAt
        spacememberrole role
        datetime lastSelectedAt
    }
    space_member_invites {
        string id PK
        string spaceId FK, UK
        string email UK
        datetime createdAt
        datetime updatedAt
        spacememberrole role
        string inviterId FK
    }
    space_api_keys {
        string id PK
        string spaceId FK
        string name
        string prefix UK
        string hashedKey
        datetime lastUsedAt
        datetime expiresAt
        datetime revokedAt
        datetime createdAt
        datetime updatedAt
    }
    space_webhooks {
        string id PK
        string spaceId FK
        string url
        string secret "AES-256-GCM under SECRET_PASSWORD (see @rallly/utils/encryption); the plaintext is shown once at creation"
        string[] events "Subscribed event names (`poll.closed`, ...); validated by the feature schema, not a DB enum, so the vocabulary grows without migrations"
        boolean enabled
        string version "The payload contract this endpoint was built against (WEBHOOK_VERSION at creation). No default: a writer that forgets it fails instead of recording a stale contract. A breaking payload change branches on this so existing endpoints keep their shape"
        datetime cursor "Fan-out position in the poll activity log: activities created after this instant are still to be delivered. Starts at creation so a new endpoint never replays history"
        datetime lastDeliveredAt
        datetime lastFailedAt
        int consecutiveFailures "Exhausted deliveries in a row; reset by any success. The dispatcher disables the endpoint once it reaches its cap"
        datetime createdAt
        datetime updatedAt
    }
    webhook_deliveries {
        string id PK
        string webhookId FK, UK
        string activityId UK "Soft reference to the PollActivity row this delivery was fanned out from; with webhookId it makes fan-out idempotent"
        string eventType
        json payload "Frozen at fan-out: retries resend exactly what the first attempt sent"
        webhookdeliverystatus status
        int attempts
        datetime nextAttemptAt
        int lastResponseStatus
        string lastError
        datetime createdAt
        datetime updatedAt
    }
    accounts {
        string id PK
        string userId FK
        string provider UK
        string providerAccountId UK
        string refresh_token
        string access_token
        int expires_at
        string scope
        string id_token
        datetime accessTokenExpiresAt
        datetime refreshTokenExpiresAt
        string password
        datetime createdAt
        datetime updatedAt
    }
    users {
        string id PK
        string name
        string email UK
        boolean emailVerified
        string image
        string timeZone
        int weekStart
        timeformat timeFormat
        string locale
        datetime createdAt
        datetime updatedAt
        string customerId
        boolean banned
        datetime bannedAt
        string banReason
        datetime banExpires
        userrole role
        string lastLoginMethod
        boolean isAnonymous
        datetime lastSeenAt
        datetime deletedAt
        string jobTitle
        datetime reviewRequestedAt
        string defaultDestinationCalendarId FK
    }
    sessions {
        string id PK
        datetime expiresAt
        string token UK
        datetime createdAt
        datetime updatedAt
        string ipAddress
        string userAgent
        string userId FK
        string impersonatedBy FK
    }
    verifications {
        string id PK
        string identifier UK
        string value
        datetime expiresAt
        datetime createdAt
        datetime updatedAt
    }
```