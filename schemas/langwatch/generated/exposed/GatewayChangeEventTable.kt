package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayChangeEventTable : IdTable<Long>("\"GatewayChangeEvent\"") {
    override val id = long("revision").autoIncrement().entityId()
    val organizationId = text("organizationId")
    val kind = pgEnum("kind", "\"GatewayChangeEventKind\"", GatewayChangeEventKind.entries, GatewayChangeEventKind::dbName)
    val virtualKeyId = text("virtualKeyId").nullable()
    val budgetId = text("budgetId").nullable()
    val modelProviderId = text("modelProviderId").nullable()
    val projectId = text("projectId").nullable()
    val payload = jsonb<String>("payload", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"GatewayChangeEvent_pkey\"")

    init {
        index("\"GatewayChangeEvent_organizationId_revision_idx\"", false, organizationId, id)
        index("\"GatewayChangeEvent_projectId_revision_idx\"", false, projectId, id)
        index("\"GatewayChangeEvent_virtualKeyId_idx\"", false, virtualKeyId)
        index("\"GatewayChangeEvent_budgetId_idx\"", false, budgetId)
    }
}
