package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimSubjectTable : IdTable<String>("\"scimSubject\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"scimSubject_userId_fkey\"",
        ).uniqueIndex("\"scimSubject_userId_key\"")
    val profileSourceId = text("profileSourceId").nullable()
    val revision = integer("revision")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"scimSubject_pkey\"")

    init {
        index("\"scimSubject_profileSourceId_idx\"", false, profileSourceId)
    }
}
