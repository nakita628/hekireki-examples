package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SessionTable : IdTable<String>("\"Session\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val sessionToken = text("session_token").uniqueIndex("\"Session_session_token_key\"")
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Session_user_id_fkey\"",
        )
    val expires = pgTimestamp("expires", 3)
    override val primaryKey = PrimaryKey(id, name = "\"Session_pkey\"")
}
