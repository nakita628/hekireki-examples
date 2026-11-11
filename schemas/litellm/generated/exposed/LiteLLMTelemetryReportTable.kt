package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMTelemetryReportTable : IdTable<String>("\"LiteLLM_TelemetryReport\"") {
    override val id = text("id").entityId()
    val windowStart = double("window_start")
    val windowEnd = double("window_end")
    val report = jsonb<String>("report", { it }, { it })
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"LiteLLM_TelemetryReport_pkey\"")

    init {
        index("\"LiteLLM_TelemetryReport_window_end_idx\"", false, windowEnd)
        index("\"LiteLLM_TelemetryReport_created_at_idx\"", false, createdAt)
    }
}
