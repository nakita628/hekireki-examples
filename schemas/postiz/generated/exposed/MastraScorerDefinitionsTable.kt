package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraScorerDefinitionsTable : IdTable<String>("mastra_scorer_definitions") {
    override val id = text("id").entityId()
    val status = text("status")
    val activeVersionId = text("activeVersionId").nullable()
    val authorId = text("authorId").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val updatedAt = pgTimestamp("updatedAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAtZ =
        pgTimestamptz("updatedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val organizationId = text("organizationId").nullable()
    val projectId = text("projectId").nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_scorer_definitions_pkey")
}
