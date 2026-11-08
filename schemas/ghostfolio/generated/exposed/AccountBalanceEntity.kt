package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AccountBalanceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AccountBalanceEntity>(AccountBalanceTable)

    var account by AccountEntity referencedOn AccountBalanceTable
    var accountId by AccountBalanceTable.accountId
    var comment by AccountBalanceTable.comment
    var createdAt by AccountBalanceTable.createdAt
    var date by AccountBalanceTable.date
    var updatedAt by AccountBalanceTable.updatedAt
    var userId by AccountBalanceTable.userId
    var value by AccountBalanceTable.value

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AccountBalanceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
