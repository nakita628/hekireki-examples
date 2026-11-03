package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ReferralInviteTable : IdTable<String>("\"ReferralInvite\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ReferralInvite_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ReferralInvite_teamId_fkey\"",
        )
    val scopeId = text("scopeId")
    val email = text("email")
    val referralLink = text("referralLink").nullable()
    val sentAt = pgTimestamp("sentAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ReferralInvite_pkey\"")

    init {
        index("\"ReferralInvite_userId_idx\"", false, userId)
        index("\"ReferralInvite_teamId_idx\"", false, teamId)
        uniqueIndex("\"ReferralInvite_scopeId_email_key\"", scopeId, email)
    }
}
