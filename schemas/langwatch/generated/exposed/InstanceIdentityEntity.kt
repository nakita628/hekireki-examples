package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InstanceIdentityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InstanceIdentityEntity>(InstanceIdentityTable)

    var instanceId by InstanceIdentityTable.instanceId
    var createdAt by InstanceIdentityTable.createdAt
    var lastReportAt by InstanceIdentityTable.lastReportAt
    var lastReportError by InstanceIdentityTable.lastReportError
    var optionalMetricsOptOut by InstanceIdentityTable.optionalMetricsOptOut
    var hostnameOptOut by InstanceIdentityTable.hostnameOptOut
    var upgradeDiagnosticsOptOut by InstanceIdentityTable.upgradeDiagnosticsOptOut
}
