package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AlertTable : IdTable<String>("\"Alert\"") {
    override val id = text("id").entityId()
    val organizationId = text("organizationId")
    val projectId = text("projectId")
    val environmentId = text("environmentId")
    val capabilityKey = text("capabilityKey")
    val params = jsonb<String>("params", { it }, { it })
    val condition = jsonb<String>("condition", { it }, { it })
    val query = text("query").nullable()
    val expiresAt = pgTimestamptz("expiresAt", 6).nullable()
    val lifecycle = text("lifecycle").default("active")
    val attention = text("attention").default("clear")
    val observedCondition = text("observedCondition").default("unknown")
    val observedConditionChangedAt = pgTimestamptz("observedConditionChangedAt", 6).nullable()
    val pendingMetSince = pgTimestamptz("pendingMetSince", 6).nullable()
    val evaluatorState = jsonb<String>("evaluatorState", { it }, { it }).nullable()
    val evaluationLeaseId = text("evaluationLeaseId").nullable()
    val evaluationLeaseUntil = pgTimestamptz("evaluationLeaseUntil", 6).nullable()
    val lastEvaluatedAt = pgTimestamptz("lastEvaluatedAt", 6).nullable()
    val nextEvaluationAt = pgTimestamptz("nextEvaluationAt", 6).nullable()
    val notifiers = jsonb<String>("notifiers", { it }, { it }).default("[]")
    val name = text("name").nullable()
    val createdBy = text("createdBy")
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val resolvedAt = pgTimestamptz("resolvedAt", 6).nullable()
    val resolvedBy = text("resolvedBy").nullable()
    val stoppedAt = pgTimestamptz("stoppedAt", 6).nullable()
    val stoppedBy = text("stoppedBy").nullable()
    val lastOccurrenceAt = pgTimestamptz("lastOccurrenceAt", 6).nullable()
    val occurrencesSinceRaise = integer("occurrencesSinceRaise").default(0)
    val occurrencesTotal = long("occurrencesTotal").default(0L)
    override val primaryKey = PrimaryKey(id, name = "\"Alert_pkey\"")

    init {
        index("\"Alert_environmentId_lifecycle_idx\"", false, environmentId, lifecycle)
        index("\"Alert_lifecycle_evaluationLeaseUntil_idx\"", false, lifecycle, evaluationLeaseUntil)
        index("\"Alert_projectId_idx\"", false, projectId)
        index("\"Alert_organizationId_idx\"", false, organizationId)
    }
}
