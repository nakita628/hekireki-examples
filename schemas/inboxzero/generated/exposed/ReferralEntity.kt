package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ReferralEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReferralEntity>(ReferralTable)

    var createdAt by ReferralTable.createdAt
    var updatedAt by ReferralTable.updatedAt
    var referrerUserId by ReferralTable.referrerUserId
    var referrerUser by UserEntity referencedOn ReferralTable.referrerUserId
    var referredUserId by ReferralTable.referredUserId
    var referredUser by UserEntity referencedOn ReferralTable.referredUserId
    var referralCodeUsed by ReferralTable.referralCodeUsed
    var status by ReferralTable.status
    var rewardGrantedAt by ReferralTable.rewardGrantedAt
    var stripeBalanceTransactionId by ReferralTable.stripeBalanceTransactionId
    var rewardAmount by ReferralTable.rewardAmount

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ReferralTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
