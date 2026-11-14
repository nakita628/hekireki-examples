package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InstantEvalJudgeUsageBillingEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InstantEvalJudgeUsageBillingEntity>(InstantEvalJudgeUsageBillingTable)

    var usageBilled by InstantEvalJudgeUsageBillingTable.usageBilled
    var occurredAt by InstantEvalJudgeUsageBillingTable.occurredAt
    var fromCatchUp by InstantEvalJudgeUsageBillingTable.fromCatchUp
}
