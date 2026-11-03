package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraWorkflowDefinitionsTable : IdTable<String>("mastra_workflow_definitions") {
    override val id = text("id").entityId()
    val description = text("description").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val inputSchema = jsonb<String>("inputSchema", { it }, { it })
    val outputSchema = jsonb<String>("outputSchema", { it }, { it })
    val stateSchema = jsonb<String>("stateSchema", { it }, { it }).nullable()
    val requestContextSchema = jsonb<String>("requestContextSchema", { it }, { it }).nullable()
    val graph = jsonb<String>("graph", { it }, { it })
    val schedule = jsonb<String>("schedule", { it }, { it }).nullable()
    val status = text("status")
    val source1 = text("source")
    val authorId = text("authorId").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_workflow_definitions_pkey")

    init {
        index("idx_workflow_definitions_status", false, status)
    }
}
