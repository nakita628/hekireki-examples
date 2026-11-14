package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SelfHostedInstanceReportTable : IdTable<String>("\"SelfHostedInstanceReport\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val instanceId = text("instanceId")
    val receivedAt = pgTimestamp("receivedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val version = text("version").nullable()
    val reportSchemaVersion = integer("reportSchemaVersion").nullable()
    val unknownFields = integer("unknownFields").default(0)
    val payload = jsonb<String>("payload", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"SelfHostedInstanceReport_pkey\"")

    init {
        index("\"SelfHostedInstanceReport_instanceId_receivedAt_idx\"", false, instanceId, receivedAt)
        index("\"SelfHostedInstanceReport_receivedAt_idx\"", false, receivedAt)
    }
}
