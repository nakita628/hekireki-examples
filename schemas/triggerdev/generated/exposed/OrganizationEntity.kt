package triggerdev.models

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

    var slug by OrganizationTable.slug
    var title by OrganizationTable.title
    var maximumExecutionTimePerRunInMs by OrganizationTable.maximumExecutionTimePerRunInMs
    var maximumConcurrencyLimit by OrganizationTable.maximumConcurrencyLimit
    var maximumSchedulesLimit by OrganizationTable.maximumSchedulesLimit
    var maximumDevQueueSize by OrganizationTable.maximumDevQueueSize
    var maximumDeployedQueueSize by OrganizationTable.maximumDeployedQueueSize
    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt
    var deletedAt by OrganizationTable.deletedAt
    var companySize by OrganizationTable.companySize
    var onboardingData by OrganizationTable.onboardingData
    var avatar by OrganizationTable.avatar
    var runsEnabled by OrganizationTable.runsEnabled
    var isActivated by OrganizationTable.isActivated
    var v2Enabled by OrganizationTable.v2Enabled
    var v2MarqsEnabled by OrganizationTable.v2MarqsEnabled
    var hasRequestedV3 by OrganizationTable.hasRequestedV3
    val environments by RuntimeEnvironmentEntity referrersOn RuntimeEnvironmentTable.organizationId
    var apiRateLimiterConfig by OrganizationTable.apiRateLimiterConfig
    var realtimeRateLimiterConfig by OrganizationTable.realtimeRateLimiterConfig
    var batchRateLimitConfig by OrganizationTable.batchRateLimitConfig
    var batchQueueConcurrencyConfig by OrganizationTable.batchQueueConcurrencyConfig
    var webhookLimitsConfig by OrganizationTable.webhookLimitsConfig
    var featureFlags by OrganizationTable.featureFlags
    var maximumProjectCount by OrganizationTable.maximumProjectCount
    var maxSessionDuration by OrganizationTable.maxSessionDuration
    val projects by ProjectEntity referrersOn ProjectTable.organizationId
    val members by OrgMemberEntity referrersOn OrgMemberTable.organizationId
    val invites by OrgMemberInviteEntity referrersOn OrgMemberInviteTable.organizationId
    val organizationIntegrations by OrganizationIntegrationEntity referrersOn OrganizationIntegrationTable.organizationId
    val organizationAccessTokens by OrganizationAccessTokenEntity referrersOn OrganizationAccessTokenTable.organizationId
    val workerGroups by WorkerInstanceGroupEntity optionalReferrersOn WorkerInstanceGroupTable.organizationId
    val workerInstances by WorkerInstanceEntity optionalReferrersOn WorkerInstanceTable.organizationId
    val githubAppInstallations by GithubAppInstallationEntity referrersOn GithubAppInstallationTable.organizationId
    val customerQueries by CustomerQueryEntity referrersOn CustomerQueryTable.organizationId
    val metricsDashboards by MetricsDashboardEntity referrersOn MetricsDashboardTable.organizationId
    val prompts by PromptEntity referrersOn PromptTable.organizationId
    val platformNotifications by PlatformNotificationEntity optionalReferrersOn PlatformNotificationTable.organizationId
    val errorGroupStates by ErrorGroupStateEntity referrersOn ErrorGroupStateTable.organizationId
    var streamBasinName by OrganizationTable.streamBasinName
    var supportAccessMode by OrganizationTable.supportAccessMode
    var supportAccessModeUpdatedAt by OrganizationTable.supportAccessModeUpdatedAt
    var supportAccessModeUpdatedById by OrganizationTable.supportAccessModeUpdatedById
    val supportAccessRequests by SupportAccessRequestEntity referrersOn SupportAccessRequestTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
