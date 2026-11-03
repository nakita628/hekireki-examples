package formbricks.models

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

    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt
    var name by OrganizationTable.name
    val memberships by MembershipEntity referrersOn MembershipTable.organizationId
    val workspaces by WorkspaceEntity referrersOn WorkspaceTable.organizationId
    val billing by OrganizationBillingEntity optionalBackReferencedOn OrganizationBillingTable.id
    var whitelabel by OrganizationTable.whitelabel
    val invites by InviteEntity referrersOn InviteTable.organizationId
    var isAISmartToolsEnabled by OrganizationTable.isAISmartToolsEnabled
    var displayTimeZone by OrganizationTable.displayTimeZone
    val teams by TeamEntity referrersOn TeamTable.organizationId
    val apiKeys by ApiKeyEntity referrersOn ApiKeyTable.organizationId
    val feedbackDirectories by FeedbackDirectoryEntity referrersOn FeedbackDirectoryTable.organizationId
    val retentionPolicies by RetentionPolicyEntity referrersOn RetentionPolicyTable.organizationId
    val retentionExemptions by RetentionExemptionEntity referrersOn RetentionExemptionTable.organizationId
    val retentionRuns by RetentionRunEntity referrersOn RetentionRunTable.organizationId
    val retentionNotices by RetentionNoticeEntity referrersOn RetentionNoticeTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
