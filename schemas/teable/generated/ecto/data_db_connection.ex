defmodule Teable.DataDbConnection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          provider: atom(),
          encrypted_url: String.t(),
          url_fingerprint: String.t(),
          display_host: String.t() | nil,
          display_database: String.t() | nil,
          internal_schema: String.t(),
          status: atom(),
          schema_version: String.t() | nil,
          capabilities: map() | nil,
          last_validated_at: DateTime.t() | nil,
          last_error: String.t() | nil,
          health_state: atom(),
          health_reason: String.t() | nil,
          health_changed_at: DateTime.t() | nil,
          last_health_check_at: DateTime.t() | nil,
          consecutive_health_failures: integer(),
          created_by: String.t(),
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil,
          space_bindings: [Teable.SpaceDataDbBinding.t()],
          source_migration_jobs: [Teable.SpaceDataDbMigrationJob.t()],
          target_migration_jobs: [Teable.SpaceDataDbMigrationJob.t()]
        }

  schema "data_db_connection" do
    field(:id, :string, primary_key: true)
    field(:provider, Ecto.Enum, values: [:postgres], default: :postgres)
    field(:encrypted_url, :string)
    field(:url_fingerprint, :string)
    field(:display_host, :string)
    field(:display_database, :string)
    field(:internal_schema, :string)
    field(:status, Ecto.Enum, values: [:pending, :validating, :ready, :error, :migrating, :disabled], default: :pending)
    field(:schema_version, :string)
    field(:capabilities, :map)
    field(:last_validated_at, Teable.PrismaDateTime)
    field(:last_error, :string)
    field(:health_state, Ecto.Enum, values: [:healthy, :read_only, :unreachable, :degraded], default: :healthy)
    field(:health_reason, :string)
    field(:health_changed_at, Teable.PrismaDateTime)
    field(:last_health_check_at, Teable.PrismaDateTime)
    field(:consecutive_health_failures, :integer, default: 0)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    has_many(:space_bindings, Teable.SpaceDataDbBinding, foreign_key: :data_db_connection_id)
    has_many(:source_migration_jobs, Teable.SpaceDataDbMigrationJob, foreign_key: :source_connection_id)
    has_many(:target_migration_jobs, Teable.SpaceDataDbMigrationJob, foreign_key: :target_connection_id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end