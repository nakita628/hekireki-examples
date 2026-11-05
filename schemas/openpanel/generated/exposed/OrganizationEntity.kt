package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationEntity>(OrganizationTable)

    var name by OrganizationTable.name
    var slug by OrganizationTable.slug
    val projects by ProjectEntity referrersOn ProjectTable.organizationId
    val members by MemberEntity referrersOn MemberTable.organizationId
    var createdByUserId by OrganizationTable.createdByUserId
    var createdBy by UserEntity optionalReferencedOn OrganizationTable.createdByUserId
    val client by ClientEntity referrersOn ClientTable.organizationId
    val dashboard by DashboardEntity referrersOn DashboardTable.organizationId
    val shareOverview by ShareOverviewEntity referrersOn ShareOverviewTable.organizationId
    val shareDashboard by ShareDashboardEntity referrersOn ShareDashboardTable.organizationId
    val shareReport by ShareReportEntity referrersOn ShareReportTable.organizationId
    val shareWidget by ShareWidgetEntity referrersOn ShareWidgetTable.organizationId
    val integrations by IntegrationEntity referrersOn IntegrationTable.organizationId
    val invites by InviteEntity referrersOn InviteTable.organizationId
    val conversations by ConversationEntity referrersOn ConversationTable.organizationId
    var timezone by OrganizationTable.timezone
    var onboarding by OrganizationTable.onboarding
    var attribution by OrganizationTable.attribution
    var subscriptionId by OrganizationTable.subscriptionId
    var subscriptionCustomerId by OrganizationTable.subscriptionCustomerId
    var subscriptionPriceId by OrganizationTable.subscriptionPriceId
    var subscriptionProductId by OrganizationTable.subscriptionProductId
    var subscriptionStatus by OrganizationTable.subscriptionStatus
    var subscriptionStartsAt by OrganizationTable.subscriptionStartsAt
    var subscriptionEndsAt by OrganizationTable.subscriptionEndsAt
    var subscriptionCanceledAt by OrganizationTable.subscriptionCanceledAt
    var subscriptionCreatedByUserId by OrganizationTable.subscriptionCreatedByUserId
    var subscriptionCreatedBy by UserEntity optionalReferencedOn OrganizationTable.subscriptionCreatedByUserId
    var subscriptionPeriodEventsCount by OrganizationTable.subscriptionPeriodEventsCount
    var subscriptionPeriodEventsCountExceededAt by OrganizationTable.subscriptionPeriodEventsCountExceededAt
    var subscriptionPeriodEventsLimit by OrganizationTable.subscriptionPeriodEventsLimit
    var subscriptionInterval by OrganizationTable.subscriptionInterval
    var subscriptionCancelReason by OrganizationTable.subscriptionCancelReason
    var subscriptionCancelComment by OrganizationTable.subscriptionCancelComment
    var subscriptionSaveDiscountAppliedAt by OrganizationTable.subscriptionSaveDiscountAppliedAt
    var subscriptionPauseAtPeriodEnd by OrganizationTable.subscriptionPauseAtPeriodEnd
    var subscriptionResumesAt by OrganizationTable.subscriptionResumesAt
    var subscriptionFirstStartedAt by OrganizationTable.subscriptionFirstStartedAt
    var usageWarningSentAt by OrganizationTable.usageWarningSentAt
    var usageExceededSentAt by OrganizationTable.usageExceededSentAt
    var subscriptionDiscount by OrganizationTable.subscriptionDiscount
    var windDownStartedAt by OrganizationTable.windDownStartedAt
    var windDownStep by OrganizationTable.windDownStep
    var deleteAt by OrganizationTable.deleteAt
    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
