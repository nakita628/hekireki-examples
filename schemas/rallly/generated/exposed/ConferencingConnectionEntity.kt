package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ConferencingConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConferencingConnectionEntity>(ConferencingConnectionTable)

    var userId by ConferencingConnectionTable.userId
    var provider by ConferencingConnectionTable.provider
    var integrationId by ConferencingConnectionTable.integrationId
    var providerAccountId by ConferencingConnectionTable.providerAccountId
    var email by ConferencingConnectionTable.email
    var displayName by ConferencingConnectionTable.displayName
    var createdAt by ConferencingConnectionTable.createdAt
    var updatedAt by ConferencingConnectionTable.updatedAt
    var credential by CredentialEntity referencedOn ConferencingConnectionTable.credentialId
    var credentialId by ConferencingConnectionTable.credentialId
    var user by UserEntity referencedOn ConferencingConnectionTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ConferencingConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
