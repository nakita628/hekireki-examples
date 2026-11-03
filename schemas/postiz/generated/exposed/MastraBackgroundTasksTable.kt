package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraBackgroundTasksTable : IdTable<String>("mastra_background_tasks") {
    override val id = text("id").entityId()
    val toolCallId = text("tool_call_id")
    val toolName = text("tool_name")
    val agentId = text("agent_id")
    val runId = text("run_id")
    val threadId = text("thread_id").nullable()
    val resourceId = text("resource_id").nullable()
    val status = text("status")
    val args = jsonb<String>("args", { it }, { it })
    val result = jsonb<String>("result", { it }, { it }).nullable()
    val error = jsonb<String>("error", { it }, { it }).nullable()
    val suspendPayload = jsonb<String>("suspend_payload", { it }, { it }).nullable()
    val retryCount = integer("retry_count")
    val maxRetries = integer("max_retries")
    val timeoutMs = integer("timeout_ms")
    val createdAt = pgTimestamp("createdAt", 6)
    val startedAt = pgTimestamp("startedAt", 6).nullable()
    val suspendedAt = pgTimestamp("suspendedAt", 6).nullable()
    val completedAt = pgTimestamp("completedAt", 6).nullable()
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val startedAtZ =
        pgTimestamptz("startedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val suspendedAtZ =
        pgTimestamptz("suspendedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAtZ =
        pgTimestamptz("completedAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_background_tasks_pkey")

    init {
        index("mastra_bg_tasks_agent_status_idx", false, agentId, status)
        index("mastra_bg_tasks_status_created_at_idx", false, status, createdAt)
        index("mastra_bg_tasks_thread_idx", false, threadId, createdAt)
        index("mastra_bg_tasks_tool_call_idx", false, toolCallId)
    }
}
