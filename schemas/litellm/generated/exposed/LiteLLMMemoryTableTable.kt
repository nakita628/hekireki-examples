package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object LiteLLMMemoryTableTable : IdTable<String>("\"LiteLLM_MemoryTable\"") {
    override val id = text("memory_id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val key = text("key").uniqueIndex("\"LiteLLM_MemoryTable_key_key\"")
    val value = text("value")
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val userId = text("user_id").nullable()
    val teamId = text("team_id").nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by").nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_MemoryTable_pkey\"")

    init {
        index("\"LiteLLM_MemoryTable_user_id_idx\"", false, userId)
        index("\"LiteLLM_MemoryTable_team_id_idx\"", false, teamId)
    }
}
