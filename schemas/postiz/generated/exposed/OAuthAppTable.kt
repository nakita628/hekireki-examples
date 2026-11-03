package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OAuthAppTable : IdTable<String>("\"OAuthApp\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val organizationId =
        optReference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthApp_organizationId_fkey\"",
        )
    val name = text("name")
    val description = text("description").nullable()
    val pictureId =
        optReference(
            "pictureId",
            MediaTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthApp_pictureId_fkey\"",
        )
    val redirectUrl = text("redirectUrl")
    val redirectUris = text("redirectUris").nullable()
    val clientId = text("clientId").uniqueIndex("\"OAuthApp_clientId_key\"")
    val clientSecret = text("clientSecret").nullable()
    val dynamic = bool("dynamic").default(false)
    val tokenEndpointAuthMethod = text("tokenEndpointAuthMethod").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"OAuthApp_pkey\"")

    init {
        index("\"OAuthApp_clientId_idx\"", false, clientId)
        index("\"OAuthApp_organizationId_idx\"", false, organizationId)
        index("\"OAuthApp_deletedAt_idx\"", false, deletedAt)
        index("\"OAuthApp_dynamic_idx\"", false, dynamic)
        uniqueIndex("\"OAuthApp_organizationId_deletedAt_key\"", organizationId, deletedAt)
    }
}
