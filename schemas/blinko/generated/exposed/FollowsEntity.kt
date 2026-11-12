package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class FollowsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, FollowsEntity>(FollowsTable)

    var siteName by FollowsTable.siteName
    var siteUrl by FollowsTable.siteUrl
    var siteAvatar by FollowsTable.siteAvatar
    var description by FollowsTable.description
    var createdAt by FollowsTable.createdAt
    var updatedAt by FollowsTable.updatedAt
    var followType by FollowsTable.followType
    var accountId by FollowsTable.accountId
    var account by AccountsEntity referencedOn FollowsTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(FollowsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
