package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ProjectEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ProjectEntity>(ProjectTable)

    var name by ProjectTable.name
    var public by ProjectTable.public
    var secret by ProjectTable.secret
    var disabled by ProjectTable.disabled
    var disabledReason by ProjectTable.disabledReason
    var customer by ProjectTable.customer
    var subscription by ProjectTable.subscription
    var cardVerification by ProjectTable.cardVerification
    var cardVerificationIntent by ProjectTable.cardVerificationIntent
    var cardVerificationAt by ProjectTable.cardVerificationAt
    var cardVerificationSession by ProjectTable.cardVerificationSession
    var cardVerificationPaymentMethod by ProjectTable.cardVerificationPaymentMethod
    var cardVerificationFingerprint by ProjectTable.cardVerificationFingerprint
    var cardVerificationAttempts by ProjectTable.cardVerificationAttempts
    var billingLimitWorkflows by ProjectTable.billingLimitWorkflows
    var billingLimitCampaigns by ProjectTable.billingLimitCampaigns
    var billingLimitTransactional by ProjectTable.billingLimitTransactional
    var billingLimitInbound by ProjectTable.billingLimitInbound
    var tracking by ProjectTable.tracking
    var language by ProjectTable.language
    val members by MembershipEntity referrersOn MembershipTable.projectId
    val contacts by ContactEntity referrersOn ContactTable.projectId
    val templates by TemplateEntity referrersOn TemplateTable.projectId
    val segments by SegmentEntity referrersOn SegmentTable.projectId
    val workflows by WorkflowEntity referrersOn WorkflowTable.projectId
    val campaigns by CampaignEntity referrersOn CampaignTable.projectId
    val emails by EmailEntity referrersOn EmailTable.projectId
    val events by EventEntity referrersOn EventTable.projectId
    val domains by DomainEntity referrersOn DomainTable.projectId
    val apiRequests by ApiRequestEntity optionalReferrersOn ApiRequestTable.projectId
    val idempotencyKeys by IdempotencyKeyEntity referrersOn IdempotencyKeyTable.projectId
    val throttleClaims by ThrottleClaimEntity referrersOn ThrottleClaimTable.projectId
    var createdAt by ProjectTable.createdAt
    var updatedAt by ProjectTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ProjectTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
