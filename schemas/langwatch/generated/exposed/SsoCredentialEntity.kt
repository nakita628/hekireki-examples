package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SsoCredentialEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoCredentialEntity>(SsoCredentialTable)

    var organizationId by SsoCredentialTable.organizationId
    var connectionId by SsoCredentialTable.connectionId
    var kind by SsoCredentialTable.kind
    var ciphertext by SsoCredentialTable.ciphertext
    var createdAt by SsoCredentialTable.createdAt
    var updatedAt by SsoCredentialTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SsoCredentialTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
