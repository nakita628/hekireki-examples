package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnomalyAlertTable : IdTable<String>("\"AnomalyAlert\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val ruleId = text("ruleId")
    val severity = text("severity")
    val ruleName = text("ruleName")
    val ruleType = text("ruleType")
    val triggerWindowStart = pgTimestamp("triggerWindowStart", 3)
    val triggerWindowEnd = pgTimestamp("triggerWindowEnd", 3)
    val triggerSpendUsd = decimal("triggerSpendUsd", 18, 6).nullable()
    val triggerEventCount = integer("triggerEventCount").nullable()
    val detail = jsonb<String>("detail", { it }, { it }).default("{}")
    val state = text("state").default("open")
    val destinationStatus = jsonb<String>("destinationStatus", { it }, { it }).default("{}")
    val detectedAt = pgTimestamp("detectedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val acknowledgedAt = pgTimestamp("acknowledgedAt", 3).nullable()
    val resolvedAt = pgTimestamp("resolvedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"AnomalyAlert_pkey\"")

    init {
        index("\"AnomalyAlert_organizationId_detectedAt_idx\"", false, organizationId, detectedAt)
        index("\"AnomalyAlert_ruleId_idx\"", false, ruleId)
        index("\"AnomalyAlert_state_idx\"", false, state)
        uniqueIndex("\"AnomalyAlert_ruleId_triggerWindowStart_key\"", ruleId, triggerWindowStart)
    }
}
