package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMPerformanceAlertStateTable : IdTable<Int>("\"LiteLLM_PerformanceAlertState\"") {
    val enabled = bool("enabled").default(true)
    val alertsUsed = integer("alerts_used").nullable()
    override val id = integer("id").entityId()
    val owner = varchar("owner", 32)
    val nextRunAt = pgTimestamptz("next_run_at", 3)
    val day = varchar("day", 10).nullable()
    val scope = varchar("scope", 16).nullable()
    val resumeAfter = varchar("resume_after", 1024).default("")
    val wrapped = bool("wrapped").default(false)
    val checked = integer("checked").default(0)
    val skipped = integer("skipped").default(0)
    val startScope = varchar("start_scope", 16).nullable()
    val startAfter = varchar("start_after", 1024).default("")
    val completedAt = pgTimestamp("completed_at", 3).nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_PerformanceAlertState_pkey\"")
}
