package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayGuardrailTable : IdTable<String>("\"GatewayGuardrail\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val name = text("name")
    val description = text("description").nullable()
    val evaluatorId = text("evaluatorId")
    val direction =
        pgEnum("direction", "\"GatewayGuardrailDirection\"", GatewayGuardrailDirection.entries, GatewayGuardrailDirection::dbName)
    val failureMode =
        pgEnum("failureMode", "\"GatewayGuardrailFailureMode\"", GatewayGuardrailFailureMode.entries, GatewayGuardrailFailureMode::dbName)
            .default(GatewayGuardrailFailureMode.FAIL_CLOSED)
    val archivedAt = pgTimestamp("archivedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"GatewayGuardrail_pkey\"")

    init {
        index("\"GatewayGuardrail_projectId_idx\"", false, projectId)
        index("\"GatewayGuardrail_evaluatorId_idx\"", false, evaluatorId)
        index("\"GatewayGuardrail_projectId_archivedAt_idx\"", false, projectId, archivedAt)
    }
}
