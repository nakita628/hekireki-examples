package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TwoFactorTable : IdTable<String>("\"TwoFactor\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val secret = text("secret")
    val backupCodes = text("backupCodes")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TwoFactor_userId_fkey\"",
        ).uniqueIndex("\"TwoFactor_userId_key\"")
    val verified = bool("verified").default(true)
    val failedVerificationCount = integer("failedVerificationCount").default(0)
    val lockedUntil = pgTimestamp("lockedUntil", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"TwoFactor_pkey\"")
}
