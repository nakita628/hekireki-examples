package hoppscotch.models

import org.jetbrains.exposed.v1.core.Schema
import org.jetbrains.exposed.v1.core.Table

object PrismaSchema {
    val schemas: List<Schema> = emptyList()
    val enumTypes: List<String> =
        listOf(
            "CREATE TYPE \"WorkspaceType\" AS ENUM ('USER', 'TEAM')",
            "CREATE TYPE \"ReqType\" AS ENUM ('REST', 'GQL')",
            "CREATE TYPE \"TeamAccessRole\" AS ENUM ('OWNER', 'VIEWER', 'EDITOR')",
            "CREATE TYPE \"MockServerAction\" AS ENUM ('CREATED', 'DELETED', 'ACTIVATED', 'DEACTIVATED')",
        )
    val tables: List<Table> =
        listOf(
            TeamTable,
            TeamMemberTable,
            TeamInvitationTable,
            TeamCollectionTable,
            TeamRequestTable,
            TeamEnvironmentTable,
            UserTable,
            AccountTable,
            VerificationTokenTable,
            UserSettingsTable,
            UserHistoryTable,
            UserEnvironmentTable,
            InvitedUsersTable,
            UserRequestTable,
            UserCollectionTable,
            MockServerTable,
            MockServerLogTable,
            MockServerActivityTable,
            ShortcodeTable,
            InfraConfigTable,
            PersonalAccessTokenTable,
            InfraTokenTable,
            PublishedDocsTable,
        )
}
