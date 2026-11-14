package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object InstantEvalJudgeSpendTable : CompositeIdTable("\"InstantEvalJudgeSpend\"") {
    val organizationId = text("organizationId").entityId()
    val requestId = text("requestId").entityId()
    val spendNanoUsd = long("spendNanoUsd")
    val occurredAt = pgTimestamp("occurredAt", 3)
    override val primaryKey = PrimaryKey(organizationId, requestId, name = "\"InstantEvalJudgeSpend_pkey\"")
}
