package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyRequestErrorsTable : CompositeIdTable("\"LiteLLM_DailyRequestErrors\"") {
    val date = text("date").entityId()
    val apiKey = text("api_key").entityId()
    val teamId = text("team_id").default("").entityId()
    val userId = text("user_id").default("").entityId()
    val modelGroup = text("model_group").default("").entityId()
    val statusCode = integer("status_code").entityId()
    val failedRequests = long("failed_requests").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(date, apiKey, teamId, userId, modelGroup, statusCode, name = "\"LiteLLM_DailyRequestErrors_pkey\"")

    init {
        index("\"LiteLLM_DailyRequestErrors_date_idx\"", false, date)
    }
}
