package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EventTypeTranslationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EventTypeTranslationEntity>(EventTypeTranslationTable)

    var eventType by EventTypeEntity referencedOn EventTypeTranslationTable.eventTypeId
    var eventTypeId by EventTypeTranslationTable.eventTypeId
    var field by EventTypeTranslationTable.field
    var sourceLocale by EventTypeTranslationTable.sourceLocale
    var targetLocale by EventTypeTranslationTable.targetLocale
    var translatedText by EventTypeTranslationTable.translatedText
    var createdAt by EventTypeTranslationTable.createdAt
    var createdBy by EventTypeTranslationTable.createdBy
    var updatedAt by EventTypeTranslationTable.updatedAt
    var updatedBy by EventTypeTranslationTable.updatedBy
    var creator by UserEntity referencedOn EventTypeTranslationTable.createdBy
    var updater by UserEntity optionalReferencedOn EventTypeTranslationTable.updatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EventTypeTranslationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
