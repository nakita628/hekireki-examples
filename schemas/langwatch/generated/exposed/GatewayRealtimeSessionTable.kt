package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayRealtimeSessionTable : IdTable<String>("\"GatewayRealtimeSession\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val organizationId = text("organizationId")
    val virtualKeyId = text("virtualKeyId")
    val modelProviderId = text("modelProviderId")
    val vendor = text("vendor")
    val agentId = text("agentId").nullable()
    val model = text("model")
    val requestedModel = text("requestedModel").nullable()
    val vendorConversationId = text("vendorConversationId").nullable()
    val status =
        pgEnum("status", "\"GatewayRealtimeSessionStatus\"", GatewayRealtimeSessionStatus.entries, GatewayRealtimeSessionStatus::dbName)
            .default(GatewayRealtimeSessionStatus.OPEN)
    val mintedAt = pgTimestamp("mintedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val closedAt = pgTimestamp("closedAt", 3).nullable()
    val closeReason = text("closeReason").nullable()
    val traceId = text("traceId").nullable()
    val vendorCostRaw = jsonb<String>("vendorCostRaw", { it }, { it }).nullable()
    val kind = text("kind").nullable()
    val metering = text("metering").nullable()
    val credentialExpiresAt = pgTimestamp("credentialExpiresAt", 3).nullable()
    val transcriptionModel = text("transcriptionModel").nullable()
    val endUserId = text("endUserId").nullable()
    val lastReportAt = pgTimestamp("lastReportAt", 3).nullable()
    val reportedCostNanoUsd = long("reportedCostNanoUsd").default(0L)
    val reportCount = integer("reportCount").default(0)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayRealtimeSession_pkey\"")

    init {
        index("\"GatewayRealtimeSession_virtualKeyId_status_mintedAt_idx\"", false, virtualKeyId, status, mintedAt)
        index("\"GatewayRealtimeSession_projectId_status_mintedAt_idx\"", false, projectId, status, mintedAt)
        index("\"GatewayRealtimeSession_status_metering_mintedAt_idx\"", false, status, metering, mintedAt)
        uniqueIndex("\"GatewayRealtimeSession_vendor_vendorConversationId_key\"", vendor, vendorConversationId)
    }
}
