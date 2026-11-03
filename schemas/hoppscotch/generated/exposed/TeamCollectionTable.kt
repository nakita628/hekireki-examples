package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamCollectionTable : IdTable<String>("\"TeamCollection\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val parentID =
        optReference(
            "parentID",
            TeamCollectionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamCollection_parentID_fkey\"",
        )
    val teamID =
        reference(
            "teamID",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamCollection_teamID_fkey\"",
        )
    val title = text("title")
    val orderIndex = integer("orderIndex")
    val data = jsonb<String>("data", { it }, { it }).nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TeamCollection_pkey\"")

    init {
        uniqueIndex("\"TeamCollection_teamID_parentID_orderIndex_key\"", teamID, parentID, orderIndex)
    }
}
