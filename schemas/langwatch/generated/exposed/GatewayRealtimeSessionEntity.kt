package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayRealtimeSessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayRealtimeSessionEntity>(GatewayRealtimeSessionTable)

    var projectId by GatewayRealtimeSessionTable.projectId
    var organizationId by GatewayRealtimeSessionTable.organizationId
    var virtualKeyId by GatewayRealtimeSessionTable.virtualKeyId
    var modelProviderId by GatewayRealtimeSessionTable.modelProviderId
    var vendor by GatewayRealtimeSessionTable.vendor
    var agentId by GatewayRealtimeSessionTable.agentId
    var model by GatewayRealtimeSessionTable.model
    var requestedModel by GatewayRealtimeSessionTable.requestedModel
    var vendorConversationId by GatewayRealtimeSessionTable.vendorConversationId
    var status by GatewayRealtimeSessionTable.status
    var mintedAt by GatewayRealtimeSessionTable.mintedAt
    var closedAt by GatewayRealtimeSessionTable.closedAt
    var closeReason by GatewayRealtimeSessionTable.closeReason
    var traceId by GatewayRealtimeSessionTable.traceId
    var vendorCostRaw by GatewayRealtimeSessionTable.vendorCostRaw
    var kind by GatewayRealtimeSessionTable.kind
    var metering by GatewayRealtimeSessionTable.metering
    var credentialExpiresAt by GatewayRealtimeSessionTable.credentialExpiresAt
    var transcriptionModel by GatewayRealtimeSessionTable.transcriptionModel
    var endUserId by GatewayRealtimeSessionTable.endUserId
    var lastReportAt by GatewayRealtimeSessionTable.lastReportAt
    var reportedCostNanoUsd by GatewayRealtimeSessionTable.reportedCostNanoUsd
    var reportCount by GatewayRealtimeSessionTable.reportCount
    var createdAt by GatewayRealtimeSessionTable.createdAt
    var updatedAt by GatewayRealtimeSessionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayRealtimeSessionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
