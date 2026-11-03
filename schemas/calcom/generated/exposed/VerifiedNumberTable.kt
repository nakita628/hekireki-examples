package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object VerifiedNumberTable : IdTable<Int>("\"VerifiedNumber\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        optReference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerifiedNumber_userId_fkey\"",
        )
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerifiedNumber_teamId_fkey\"",
        )
    val phoneNumber = text("phoneNumber")
    override val primaryKey = PrimaryKey(id, name = "\"VerifiedNumber_pkey\"")

    init {
        index("\"VerifiedNumber_userId_idx\"", false, userId)
        index("\"VerifiedNumber_teamId_idx\"", false, teamId)
    }
}
