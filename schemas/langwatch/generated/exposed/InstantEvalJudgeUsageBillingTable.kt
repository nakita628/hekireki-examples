package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object InstantEvalJudgeUsageBillingTable : IdTable<String>("\"InstantEvalJudgeUsageBilling\"") {
    override val id = text("organizationId").entityId()
    val usageBilled = bool("usageBilled")
    val occurredAt = pgTimestamp("occurredAt", 3)
    val fromCatchUp = bool("fromCatchUp")
    override val primaryKey = PrimaryKey(id, name = "\"InstantEvalJudgeUsageBilling_pkey\"")
}
