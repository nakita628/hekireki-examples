package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationDataStoreTable : IdTable<String>("\"OrganizationDataStore\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val key = text("key").uniqueIndex("\"OrganizationDataStore_key_key\"")
    val organizationIds = array("organizationIds", TextColumnType()).nullable()
    val kind = pgEnum("kind", "\"DataStoreKind\"", DataStoreKind.entries, DataStoreKind::dbName)
    val config = jsonb<String>("config", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationDataStore_pkey\"")

    init {
        index("\"OrganizationDataStore_kind_idx\"", false, kind)
    }
}
