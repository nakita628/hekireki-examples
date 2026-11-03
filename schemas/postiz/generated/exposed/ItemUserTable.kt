package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object ItemUserTable : IdTable<String>("\"ItemUser\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ItemUser_userId_fkey\"",
        )
    val key = text("key")
    override val primaryKey = PrimaryKey(id, name = "\"ItemUser_pkey\"")

    init {
        index("\"ItemUser_userId_idx\"", false, userId)
        index("\"ItemUser_key_idx\"", false, key)
        uniqueIndex("\"ItemUser_userId_key_key\"", userId, key)
    }
}
