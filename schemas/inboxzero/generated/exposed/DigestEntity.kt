package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DigestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DigestEntity>(DigestTable)

    var createdAt by DigestTable.createdAt
    var updatedAt by DigestTable.updatedAt
    var emailAccountId by DigestTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn DigestTable.emailAccountId
    val items by DigestItemEntity referrersOn DigestItemTable.digestId
    var sentAt by DigestTable.sentAt
    var status by DigestTable.status

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DigestTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
