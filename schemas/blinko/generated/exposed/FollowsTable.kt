package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object FollowsTable : IdTable<Int>("follows") {
    override val id = integer("id").autoIncrement().entityId()
    val siteName = pgVarchar("siteName").nullable()
    val siteUrl = pgVarchar("siteUrl")
    val siteAvatar = pgVarchar("siteAvatar").nullable()
    val description = text("description").nullable()
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val followType = pgVarchar("followType").default("following")
    val accountId =
        reference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"follows_accountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "follows_pkey")
}
