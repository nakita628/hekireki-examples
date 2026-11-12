package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AiProvidersEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AiProvidersEntity>(AiProvidersTable)

    var title by AiProvidersTable.title
    var provider by AiProvidersTable.provider
    var baseURL by AiProvidersTable.baseURL
    var apiKey by AiProvidersTable.apiKey
    var config by AiProvidersTable.config
    var sortOrder by AiProvidersTable.sortOrder
    var createdAt by AiProvidersTable.createdAt
    var updatedAt by AiProvidersTable.updatedAt
    val models by AiModelsEntity referrersOn AiModelsTable.providerId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AiProvidersTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
