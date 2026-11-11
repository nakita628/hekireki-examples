package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyGuardrailUsageUnitsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyGuardrailUsageUnitsEntity>(LiteLLMDailyGuardrailUsageUnitsTable)

    val guardrailId by LiteLLMDailyGuardrailUsageUnitsTable.guardrailId
    val date by LiteLLMDailyGuardrailUsageUnitsTable.date
    val teamId by LiteLLMDailyGuardrailUsageUnitsTable.teamId
    val apiKey by LiteLLMDailyGuardrailUsageUnitsTable.apiKey
    val usageUnit by LiteLLMDailyGuardrailUsageUnitsTable.usageUnit
    var units by LiteLLMDailyGuardrailUsageUnitsTable.units
    var cost by LiteLLMDailyGuardrailUsageUnitsTable.cost
    var untrackedUnits by LiteLLMDailyGuardrailUsageUnitsTable.untrackedUnits
    var createdAt by LiteLLMDailyGuardrailUsageUnitsTable.createdAt
    var updatedAt by LiteLLMDailyGuardrailUsageUnitsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyGuardrailUsageUnitsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
