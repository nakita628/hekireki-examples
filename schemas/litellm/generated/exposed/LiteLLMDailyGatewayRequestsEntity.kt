package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMDailyGatewayRequestsEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<LiteLLMDailyGatewayRequestsEntity>(LiteLLMDailyGatewayRequestsTable)

    val date by LiteLLMDailyGatewayRequestsTable.date
    val category by LiteLLMDailyGatewayRequestsTable.category
    val route by LiteLLMDailyGatewayRequestsTable.route
    var successfulRequests by LiteLLMDailyGatewayRequestsTable.successfulRequests
    var failedRequests by LiteLLMDailyGatewayRequestsTable.failedRequests
    var createdAt by LiteLLMDailyGatewayRequestsTable.createdAt
    var updatedAt by LiteLLMDailyGatewayRequestsTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMDailyGatewayRequestsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
