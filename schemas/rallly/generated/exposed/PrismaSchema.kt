package rallly.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"api_usage_resource\" AS ENUM ('poll')",
            "CREATE TYPE \"subscription_status\" AS ENUM ('incomplete', 'incomplete_expired', 'active', 'paused', 'trialing', 'past_due', 'canceled', 'unpaid')",
            "CREATE TYPE \"subscription_interval\" AS ENUM ('month', 'year')",
            "CREATE TYPE \"nonprofit_application_status\" AS ENUM ('approved', 'rejected', 'failed')",
            "CREATE TYPE \"queued_email_status\" AS ENUM ('pending', 'sent', 'skipped', 'failed')",
            "CREATE TYPE \"scheduled_event_status\" AS ENUM ('confirmed', 'canceled', 'unconfirmed')",
            "CREATE TYPE \"scheduled_event_invite_status\" AS ENUM ('pending', 'accepted', 'declined', 'tentative')",
            "CREATE TYPE \"CredentialType\" AS ENUM ('OAUTH')",
            "CREATE TYPE \"LicenseType\" AS ENUM ('PLUS', 'ORGANIZATION', 'ENTERPRISE')",
            "CREATE TYPE \"LicenseStatus\" AS ENUM ('ACTIVE', 'REVOKED')",
            "CREATE TYPE \"participant_visibility\" AS ENUM ('full', 'scoresOnly', 'limited')",
            "CREATE TYPE \"poll_status\" AS ENUM ('open', 'closed', 'scheduled', 'canceled')",
            "CREATE TYPE \"poll_kind\" AS ENUM ('date', 'time')",
            "CREATE TYPE \"poll_closed_reason\" AS ENUM ('auto', 'manual')",
            "CREATE TYPE \"vote_type\" AS ENUM ('yes', 'no', 'ifNeedBe')",
            "CREATE TYPE \"SpaceMemberRole\" AS ENUM ('ADMIN', 'MEMBER')",
            "CREATE TYPE \"space_types\" AS ENUM ('personal', 'work')",
            "CREATE TYPE \"space_tiers\" AS ENUM ('hobby', 'pro')",
            "CREATE TYPE \"webhook_delivery_status\" AS ENUM ('pending', 'in_flight', 'succeeded', 'failed', 'exhausted')",
            "CREATE TYPE \"time_format\" AS ENUM ('hours12', 'hours24')",
            "CREATE TYPE \"user_role\" AS ENUM ('admin', 'user')",
        )
    val tables: List<Table> =
        listOf(
            ApiUsageTable,
            SubscriptionTable,
            PaymentMethodTable,
            NonprofitApplicationTable,
            QueuedEmailTable,
            EventTypeTable,
            ScheduledEventTable,
            RescheduledEventDateTable,
            ScheduledEventInviteTable,
            InstanceSettingsTable,
            CredentialTable,
            CalendarConnectionTable,
            ProviderCalendarTable,
            ConferencingConnectionTable,
            LicenseTable,
            LicenseValidationTable,
            InstanceLicenseTable,
            UserNotificationPreferencesTable,
            PollTable,
            ParticipantTable,
            PollInviteTable,
            PollActivityTable,
            OptionTable,
            VoteTable,
            CommentTable,
            RegisteredInstanceTable,
            SheetTable,
            SheetSlotTable,
            SpaceTable,
            SpaceMemberTable,
            SpaceMemberInviteTable,
            SpaceApiKeyTable,
            SpaceWebhookTable,
            WebhookDeliveryTable,
            AccountTable,
            UserTable,
            SessionTable,
            VerificationTable,
        )
}
