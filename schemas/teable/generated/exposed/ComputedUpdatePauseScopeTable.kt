package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ComputedUpdatePauseScopeTable : IdTable<String>("computed_update_pause_scope") {
    override val id = text("id").entityId()
    val scopeType = text("scope_type")
    val scopeId = text("scope_id")
    val pausedAt = pgTimestamp("paused_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val pausedBy = text("paused_by").nullable()
    val resumeAt = pgTimestamp("resume_at", 3).nullable()
    val reason = text("reason").nullable()
    val writePolicy = text("write_policy").default("allow_bounded")
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedBy = text("updated_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "computed_update_pause_scope_pkey")

    init {
        index("computed_update_pause_scope_resume_at_idx", false, resumeAt)
        uniqueIndex("computed_update_pause_scope_scope_type_scope_id_key", scopeType, scopeId)
    }
}
