package langwatch.models

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
    var kind by OrganizationTable.kind
    var phoneNumber by OrganizationTable.phoneNumber
    var slug by OrganizationTable.slug
    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt
    var usageSpendingMaxLimit by OrganizationTable.usageSpendingMaxLimit
    var datasetAttachmentMaxMb by OrganizationTable.datasetAttachmentMaxMb
    var privateClickHouseLabel by OrganizationTable.privateClickHouseLabel
    var privateS3Label by OrganizationTable.privateS3Label
    var privateDataplaneSeenAt by OrganizationTable.privateDataplaneSeenAt
    var maxSessionDurationDays by OrganizationTable.maxSessionDurationDays
    var mfaRequired by OrganizationTable.mfaRequired
    var lockoutAfterFailedAttempts by OrganizationTable.lockoutAfterFailedAttempts
    var lockoutMinutes by OrganizationTable.lockoutMinutes
    var sessionIdleTimeoutMinutes by OrganizationTable.sessionIdleTimeoutMinutes
    var sessionMaxLifetimeMinutes by OrganizationTable.sessionMaxLifetimeMinutes
    var signupData by OrganizationTable.signupData
    var signedDPA by OrganizationTable.signedDPA
    var elasticsearchNodeUrl by OrganizationTable.elasticsearchNodeUrl
    var elasticsearchApiKey by OrganizationTable.elasticsearchApiKey
    var useCustomElasticsearch by OrganizationTable.useCustomElasticsearch
    var s3Endpoint by OrganizationTable.s3Endpoint
    var s3AccessKeyId by OrganizationTable.s3AccessKeyId
    var s3SecretAccessKey by OrganizationTable.s3SecretAccessKey
    var s3Bucket by OrganizationTable.s3Bucket
    var useCustomS3 by OrganizationTable.useCustomS3
    var sentPlanLimitAlert by OrganizationTable.sentPlanLimitAlert
    var ssoDomain by OrganizationTable.ssoDomain
    var ssoProvider by OrganizationTable.ssoProvider
    var domainJoin by OrganizationTable.domainJoin
    var joinerRole by OrganizationTable.joinerRole
    var joinDomains by OrganizationTable.joinDomains
    var presenceEnabled by OrganizationTable.presenceEnabled
    var traceSharingEnabled by OrganizationTable.traceSharingEnabled
    var supportContact by OrganizationTable.supportContact
    var primaryIntent by OrganizationTable.primaryIntent
    var promoCode by OrganizationTable.promoCode
    var stripeCustomerId by OrganizationTable.stripeCustomerId
    var currency by OrganizationTable.currency
    var pricingModel by OrganizationTable.pricingModel
    var license by OrganizationTable.license
    var licenseExpiresAt by OrganizationTable.licenseExpiresAt
    var licenseLastValidatedAt by OrganizationTable.licenseLastValidatedAt
    var selfHostedCustomer by OrganizationTable.selfHostedCustomer
    var connectServicesDisabled by OrganizationTable.connectServicesDisabled
    var instantEvalsEnabledAt by OrganizationTable.instantEvalsEnabledAt
    var instantEvalsEnabledByUserId by OrganizationTable.instantEvalsEnabledByUserId
    var connectLastSyncAt by OrganizationTable.connectLastSyncAt
    var connectLastSyncError by OrganizationTable.connectLastSyncError

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
