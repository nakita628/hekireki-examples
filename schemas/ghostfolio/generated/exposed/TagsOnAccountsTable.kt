package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TagsOnAccountsTable : CompositeIdTable("\"TagsOnAccounts\"") {
    val accountId = text("accountId").entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val tagId =
        reference(
            "tagId",
            TagTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TagsOnAccounts_tagId_fkey\"",
        )
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId = text("userId").entityId()
    override val primaryKey = PrimaryKey(accountId, tagId, userId, name = "\"TagsOnAccounts_pkey\"")

    init {
        addIdColumn(tagId)
        index("\"TagsOnAccounts_tagId_idx\"", false, tagId)
        foreignKey(
            accountId to AccountTable.id1,
            userId to AccountTable.userId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"TagsOnAccounts_accountId_userId_fkey\"",
        )
    }
}
