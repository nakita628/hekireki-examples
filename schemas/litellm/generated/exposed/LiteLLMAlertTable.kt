package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMAlertTable : IdTable<Long>("\"LiteLLM_Alert\"") {
    override val id = long("id").autoIncrement().entityId()
    val detectionKey = varchar("detection_key", 64).uniqueIndex("\"LiteLLM_Alert_detection_key_key\"")
    val type = varchar("type", 32)
    val resourceType = varchar("resource_type", 16)
    val resourceId = varchar("resource_id", 1024)
    val status = varchar("status", 16).default("open")
    val windowStart = pgTimestamp("window_start", 3)
    val windowEnd = pgTimestamp("window_end", 3)
    val evidence = jsonb<String>("evidence", { it }, { it })
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = varchar("updated_by", 1024).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_Alert_pkey\"")

    init {
        index("\"LiteLLM_Alert_status_id_idx\"", false, status, id)
    }
}
