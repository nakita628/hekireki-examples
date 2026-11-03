package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CredentialEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CredentialEntity>(CredentialTable)

    var userId by CredentialTable.userId
    var provider by CredentialTable.provider
    var providerAccountId by CredentialTable.providerAccountId
    var type by CredentialTable.type
    var secret by CredentialTable.secret
    var scopes by CredentialTable.scopes
    var expiresAt by CredentialTable.expiresAt
    var createdAt by CredentialTable.createdAt
    var updatedAt by CredentialTable.updatedAt
    val calendarConnections by CalendarConnectionEntity referrersOn CalendarConnectionTable.credentialId
    val conferencingConnections by ConferencingConnectionEntity referrersOn ConferencingConnectionTable.credentialId
    var user by UserEntity referencedOn CredentialTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CredentialTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
