package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccountEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<AccountEntity>(AccountTable)

    val activities by OrderEntity optionalReferrersOn OrderTable
    val balances by AccountBalanceEntity referrersOn AccountBalanceTable
    var comment by AccountTable.comment
    var createdAt by AccountTable.createdAt
    var currency by AccountTable.currency
    val id1 by AccountTable.id1
    var name by AccountTable.name
    var platform by PlatformEntity optionalReferencedOn AccountTable.platformId
    var platformId by AccountTable.platformId
    val tags by TagsOnAccountsEntity referrersOn TagsOnAccountsTable
    var updatedAt by AccountTable.updatedAt
    val user by UserEntity referencedOn AccountTable.userId
    val userId by AccountTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
