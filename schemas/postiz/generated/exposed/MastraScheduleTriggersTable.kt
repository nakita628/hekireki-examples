package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object MastraScheduleTriggersTable : IdTable<String>("mastra_schedule_triggers") {
    override val id = text("id").entityId()
    val scheduleId = text("schedule_id")
    val runId = text("run_id").nullable()
    val scheduledFireAt = long("scheduled_fire_at")
    val actualFireAt = long("actual_fire_at")
    val outcome = text("outcome")
    val error = text("error").nullable()
    val triggerKind = text("trigger_kind")
    val parentTriggerId = text("parent_trigger_id").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "mastra_schedule_triggers_pkey")

    init {
        index(
            "idx_mastra_schedule_triggers_schedule_fire",
            false,
            functions = listOf(indexExpression("\"schedule_id\""), indexExpression("\"actual_fire_at\" DESC")),
        )
    }
}
