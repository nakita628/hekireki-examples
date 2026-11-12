package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AiModelsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AiModelsEntity>(AiModelsTable)

    var providerId by AiModelsTable.providerId
    var title by AiModelsTable.title
    var modelKey by AiModelsTable.modelKey
    var capabilities by AiModelsTable.capabilities
    var config by AiModelsTable.config
    var sortOrder by AiModelsTable.sortOrder
    var createdAt by AiModelsTable.createdAt
    var updatedAt by AiModelsTable.updatedAt
    var provider by AiProvidersEntity referencedOn AiModelsTable.providerId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AiModelsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
