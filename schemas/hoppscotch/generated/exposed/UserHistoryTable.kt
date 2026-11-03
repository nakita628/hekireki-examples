package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserHistoryTable : IdTable<String>("\"UserHistory\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userUid =
        reference(
            "userUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserHistory_userUid_fkey\"",
        )
    val reqType = pgEnum("reqType", "\"ReqType\"", ReqType.entries, ReqType::dbName)
    val request = jsonb<String>("request", { it }, { it })
    val responseMetadata = jsonb<String>("responseMetadata", { it }, { it })
    val isStarred = bool("isStarred")
    val executedOn = pgTimestamptz("executedOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserHistory_pkey\"")
}
