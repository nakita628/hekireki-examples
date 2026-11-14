package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class GatewayRealtimeSessionReportEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayRealtimeSessionReportEntity>(GatewayRealtimeSessionReportTable)

    var sessionId by GatewayRealtimeSessionReportTable.sessionId
    var reportKey by GatewayRealtimeSessionReportTable.reportKey
    var projectId by GatewayRealtimeSessionReportTable.projectId
    var model by GatewayRealtimeSessionReportTable.model
    var usage by GatewayRealtimeSessionReportTable.usage
    var costNanoUsd by GatewayRealtimeSessionReportTable.costNanoUsd
    var createdAt by GatewayRealtimeSessionReportTable.createdAt
}
