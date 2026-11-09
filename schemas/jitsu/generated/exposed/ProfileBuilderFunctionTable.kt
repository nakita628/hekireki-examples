package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProfileBuilderFunctionTable : IdTable<String>("\"ProfileBuilderFunction\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val profileBuilderId =
        reference(
            "profileBuilderId",
            ProfileBuilderTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProfileBuilderFunction_profileBuilderId_fkey\"",
        )
    val functionId =
        reference(
            "functionId",
            ConfigurationObjectTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ProfileBuilderFunction_functionId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ProfileBuilderFunction_pkey\"")
}
