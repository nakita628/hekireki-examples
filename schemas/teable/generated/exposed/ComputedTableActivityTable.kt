package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ComputedTableActivityTable : IdTable<String>("computed_table_activity") {
    override val id = text("table_id").entityId()
    val baseId = text("base_id")
    val status = text("status")
    val calculatingFieldCount = integer("calculating_field_count").default(0)
    val queuedFieldCount = integer("queued_field_count").default(0)
    val estimatedComplexity = long("estimated_complexity").default(0L)
    val recentCompletions = jsonb<String>("recent_completions", { it }, { it }).default("[]")
    val generation = long("generation").default(0L)
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "computed_table_activity_pkey")

    init {
        index("computed_table_activity_base_id_status_idx", false, baseId, status)
    }
}
