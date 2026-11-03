package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SecondaryEmailTable : IdTable<Int>("\"SecondaryEmail\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SecondaryEmail_userId_fkey\"",
        )
    val email = text("email").uniqueIndex("\"SecondaryEmail_email_key\"")
    val emailVerified = pgTimestamp("emailVerified", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"SecondaryEmail_pkey\"")

    init {
        index("\"SecondaryEmail_userId_idx\"", false, userId)
        index("\"SecondaryEmail_email_emailVerified_idx\"", false, email, emailVerified)
        uniqueIndex("\"SecondaryEmail_userId_email_key\"", userId, email)
    }
}
