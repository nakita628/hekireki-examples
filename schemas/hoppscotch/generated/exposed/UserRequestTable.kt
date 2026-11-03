package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserRequestTable : IdTable<String>("\"UserRequest\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val collectionID =
        reference(
            "collectionID",
            UserCollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserRequest_collectionID_fkey\"",
        )
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserRequest_userUid_fkey\"",
        )
    val title = text("title")
    val request = jsonb<String>("request", { it }, { it })
    val type = pgEnum("type", "\"ReqType\"", ReqType.entries, ReqType::dbName)
    val mockExamples = jsonb<String>("mockExamples", { it }, { it }).nullable()
    val orderIndex = integer("orderIndex")
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserRequest_pkey\"")

    init {
        uniqueIndex("\"UserRequest_userUid_collectionID_orderIndex_key\"", userUid, collectionID, orderIndex)
    }
}
