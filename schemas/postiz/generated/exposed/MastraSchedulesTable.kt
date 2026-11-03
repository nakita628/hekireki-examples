package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object MastraSchedulesTable : IdTable<String>("mastra_schedules") {
    override val id = text("id").entityId()
    val target = jsonb<String>("target", { it }, { it })
    val cron = text("cron")
    val timezone = text("timezone").nullable()
    val status = text("status")
    val nextFireAt = long("next_fire_at")
    val lastFireAt = long("last_fire_at").nullable()
    val lastRunId = text("last_run_id").nullable()
    val createdAt = long("created_at")
    val updatedAt = long("updated_at")
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val ownerType = text("owner_type").nullable()
    val ownerId = text("owner_id").nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_schedules_pkey")

    init {
        index("idx_mastra_schedules_status_next_fire", false, status, nextFireAt)
    }
}
