package langfuse.models

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
    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt
    var cloudConfig by OrganizationTable.cloudConfig
    var metadata by OrganizationTable.metadata
    var cloudBillingCycleAnchor by OrganizationTable.cloudBillingCycleAnchor
    var cloudBillingCycleUpdatedAt by OrganizationTable.cloudBillingCycleUpdatedAt
    var cloudCurrentCycleUsage by OrganizationTable.cloudCurrentCycleUsage
    var cloudFreeTierUsageThresholdState by OrganizationTable.cloudFreeTierUsageThresholdState
    var aiFeaturesEnabled by OrganizationTable.aiFeaturesEnabled
    var aiTelemetryEnabled by OrganizationTable.aiTelemetryEnabled
    var accessAuditLogEnabled by OrganizationTable.accessAuditLogEnabled
    var featureFlagOrgDefaults by OrganizationTable.featureFlagOrgDefaults
    var sfdcOrgId by OrganizationTable.sfdcOrgId
    val organizationMemberships by OrganizationMembershipEntity referrersOn OrganizationMembershipTable.orgId
    val projects by ProjectEntity referrersOn ProjectTable.orgId
    val membershipInvitation by MembershipInvitationEntity referrersOn MembershipInvitationTable.orgId
    val apiKey by ApiKeyEntity optionalReferrersOn ApiKeyTable.orgId
    val surveys by SurveyEntity optionalReferrersOn SurveyTable.orgId
    val cloudSpendAlerts by CloudSpendAlertEntity referrersOn CloudSpendAlertTable.orgId
    val verifiedDomains by VerifiedDomainEntity referrersOn VerifiedDomainTable.organizationId
    val gatewayConfig by GatewayConfigEntity optionalBackReferencedOn GatewayConfigTable.id
    val gatewayAiConnections by GatewayAiConnectionEntity referrersOn GatewayAiConnectionTable.organizationId
    val organizationLlmApiKeys by LlmApiKeysEntity optionalReferrersOn LlmApiKeysTable.organizationId
    val roleAssignments by RoleAssignmentEntity referrersOn RoleAssignmentTable.orgId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
