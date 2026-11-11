package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LiteLLMDailyGuardrailUsageUnitsTable : CompositeIdTable("\"LiteLLM_DailyGuardrailUsageUnits\"") {
    val guardrailId = text("guardrail_id").entityId()
    val date = text("date").entityId()
    val teamId = text("team_id").entityId()
    val apiKey = text("api_key").entityId()
    val usageUnit = text("usage_unit").entityId()
    val units = long("units").default(0L)
    val cost = double("cost").nullable()
    val untrackedUnits = long("untracked_units").default(0L)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(guardrailId, date, teamId, apiKey, usageUnit, name = "\"LiteLLM_DailyGuardrailUsageUnits_pkey\"")

    init {
        index("\"LiteLLM_DailyGuardrailUsageUnits_date_idx\"", false, date)
    }
}
