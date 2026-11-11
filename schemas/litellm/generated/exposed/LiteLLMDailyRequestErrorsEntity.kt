package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyRequestErrorsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyRequestErrorsEntity>(LiteLLMDailyRequestErrorsTable)

    val date by LiteLLMDailyRequestErrorsTable.date
    val apiKey by LiteLLMDailyRequestErrorsTable.apiKey
    val teamId by LiteLLMDailyRequestErrorsTable.teamId
    val userId by LiteLLMDailyRequestErrorsTable.userId
    val modelGroup by LiteLLMDailyRequestErrorsTable.modelGroup
    val statusCode by LiteLLMDailyRequestErrorsTable.statusCode
    var failedRequests by LiteLLMDailyRequestErrorsTable.failedRequests
    var createdAt by LiteLLMDailyRequestErrorsTable.createdAt
    var updatedAt by LiteLLMDailyRequestErrorsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyRequestErrorsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
