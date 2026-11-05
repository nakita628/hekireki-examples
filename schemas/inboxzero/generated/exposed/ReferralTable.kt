package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReferralTable : IdTable<String>("\"Referral\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val referrerUserId =
        reference(
            "referrerUserId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Referral_referrerUserId_fkey\"",
        )
    val referredUserId =
        reference(
            "referredUserId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Referral_referredUserId_fkey\"",
        ).uniqueIndex("\"Referral_referredUserId_key\"")
    val referralCodeUsed = text("referralCodeUsed")
    val status = pgEnum("status", "\"ReferralStatus\"", ReferralStatus.entries, ReferralStatus::dbName).default(ReferralStatus.PENDING)
    val rewardGrantedAt = pgTimestamp("rewardGrantedAt", 3).nullable()
    val stripeBalanceTransactionId = text("stripeBalanceTransactionId").nullable()
    val rewardAmount = integer("rewardAmount").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Referral_pkey\"")

    init {
        index("\"Referral_referrerUserId_idx\"", false, referrerUserId)
        index("\"Referral_status_idx\"", false, status)
    }
}
