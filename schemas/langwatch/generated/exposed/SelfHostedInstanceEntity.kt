package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SelfHostedInstanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SelfHostedInstanceEntity>(SelfHostedInstanceTable)

    var instanceId by SelfHostedInstanceTable.instanceId
    var firstSeenAt by SelfHostedInstanceTable.firstSeenAt
    var lastSeenAt by SelfHostedInstanceTable.lastSeenAt
    var version by SelfHostedInstanceTable.version
    var installMethod by SelfHostedInstanceTable.installMethod
    var chartVersion by SelfHostedInstanceTable.chartVersion
    var hostname by SelfHostedInstanceTable.hostname
    var environment by SelfHostedInstanceTable.environment
    var installedAt by SelfHostedInstanceTable.installedAt
    var reportSchemaVersion by SelfHostedInstanceTable.reportSchemaVersion
    var organizationId by SelfHostedInstanceTable.organizationId
    var issuedLicenseId by SelfHostedInstanceTable.issuedLicenseId
    var userEmailDomains by SelfHostedInstanceTable.userEmailDomains
    var userDomains by SelfHostedInstanceTable.userDomains
    var latestReport by SelfHostedInstanceTable.latestReport
    var optionalMetricsReported by SelfHostedInstanceTable.optionalMetricsReported
    var hostnameReported by SelfHostedInstanceTable.hostnameReported
    var reportCount by SelfHostedInstanceTable.reportCount
    var lastUnknownFields by SelfHostedInstanceTable.lastUnknownFields
    var raisedSignals by SelfHostedInstanceTable.raisedSignals
    var createdAt by SelfHostedInstanceTable.createdAt
    var updatedAt by SelfHostedInstanceTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SelfHostedInstanceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
