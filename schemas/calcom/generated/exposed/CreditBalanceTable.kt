package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object CreditBalanceTable : IdTable<String>("\"CreditBalance\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreditBalance_teamId_fkey\"",
        ).uniqueIndex("\"CreditBalance_teamId_key\"")
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreditBalance_userId_fkey\"",
        ).uniqueIndex("\"CreditBalance_userId_key\"")
    val additionalCredits = integer("additionalCredits").default(0)
    val limitReachedAt = pgTimestamp("limitReachedAt", 3).nullable()
    val warningSentAt = pgTimestamp("warningSentAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"CreditBalance_pkey\"")
}
