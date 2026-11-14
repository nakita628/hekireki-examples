package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayRealtimeSessionReportTable : IdTable<String>("\"GatewayRealtimeSessionReport\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val sessionId = text("sessionId")
    val reportKey = text("reportKey")
    val projectId = text("projectId")
    val model = text("model")
    val usage = jsonb<String>("usage", { it }, { it })
    val costNanoUsd = long("costNanoUsd")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayRealtimeSessionReport_pkey\"")

    init {
        index("\"GatewayRealtimeSessionReport_projectId_idx\"", false, projectId)
        uniqueIndex("\"GatewayRealtimeSessionReport_sessionId_reportKey_key\"", sessionId, reportKey)
    }
}
