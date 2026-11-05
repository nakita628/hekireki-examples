package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CalendarConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CalendarConnectionEntity>(CalendarConnectionTable)

    var createdAt by CalendarConnectionTable.createdAt
    var updatedAt by CalendarConnectionTable.updatedAt
    var provider by CalendarConnectionTable.provider
    var email by CalendarConnectionTable.email
    var accessToken by CalendarConnectionTable.accessToken
    var refreshToken by CalendarConnectionTable.refreshToken
    var expiresAt by CalendarConnectionTable.expiresAt
    var isConnected by CalendarConnectionTable.isConnected
    var emailAccountId by CalendarConnectionTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn CalendarConnectionTable.emailAccountId
    val calendars by CalendarEntity referrersOn CalendarTable.connectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CalendarConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
