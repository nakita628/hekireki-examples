package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class MessagesGroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MessagesGroupEntity>(MessagesGroupTable)

    var buyerOrganizationId by MessagesGroupTable.buyerOrganizationId
    var buyerId by MessagesGroupTable.buyerId
    var sellerId by MessagesGroupTable.sellerId
    var createdAt by MessagesGroupTable.createdAt
    var updatedAt by MessagesGroupTable.updatedAt
    val messages by MessagesEntity referrersOn MessagesTable.groupId
    var buyer by UserEntity referencedOn MessagesGroupTable.buyerId
    var buyerOrganization by OrganizationEntity referencedOn MessagesGroupTable.buyerOrganizationId
    var seller by UserEntity referencedOn MessagesGroupTable.sellerId
    val orders by OrdersEntity referrersOn OrdersTable.messageGroupId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(MessagesGroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
