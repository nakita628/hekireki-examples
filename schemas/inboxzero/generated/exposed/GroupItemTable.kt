package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object GroupItemTable : IdTable<String>("\"GroupItem\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val groupId =
        optReference(
            "groupId",
            GroupTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"GroupItem_groupId_fkey\"",
        )
    val type = pgEnum("type", "\"GroupItemType\"", GroupItemType.entries, GroupItemType::dbName)
    val value = text("value")
    val exclude = bool("exclude").default(false)
    val reason = text("reason").nullable()
    val threadId = text("threadId").nullable()
    val messageId = text("messageId").nullable()
    val source1 = pgEnum("source", "\"GroupItemSource\"", GroupItemSource.entries, GroupItemSource::dbName).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"GroupItem_pkey\"")

    init {
        uniqueIndex("\"GroupItem_groupId_type_value_key\"", groupId, type, value)
    }
}
