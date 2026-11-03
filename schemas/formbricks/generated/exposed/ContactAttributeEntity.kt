package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ContactAttributeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ContactAttributeEntity>(ContactAttributeTable)

    var createdAt by ContactAttributeTable.createdAt
    var updatedAt by ContactAttributeTable.updatedAt
    var attributeKey by ContactAttributeKeyEntity referencedOn ContactAttributeTable.attributeKeyId
    var attributeKeyId by ContactAttributeTable.attributeKeyId
    var contact by ContactEntity referencedOn ContactAttributeTable.contactId
    var contactId by ContactAttributeTable.contactId
    var value by ContactAttributeTable.value
    var valueNumber by ContactAttributeTable.valueNumber
    var valueDate by ContactAttributeTable.valueDate

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ContactAttributeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
