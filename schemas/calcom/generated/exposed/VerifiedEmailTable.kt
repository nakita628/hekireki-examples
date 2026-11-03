package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object VerifiedEmailTable : IdTable<Int>("\"VerifiedEmail\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerifiedEmail_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerifiedEmail_teamId_fkey\"",
        )
    val email = text("email")
    override val primaryKey = PrimaryKey(id, name = "\"VerifiedEmail_pkey\"")

    init {
        index("\"VerifiedEmail_userId_idx\"", false, userId)
        index("\"VerifiedEmail_teamId_idx\"", false, teamId)
    }
}
