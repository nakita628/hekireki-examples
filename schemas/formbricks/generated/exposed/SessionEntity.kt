package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SessionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SessionEntity>(SessionTable)

    var createdAt by SessionTable.createdAt
    var updatedAt by SessionTable.updatedAt
    var sessionToken by SessionTable.sessionToken
    var user by UserEntity referencedOn SessionTable.userId
    var userId by SessionTable.userId
    var expires by SessionTable.expires
    var ipAddress by SessionTable.ipAddress
    var userAgent by SessionTable.userAgent
    val oauthAccessTokens by OauthAccessTokenEntity optionalReferrersOn OauthAccessTokenTable.sessionId
    val oauthRefreshTokens by OauthRefreshTokenEntity optionalReferrersOn OauthRefreshTokenTable.sessionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SessionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
