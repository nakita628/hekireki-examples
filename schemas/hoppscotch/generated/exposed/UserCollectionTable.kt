package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserCollectionTable : IdTable<String>("\"UserCollection\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val parentID =
        optReference(
            "parentID",
            UserCollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserCollection_parentID_fkey\"",
        )
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserCollection_userUid_fkey\"",
        )
    val title = text("title")
    val type = pgEnum("type", "\"ReqType\"", ReqType.entries, ReqType::dbName)
    val orderIndex = integer("orderIndex")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserCollection_pkey\"")

    init {
        uniqueIndex("\"UserCollection_userUid_parentID_orderIndex_key\"", userUid, parentID, orderIndex)
    }
}
