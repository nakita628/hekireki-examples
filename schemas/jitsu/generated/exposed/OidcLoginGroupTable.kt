package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OidcLoginGroupTable : IdTable<String>("\"OidcLoginGroup\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OidcLoginGroup_workspaceId_fkey\"",
        )
    val oidcProviderId =
        reference(
            "oidcProviderId",
            OidcProviderTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OidcLoginGroup_oidcProviderId_fkey\"",
        )
    val groupValue = text("groupValue").nullable()
    val allowAllUsers = bool("allowAllUsers").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"OidcLoginGroup_pkey\"")

    init {
        index("\"OidcLoginGroup_workspaceId_idx\"", false, workspaceId)
        index("\"OidcLoginGroup_oidcProviderId_idx\"", false, oidcProviderId)
        uniqueIndex("\"OidcLoginGroup_workspaceId_oidcProviderId_key\"", workspaceId, oidcProviderId)
    }
}
