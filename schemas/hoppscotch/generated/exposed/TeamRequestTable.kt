package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamRequestTable : IdTable<String>("\"TeamRequest\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val collectionID =
        reference(
            "collectionID",
            TeamCollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamRequest_collectionID_fkey\"",
        )
    val teamID =
        reference(
            "teamID",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamRequest_teamID_fkey\"",
        )
    val title = text("title")
    val request = jsonb<String>("request", { it }, { it })
    val mockExamples = jsonb<String>("mockExamples", { it }, { it }).nullable()
    val orderIndex = integer("orderIndex")
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TeamRequest_pkey\"")

    init {
        uniqueIndex("\"TeamRequest_teamID_collectionID_orderIndex_key\"", teamID, collectionID, orderIndex)
    }
}
