package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraScorerDefinitionVersionsTable : IdTable<String>("mastra_scorer_definition_versions") {
    override val id = text("id").entityId()
    val scorerDefinitionId = text("scorerDefinitionId")
    val versionNumber = integer("versionNumber")
    val name = text("name")
    val description = text("description").nullable()
    val type = text("type")
    val model = jsonb<String>("model", { it }, { it }).nullable()
    val instructions = text("instructions").nullable()
    val scoreRange = jsonb<String>("scoreRange", { it }, { it }).nullable()
    val presetConfig = jsonb<String>("presetConfig", { it }, { it }).nullable()
    val defaultSampling = jsonb<String>("defaultSampling", { it }, { it }).nullable()
    val changedFields = jsonb<String>("changedFields", { it }, { it }).nullable()
    val changeMessage = text("changeMessage").nullable()
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_scorer_definition_versions_pkey")

    init {
        uniqueIndex("idx_scorer_definition_versions_def_version", scorerDefinitionId, versionNumber)
    }
}
