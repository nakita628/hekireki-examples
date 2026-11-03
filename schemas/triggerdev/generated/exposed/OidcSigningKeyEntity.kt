package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OidcSigningKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OidcSigningKeyEntity>(OidcSigningKeyTable)

    var kid by OidcSigningKeyTable.kid
    var purpose by OidcSigningKeyTable.purpose
    var status by OidcSigningKeyTable.status
    var publicJwk by OidcSigningKeyTable.publicJwk
    var readyAt by OidcSigningKeyTable.readyAt
    var publicUntil by OidcSigningKeyTable.publicUntil
    var generatedByUserId by OidcSigningKeyTable.generatedByUserId
    var activatedAt by OidcSigningKeyTable.activatedAt
    var activatedByUserId by OidcSigningKeyTable.activatedByUserId
    var retiredAt by OidcSigningKeyTable.retiredAt
    var retiredByUserId by OidcSigningKeyTable.retiredByUserId
    var createdAt by OidcSigningKeyTable.createdAt
    var updatedAt by OidcSigningKeyTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OidcSigningKeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
