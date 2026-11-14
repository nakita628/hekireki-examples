package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SelfHostedInstanceReportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SelfHostedInstanceReportEntity>(SelfHostedInstanceReportTable)

    var instanceId by SelfHostedInstanceReportTable.instanceId
    var receivedAt by SelfHostedInstanceReportTable.receivedAt
    var version by SelfHostedInstanceReportTable.version
    var reportSchemaVersion by SelfHostedInstanceReportTable.reportSchemaVersion
    var unknownFields by SelfHostedInstanceReportTable.unknownFields
    var payload by SelfHostedInstanceReportTable.payload
}
