defmodule LangWatch.SystemMigrationEnrollment do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          migration_name: String.t(),
          enrolled_by_user_id: String.t(),
          inserted_at: DateTime.t()
        }

  schema "SystemMigrationEnrollment" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:migration_name, :string, primary_key: true, source: :migrationName)
    field(:enrolled_by_user_id, :string, source: :enrolledByUserId)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end