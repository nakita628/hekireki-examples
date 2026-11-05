package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object ScimIdentityLinkTable : IdTable<String>("\"ScimIdentityLink\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val connectionId = text("connectionId")
    val externalId = text("externalId")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ScimIdentityLink_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ScimIdentityLink_pkey\"")

    init {
        index("\"ScimIdentityLink_userId_idx\"", false, userId)
        uniqueIndex("\"ScimIdentityLink_connectionId_externalId_key\"", connectionId, externalId)
    }
}
