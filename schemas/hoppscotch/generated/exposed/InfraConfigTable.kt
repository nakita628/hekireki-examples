package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InfraConfigTable : IdTable<String>("\"InfraConfig\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name").uniqueIndex("\"InfraConfig_name_key\"")
    val value = text("value").nullable()
    val isEncrypted = bool("isEncrypted").default(false)
    val lastSyncedEnvFileValue = text("lastSyncedEnvFileValue").nullable()
    val createdOn = pgTimestamptz("createdOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedOn = pgTimestamptz("updatedOn", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"InfraConfig_pkey\"")
}
