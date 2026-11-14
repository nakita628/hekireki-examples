package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TriggerLatestEvaluationTable : IdTable<String>("\"TriggerLatestEvaluation\"") {
    override val id = text("triggerId").entityId()
    val projectId = text("projectId")
    val evaluatedAt = pgTimestamptz("evaluatedAt", 3)
    val verdict = text("verdict")
    val observedValue = double("observedValue").nullable()
    val threshold = double("threshold").nullable()
    val operator = text("operator").nullable()
    val timePeriodMinutes = integer("timePeriodMinutes").nullable()
    val skipCode = text("skipCode").nullable()
    val updatedAt = pgTimestamptz("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TriggerLatestEvaluation_pkey\"")

    init {
        index("\"TriggerLatestEvaluation_projectId_idx\"", false, projectId)
    }
}
