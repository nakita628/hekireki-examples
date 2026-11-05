package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object OauthClientResourceTable : IdTable<String>("\"OauthClientResource\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val clientId =
        text("clientId").references(
            OauthClientTable.clientId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthClientResource_clientId_fkey\"",
        )
    val resourceId =
        text("resourceId").references(
            OauthResourceTable.identifier,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OauthClientResource_resourceId_fkey\"",
        )
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val createdAt = pgTimestamp("createdAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OauthClientResource_pkey\"")

    init {
        index("\"OauthClientResource_clientId_idx\"", false, clientId)
        index("\"OauthClientResource_resourceId_idx\"", false, resourceId)
        uniqueIndex("\"oauthClientResource_clientId_resourceId_uidx\"", clientId, resourceId)
    }
}
