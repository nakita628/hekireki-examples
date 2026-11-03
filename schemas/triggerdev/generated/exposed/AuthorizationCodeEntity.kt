package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AuthorizationCodeEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuthorizationCodeEntity>(AuthorizationCodeTable)

    var code by AuthorizationCodeTable.code
    var personalAccessToken by PersonalAccessTokenEntity optionalReferencedOn AuthorizationCodeTable.personalAccessTokenId
    var personalAccessTokenId by AuthorizationCodeTable.personalAccessTokenId
    var requesterIp by AuthorizationCodeTable.requesterIp
    var codeChallenge by AuthorizationCodeTable.codeChallenge
    var callbackPort by AuthorizationCodeTable.callbackPort
    var approvalCodeHash by AuthorizationCodeTable.approvalCodeHash
    var createdAt by AuthorizationCodeTable.createdAt
    var updatedAt by AuthorizationCodeTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AuthorizationCodeTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
