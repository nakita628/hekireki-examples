package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object UserPasswordTable : IdTable<Int>("\"UserPassword\"") {
    val hash = text("hash")
    override val id =
        integer("userId")
            .references(
                UserTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"UserPassword_userId_fkey\"",
            ).entityId()

    init {
        uniqueIndex("\"UserPassword_userId_key\"", id)
    }
}
