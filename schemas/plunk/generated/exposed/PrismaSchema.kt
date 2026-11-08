package plunk.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"ProjectDisabledReason\" AS ENUM ('PAYMENT_FAILED', 'EMAIL_REPUTATION', 'PHISHING_DETECTED', 'CARD_VERIFICATION_FAILED', 'CARD_VERIFICATION_REQUIRED', 'MANUAL')",
            "CREATE TYPE \"CardVerificationStatus\" AS ENUM ('PENDING', 'VERIFIED', 'REQUIRES_ACTION', 'FAILED')",
            "CREATE TYPE \"AuthMethod\" AS ENUM ('PASSWORD', 'GOOGLE_OAUTH', 'GITHUB_OAUTH')",
            "CREATE TYPE \"Role\" AS ENUM ('OWNER', 'ADMIN', 'MEMBER')",
            "CREATE TYPE \"TemplateType\" AS ENUM ('TRANSACTIONAL', 'MARKETING', 'HEADLESS')",
            "CREATE TYPE \"TrackingMode\" AS ENUM ('ENABLED', 'DISABLED', 'MARKETING_ONLY')",
            "CREATE TYPE \"CampaignStatus\" AS ENUM ('DRAFT', 'SCHEDULED', 'SENDING', 'SENT', 'CANCELLED')",
            "CREATE TYPE \"CampaignAudienceType\" AS ENUM ('ALL', 'FILTERED', 'SEGMENT')",
            "CREATE TYPE \"WorkflowTriggerType\" AS ENUM ('EVENT', 'MANUAL', 'SCHEDULE')",
            "CREATE TYPE \"WorkflowStepType\" AS ENUM ('TRIGGER', 'SEND_EMAIL', 'DELAY', 'WAIT_FOR_EVENT', 'CONDITION', 'EXIT', 'WEBHOOK', 'UPDATE_CONTACT')",
            "CREATE TYPE \"WorkflowExecutionStatus\" AS ENUM ('RUNNING', 'WAITING', 'COMPLETED', 'EXITED', 'FAILED', 'CANCELLED')",
            "CREATE TYPE \"StepExecutionStatus\" AS ENUM ('PENDING', 'SCHEDULED', 'WAITING', 'RUNNING', 'COMPLETED', 'SKIPPED', 'FAILED')",
            "CREATE TYPE \"SegmentType\" AS ENUM ('DYNAMIC', 'STATIC')",
            "CREATE TYPE \"EmailSourceType\" AS ENUM ('TRANSACTIONAL', 'CAMPAIGN', 'WORKFLOW', 'INBOUND')",
            "CREATE TYPE \"EmailStatus\" AS ENUM ('SCHEDULED', 'CANCELLED', 'PENDING', 'SENDING', 'SENT', 'DELIVERED', 'RECEIVED', 'OPENED', 'CLICKED', 'BOUNCED', 'COMPLAINED', 'FAILED')",
        )
    val tables: List<Table> =
        listOf(
            UserTable,
            ProjectTable,
            MembershipTable,
            DomainTable,
            ContactTable,
            TemplateTable,
            SegmentTable,
            SegmentMembershipTable,
            CampaignTable,
            WorkflowTable,
            WorkflowStepTable,
            WorkflowTransitionTable,
            WorkflowExecutionTable,
            WorkflowStepExecutionTable,
            EmailTable,
            EventTable,
            IdempotencyKeyTable,
            ThrottleClaimTable,
            ApiRequestTable,
        )
}
