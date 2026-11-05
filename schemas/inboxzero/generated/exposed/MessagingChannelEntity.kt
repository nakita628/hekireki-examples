package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MessagingChannelEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MessagingChannelEntity>(MessagingChannelTable)

    var createdAt by MessagingChannelTable.createdAt
    var updatedAt by MessagingChannelTable.updatedAt
    var provider by MessagingChannelTable.provider
    var isConnected by MessagingChannelTable.isConnected
    var teamId by MessagingChannelTable.teamId
    var teamName by MessagingChannelTable.teamName
    var providerUserId by MessagingChannelTable.providerUserId
    var botUserId by MessagingChannelTable.botUserId
    var accessToken by MessagingChannelTable.accessToken
    var refreshToken by MessagingChannelTable.refreshToken
    var expiresAt by MessagingChannelTable.expiresAt
    var emailAccountId by MessagingChannelTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn MessagingChannelTable.emailAccountId
    val routes by MessagingRouteEntity referrersOn MessagingRouteTable.messagingChannelId
    val executedActions by ExecutedActionEntity optionalReferrersOn ExecutedActionTable.messagingChannelId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MessagingChannelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
