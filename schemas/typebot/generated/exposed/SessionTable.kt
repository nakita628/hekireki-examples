package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SessionTable : IdTable<String>("\"Session\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val sessionToken = text("sessionToken").uniqueIndex("\"Session_sessionToken_key\"")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Session_userId_fkey\"",
        )
    val expires = pgTimestamp("expires", 3)
    override val primaryKey = PrimaryKey(id, name = "\"Session_pkey\"")
}
